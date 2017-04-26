
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

namespace capex.net
{
	public class WSSocketGeneric : capex.net.WSSocket
	{
		private class MyWSMessage
		{
			public MyWSMessage() {
			}

			public const int CLOSE_FRAME = 8;
			public const int PING_FRAME = 9;
			public const int PONG_FRAME = 10;
			private int fin = 0;
			private int rsv1 = 0;
			private int rsv2 = 0;
			private int rsv3 = 0;
			private int opcode = 0;
			private byte[] maskingBuffer = null;
			private byte[] payloadBuffer = null;
			private byte[] messageBuffer = null;

			public static capex.net.WSSocketGeneric.MyWSMessage create(int fin, int rsv1, int rsv2, int rsv3, int opcode, byte[] maskingBuffer, byte[] payloadBuffer) {
				return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(fin).setRsv1(rsv1).setRsv2(rsv2).setRsv3(rsv3).setOpcode(opcode).setMaskingBuffer(maskingBuffer).setPayloadBuffer(payloadBuffer));
			}

			~MyWSMessage() {
				messageBuffer = null;
				maskingBuffer = null;
				payloadBuffer = null;
			}

			private void onChanged() {
				messageBuffer = null;
			}

			public capex.net.WSSocketGeneric.MyWSMessage setFin(int fin) {
				this.fin = fin;
				onChanged();
				return(this);
			}

