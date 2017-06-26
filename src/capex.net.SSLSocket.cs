
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

namespace capex.net {
	public abstract class SSLSocket : capex.net.ConnectedSocket
	{
		public SSLSocket() {
		}

		public static capex.net.SSLSocket createInstance(capex.net.ConnectedSocket cSocket, string serverAddress = null, cape.LoggingContext ctx = null, cape.File certFile = null, cape.File keyFile = null, bool isServer = false, bool acceptInvalidCertificate = false, string passphrase = null) {
			if(!(cSocket != null)) {
				return(null);
			}
			capex.net.SSLSocket v = null;
			return(v);
		}

		public static capex.net.SSLSocket forClient(capex.net.ConnectedSocket cSocket, string hostAddress, cape.LoggingContext ctx = null, bool acceptInvalidCertificate = false, string passphrase = null) {
			return(capex.net.SSLSocket.createInstance(cSocket, hostAddress, ctx, null, null, false, acceptInvalidCertificate, passphrase));
		}

		public static capex.net.SSLSocket forServer(capex.net.ConnectedSocket cSocket, cape.File certFile = null, cape.File keyFile = null, cape.LoggingContext ctx = null, bool acceptInvalidCertificate = false, string passphrase = null) {
			return(capex.net.SSLSocket.createInstance(cSocket, null, ctx, certFile, keyFile, true, acceptInvalidCertificate, passphrase));
		}

		public abstract void setAcceptInvalidCertificate(bool accept);
		public abstract void setRequireClientCertificate(bool require);
		public abstract void close();
		public abstract int read(byte[] buffer);
		public abstract int readWithTimeout(byte[] buffer, int timeout);
		public abstract int write(byte[] buffer, int size);
		public abstract capex.net.ConnectedSocket getSocket();
	}
}
