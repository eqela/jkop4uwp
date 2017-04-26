
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
	public abstract class BlockCipher
	{
		public BlockCipher() {
		}

		public static byte[] encryptString(string data, capex.crypto.BlockCipher cipher) {
			if(object.Equals(data, null)) {
				return(null);
			}
			return(capex.crypto.BlockCipher.encryptBuffer(cape.String.toUTF8Buffer(data), cipher));
		}

		public static string decryptString(byte[] data, capex.crypto.BlockCipher cipher) {
			var db = capex.crypto.BlockCipher.decryptBuffer(data, cipher);
			if(db == null) {
				return(null);
			}
			return(cape.String.forUTF8Buffer(db));
		}

		public static byte[] encryptBuffer(byte[] data, capex.crypto.BlockCipher cipher) {
			if((cipher == null) || (data == null)) {
				return(null);
			}
			var bw = new cape.BufferWriter();
			if(bw == null) {
				return(null);
			}
			var ww = capex.crypto.BlockCipherWriter.create((cape.Writer)bw, cipher);
			if(ww == null) {
				return(null);
			}
			var r = ww.write(data);
			ww.close();
			if(r < cape.Buffer.getSize(data)) {
				return(null);
			}
			return(bw.getBuffer());
		}

		public static byte[] decryptBuffer(byte[] data, capex.crypto.BlockCipher cipher) {
			if((cipher == null) || (data == null)) {
				return(null);
			}
			var br = cape.BufferReader.forBuffer(data);
			if(br == null) {
				return(null);
			}
			var rr = capex.crypto.BlockCipherReader.create((cape.SizedReader)br, cipher);
			if(rr == null) {
				return(null);
			}
			var db = cape.Buffer.allocate(cape.Buffer.getSize(data));
			if(db == null) {
				return(null);
			}
			var ll = rr.read(db);
			if(ll < 0) {
				return(null);
			}
			if(ll < cape.Buffer.getSize(db)) {
				cape.Buffer.allocate((long)ll);
			}
			return(db);
		}

		public abstract int getBlockSize();
		public abstract void encryptBlock(byte[] src, byte[] dest);
		public abstract void decryptBlock(byte[] src, byte[] dest);
	}
}