			public int getFin() {
				return(fin);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setRsv1(int rsv1) {
				this.rsv1 = rsv1;
				onChanged();
				return(this);
			}

			public int getRsv1() {
				return(rsv1);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setRsv2(int rsv2) {
				this.rsv2 = rsv2;
				onChanged();
				return(this);
			}

			public int getRsv2() {
				return(rsv2);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setRsv3(int rsv3) {
				this.rsv3 = rsv3;
				onChanged();
				return(this);
			}

			public int getRsv3() {
				return(rsv3);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setOpcode(int opcode) {
				this.opcode = opcode;
				onChanged();
				return(this);
			}

			public int getOpcode() {
				return(opcode);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setMaskingBuffer(byte[] maskingBuffer) {
				this.maskingBuffer = maskingBuffer;
				onChanged();
				return(this);
			}

			public byte[] getMaskingBuffer() {
				return(maskingBuffer);
			}

			public capex.net.WSSocketGeneric.MyWSMessage setPayloadBuffer(byte[] payloadBuffer) {
				this.payloadBuffer = payloadBuffer;
				onChanged();
				return(this);
			}

			public byte[] getPayloadBuffer() {
				return(payloadBuffer);
			}

			public static capex.net.WSSocketGeneric.MyWSMessage forCloseControlFrame(int statusCode = -1, string reason = null) {
				if((statusCode < 65536) && (statusCode > -1)) {
					if(cape.String.isEmpty(reason) == false) {
						var rBuffer = cape.String.toUTF8Buffer(reason);
						var size = rBuffer.Length;
						var plBuffer = cape.Buffer.allocate((long)(size + 2));
						cape.Buffer.setByte(plBuffer, (long)0, (byte)((statusCode >> 8) & 255));
						cape.Buffer.setByte(plBuffer, (long)1, (byte)(statusCode & 255));
						cape.Buffer.copyFrom(plBuffer, rBuffer, (long)0, (long)2, (long)size);
						return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(1).setOpcode(capex.net.WSSocketGeneric.MyWSMessage.CLOSE_FRAME).setPayloadBuffer(plBuffer));
					}
					var plBuffer1 = cape.Buffer.allocate((long)2);
					cape.Buffer.setByte(plBuffer1, (long)0, (byte)((statusCode >> 8) & 255));
					cape.Buffer.setByte(plBuffer1, (long)1, (byte)(statusCode & 255));
					return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(1).setOpcode(capex.net.WSSocketGeneric.MyWSMessage.CLOSE_FRAME).setPayloadBuffer(plBuffer1));
				}
				return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(1).setOpcode(capex.net.WSSocketGeneric.MyWSMessage.CLOSE_FRAME));
			}

			public static capex.net.WSSocketGeneric.MyWSMessage forPingControlFrame() {
				return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(1).setOpcode(capex.net.WSSocketGeneric.MyWSMessage.PING_FRAME));
			}

			public static capex.net.WSSocketGeneric.MyWSMessage forPongControlFrame() {
				return(new capex.net.WSSocketGeneric.MyWSMessage().setFin(1).setOpcode(capex.net.WSSocketGeneric.MyWSMessage.PONG_FRAME));
			}

			public bool isDataText() {
				return(opcode == 1);
			}

			public bool isDataBinary() {
				return(opcode == 1);
			}

			public bool isCloseControlFrame() {
				return(opcode == capex.net.WSSocketGeneric.MyWSMessage.CLOSE_FRAME);
			}

			public bool isPingControlFrame() {
				return(opcode == capex.net.WSSocketGeneric.MyWSMessage.PING_FRAME);
			}

			public bool isPongControlFrame() {
				return(opcode == capex.net.WSSocketGeneric.MyWSMessage.PONG_FRAME);
			}

			public byte[] toBuffer() {
				if(messageBuffer != null) {
					return(messageBuffer);
				}
				var size = 2;
				if(maskingBuffer != null) {
					size += 4;
				}
				var payloadLength = 0;
				if(payloadBuffer != null) {
					payloadLength = payloadBuffer.Length;
				}
				var plb = 0;
				if((payloadLength < 126) && (payloadLength >= 0)) {
					plb = payloadLength;
				}
				else if(payloadLength < 65536) {
					plb = 126;
					size += 2;
				}
				else if(payloadLength > 65535) {
					plb = 127;
					size += 8;
				}
				else {
					return(null);
				}
				size += payloadLength;
				messageBuffer = cape.Buffer.allocate((long)size);
				var b1 = (byte)opcode;
				if(fin != 0) {
					b1 |= (byte)128;
				}
				if(rsv1 != 0) {
					b1 |= (byte)64;
				}
				if(rsv2 != 0) {
					b1 |= (byte)32;
				}
				if(rsv3 != 0) {
					b1 |= (byte)16;
				}
				cape.Buffer.setByte(messageBuffer, (long)0, b1);
				var b2 = (byte)0;
				b2 = (byte)plb;
				if(maskingBuffer != null) {
					b2 |= (byte)128;
				}
				cape.Buffer.setByte(messageBuffer, (long)1, b2);
				var p = 2;
				var tp = p;
				var n = 0;
				if(plb == 126) {
					n = 8;
					tp += 2;
				}
				else if(plb == 127) {
					n = 56;
					tp += 8;
				}
				while(p < tp) {
					if(n != 0) {
						cape.Buffer.setByte(messageBuffer, (long)p, (byte)((payloadLength >> n) & 255));
					}
					else {
						cape.Buffer.setByte(messageBuffer, (long)p, (byte)(payloadLength & 255));
					}
					p++;
					n -= 8;
				}
				if(maskingBuffer != null) {
					cape.Buffer.copyFrom(messageBuffer, maskingBuffer, (long)0, (long)p, (long)4);
					p += 4;
					if(payloadBuffer != null) {
						var i = 0;
						while(i < payloadLength) {
							var b = cape.Buffer.getByte(payloadBuffer, (long)i);
							var ob = (byte)(b ^ cape.Buffer.getByte(maskingBuffer, (long)(i % 4)));
							cape.Buffer.setByte(payloadBuffer, (long)i, ob);
							i++;
						}
					}
				}
				if(payloadBuffer != null) {
					cape.Buffer.copyFrom(messageBuffer, payloadBuffer, (long)0, (long)p, (long)payloadLength);
				}
				return(messageBuffer);
			}
		}

		private class MyWSCloseEvent : capex.net.WSCloseEvent
		{
			public MyWSCloseEvent() : base() {
			}

			private int statusCode = 0;
			private string reason = null;

			public capex.net.WSSocketGeneric.MyWSCloseEvent setStatusCode(int v) {
				statusCode = v;
				return(this);
			}

			public capex.net.WSSocketGeneric.MyWSCloseEvent setReason(string v) {
				reason = v;
				return(this);
			}

			public override int getStatusCode() {
				return(statusCode);
			}

