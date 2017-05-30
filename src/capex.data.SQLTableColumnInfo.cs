
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

namespace capex.data
{
	public class SQLTableColumnInfo
	{
		public SQLTableColumnInfo() {
		}

		public static capex.data.SQLTableColumnInfo instance(string name, int type) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(type));
		}

		public static capex.data.SQLTableColumnInfo forInteger(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_INTEGER));
		}

		public static capex.data.SQLTableColumnInfo forString(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_STRING));
		}

		public static capex.data.SQLTableColumnInfo forStringKey(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_STRING_KEY));
		}

		public static capex.data.SQLTableColumnInfo forText(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_TEXT));
		}

		public static capex.data.SQLTableColumnInfo forIntegerKey(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_INTEGER_KEY));
		}

		public static capex.data.SQLTableColumnInfo forLongKey(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_LONG_KEY));
		}

		public static capex.data.SQLTableColumnInfo forLong(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_LONG));
		}

		public static capex.data.SQLTableColumnInfo forDouble(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_DOUBLE));
		}

		public static capex.data.SQLTableColumnInfo forBlob(string name) {
			return(new capex.data.SQLTableColumnInfo().setName(name).setType(capex.data.SQLTableColumnInfo.TYPE_BLOB));
		}

		public const int TYPE_INTEGER = 0;
		public const int TYPE_STRING = 1;
		public const int TYPE_TEXT = 2;
		public const int TYPE_INTEGER_KEY = 3;
		public const int TYPE_DOUBLE = 4;
		public const int TYPE_BLOB = 5;
		public const int TYPE_STRING_KEY = 6;
		public const int TYPE_LONG = 7;
		public const int TYPE_LONG_KEY = 8;
		private string name = null;
		private int type = 0;

		public string getName() {
			return(name);
		}

		public capex.data.SQLTableColumnInfo setName(string v) {
			name = v;
			return(this);
		}

		public int getType() {
			return(type);
		}

		public capex.data.SQLTableColumnInfo setType(int v) {
			type = v;
			return(this);
		}
	}
}
