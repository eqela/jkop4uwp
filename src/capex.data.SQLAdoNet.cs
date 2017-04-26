
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

namespace capex.data
{
	public class SQLAdoNet
	{
		public SQLAdoNet() {
		}

		public static string convertSQLString(string sql) {
			if(object.Equals(sql, null)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			var quote = false;
			var dquote = false;
			var slash = false;
			var n = 1;
			var it = cape.String.iterate(sql);
			if(it == null) {
				return(null);
			}
			while(true) {
				var c = it.getNextChar();
				if(c < 1) {
					break;
				}
				if(quote) {
					if((c == '\\') && (slash == false)) {
						slash = true;
					}
					else {
						if((c == '\'') && (slash == false)) {
							quote = false;
						}
						slash = false;
					}
					sb.append(c);
				}
				else if(dquote) {
					if((c == '\\') && (slash == false)) {
						slash = true;
					}
					else {
						if((c == '\"') && (slash == false)) {
							dquote = false;
						}
						slash = false;
					}
					sb.append(c);
				}
				else if(c == '?') {
					sb.append("@p" + cape.String.forInteger(n));
					n++;
				}
				else if(c == '\'') {
					sb.append(c);
					quote = true;
				}
				else if(c == '\"') {
					sb.append(c);
					dquote = true;
				}
				else {
					sb.append(c);
				}
			}
			return(sb.toString());
		}
	}
}
