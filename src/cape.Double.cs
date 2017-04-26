
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
	public class Double
	{
		public Double() {
		}

		private class MyDoubleObject : cape.DoubleObject
		{
			public MyDoubleObject() {
			}

			private double value = 0.00;

			public virtual double toDouble() {
				return(value);
			}

			public double getValue() {
				return(value);
			}

			public cape.Double.MyDoubleObject setValue(double v) {
				value = v;
				return(this);
			}
		}

		public static cape.DoubleObject asObject(double value) {
			var v = new cape.Double.MyDoubleObject();
			v.setValue(value);
			return((cape.DoubleObject)v);
		}

		public static double asDouble(object obj) {
			if(obj == null) {
				return(0.00);
			}
			if(obj is cape.DoubleObject) {
				return(((cape.DoubleObject)obj).toDouble());
			}
			if(obj is cape.IntegerObject) {
				return((double)((cape.IntegerObject)obj).toInteger());
			}
			if(obj is string) {
				return(cape.String.toDouble((string)obj));
			}
			if(obj is cape.StringObject) {
				return(cape.String.toDouble(((cape.StringObject)obj).toString()));
			}
			if(obj is cape.BooleanObject) {
				if(((cape.BooleanObject)obj).toBoolean()) {
					return(1.00);
				}
				return(0.00);
			}
			if(obj is cape.CharacterObject) {
				return((double)((cape.CharacterObject)obj).toCharacter());
			}
			return(0.00);
		}
	}
}
