
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

namespace capex.net
{
	public abstract class WSCloseEvent
	{
		public WSCloseEvent() {
		}

		public const int CLOSE_NORMAL = 1000;
		public const int CLOSE_GOING_AWAY = 1001;
		public const int CLOSE_PROTOCOL_ERROR = 1002;
		public const int CLOSE_UNSUPPORTED = 1003;
		public const int CLOSE_NO_STATUS = 1005;
		public const int CLOSE_ABNORMAL = 1006;
		public const int UNSUPPORTED_DATA = 1007;
		public const int POLICY_VIOLATION = 1008;
		public const int CLOSE_TOO_LARGE = 1009;
		public const int MISSING_EXTENSION = 1010;
		public const int INTERNAL_ERROR = 1011;
		public const int SERVICE_RESTART = 1012;
		public const int TRY_AGAIN_LATER = 1013;
		public const int TLS_HANDSHAKE = 1015;
		public abstract int getStatusCode();
		public abstract string getReason();
	}
}
