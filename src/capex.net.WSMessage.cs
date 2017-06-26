
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
	public class WSMessage
	{
		public WSMessage() {
		}

		public static capex.net.WSMessage forStringData(string data) {
			if(!(data != null)) {
				return(null);
			}
			return(new capex.net.WSMessage().setData(data));
		}

		public static capex.net.WSMessage forData(byte[] data) {
			if(!(data != null)) {
				return(null);
			}
			return(new capex.net.WSMessage().setData(data));
		}

		private byte[] data = null;
		private bool isString = false;

		public bool isText() {
			return(isString);
		}

		public capex.net.WSMessage setData(string v) {
			if(cape.String.isEmpty(v)) {
				return(this);
			}
			isString = true;
			data = cape.String.toUTF8Buffer(v);
			return(this);
		}

		public capex.net.WSMessage setData(byte[] v) {
			isString = false;
			data = v;
			return(this);
		}

		public string getDataAsString() {
			if(isText() == false) {
				return(null);
			}
			return(cape.String.forUTF8Buffer(data));
		}

		public byte[] getData() {
			return(data);
		}

		~WSMessage() {
			data = null;
		}
	}
}
