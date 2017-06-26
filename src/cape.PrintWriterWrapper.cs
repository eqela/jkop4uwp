
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
	public class PrintWriterWrapper : cape.Writer, cape.PrintWriter, cape.Closable, cape.FlushableWriter
	{
		public PrintWriterWrapper() {
		}

		public static cape.PrintWriter forWriter(cape.Writer writer) {
			if(writer == null) {
				return(null);
			}
			if(writer is cape.PrintWriter) {
				return((cape.PrintWriter)writer);
			}
			var v = new cape.PrintWriterWrapper();
			v.setWriter(writer);
			return((cape.PrintWriter)v);
		}

		private cape.Writer writer = null;

		public virtual bool print(string str) {
			if(object.Equals(str, null)) {
				return(false);
			}
			var buffer = cape.String.toUTF8Buffer(str);
			if(buffer == null) {
				return(false);
			}
			var sz = (int)cape.Buffer.getSize(buffer);
			if(writer.write(buffer, -1) != sz) {
				return(false);
			}
			return(true);
		}

		public virtual bool println(string str) {
			return(print(str + "\n"));
		}

		public virtual int write(byte[] buf, int size = -1) {
			if(writer == null) {
				return(-1);
			}
			return(writer.write(buf, size));
		}

		public virtual void close() {
			var cw = writer as cape.Closable;
			if(cw != null) {
				cw.close();
			}
		}

		public virtual void flush() {
			var cw = writer as cape.FlushableWriter;
			if(cw != null) {
				cw.flush();
			}
		}

		public cape.Writer getWriter() {
			return(writer);
		}

		public cape.PrintWriterWrapper setWriter(cape.Writer v) {
			writer = v;
			return(this);
		}
	}
}
