
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
	public class StringBuilder : cape.StringObject
	{
		private System.Text.StringBuilder builder = null;

		public StringBuilder() {
			initialize();
		}

		public StringBuilder(string initial) {
			initialize();
			append(initial);
		}

		public StringBuilder(cape.StringBuilder initial) {
			initialize();
			if(initial != null) {
				append(initial.toString());
			}
		}

		public void initialize() {
			builder = new System.Text.StringBuilder();
		}

		public void clear() {
			builder.Clear();
		}

		public int count() {
			return(builder.Length);
		}

		public cape.StringBuilder append(int c) {
			return(append(cape.String.forInteger(c)));
		}

		public cape.StringBuilder append(char c) {
			if(c == 0) {
				return(this);
			}
			return(append(cape.String.forCharacter(c)));
		}

		public cape.StringBuilder append(double c) {
			return(append(cape.String.forDouble(c)));
		}

		public cape.StringBuilder append(float c) {
			return(append(cape.String.forFloat(c)));
		}

		public cape.StringBuilder append(string str) {
			if(object.Equals(str, null)) {
				return(this);
			}
			builder.Append(str);
			return(this);
		}

		public cape.StringBuilder insert(int index, int c) {
			return(insert(index, cape.String.forInteger(c)));
		}

		public cape.StringBuilder insert(int index, char c) {
			if(c == 0) {
				return(this);
			}
			return(insert(index, cape.String.forCharacter(c)));
		}

		public cape.StringBuilder insert(int index, double c) {
			return(insert(index, cape.String.forDouble(c)));
		}

		public cape.StringBuilder insert(int index, float c) {
			return(insert(index, cape.String.forFloat(c)));
		}

		public cape.StringBuilder insert(int index, string str) {
			if(object.Equals(str, null)) {
				return(this);
			}
			builder.Insert(index, str);
			return(this);
		}

		public virtual string toString() {
			return(builder.ToString());
		}
	}
}
