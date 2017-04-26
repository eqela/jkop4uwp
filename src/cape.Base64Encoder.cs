
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

namespace cape
{
	public class Base64Encoder
	{
		public Base64Encoder() {
		}

		public static string encodeString(string str) {
			if(!(str != null)) {
				return(null);
			}
			return(cape.Base64Encoder.encode(cape.String.toUTF8Buffer(str)));
		}

		public static string encode(byte[] buffer) {
			if(!(buffer != null)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			var i = (long)0;
			var b64i = 0;
			var size = cape.Buffer.getSize(buffer);
			while(i < size) {
				b64i = (int)(cape.Buffer.getByte(buffer, i) >> 2);
				sb.append(cape.Base64Encoder.toASCIIChar(b64i));
				b64i = (int)((cape.Buffer.getByte(buffer, i) & 3) << 4);
				if((i + 1) < size) {
					b64i += (int)(cape.Buffer.getByte(buffer, i + 1) >> 4);
					sb.append(cape.Base64Encoder.toASCIIChar(b64i));
					b64i = (int)((cape.Buffer.getByte(buffer, i + 1) & 15) << 2);
					if((i + 2) < size) {
						b64i += (int)(cape.Buffer.getByte(buffer, i + 2) >> 6);
						sb.append(cape.Base64Encoder.toASCIIChar(b64i));
						b64i = (int)(cape.Buffer.getByte(buffer, i + 2) & 63);
						sb.append(cape.Base64Encoder.toASCIIChar(b64i));
					}
					else {
						sb.append(cape.Base64Encoder.toASCIIChar(b64i));
						sb.append("=");
					}
				}
				else {
					sb.append(cape.Base64Encoder.toASCIIChar(b64i));
					sb.append("==");
				}
				i += (long)3;
			}
			if(sb != null) {
				return(sb.toString());
			}
			return(null);
		}

		public static char toASCIIChar(int lookup) {
			var c = 0;
			if((lookup < 0) || (lookup > 63)) {
				return((char)c);
			}
			if(lookup <= 25) {
				c = lookup + 65;
			}
			else if(lookup <= 51) {
				c = lookup + 71;
			}
			else if(lookup <= 61) {
				c = lookup - 4;
			}
			else if(lookup == 62) {
				c = (int)'+';
			}
			else if(lookup == 63) {
				c = (int)'/';
			}
			return((char)c);
		}
	}
}
