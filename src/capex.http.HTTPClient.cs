
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
	public abstract class HTTPClient
	{
		public HTTPClient() {
		}

		public static capex.http.HTTPClient createDefault() {
			return((capex.http.HTTPClient)new capex.http.HTTPClientOperation());
		}

		public static void execute(capex.http.HTTPClient client, capex.http.HTTPClientRequest request, capex.http.HTTPClientListener listener) {
			if(!(client != null)) {
				return;
			}
			client.executeRequest(request, listener);
		}

		private class MyListener : capex.http.HTTPClientListener
		{
			public MyListener() : base() {
			}

			private capex.http.HTTPClientResponse response = null;
			private byte[] buffer = null;

			public override bool onResponseReceived(capex.http.HTTPClientResponse response) {
				this.response = response;
				return(true);
			}

			public override bool onDataReceived(byte[] buffer) {
				this.buffer = cape.Buffer.append(this.buffer, buffer);
				return(true);
			}

			public override void onAborted() {
			}

			public override void onError(string message) {
			}

			public override void onResponseCompleted() {
			}

			public capex.http.HTTPClientResponse getResponse() {
				return(response);
			}

			public capex.http.HTTPClient.MyListener setResponse(capex.http.HTTPClientResponse v) {
				response = v;
				return(this);
			}

			public byte[] getBuffer() {
				return(buffer);
			}

			public capex.http.HTTPClient.MyListener setBuffer(byte[] v) {
				buffer = v;
				return(this);
			}
		}

		public static void execute(capex.http.HTTPClient client, capex.http.HTTPClientRequest request, System.Action<capex.http.HTTPClientResponse> listener) {
			var ll = new capex.http.HTTPClient.MyListener();
			capex.http.HTTPClient.execute(client, request, (capex.http.HTTPClientListener)ll);
			if(listener != null) {
				listener(ll.getResponse());
			}
		}

		public static void executeForBuffer(capex.http.HTTPClient client, capex.http.HTTPClientRequest request, System.Action<capex.http.HTTPClientResponse, byte[]> listener) {
			var ll = new capex.http.HTTPClient.MyListener();
			capex.http.HTTPClient.execute(client, request, (capex.http.HTTPClientListener)ll);
			if(listener != null) {
				listener(ll.getResponse(), ll.getBuffer());
			}
		}

		public static void executeForString(capex.http.HTTPClient client, capex.http.HTTPClientRequest request, System.Action<capex.http.HTTPClientResponse, string> listener) {
			var ll = new capex.http.HTTPClient.MyListener();
			capex.http.HTTPClient.execute(client, request, (capex.http.HTTPClientListener)ll);
			if(listener != null) {
				listener(ll.getResponse(), cape.String.forUTF8Buffer(ll.getBuffer()));
			}
		}

		public abstract void executeRequest(capex.http.HTTPClientRequest request, capex.http.HTTPClientListener listener);
	}
}