			public override string getReason() {
				return(reason);
			}

			public static capex.net.WSCloseEvent forPayloadBuffer(byte[] payloadBuffer) {
				if(!(payloadBuffer != null)) {
					return(null);
				}
				var size = payloadBuffer.Length;
				var e = new capex.net.WSSocketGeneric.MyWSCloseEvent();
				var v = (ushort)0;
				v |= (ushort)(cape.Buffer.getByte(payloadBuffer, (long)0) << 8);
				v |= (ushort)cape.Buffer.getByte(payloadBuffer, (long)1);
				e.setStatusCode((int)v);
				if((size - 2) > 0) {
					e.setReason(cape.String.forUTF8Buffer(cape.Buffer.getSubBuffer(payloadBuffer, (long)2, (long)(size - 2))));
				}
				return((capex.net.WSCloseEvent)e);
			}
		}

		private class HTTPClientResponse : cape.StringObject
		{
			public HTTPClientResponse() {
			}

			private string httpVersion = null;
			private string httpStatus = null;
			private string httpStatusDescription = null;
			private cape.KeyValueListForStrings rawHeaders = null;
			private System.Collections.Generic.Dictionary<string,string> headers = null;

			public void addHeader(string key, string value) {
				if(!(rawHeaders != null)) {
					rawHeaders = new cape.KeyValueListForStrings();
				}
				if(!(headers != null)) {
					headers = new System.Collections.Generic.Dictionary<string,string>();
				}
				rawHeaders.add(key, value);
				headers[cape.String.toLowerCase(key)] = value;
			}

			public string getHeader(string key) {
				if(!(headers != null)) {
					return(null);
				}
				return(cape.Map.get(headers, key));
			}

			public virtual string toString() {
				return(cape.String.asString((object)rawHeaders));
			}

			public string getHttpVersion() {
				return(httpVersion);
			}

			public capex.net.WSSocketGeneric.HTTPClientResponse setHttpVersion(string v) {
				httpVersion = v;
				return(this);
			}

			public string getHttpStatus() {
				return(httpStatus);
			}

			public capex.net.WSSocketGeneric.HTTPClientResponse setHttpStatus(string v) {
				httpStatus = v;
				return(this);
			}

			public string getHttpStatusDescription() {
				return(httpStatusDescription);
			}

			public capex.net.WSSocketGeneric.HTTPClientResponse setHttpStatusDescription(string v) {
				httpStatusDescription = v;
				return(this);
			}

			public cape.KeyValueListForStrings getRawHeaders() {
				return(rawHeaders);
			}

			public capex.net.WSSocketGeneric.HTTPClientResponse setRawHeaders(cape.KeyValueListForStrings v) {
				rawHeaders = v;
				return(this);
			}

			public System.Collections.Generic.Dictionary<string,string> getHeaders() {
				return(headers);
			}

			public capex.net.WSSocketGeneric.HTTPClientResponse setHeaders(System.Collections.Generic.Dictionary<string,string> v) {
				headers = v;
				return(this);
			}
		}

		private class HTTPResponseParser
		{
			public HTTPResponseParser() {
			}

			private byte[] receivedData = null;
			public capex.net.WSSocketGeneric.HTTPClientResponse headers = null;
			public byte[] bodyData = null;
			public bool isChunked = false;
			public int contentLength = 0;
			public int dataCounter = 0;
			private bool endOfResponse = false;
			private bool aborted = false;

			public virtual void reset() {
				isChunked = false;
				headers = null;
				bodyData = null;
				contentLength = 0;
				dataCounter = 0;
				endOfResponse = false;
				aborted = false;
			}

			private bool hasEndOfHeaders(byte[] buf, long size) {
				var n = 0;
				var v = false;
				while(n <= (size - 4)) {
					if((((cape.Buffer.getByte(buf, (long)n) == '\r') && (cape.Buffer.getByte(buf, (long)(n + 1)) == '\n')) && (cape.Buffer.getByte(buf, (long)(n + 2)) == '\r')) && (cape.Buffer.getByte(buf, (long)(n + 3)) == '\n')) {
						v = true;
						break;
					}
					n++;
				}
				return(v);
			}

