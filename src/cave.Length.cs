
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

namespace cave
{
	public class Length
	{
		public Length() {
		}

		public static int asPoints(string value, int ppi) {
			return(cave.Length.forString(value).toPoints(ppi));
		}

		public static cave.Length forString(string value) {
			var v = new cave.Length();
			v.parse(value);
			return(v);
		}

		public static cave.Length forPoints(double value) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(cave.Length.UNIT_POINT);
			return(v);
		}

		public static cave.Length forMilliMeters(double value) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(cave.Length.UNIT_MILLIMETER);
			return(v);
		}

		public static cave.Length forMicroMeters(double value) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(cave.Length.UNIT_MICROMETER);
			return(v);
		}

		public static cave.Length forNanoMeters(double value) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(cave.Length.UNIT_NANOMETER);
			return(v);
		}

		public static cave.Length forInches(double value) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(cave.Length.UNIT_INCH);
			return(v);
		}

		public static cave.Length forValue(double value, int unit) {
			var v = new cave.Length();
			v.setValue(value);
			v.setUnit(unit);
			return(v);
		}

		public static cave.Length forStringAsPoints(string value, int ppi) {
			var v = new cave.Length();
			v.parse(value);
			v.setValue((double)v.toPoints(ppi));
			v.setUnit(cave.Length.UNIT_POINT);
			return(v);
		}

		public const int UNIT_POINT = 0;
		public const int UNIT_MILLIMETER = 1;
		public const int UNIT_MICROMETER = 2;
		public const int UNIT_NANOMETER = 3;
		public const int UNIT_INCH = 4;
		private double value = 0.00;
		private int unit = 0;

		private void parse(string value) {
			if(object.Equals(value, null)) {
				this.value = (double)0;
				unit = cave.Length.UNIT_POINT;
				return;
			}
			var i = 0;
			var n = 0;
			var it = cape.String.iterate(value);
			while(true) {
				var c = it.getNextChar();
				if(c < 1) {
					break;
				}
				else if(c >= '0' && c <= '9') {
					i *= 10;
					i += (int)(c - '0');
				}
				else {
					break;
				}
				n++;
			}
			this.value = (double)i;
			var suffix = cape.String.subString(value, n);
			if(cape.String.isEmpty(suffix)) {
				unit = cave.Length.UNIT_POINT;
			}
			else if(object.Equals(suffix, "pt") || object.Equals(suffix, "px")) {
				unit = cave.Length.UNIT_POINT;
			}
			else if(object.Equals(suffix, "mm")) {
				unit = cave.Length.UNIT_MILLIMETER;
			}
			else if(object.Equals(suffix, "um")) {
				unit = cave.Length.UNIT_MICROMETER;
			}
			else if(object.Equals(suffix, "nm")) {
				unit = cave.Length.UNIT_NANOMETER;
			}
			else if(object.Equals(suffix, "in")) {
				unit = cave.Length.UNIT_INCH;
			}
			else {
				unit = cave.Length.UNIT_POINT;
			}
		}

		public int toPoints(int ppi) {
			if(unit == cave.Length.UNIT_POINT) {
				return((int)value);
			}
			if(unit == cave.Length.UNIT_MILLIMETER) {
				var v = value * ppi / 25;
				if(value > 0 && v < 1) {
					v = (double)1;
				}
				return((int)v);
			}
			if(unit == cave.Length.UNIT_MICROMETER) {
				var v1 = value * ppi / 25000;
				if(value > 0 && v1 < 1) {
					v1 = (double)1;
				}
				return((int)v1);
			}
			if(unit == cave.Length.UNIT_NANOMETER) {
				var v2 = value * ppi / 25000000;
				if(value > 0 && v2 < 1) {
					v2 = (double)1;
				}
				return((int)v2);
			}
			if(unit == cave.Length.UNIT_INCH) {
				var v3 = value * ppi;
				if(value > 0 && v3 < 1) {
					v3 = (double)1;
				}
				return((int)v3);
			}
			return(0);
		}

		public double getValue() {
			return(value);
		}

		public cave.Length setValue(double v) {
			value = v;
			return(this);
		}

		public int getUnit() {
			return(unit);
		}

		public cave.Length setUnit(int v) {
			unit = v;
			return(this);
		}
	}
}
