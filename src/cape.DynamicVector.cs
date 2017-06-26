
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
	public class DynamicVector : cape.Duplicateable, cape.Iterateable<object>, cape.VectorObject<object>, cape.ObjectWithSize
	{
		public static cape.DynamicVector asDynamicVector(object @object) {
			if(!(@object != null)) {
				return(null);
			}
			if(@object is cape.DynamicVector) {
				return((cape.DynamicVector)@object);
			}
			if(@object is System.Collections.Generic.List<object>) {
				return(cape.DynamicVector.forObjectVector((System.Collections.Generic.List<object>)@object));
			}
			return(null);
		}

		public static cape.DynamicVector forStringVector(System.Collections.Generic.List<string> vector) {
			var v = new cape.DynamicVector();
			if(vector != null) {
				var n = 0;
				var m = vector.Count;
				for(n = 0 ; n < m ; n++) {
					var item = vector[n];
					if(item != null) {
						v.append((object)item);
					}
				}
			}
			return(v);
		}

		public static cape.DynamicVector forObjectVector(System.Collections.Generic.List<object> vector) {
			var v = new cape.DynamicVector();
			if(vector != null) {
				var n = 0;
				var m = vector.Count;
				for(n = 0 ; n < m ; n++) {
					var item = vector[n];
					if(item != null) {
						v.append(item);
					}
				}
			}
			return(v);
		}

		private System.Collections.Generic.List<object> vector = null;

		public DynamicVector() {
			vector = new System.Collections.Generic.List<object>();
		}

		public virtual System.Collections.Generic.List<object> toVector() {
			return(vector);
		}

		public System.Collections.Generic.List<string> toVectorOfStrings() {
			var v = new System.Collections.Generic.List<string>();
			if(vector != null) {
				var n = 0;
				var m = vector.Count;
				for(n = 0 ; n < m ; n++) {
					var o = vector[n] as string;
					if(o != null) {
						v.Add(o);
					}
				}
			}
			return(v);
		}

		public System.Collections.Generic.List<cape.DynamicMap> toVectorOfDynamicMaps() {
			var v = new System.Collections.Generic.List<cape.DynamicMap>();
			if(vector != null) {
				var n = 0;
				var m = vector.Count;
				for(n = 0 ; n < m ; n++) {
					var o = vector[n] as cape.DynamicMap;
					if(o != null) {
						v.Add(o);
					}
				}
			}
			return(v);
		}

		public virtual object duplicate() {
			var v = new cape.DynamicVector();
			var it = iterate();
			while(it != null) {
				var o = it.next();
				if(o == null) {
					break;
				}
				v.append(o);
			}
			return((object)v);
		}

		public cape.DynamicVector mergeDynamicVector(cape.DynamicVector dynamicVector) {
			if(!(dynamicVector != null) || dynamicVector.getSize() < 1) {
				return(this);
			}
			return(mergeVector(dynamicVector.toVector()));
		}

		public cape.DynamicVector mergeVector(System.Collections.Generic.List<object> vector) {
			if(vector != null) {
				var n = 0;
				var m = vector.Count;
				for(n = 0 ; n < m ; n++) {
					var item = vector[n];
					if(item != null) {
						append(item);
					}
				}
			}
			return(this);
		}

		public cape.DynamicVector append(object @object) {
			vector.Add(@object);
			return(this);
		}

		public cape.DynamicVector append(int value) {
			vector.Add(cape.Integer.asObject(value));
			return(this);
		}

		public cape.DynamicVector append(long value) {
			vector.Add(cape.LongInteger.asObject(value));
			return(this);
		}

		public cape.DynamicVector append(bool value) {
			vector.Add(cape.Boolean.asObject(value));
			return(this);
		}

		public cape.DynamicVector append(double value) {
			vector.Add(cape.Double.asObject(value));
			return(this);
		}

		public cape.DynamicVector set(int index, object @object) {
			cape.Vector.set(vector, index, @object);
			return(this);
		}

		public cape.DynamicVector set(int index, int value) {
			cape.Vector.set(vector, index, cape.Integer.asObject(value));
			return(this);
		}

		public cape.DynamicVector set(int index, long value) {
			cape.Vector.set(vector, index, cape.LongInteger.asObject(value));
			return(this);
		}

		public cape.DynamicVector set(int index, bool value) {
			cape.Vector.set(vector, index, cape.Boolean.asObject(value));
			return(this);
		}

		public cape.DynamicVector set(int index, double value) {
			cape.Vector.set(vector, index, cape.Double.asObject(value));
			return(this);
		}

		public object get(int index) {
			return(cape.Vector.getAt(vector, index));
		}

		public string getString(int index, string defval = null) {
			var v = cape.String.asString(get(index));
			if(object.Equals(v, null)) {
				return(defval);
			}
			return(v);
		}

		public int getInteger(int index, int defval = 0) {
			var vv = get(index);
			if(vv == null) {
				return(defval);
			}
			return(cape.Integer.asInteger(vv));
		}

		public long getLongInteger(int index, long defval = (long)0) {
			var vv = get(index);
			if(vv == null) {
				return(defval);
			}
			return(cape.LongInteger.asLong(vv));
		}

		public bool getBoolean(int index, bool defval = false) {
			var vv = get(index);
			if(vv == null) {
				return(defval);
			}
			return(cape.Boolean.asBoolean(vv));
		}

		public double getDouble(int index, double defval = 0.00) {
			var vv = get(index);
			if(vv == null) {
				return(defval);
			}
			return(cape.Double.asDouble(vv));
		}

		public cape.DynamicMap getMap(int index) {
			return(get(index) as cape.DynamicMap);
		}

		public cape.DynamicVector getVector(int index) {
			return(get(index) as cape.DynamicVector);
		}

		public virtual cape.Iterator<object> iterate() {
			cape.Iterator<object> v = cape.Vector.iterate(vector);
			return(v);
		}

		public cape.Iterator<object> iterateReverse() {
			cape.Iterator<object> v = cape.Vector.iterateReverse(vector);
			return(v);
		}

		public void remove(int index) {
			cape.Vector.remove(vector, index);
		}

		public void clear() {
			cape.Vector.clear(vector);
		}

		public virtual int getSize() {
			return(cape.Vector.getSize(vector));
		}

		public void setCapacity(int capacity) {
			cape.Vector.setCapacity(vector, capacity);
		}

		public void sort() {
			cape.Vector.sort(vector, (object a, object b) => {
				return(cape.String.compare(cape.String.asString(a), cape.String.asString(b)));
			});
		}

		public void sort(System.Func<object, object, int> comparer) {
			if(comparer == null) {
				sort();
				return;
			}
			cape.Vector.sort(vector, comparer);
		}

		public void sortReverse() {
			cape.Vector.sortReverse(vector, (object a, object b) => {
				return(cape.String.compare(cape.String.asString(a), cape.String.asString(b)));
			});
		}

		public void sortReverse(System.Func<object, object, int> comparer) {
			if(comparer == null) {
				sortReverse();
				return;
			}
			cape.Vector.sortReverse(vector, comparer);
		}
	}
}
