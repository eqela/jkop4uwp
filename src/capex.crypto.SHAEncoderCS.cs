
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

namespace capex.crypto {
	public class SHAEncoderCS : capex.crypto.SHAEncoder
	{
		public SHAEncoderCS() : base() {
		}

		public override byte[] encodeAsBuffer(byte[] data, int version) {
			if(!(data != null)) {
				return(null);
			}
			System.Security.Cryptography.HashAlgorithm hashAlgorithm = null;
			if(capex.crypto.SHAEncoder.SHA1 == version) {
				hashAlgorithm = System.Security.Cryptography.SHA1.Create();
			}
			else if(capex.crypto.SHAEncoder.SHA256 == version) {
				hashAlgorithm = System.Security.Cryptography.SHA256.Create();
			}
			else if(capex.crypto.SHAEncoder.SHA384 == version) {
				hashAlgorithm = System.Security.Cryptography.SHA384.Create();
			}
			else if(capex.crypto.SHAEncoder.SHA512 == version) {
				hashAlgorithm = System.Security.Cryptography.SHA512.Create();
			}
			if(!(hashAlgorithm != null)) {
				return(null);
			}
			return(hashAlgorithm.ComputeHash(data));
		}

		public override string encodeAsString(byte[] data, int version) {
			var encodedBytes = encodeAsBuffer(data, version);
			if(encodedBytes == null) {
				return(null);
			}
			string result = null;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			foreach(byte b in encodedBytes) {
				sb.Append(b.ToString("x2"));
			}
			result = sb.ToString();
			return(result);
		}
	}
}
