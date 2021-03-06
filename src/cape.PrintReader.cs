
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
	public class PrintReader : cape.Reader, cape.LineReader, cape.Closable
	{
		private cape.Reader reader = null;
		private cape.CharacterIteratorForReader iterator = null;

		public PrintReader(cape.Reader reader) {
			setReader(reader);
		}

		public void setReader(cape.Reader reader) {
			this.reader = reader;
			if(reader == null) {
				this.iterator = null;
			}
			else {
				this.iterator = new cape.CharacterIteratorForReader(reader);
			}
		}

		public virtual string readLine() {
			if(iterator == null) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			while(true) {
				var c = iterator.getNextChar();
				if(c < 1) {
					if(sb.count() < 1) {
						return(null);
					}
					break;
				}
				if(c == '\r') {
					continue;
				}
				if(c == '\n') {
					break;
				}
				sb.append(c);
			}
			if(sb.count() < 1) {
				return("");
			}
			return(sb.toString());
		}

		public virtual int read(byte[] buffer) {
			if(reader == null) {
				return(-1);
			}
			return(reader.read(buffer));
		}

		public virtual void close() {
			var rc = reader as cape.Closable;
			if(rc != null) {
				rc.close();
			}
		}
	}
}
