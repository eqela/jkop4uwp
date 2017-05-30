
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

namespace capex.crypto
{
	public class BlockCipherReader : cape.Reader, cape.SizedReader, cape.SeekableReader
	{
		public BlockCipherReader() {
		}

		public static cape.SizedReader create(cape.SizedReader reader, capex.crypto.BlockCipher cipher) {
			if(reader == null) {
				return(null);
			}
			if(cipher == null) {
				return(reader);
			}
			var v = new capex.crypto.BlockCipherReader();
			v.reader = reader;
			v.cipher = cipher;
			v.bcurrent = cape.Buffer.allocate((long)cipher.getBlockSize());
			v.bnext = cape.Buffer.allocate((long)cipher.getBlockSize());
			v.ddata = cape.Buffer.allocate((long)cipher.getBlockSize());
			v.csize = 0;
			v.cindex = 0;
			v.nsize = 0;
			return((cape.SizedReader)v);
		}

		private capex.crypto.BlockCipher cipher = null;
		private cape.SizedReader reader = null;
		private byte[] bcurrent = null;
		private int csize = 0;
		private int cindex = 0;
		private byte[] bnext = null;
		private int nsize = 0;
		private byte[] ddata = null;

		public virtual int getSize() {
			if(reader != null) {
				return(reader.getSize());
			}
			return(0);
		}

		public virtual bool setCurrentPosition(long n) {
			var rem = (int)(n % cipher.getBlockSize());
			var ss = (int)(n - rem);
			csize = 0;
			cindex = 0;
			nsize = 0;
			var v = false;
			if(reader != null && reader is cape.SeekableReader) {
				v = ((cape.SeekableReader)reader).setCurrentPosition((long)ss);
			}
			if(v && rem > 0) {
				var bb = cape.Buffer.allocate((long)rem);
				if(read(bb) != rem) {
					v = false;
				}
			}
			return(v);
		}

		public virtual long getCurrentPosition() {
			if(reader != null && reader is cape.SeekableReader) {
				return(((cape.SeekableReader)reader).getCurrentPosition());
			}
			return((long)-1);
		}

		public virtual int read(byte[] buf) {
			if(buf == null || cape.Buffer.getSize(buf) < 1) {
				return(0);
			}
			var ptr = buf;
			if(ptr == null) {
				return(0);
			}
			var v = 0;
			var bs = cipher.getBlockSize();
			while(v < cape.Buffer.getSize(buf)) {
				var x = bs;
				if(v + x > cape.Buffer.getSize(buf)) {
					x = (int)(cape.Buffer.getSize(buf) - v);
				}
				var r = readBlock(ptr, v, x);
				if(r < 1) {
					break;
				}
				v += r;
			}
			return(v);
		}

		public int readAndDecrypt(byte[] buf) {
			var v = reader.read(ddata);
			if(v == cipher.getBlockSize()) {
				cipher.decryptBlock(ddata, buf);
			}
			else {
				cape.Buffer.copyFrom(ddata, buf, (long)0, (long)0, (long)v);
			}
			return(v);
		}

		public int readBlock(byte[] ptr, int offset, int size) {
			if(cindex >= csize) {
				csize = 0;
			}
			if(nsize < 1) {
				nsize = readAndDecrypt(bnext);
			}
			if(csize < 1) {
				if(nsize < cipher.getBlockSize()) {
					return(0);
				}
				var nn = bcurrent;
				bcurrent = bnext;
				csize = nsize;
				cindex = 0;
				bnext = nn;
				nsize = readAndDecrypt(bnext);
			}
			var data = cipher.getBlockSize();
			if(nsize < cipher.getBlockSize()) {
				var ptr2 = bnext;
				if(ptr2 != null) {
					data -= (int)cape.Buffer.getByte(ptr2, (long)0);
				}
			}
			data -= cindex;
			if(data < 1) {
				csize = 0;
				return(readBlock(ptr, offset, size));
			}
			if(data < size) {
				cape.Buffer.copyFrom(bcurrent, ptr, (long)cindex, (long)offset, (long)data);
				cindex += data;
				return(data);
			}
			cape.Buffer.copyFrom(bcurrent, ptr, (long)cindex, (long)offset, (long)size);
			cindex += size;
			return(size);
		}
	}
}
