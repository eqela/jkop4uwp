
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

namespace capex.data {
	public abstract class SQLResultSetIterator : cape.Iterator<cape.DynamicMap>
	{
		public SQLResultSetIterator() {
		}

		public abstract cape.DynamicMap next();
		public abstract bool nextValues(System.Collections.Generic.List<object> values);
		public abstract bool step();
		public abstract int getColumnCount();
		public abstract string getColumnName(int n);
		public abstract System.Collections.Generic.List<string> getColumnNames();
		public abstract object getColumnObject(int n);
		public abstract int getColumnInt(int n);
		public abstract long getColumnLong(int n);
		public abstract double getColumnDouble(int n);
		public abstract byte[] getColumnBuffer(int n);
		public abstract void close();

		public cape.DynamicVector toVectorOfMaps() {
			var v = new cape.DynamicVector();
			while(true) {
				var o = next();
				if(o == null) {
					break;
				}
				v.append((object)o);
			}
			return(v);
		}

		public System.Collections.Generic.List<object> toVectorList() {
			var data = new System.Collections.Generic.List<object>();
			var cc = getColumnCount();
			var cols = new System.Collections.Generic.List<object>();
			cape.Vector.setCapacity(cols, cc);
			for(var n = 0 ; n < cc ; n++) {
				cols.Add(getColumnName(n));
			}
			data.Add(cols);
			while(true) {
				if(!step()) {
					break;
				}
				var record = new System.Collections.Generic.List<object>();
				cape.Vector.setCapacity(record, cc);
				for(var n1 = 0 ; n1 < cc ; n1++) {
					var co = getColumnObject(n1);
					if(co == null) {
						co = (object)"";
					}
					record.Add(co);
				}
				data.Add(record);
			}
			return(data);
		}

		public void headerJSON(cape.StringBuilder sb) {
			sb.append('[');
			var cc = getColumnCount();
			for(var n = 0 ; n < cc ; n++) {
				if(n > 0) {
					sb.append(',');
				}
				cape.JSONEncoder.encodeStringToBuilder(getColumnName(n), sb);
			}
			sb.append(']');
		}

		public bool nextJSON(cape.StringBuilder sb) {
			if(!step()) {
				return(false);
			}
			var cc = getColumnCount();
			sb.append(",[");
			for(var n = 0 ; n < cc ; n++) {
				if(n > 0) {
					sb.append(',');
				}
				cape.JSONEncoder.encodeStringToBuilder(cape.String.asString(getColumnObject(n)), sb);
			}
			sb.append(']');
			return(true);
		}

		public string toVectorListJSON() {
			var sb = new cape.StringBuilder();
			sb.append('[');
			headerJSON(sb);
			while(true) {
				if(!nextJSON(sb)) {
					break;
				}
			}
			sb.append(']');
			return(sb.toString());
		}
	}
}
