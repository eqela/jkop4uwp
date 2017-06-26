
/*
 * This file is part of Jkop for UWP
 * Copyright (c) 2016-2017 Job and Esther Technologies, Inc.
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace capex.http {
	public class HTTPClientOperation : capex.http.HTTPClient
	{
		private class MyResponseParser
		{
			public MyResponseParser() {
			}

			private class Chunk
			{
				public Chunk() {
				}

				public byte[] data = null;
				public bool completed = true;
			}

			private byte[] receivedData = null;
			public capex.http.HTTPClientResponse headers = null;
			public bool isChunked = false;
			public int contentLength = 0;
			public int dataCounter = 0;
			private capex.http.HTTPClientListener listener = null;
			private bool endOfResponse = false;
			private bool aborted = false;

			public virtual void reset() {
				isChunked = false;
				headers = null;
				contentLength = 0;
				dataCounter = 0;
				endOfResponse = false;
				aborted = false;
			}

			private bool hasEndOfHeaders(byte[] buf, long size) {
				var n = 0;
				var v = false;
				while(n <= size - 4) {
					if(cape.Buffer.getByte(buf, (long)n) == '\r' && cape.Buffer.getByte(buf, (long)(n + 1)) == '\n' && cape.Buffer.getByte(buf, (long)(n + 2)) == '\r' && cape.Buffer.getByte(buf, (long)(n + 3)) == '\n') {
						v = true;
						break;
					}
					n++;
				}
				return(v);
			}

			private capex.http.HTTPClientResponse parseResponse(byte[] buf) {
				var i = 0;
				var p = (byte)'0';
				capex.http.HTTPClientResponse v = null;
				var first = true;
				var isChunked = false;
				while(true) {
					var sb = new cape.StringBuilder();
					while((p = cape.Buffer.getByte(buf, (long)i)) != 0) {
						if(p == '\r') {
							;
						}
						else if(p == '\n') {
							i++;
							break;
						}
						else {
							sb.append((char)p);
						}
						i++;
					}
					var t = sb.toString();
					if(cape.String.isEmpty(t)) {
						break;
					}
					if(first) {
						var comps = cape.String.split(t, ' ', 3);
						v = new capex.http.HTTPClientResponse();
						v.setHttpVersion(cape.Vector.get(comps, 0));
						v.setHttpStatus(cape.Vector.get(comps, 1));
						v.setHttpStatusDescription(cape.Vector.get(comps, 2));
					}
					else {
						var comps1 = cape.String.split(t, ':', 2);
						var key = cape.Vector.get(comps1, 0);
						if(cape.String.isEmpty(key) == false) {
							var val = cape.String.strip(cape.Vector.get(comps1, 1));
							v.addHeader(key, val);
							if(isChunked == false && cape.String.equalsIgnoreCase(key, "transfer-encoding")) {
								if(object.Equals(val, "chunked")) {
									isChunked = true;
								}
							}
							else if(contentLength < 1 && cape.String.equalsIgnoreCase(key, "content-length")) {
								contentLength = cape.String.toInteger(val);
							}
						}
					}
					first = false;
				}
				var l = (long)(cape.Buffer.getSize(buf) - i);
				if(l > 0) {
					receivedData = cape.Buffer.getSubBuffer(buf, (long)i, l);
				}
				else {
					receivedData = null;
				}
				this.isChunked = isChunked;
				return(v);
			}

			private capex.http.HTTPClientOperation.MyResponseParser.Chunk getChunk() {
				if(!(receivedData != null)) {
					return(null);
				}
				var i = 0;
				var sb = new cape.StringBuilder();
				while(true) {
					var p = cape.Buffer.getByte(receivedData, (long)i);
					if(p == '\r') {
						;
					}
					else if(p == '\n') {
						i++;
						break;
					}
					else {
						sb.append((char)p);
					}
					i++;
					if(sb.count() >= 16) {
						return(null);
					}
				}
				var cl = -1;
				var t = cape.String.strip(sb.toString());
				if(cape.String.isEmpty(t) == false) {
					cl = cape.String.toIntegerFromHex(t);
				}
				var chunk = new capex.http.HTTPClientOperation.MyResponseParser.Chunk();
				if(cl > 0) {
					if(cape.Buffer.getSize(receivedData) - i < cl) {
						chunk.completed = false;
						return(chunk);
					}
					chunk.data = new byte[cl];
					cape.Buffer.copyFrom(chunk.data, receivedData, (long)i, (long)0, (long)cl);
					i += cl;
				}
				while(i < cape.Buffer.getSize(receivedData) && (cape.Buffer.getByte(receivedData, (long)i) == '\r' || cape.Buffer.getByte(receivedData, (long)i) == '\n')) {
					i++;
				}
				var rem = (int)(cape.Buffer.getSize(receivedData) - i);
				if(rem > 0) {
					var tmp = receivedData;
					receivedData = new byte[rem];
					cape.Buffer.copyFrom(receivedData, tmp, (long)i, (long)0, (long)rem);
				}
				else {
					receivedData = null;
				}
				return(chunk);
			}

			public void onDataReceived(byte[] buf, long size) {
				if(size > 0) {
					receivedData = cape.Buffer.append(receivedData, buf, size);
				}
				if(headers == null) {
					if(hasEndOfHeaders(receivedData, cape.Buffer.getSize(receivedData))) {
						headers = parseResponse(receivedData);
						if(headers != null) {
							onHeadersReceived(headers);
						}
					}
				}
				if(isChunked) {
					while(true) {
						var r = getChunk();
						if(r != null) {
							if(!r.completed) {
								break;
							}
							if(!(r.data != null)) {
								reset();
								onEndOfResponse();
								break;
							}
							var sz = cape.Buffer.getSize(r.data);
							dataCounter += (int)sz;
							onBodyDataReceived(r.data, sz);
						}
						else {
							reset();
							onEndOfResponse();
							break;
						}
						if(receivedData == null) {
							break;
						}
					}
				}
				else if(contentLength > 0) {
					var rsz = cape.Buffer.getSize(receivedData);
					if(rsz > 0) {
						if(contentLength <= 0 || dataCounter + rsz <= contentLength) {
							var v = receivedData;
							receivedData = null;
							dataCounter += (int)rsz;
							onBodyDataReceived(v, rsz);
						}
						else {
							var vsz = contentLength - dataCounter;
							var v1 = cape.Buffer.getSubBuffer(receivedData, (long)0, (long)vsz);
							receivedData = cape.Buffer.getSubBuffer(receivedData, (long)vsz, rsz - vsz);
							dataCounter += vsz;
							onBodyDataReceived(v1, (long)vsz);
						}
					}
					if(dataCounter >= contentLength) {
						reset();
						onEndOfResponse();
					}
				}
				else {
					reset();
					onEndOfResponse();
				}
			}

			public void onHeadersReceived(capex.http.HTTPClientResponse headers) {
				if(listener != null && listener.onResponseReceived(headers) == false) {
					if(listener != null) {
						listener.onAborted();
					}
					aborted = true;
				}
			}

			public void onBodyDataReceived(byte[] buffer, long size) {
				if(listener != null && listener.onDataReceived(buffer) == false) {
					if(listener != null) {
						listener.onAborted();
					}
					aborted = true;
				}
			}

			public void onEndOfResponse() {
				if(listener != null) {
					listener.onResponseCompleted();
				}
				endOfResponse = true;
			}

			public capex.http.HTTPClientListener getListener() {
				return(listener);
			}

			public capex.http.HTTPClientOperation.MyResponseParser setListener(capex.http.HTTPClientListener v) {
				listener = v;
				return(this);
			}

			public bool getEndOfResponse() {
				return(endOfResponse);
			}

			public capex.http.HTTPClientOperation.MyResponseParser setEndOfResponse(bool v) {
				endOfResponse = v;
				return(this);
			}

			public bool getAborted() {
				return(aborted);
			}

			public capex.http.HTTPClientOperation.MyResponseParser setAborted(bool v) {
				aborted = v;
				return(this);
			}
		}

		private capex.net.ConnectedSocket openSocket = null;
		private string openSocketProtocol = null;
		private string openSocketAddress = null;
		private int openSocketPort = 0;
		private string defaultUserAgent = null;
		private capex.http.HTTPClientOperation.MyResponseParser parser = null;
		private byte[] receiveBuffer = null;
		private bool acceptInvalidCertificate = false;

		public HTTPClientOperation() {
			receiveBuffer = new byte[64 * 1024];
		}

		public bool openConnection(string protocol, string address, int aport, capex.http.HTTPClientListener listener) {
			closeConnection(listener);
			if(cape.String.isEmpty(address)) {
				if(listener != null) {
					listener.onError("No server address");
				}
				return(false);
			}
			if(!(object.Equals(protocol, "http")) && !(object.Equals(protocol, "https"))) {
				if(listener != null) {
					listener.onError("Protocol must be http or https");
				}
				return(false);
			}
			var port = aport;
			if(port < 1) {
				if(object.Equals(protocol, "https")) {
					port = 443;
				}
				else {
					port = 80;
				}
			}
			if(listener != null) {
				listener.onStatus("Connecting to server `" + address + ":" + cape.String.forInteger(port) + "' ..");
			}
			openSocket = (capex.net.ConnectedSocket)capex.net.TCPSocket.createAndConnect(address, port);
			if(listener != null) {
				listener.onStatus(null);
			}
			if(openSocket == null) {
				if(listener != null) {
					listener.onError("Connection failed: `" + address + ":" + cape.String.forInteger(port) + "'");
				}
				return(false);
			}
			if(object.Equals(protocol, "https")) {
				openSocket = (capex.net.ConnectedSocket)capex.net.SSLSocket.forClient(openSocket, address, null, acceptInvalidCertificate);
				if(openSocket == null && listener != null) {
					listener.onError("FAILED to create SSL socket for HTTPS");
					closeConnection(listener);
					return(false);
				}
			}
			openSocketProtocol = protocol;
			openSocketAddress = address;
			openSocketPort = port;
			parser = new capex.http.HTTPClientOperation.MyResponseParser();
			return(true);
		}

		public bool openConnection(capex.http.HTTPClientRequest request, capex.http.HTTPClientListener listener) {
			if(request == null) {
				if(listener != null) {
					listener.onError("No request");
				}
				return(false);
			}
			return(openConnection(request.getProtocol(), request.getServerAddress(), request.getServerPort(), listener));
		}

		public void closeConnection(capex.http.HTTPClientListener listener) {
			if(!(openSocket != null)) {
				return;
			}
			if(listener != null) {
				listener.onStatus("Closing connection");
			}
			openSocket.close();
			openSocket = null;
			openSocketProtocol = null;
			openSocketAddress = null;
			openSocketPort = 0;
			parser = null;
			if(listener != null) {
				listener.onStatus(null);
			}
		}

		public bool sendRequest(capex.http.HTTPClientRequest request, capex.http.HTTPClientListener listener) {
			if(request == null) {
				if(listener != null) {
					listener.onError("No request");
				}
				return(false);
			}
			if(listener != null && listener.onStartRequest(request) == false) {
				return(false);
			}
			if(openSocket != null) {
				if(!(object.Equals(request.getServerAddress(), openSocketAddress)) || !(object.Equals(request.getProtocol(), openSocketProtocol)) || request.getServerPort() != openSocketPort) {
					closeConnection(listener);
				}
			}
			if(openSocket == null) {
				openConnection(request, listener);
				if(!(openSocket != null)) {
					return(false);
				}
			}
			if(listener != null) {
				listener.onStatus("Sending request headers ..");
			}
			var rqs = request.toString(defaultUserAgent);
			var pww = cape.PrintWriterWrapper.forWriter((cape.Writer)openSocket);
			var whr = pww.print(rqs);
			if(listener != null) {
				listener.onStatus(null);
			}
			if(whr == false) {
				if(listener != null) {
					listener.onError("Failed to send HTTP request headers");
				}
				closeConnection(listener);
				return(false);
			}
			var body = request.getBody();
			if(body != null) {
				if(listener != null) {
					listener.onStatus("Sending request body ..");
				}
				var rv = true;
				var bf = new byte[4096 * 4];
				while(true) {
					var r = body.read(bf);
					if(r < 1) {
						break;
					}
					if(openSocket.write(bf, r) < r) {
						if(listener != null) {
							listener.onError("Failed to send request body");
						}
						closeConnection(listener);
						rv = false;
						break;
					}
				}
				if(listener != null) {
					listener.onStatus(null);
				}
				if(rv == false) {
					return(false);
				}
			}
			return(true);
		}

		public bool readResponse(capex.http.HTTPClientListener listener, int timeout) {
			if(openSocket == null) {
				if(listener != null) {
					listener.onError("No open socket");
				}
				return(false);
			}
			if(listener != null) {
				listener.onStatus("Receiving response ..");
			}
			var rv = true;
			parser.setListener(listener);
			while(true) {
				var r = 0;
				if(openSocket is capex.net.SSLSocket) {
					r = ((capex.net.SSLSocket)openSocket).readWithTimeout(receiveBuffer, timeout);
				}
				else {
					r = ((capex.net.TCPSocket)openSocket).readWithTimeout(receiveBuffer, timeout);
				}
				if(r == 0) {
					rv = false;
					break;
				}
				if(r < 1) {
					closeConnection(listener);
					if(listener != null) {
						listener.onAborted();
					}
					rv = false;
					break;
				}
				parser.onDataReceived(receiveBuffer, (long)r);
				if(parser.getAborted()) {
					closeConnection(listener);
					rv = false;
					break;
				}
				if(parser.getEndOfResponse()) {
					parser.reset();
					rv = true;
					break;
				}
			}
			if(parser != null) {
				parser.setListener(null);
			}
			if(listener != null) {
				listener.onStatus(null);
				if(listener.onEndRequest() == false) {
					rv = false;
				}
			}
			return(rv);
		}

		public override void executeRequest(capex.http.HTTPClientRequest request, capex.http.HTTPClientListener listener) {
			if(!sendRequest(request, listener)) {
				return;
			}
			if(!readResponse(listener, 30000)) {
				return;
			}
			if(object.Equals(request.getHeader("connection"), "close")) {
				closeConnection(listener);
			}
		}

		public string getDefaultUserAgent() {
			return(defaultUserAgent);
		}

		public capex.http.HTTPClientOperation setDefaultUserAgent(string v) {
			defaultUserAgent = v;
			return(this);
		}

		public bool getAcceptInvalidCertificate() {
			return(acceptInvalidCertificate);
		}

		public capex.http.HTTPClientOperation setAcceptInvalidCertificate(bool v) {
			acceptInvalidCertificate = v;
			return(this);
		}
	}
}
