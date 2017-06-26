
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

namespace capex.util {
	public class PaperSize : cape.StringObject
	{
		public PaperSize() {
		}

		public static System.Collections.Generic.List<capex.util.PaperSize> getAll() {
			var v = new System.Collections.Generic.List<capex.util.PaperSize>();
			var n = 0;
			for(n = 0 ; n < capex.util.PaperSize.COUNT ; n++) {
				cape.Vector.append(v, capex.util.PaperSize.forValue(n));
			}
			return(v);
		}

		public static bool matches(capex.util.PaperSize sz, int value) {
			if(sz != null && sz.getValue() == value) {
				return(true);
			}
			return(false);
		}

		public static capex.util.PaperSize forValue(int value) {
			return(new capex.util.PaperSize().setValue(value));
		}

		public const int LETTER = 0;
		public const int LEGAL = 1;
		public const int A3 = 2;
		public const int A4 = 3;
		public const int A5 = 4;
		public const int B4 = 5;
		public const int B5 = 6;
		public const int COUNT = 7;
		private int value = 0;

		public virtual string toString() {
			if(value == capex.util.PaperSize.LETTER) {
				return("US Letter");
			}
			if(value == capex.util.PaperSize.LEGAL) {
				return("US Legal");
			}
			if(value == capex.util.PaperSize.A3) {
				return("A3");
			}
			if(value == capex.util.PaperSize.A4) {
				return("A4");
			}
			if(value == capex.util.PaperSize.A5) {
				return("A5");
			}
			if(value == capex.util.PaperSize.B4) {
				return("B4");
			}
			if(value == capex.util.PaperSize.B5) {
				return("B5");
			}
			return("Unknown paper size");
		}

		public int getValue() {
			return(value);
		}

		public capex.util.PaperSize setValue(int v) {
			value = v;
			return(this);
		}
	}
}