			private capex.net.WSSocketGeneric.HTTPClientResponse parseResponseHeader(byte[] buf) {
				var i = 0;
				var p = (byte)'0';
				capex.net.WSSocketGeneric.HTTPClientResponse v = null;
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
						v = new capex.net.WSSocketGeneric.HTTPClientResponse();
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
							if((isChunked == false) && cape.String.equalsIgnoreCase(key, "transfer-encoding")) {
								if(object.Equals(val, "chunked")) {
									isChunked = true;
								}
							}
							else if((contentLength < 1) && cape.String.equalsIgnoreCase(key, "content-length")) {
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

			private byte[] getChunk() {
				if(receivedData == null) {
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
				byte[] v = null;
				if(cl > 0) {
					if((cape.Buffer.getSize(receivedData) - i) < cl) {
						return(null);
					}
					v = new byte[cl];
					cape.Buffer.copyFrom(v, receivedData, (long)i, (long)0, (long)cl);
					i += cl;
				}
				while((i < cape.Buffer.getSize(receivedData)) && ((cape.Buffer.getByte(receivedData, (long)i) == '\r') || (cape.Buffer.getByte(receivedData, (long)i) == '\n'))) {
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
				return(v);
			}

			public void onDataReceived(byte[] buf, long size) {
				if(size > 0) {
					receivedData = cape.Buffer.append(receivedData, buf, size);
				}
				if(headers == null) {
					if(hasEndOfHeaders(receivedData, cape.Buffer.getSize(receivedData))) {
						headers = parseResponseHeader(receivedData);
					}
				}
				if(isChunked) {
					while(true) {
						var r = getChunk();
						if(r != null) {
							var sz = cape.Buffer.getSize(r);
							dataCounter += (int)sz;
							onBodyDataReceived(r, sz);
						}
						else {
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
						if((contentLength <= 0) || ((dataCounter + rsz) <= contentLength)) {
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
						onEndOfResponse();
					}
				}
				else {
					onEndOfResponse();
				}
			}

			public void onBodyDataReceived(byte[] buffer, long size) {
				bodyData = cape.Buffer.append(bodyData, buffer);
			}

			public void onEndOfResponse() {
				endOfResponse = true;
			}

			public bool getEndOfResponse() {
				return(endOfResponse);
			}

			public capex.net.WSSocketGeneric.HTTPResponseParser setEndOfResponse(bool v) {
				endOfResponse = v;
				return(this);
			}

			public bool getAborted() {
				return(aborted);
			}

			public capex.net.WSSocketGeneric.HTTPResponseParser setAborted(bool v) {
				aborted = v;
				return(this);
			}
		}

		private class WSHelper
		{
			public WSHelper() {
			}

			public const string WEBSOCKET_GUID = "258EAFA5-E914-47DA-95CA-C5AB0DC85B11";

			public static string getAcceptValueForKey(string key) {
				var shaEncoder = capex.crypto.SHAEncoder.create();
				if(!(shaEncoder != null)) {
					return(null);
				}
				return(cape.Base64Encoder.encode(shaEncoder.encodeAsBuffer(cape.String.toUTF8Buffer(key + capex.net.WSSocketGeneric.WSHelper.WEBSOCKET_GUID), capex.crypto.SHAEncoder.SHA1)));
			}

			public static string generateKey() {
				return(cape.Base64Encoder.encode(cape.String.toUTF8Buffer("EXTREMEPERFORMED")));
			}

			public static byte[] createOpenHandshakeHTTPRequest(cape.URL url, string key) {
				if(!((url != null) && (key != null))) {
					return(null);
				}
				var sb = new cape.StringBuilder();
				sb.append("GET ");
				var path = url.toStringNohost();
				if(cape.String.isEmpty(path)) {
					sb.append('/');
				}
				else {
					if(cape.String.getIndexOf(path, '/') != 0) {
						sb.append('/');
					}
					sb.append(path);
				}
				sb.append(" HTTP/1.1\r\n");
				sb.append("Host: ");
				sb.append(url.getHost());
				var port = url.getPort();
				if(!(object.Equals(port, null))) {
					sb.append(':');
					sb.append(port);
				}
				sb.append("\r\n");
				sb.append("Upgrade: websocket\r\n");
				sb.append("Connection: Upgrade\r\n");
				sb.append("Sec-WebSocket-Key: ");
				sb.append(key);
				sb.append("\r\nSec-WebSocket-Version: 13\r\n\r\n");
				return(cape.String.toUTF8Buffer(sb.toString()));
			}

			public static byte[] createOpenHandshakeHTTPResponse(string key) {
				if(!(key != null)) {
					return(null);
				}
				var sb = new cape.StringBuilder();
				sb.append("HTTP/1.1 101 Switching Protocols\r\n");
				sb.append("Upgrade: websocket\r\n");
				sb.append("Connection: Upgrade\r\n");
				sb.append("Sec-WebSocket-Accept: ");
				sb.append(capex.net.WSSocketGeneric.WSHelper.getAcceptValueForKey(key));
				sb.append("\r\n\r\n");
				return(cape.String.toUTF8Buffer(sb.toString()));
			}

			public static byte[] generateMaskingKey() {
				return(cape.String.toUTF8Buffer("byte"));
			}
		}

		private capex.net.TCPClient client = null;
		private string key = null;
		private string accept = null;
		private capex.net.WSSocketGeneric.HTTPResponseParser parser = null;
		private byte[] leftOverBuffer = null;
		private int frameNo = 1;

		private bool processData(byte[] data, int size) {
			if((data == null) || (size < 1)) {
				return(false);
			}
			byte[] nbuffer = null;
			var nsize = 0;
			if(leftOverBuffer != null) {
				var losize = leftOverBuffer.Length;
				nsize = losize + size;
				nbuffer = cape.Buffer.allocate((long)nsize);
				cape.Buffer.copyFrom(nbuffer, leftOverBuffer, (long)0, (long)0, (long)losize);
				cape.Buffer.copyFrom(nbuffer, data, (long)0, (long)losize, (long)size);
				leftOverBuffer = null;
			}
			else {
				nbuffer = data;
				nsize = size;
			}
			var p = 0;
			var fin = 0;
			var rsv1 = 0;
			var rsv2 = 0;
			var rsv3 = 0;
			var opcode = 0;
			var mask = 0;
			var payloadLength = 0;
			var lastMaskingBufferIndex = 0;
			var mi = 0;
			byte[] maskingBuffer = null;
			byte[] payloadBuffer = null;
			while(p < nsize) {
				var b = cape.Buffer.getByte(nbuffer, (long)p);
				if(p == 0) {
					if((b & 128) != 0) {
						fin = 1;
					}
					if((b & 64) != 0) {
						rsv1 = 1;
					}
					if((b & 32) != 0) {
						rsv2 = 1;
					}
					if((b & 16) != 0) {
						rsv3 = 1;
					}
					opcode = (int)(b & 15);
				}
				else if(p == 1) {
					mask = (int)(b & 128);
					if(mask == 0) {
						;
					}
					else {
						maskingBuffer = cape.Buffer.allocate((long)4);
					}
					payloadLength = (int)(b & 127);
					if((payloadLength >= 0) && (payloadLength < 126)) {
						p++;
						if(maskingBuffer != null) {
							lastMaskingBufferIndex = p + 3;
						}
						continue;
					}
					else if(payloadLength == 126) {
						var v = (ushort)0;
						v |= (ushort)((cape.Buffer.getByte(nbuffer, (long)(p + 1)) & 255) << 8);
						v |= (ushort)(cape.Buffer.getByte(nbuffer, (long)(p + 2)) & 255);
						payloadLength = (int)v;
						p += 3;
						if(maskingBuffer != null) {
							lastMaskingBufferIndex = p + 3;
						}
						continue;
					}
					else if(payloadLength == 127) {
						var v1 = (ulong)0;
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 1)) & 127) << 56);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 2)) & 255) << 48);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 3)) & 255) << 40);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 4)) & 255) << 32);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 5)) & 255) << 24);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 6)) & 255) << 16);
						v1 |= (ulong)((cape.Buffer.getByte(nbuffer, (long)(p + 7)) & 255) << 8);
						v1 |= (ulong)(cape.Buffer.getByte(nbuffer, (long)(p + 8)) & 255);
						payloadLength = (int)v1;
						p += 9;
						if(maskingBuffer != null) {
							lastMaskingBufferIndex = p + 3;
						}
						continue;
					}
					return(false);
				}
				else if(p <= lastMaskingBufferIndex) {
					cape.Buffer.setByte(maskingBuffer, (long)mi, b);
					mi++;
				}
				else {
					break;
				}
				p++;
			}
			var frameLength = p + payloadLength;
			if(nsize < frameLength) {
				leftOverBuffer = cape.Buffer.allocate((long)nsize);
				cape.Buffer.copyFrom(leftOverBuffer, nbuffer, (long)0, (long)0, (long)nsize);
				frameNo++;
				return(true);
			}
			payloadBuffer = cape.Buffer.allocate((long)payloadLength);
			cape.Buffer.copyFrom(payloadBuffer, nbuffer, (long)p, (long)0, (long)payloadLength);
			processFrame(fin, rsv1, rsv2, rsv3, opcode, mask, maskingBuffer, (long)payloadLength, payloadBuffer);
			frameNo = 1;
			if(nsize > frameLength) {
				var losize1 = nsize - frameLength;
				leftOverBuffer = cape.Buffer.allocate((long)losize1);
				cape.Buffer.copyFrom(leftOverBuffer, nbuffer, (long)frameLength, (long)0, (long)losize1);
			}
			return(true);
		}

		private void processFrame(int fin, int rsv1, int rsv2, int rsv3, int opcode, int mask, byte[] maskingBuffer, long payloadLength, byte[] payloadBuffer) {
			if(fin == 0) {
				return;
			}
			onNewMessage(capex.net.WSSocketGeneric.MyWSMessage.create(fin, rsv1, rsv2, rsv3, opcode, maskingBuffer, payloadBuffer));
		}

		private void onNewMessage(capex.net.WSSocketGeneric.MyWSMessage message) {
			if(message.isCloseControlFrame()) {
				close(capex.net.WSCloseEvent.CLOSE_NORMAL, null);
			}
			else if(message.isPingControlFrame()) {
				doSend(capex.net.WSSocketGeneric.MyWSMessage.forPongControlFrame());
			}
			else if(message.isPongControlFrame()) {
				;
			}
			else {
				var c = getOnMessageCallback();
				if(c != null) {
					if(message.isDataText()) {
						c(capex.net.WSMessage.forStringData(cape.String.forUTF8Buffer(message.getPayloadBuffer())));
					}
					else {
						c(capex.net.WSMessage.forData(message.getPayloadBuffer()));
					}
					return;
				}
			}
		}

		public WSSocketGeneric() {
			key = capex.net.WSSocketGeneric.WSHelper.generateKey();
			accept = capex.net.WSSocketGeneric.WSHelper.getAcceptValueForKey(key);
		}

		~WSSocketGeneric() {
			key = null;
			accept = null;
			if(parser != null) {
				parser.reset();
				parser = null;
			}
		}

		public override void connect(string url, string protocols) {
			if(client != null) {
				client.disconnect((cape.Error de) => {
					var c = getOnErrorCallback();
					if(c != null) {
						c();
					}
				});
				client = null;
			}
			client = capex.net.TCPClient.instance();
			if(!(client != null)) {
				var c1 = getOnErrorCallback();
				if(c1 != null) {
					c1();
				}
				return;
			}
			var u = cape.URL.forString(url);
			if(!(u != null)) {
				var c2 = getOnErrorCallback();
				if(c2 != null) {
					c2();
				}
				return;
			}
			var port = u.getPortInt();
			if(port < 1) {
				port = 80;
			}
			client.connect(u.getHost(), port, (cape.Error e) => {
				if(!(e != null)) {
					parser = new capex.net.WSSocketGeneric.HTTPResponseParser();
					client.receive((byte[] data, int size) => {
						onOpenHandshakeResponse(data, size);
					});
					var v = capex.net.WSSocketGeneric.WSHelper.createOpenHandshakeHTTPRequest(u, key);
					client.send(v, v.Length, (cape.Error oe) => {
						if(!(oe != null)) {
							return;
						}
						client.disconnect((cape.Error de2) => {
							var c3 = getOnErrorCallback();
							if(c3 != null) {
								c3();
							}
						});
					});
					return;
				}
				var c4 = getOnErrorCallback();
				if(c4 != null) {
					c4();
				}
			});
		}

		private void onOpenHandshakeResponse(byte[] data, int size) {
			if(!(parser != null)) {
				return;
			}
			if(size < 1) {
				var c = getOnErrorCallback();
				if(c != null) {
					c();
				}
				return;
			}
			parser.onDataReceived(data, (long)size);
			if(!parser.getEndOfResponse()) {
				return;
			}
			var headers = parser.headers;
			parser.reset();
			parser = null;
			var statusCode = headers.getHttpStatus();
			if(object.Equals(statusCode, "101")) {
				if(cape.String.equalsIgnoreCase("websocket", headers.getHeader("upgrade")) == false) {
					var c1 = getOnErrorCallback();
					if(c1 != null) {
						c1();
					}
					return;
				}
				else if(cape.String.equalsIgnoreCase("websocket", headers.getHeader("upgrade")) == false) {
					var c2 = getOnErrorCallback();
					if(c2 != null) {
						c2();
					}
					return;
				}
				else if(cape.String.equalsIgnoreCase("upgrade", headers.getHeader("connection")) == false) {
					var c3 = getOnErrorCallback();
					if(c3 != null) {
						c3();
					}
					return;
				}
				else if(cape.String.equals(accept, headers.getHeader("sec-websocket-accept")) == false) {
					var c4 = getOnErrorCallback();
					if(c4 != null) {
						c4();
					}
					return;
				}
				client.receive((byte[] d, int sz) => {
					if(processData(d, sz) == false) {
						;
					}
				});
				var c5 = getOnOpenCallback();
				if(c5 != null) {
					c5();
				}
				return;
			}
			var c6 = getOnErrorCallback();
			if(c6 != null) {
				c6();
			}
		}

		public override void send(capex.net.WSMessage message) {
			if(!(message != null)) {
				return;
			}
			if(!(client != null)) {
				return;
			}
			capex.net.WSSocketGeneric.MyWSMessage v = null;
			if(message.isText()) {
				v = capex.net.WSSocketGeneric.MyWSMessage.create(1, 0, 0, 0, 1, capex.net.WSSocketGeneric.WSHelper.generateMaskingKey(), message.getData());
			}
			else {
				v = capex.net.WSSocketGeneric.MyWSMessage.create(1, 0, 0, 0, 2, capex.net.WSSocketGeneric.WSHelper.generateMaskingKey(), message.getData());
			}
			doSend(v);
		}

		private void doSend(capex.net.WSSocketGeneric.MyWSMessage message) {
			if(!(message != null)) {
				return;
			}
			var data = message.toBuffer();
			client.send(data, data.Length, (cape.Error e) => {
				if(!(e != null)) {
					return;
				}
				var c = getOnErrorCallback();
				if(c != null) {
					c();
				}
			});
		}

		public override void close(int statusCode, string reason) {
			if(!(client != null)) {
				return;
			}
			var data = capex.net.WSSocketGeneric.MyWSMessage.forCloseControlFrame(capex.net.WSCloseEvent.CLOSE_NORMAL).toBuffer();
			client.send(data, data.Length, (cape.Error e) => {
				var c = getOnCloseCallback();
				if(!(e != null)) {
					if(c != null) {
						c((capex.net.WSCloseEvent)new capex.net.WSSocketGeneric.MyWSCloseEvent().setStatusCode(capex.net.WSCloseEvent.CLOSE_NORMAL));
					}
					return;
				}
				var ec = getOnErrorCallback();
				if(ec != null) {
					ec();
				}
				if(c != null) {
					c((capex.net.WSCloseEvent)new capex.net.WSSocketGeneric.MyWSCloseEvent().setStatusCode(capex.net.WSCloseEvent.CLOSE_ABNORMAL));
				}
			});
		}
	}
}
