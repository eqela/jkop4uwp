
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

namespace cape {
	public class URLDecoder
	{
		public URLDecoder() {
		}

		public static int xcharToInteger(char c) {
			if(c >= '0' && c <= '9') {
				return((int)c - '0');
			}
			else if(c >= 'a' && c <= 'f') {
				return(10 + c - 'a');
			}
			else if(c >= 'A' && c <= 'F') {
				return(10 + c - 'A');
			}
			return(0);
		}

		public static string decode(string astr) {
			if(!(astr != null)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			var str = cape.String.strip(astr);
			var it = cape.String.iterate(str);
			while(it != null) {
				var x = it.getNextChar();
				if(x < 1) {
					break;
				}
				if(x == '%') {
					var x1 = it.getNextChar();
					var x2 = it.getNextChar();
					if(x1 > 0 && x2 > 0) {
						sb.append((char)(cape.URLDecoder.xcharToInteger(x1) * 16 + cape.URLDecoder.xcharToInteger(x2)));
					}
					else {
						break;
					}
				}
				else if(x == '+') {
					sb.append(' ');
				}
				else {
					sb.append(x);
				}
			}
			return(sb.toString());
		}
	}
}
