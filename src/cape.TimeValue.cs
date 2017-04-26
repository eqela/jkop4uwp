
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

namespace cape
{
	public class TimeValue
	{
		public TimeValue() {
		}

		public static cape.TimeValue forSeconds(long seconds) {
			var v = new cape.TimeValue();
			v.seconds = seconds;
			return(v);
		}

		private long seconds = (long)0;
		private long microSeconds = (long)0;

		public cape.TimeValue dup() {
			var v = new cape.TimeValue();
			v.copyFrom(this);
			return(v);
		}

		public void reset() {
			seconds = (long)0;
			microSeconds = (long)0;
		}

		public void copyFrom(cape.TimeValue tv) {
			seconds = tv.seconds;
			microSeconds = tv.microSeconds;
		}

		public void set(cape.TimeValue tv) {
			seconds = tv.getSeconds();
			microSeconds = tv.getMicroSeconds();
		}

		public void setSeconds(long value) {
			seconds = value;
		}

		public void setMilliSeconds(long value) {
			microSeconds = value * 1000;
		}

		public void setMicroSeconds(long value) {
			microSeconds = value;
		}

		public cape.TimeValue add(long s, long us) {
			var ts = this.getSeconds() + s;
			var tus = this.getMicroSeconds() + us;
			if(tus > 1000000) {
				ts += tus / 1000000;
				tus = tus % 1000000;
			}
			while(tus < 0) {
				ts--;
				tus += (long)1000000;
			}
			var v = new cape.TimeValue();
			v.seconds = ts;
			v.microSeconds = tus;
			return(v);
		}

		public cape.TimeValue add(cape.TimeValue tv) {
			if(tv == null) {
				return(this);
			}
			return(add(tv.getSeconds(), tv.getMicroSeconds()));
		}

		public cape.TimeValue subtract(cape.TimeValue tv) {
			if(tv == null) {
				return(this);
			}
			return(add(-tv.getSeconds(), -tv.getMicroSeconds()));
		}

		public long asMicroSeconds() {
			return((long)((this.getSeconds() * 1000000) + this.getMicroSeconds()));
		}

		public static long diff(cape.TimeValue a, cape.TimeValue b) {
			if((a == null) && (b == null)) {
				return((long)0);
			}
			if(a == null) {
				return(b.asMicroSeconds());
			}
			if(b == null) {
				return(a.asMicroSeconds());
			}
			var r = ((a.seconds - b.seconds) * 1000000) + (a.microSeconds - b.microSeconds);
			return(r);
		}

		public static double diffDouble(cape.TimeValue a, cape.TimeValue b) {
			return((double)(cape.TimeValue.diff(a, b) / 1000000.00));
		}

		public long getSeconds() {
			return(seconds);
		}

		public long getMicroSeconds() {
			return(microSeconds);
		}
	}
}
