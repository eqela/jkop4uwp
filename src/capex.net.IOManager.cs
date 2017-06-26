
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
	public abstract class IOManager
	{
		public IOManager() {
		}

		public static capex.net.IOManager createDefault() {
			return((capex.net.IOManager)new capex.net.IOManagerDotNetSelect());
		}

		private bool executable = false;

		public virtual bool execute(cape.LoggingContext ctx) {
			return(false);
		}

		public virtual capex.net.IOManagerEntry addWithReadListener(object o, System.Action rrl) {
			var v = add(o);
			if(v != null) {
				v.setReadListener(rrl);
			}
			return(v);
		}

		public virtual capex.net.IOManagerEntry addWithWriteListener(object o, System.Action wrl) {
			var v = add(o);
			if(v != null) {
				v.setWriteListener(wrl);
			}
			return(v);
		}

		public abstract capex.net.IOManagerEntry add(object o);
		public abstract capex.net.IOManagerTimer startTimer(long delay, System.Func<bool> handler);
		public abstract void stop();

		public bool getExecutable() {
			return(executable);
		}

		public capex.net.IOManager setExecutable(bool v) {
			executable = v;
			return(this);
		}
	}
}
