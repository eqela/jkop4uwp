
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
	public class BinaryReader : cape.Closable
	{
		public BinaryReader() {
		}

		public static cape.BinaryReader forReader(cape.Reader reader) {
			var v = new cape.BinaryReader();
			v.setReader(reader);
			return(v);
		}

		private cape.Reader reader = null;
		private byte[] buffer1 = null;
		private byte[] buffer2 = null;
		private byte[] buffer4 = null;

		public bool isOK() {
			if(!(reader != null)) {
				return(false);
			}
			return(true);
		}

		public virtual void close() {
			var rc = reader as cape.Closable;
			if(rc != null) {
				rc.close();
			}
			reader = null;
		}

		public bool seek(long position) {
			var sr = reader as cape.SeekableReader;
			if(!(sr != null)) {
				return(false);
			}
			return(sr.setCurrentPosition(position));
		}

		public byte[] read1() {
			if(!(reader != null)) {
				return(null);
			}
			if(!(buffer1 != null)) {
				buffer1 = new byte[1];
			}
			var r = reader.read(buffer1);
			if(!(r == 1)) {
				close();
				return(null);
			}
			return(buffer1);
		}

		public byte[] read2() {
			if(!(reader != null)) {
				return(null);
			}
			if(!(buffer2 != null)) {
				buffer2 = new byte[2];
			}
			var r = reader.read(buffer2);
			if(!(r == 2)) {
				close();
				return(null);
			}
			return(buffer2);
		}

		public byte[] read4() {
			if(!(reader != null)) {
				return(null);
			}
			if(!(buffer4 != null)) {
				buffer4 = new byte[4];
			}
			var r = reader.read(buffer4);
			if(!(r == 4)) {
				close();
				return(null);
			}
			return(buffer4);
		}

		public byte[] readBuffer(long size) {
			if(!(reader != null)) {
				return(null);
			}
			if(!(size > 0)) {
				return(null);
			}
			var b = new byte[size];
			var r = reader.read(b);
			if(!(r == size)) {
				close();
				return(null);
			}
			return(b);
		}

		public byte readInt8() {
			var b = read1();
			if(!(b != null)) {
				return((byte)0);
			}
			return(cape.Buffer.getInt8(b, (long)0));
		}

		public ushort readInt16LE() {
			var b = read2();
			if(!(b != null)) {
				return((ushort)0);
			}
			return(cape.Buffer.getInt16LE(b, (long)0));
		}

		public ushort readInt16BE() {
			var b = read2();
			if(!(b != null)) {
				return((ushort)0);
			}
			return(cape.Buffer.getInt16BE(b, (long)0));
		}

		public uint readInt32LE() {
			var b = read4();
			if(!(b != null)) {
				return((uint)0);
			}
			return(cape.Buffer.getInt32LE(b, (long)0));
		}

		public uint readInt32BE() {
			var b = read4();
			if(!(b != null)) {
				return((uint)0);
			}
			return(cape.Buffer.getInt32BE(b, (long)0));
		}

		public cape.Reader getReader() {
			return(reader);
		}

		public cape.BinaryReader setReader(cape.Reader v) {
			reader = v;
			return(this);
		}
	}
}
