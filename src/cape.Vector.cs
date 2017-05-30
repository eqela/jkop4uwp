
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
	public class Vector
	{
		public Vector() {
		}

		public static System.Collections.Generic.List<T> asVector<T>(object obj) {
			var vo = obj as cape.VectorObject<T>;
			if(vo == null) {
				return(null);
			}
			return(vo.toVector());
		}

		public static System.Collections.Generic.List<T> forIterator<T>(cape.Iterator<T> iterator) {
			if(iterator == null) {
				return(null);
			}
			var v = new System.Collections.Generic.List<T>();
			while(true) {
				var o = iterator.next();
				if(o == null) {
					break;
				}
				v.Add(o);
			}
			return(v);
		}

		public static System.Collections.Generic.List<T> forArray<T>(T[] array) {
			if(array == null) {
				return(null);
			}
			var v = new System.Collections.Generic.List<T>();
			for(var n = 0 ; n < array.Length ; n++) {
				v.Add(array[n]);
			}
			return(v);
		}

		public static void append<T>(System.Collections.Generic.List<T> vector, T @object) {
			vector.Add(@object);
		}

		public static void prepend<T>(System.Collections.Generic.List<T> vector, T @object) {
			cape.Vector.insert(vector, @object, 0);
		}

		public static void insert<T>(System.Collections.Generic.List<T> vector, T @object, int index) {
			vector.Insert(index, @object);
		}

		public static int getSize<T>(System.Collections.Generic.List<T> vector) {
			if(vector == null) {
				return(0);
			}
			return(vector.Count);
		}

		public static T getAt<T>(System.Collections.Generic.List<T> vector, int index) {
			return(cape.Vector.get(vector, index));
		}

		public static T get<T>(System.Collections.Generic.List<T> vector, int index) {
			if(index < 0 || index >= cape.Vector.getSize(vector)) {
				return((T)default(T));
			}
			return(vector[index]);
		}

		public static void set<T>(System.Collections.Generic.List<T> vector, int index, T val) {
			if(index < 0 || index >= cape.Vector.getSize(vector)) {
				return;
			}
			vector[index] = val;
		}

		public static T remove<T>(System.Collections.Generic.List<T> vector, int index) {
			if(index < 0 || index >= cape.Vector.getSize(vector)) {
				return((T)default(T));
			}
			var t = vector[index];
			vector.RemoveAt(index);
			return(t);
		}

		public static T popFirst<T>(System.Collections.Generic.List<T> vector) {
			if(vector == null || cape.Vector.getSize(vector) < 1) {
				return((T)default(T));
			}
			var v = cape.Vector.get(vector, 0);
			cape.Vector.removeFirst(vector);
			return(v);
		}

		public static void removeFirst<T>(System.Collections.Generic.List<T> vector) {
			if(vector == null || cape.Vector.getSize(vector) < 1) {
				return;
			}
			cape.Vector.remove(vector, 0);
		}

		public static T popLast<T>(System.Collections.Generic.List<T> vector) {
			if(vector == null || cape.Vector.getSize(vector) < 1) {
				return((T)default(T));
			}
			var v = cape.Vector.get(vector, cape.Vector.getSize(vector) - 1);
			cape.Vector.removeLast(vector);
			return(v);
		}

		public static void removeLast<T>(System.Collections.Generic.List<T> vector) {
			if(vector == null) {
				return;
			}
			var sz = cape.Vector.getSize(vector);
			if(sz < 1) {
				return;
			}
			cape.Vector.remove(vector, sz - 1);
		}

		public static int removeValue<T>(System.Collections.Generic.List<T> vector, T value) {
			var n = 0;
			for(n = 0 ; n < vector.Count ; n++) {
				if(object.Equals(vector[n], value)) {
					remove(vector, n);
					return(n);
				}
			}
			return(-1);
		}

		public static void clear<T>(System.Collections.Generic.List<T> vector) {
			vector.Clear();
		}

		public static bool isEmpty<T>(System.Collections.Generic.List<T> vector) {
			return(vector == null || vector.Count < 1);
		}

		public static void removeRange<T>(System.Collections.Generic.List<T> vector, int index, int count) {
			vector.RemoveRange(index, count);
		}

		private class VectorIterator<T> : cape.Iterator<T>
		{
			public System.Collections.Generic.List<T> vector = null;
			private int index = 0;
			private int increment = 1;

			public VectorIterator(System.Collections.Generic.List<T> vector, int increment) {
				this.vector = vector;
				this.increment = increment;
				if(increment < 0 && vector != null) {
					index = cape.Vector.getSize(vector) - 1;
				}
			}

			public virtual T next() {
				if(vector == null) {
					return((T)default(T));
				}
				if(index < 0 || index >= cape.Vector.getSize(vector)) {
					return((T)default(T));
				}
				var v = vector[index];
				index += increment;
				return(v);
			}
		}

		public static cape.Iterator<T> iterate<T>(System.Collections.Generic.List<T> vector) {
			return((cape.Iterator<T>)new cape.Vector.VectorIterator<T>(vector, 1));
		}

		public static cape.Iterator<T> iterateReverse<T>(System.Collections.Generic.List<T> vector) {
			return((cape.Iterator<T>)new cape.Vector.VectorIterator<T>(vector, -1));
		}

		public static void sort<T>(System.Collections.Generic.List<T> vector, System.Func<T, T, int> comparer) {
			if(vector == null) {
				return;
			}
			vector.Sort((a,b) => { return(comparer(a,b)); });
		}

		public static void sortReverse<T>(System.Collections.Generic.List<T> vector, System.Func<T, T, int> comparer) {
			var cc = comparer;
			cape.Vector.sort(vector, (T a, T b) => {
				return(-cc(a, b));
			});
		}

		public static void reverse<T>(System.Collections.Generic.List<T> vector) {
			if(!(vector != null)) {
				return;
			}
			var a = 0;
			var b = cape.Vector.getSize(vector) - 1;
			while(a < b) {
				var t = cape.Vector.getAt(vector, b);
				cape.Vector.set(vector, b, cape.Vector.getAt(vector, a));
				cape.Vector.set(vector, a, t);
				a++;
				b--;
			}
		}
	}
}
