
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

namespace capex.text
{
	public class HTMLString
	{
		public HTMLString() {
		}

		public static string sanitize(string str) {
			if(object.Equals(str, null)) {
				return(null);
			}
			if(((cape.String.indexOf(str, '<') < 0) && (cape.String.indexOf(str, '>') < 0)) && (cape.String.indexOf(str, '&') < 0)) {
				return(str);
			}
			var it = cape.String.iterate(str);
			if(it == null) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			var c = ' ';
			while((c = it.getNextChar()) > 0) {
				if(c == '<') {
					sb.append("&lt;");
				}
				else if(c == '>') {
					sb.append("&gt;");
				}
				else if(c == '&') {
					sb.append("&amp;");
				}
				else {
					sb.append(c);
				}
			}
			return(sb.toString());
		}

		public static string toQuotedString(string str) {
			var sb = new cape.StringBuilder();
			sb.append('\"');
			if(!(object.Equals(str, null))) {
				var it = cape.String.iterate(str);
				while(it != null) {
					var c = it.getNextChar();
					if(c < 1) {
						break;
					}
					if(c == '\"') {
						sb.append('\\');
						sb.append('\"');
					}
					else {
						sb.append(c);
					}
				}
			}
			sb.append('\"');
			return(sb.toString());
		}
	}
}
