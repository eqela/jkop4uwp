
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

namespace capex.net {
	public abstract class WSSocket
	{
		public WSSocket() {
		}

		public static capex.net.WSSocket create() {
			return((capex.net.WSSocket)new capex.net.WSSocketGeneric());
		}

		private System.Action onOpenCallback = null;
		private System.Action<capex.net.WSCloseEvent> onCloseCallback = null;
		private System.Action<capex.net.WSMessage> onMessageCallback = null;
		private System.Action onErrorCallback = null;
		public abstract void connect(string url, string protocols);
		public abstract void send(capex.net.WSMessage message);
		public abstract void close(int statusCode, string reason);

		public System.Action getOnOpenCallback() {
			return(onOpenCallback);
		}

		public capex.net.WSSocket setOnOpenCallback(System.Action v) {
			onOpenCallback = v;
			return(this);
		}

		public System.Action<capex.net.WSCloseEvent> getOnCloseCallback() {
			return(onCloseCallback);
		}

		public capex.net.WSSocket setOnCloseCallback(System.Action<capex.net.WSCloseEvent> v) {
			onCloseCallback = v;
			return(this);
		}

		public System.Action<capex.net.WSMessage> getOnMessageCallback() {
			return(onMessageCallback);
		}

		public capex.net.WSSocket setOnMessageCallback(System.Action<capex.net.WSMessage> v) {
			onMessageCallback = v;
			return(this);
		}

		public System.Action getOnErrorCallback() {
			return(onErrorCallback);
		}

		public capex.net.WSSocket setOnErrorCallback(System.Action v) {
			onErrorCallback = v;
			return(this);
		}
	}
}
