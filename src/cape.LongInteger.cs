
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

namespace cape {
	public class LongInteger : cape.LongIntegerObject
	{
		public LongInteger() {
		}

		public static cape.LongInteger forValue(long value) {
			return(cape.LongInteger.asObject(value));
		}

		public static cape.LongInteger asObject(long value) {
			var v = new cape.LongInteger();
			v.setValue(value);
			return(v);
		}

		public static long asLong(string str) {
			if(!(str != null)) {
				return((long)0);
			}
			return(cape.String.toLong(str));
		}

		public static long asLong(object obj) {
			if(!(obj != null)) {
				return((long)0);
			}
			if(obj is cape.IntegerObject) {
				return((long)((cape.IntegerObject)obj).toInteger());
			}
			if(obj is cape.LongIntegerObject) {
				return(((cape.LongIntegerObject)obj).toLong());
			}
			if(obj is string) {
				return(cape.String.toLong((string)obj));
			}
			if(obj is cape.StringObject) {
				return(cape.String.toLong(((cape.StringObject)obj).toString()));
			}
			if(obj is cape.DoubleObject) {
				return((long)((cape.DoubleObject)obj).toDouble());
			}
			if(obj is cape.BooleanObject) {
				if(((cape.BooleanObject)obj).toBoolean()) {
					return((long)1);
				}
				return((long)0);
			}
			if(obj is cape.CharacterObject) {
				return((long)((cape.CharacterObject)obj).toCharacter());
			}
			return((long)0);
		}

		private long value = (long)0;

		public void add(long amount) {
			value += amount;
		}

		public virtual long toLong() {
			return(value);
		}

		public long getValue() {
			return(value);
		}

		public cape.LongInteger setValue(long v) {
			value = v;
			return(this);
		}
	}
}
