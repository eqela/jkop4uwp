
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
	public class KeyValueList<K, V>
	{
		public KeyValueList() {
		}

		private System.Collections.Generic.List<cape.KeyValuePair<K, V>> values = null;

		public void add(K key, V val) {
			if(values == null) {
				values = new System.Collections.Generic.List<cape.KeyValuePair<K, V>>();
			}
			var kvp = new cape.KeyValuePair<K, V>();
			kvp.key = key;
			kvp.value = val;
			values.Add(kvp);
		}

		public void add(cape.KeyValuePair<K, V> pair) {
			if(values == null) {
				values = new System.Collections.Generic.List<cape.KeyValuePair<K, V>>();
			}
			values.Add(pair);
		}

		public cape.Iterator<cape.KeyValuePair<K, V>> iterate() {
			cape.Iterator<cape.KeyValuePair<K, V>> v = cape.Vector.iterate(values);
			return(v);
		}

		public System.Collections.Generic.List<cape.KeyValuePair<K, V>> asVector() {
			return(values);
		}

		public cape.KeyValueList<K, V> dup() {
			var v = new cape.KeyValueList<K, V>();
			var it = iterate();
			while(true) {
				var kvp = it.next();
				if(kvp == null) {
					break;
				}
				v.add(kvp.key, kvp.value);
			}
			return(v);
		}

		public K getKey(int index) {
			if(values == null) {
				return((K)(default(K)));
			}
			var kvp = cape.Vector.get(values, index);
			if(kvp == null) {
				return((K)(default(K)));
			}
			return(kvp.key);
		}

		public V getValue(int index) {
			if(values == null) {
				return((V)(default(V)));
			}
			var kvp = cape.Vector.get(values, index);
			if(kvp == null) {
				return((V)(default(V)));
			}
			return(kvp.value);
		}

		public int count() {
			if(values == null) {
				return(0);
			}
			return(cape.Vector.getSize(values));
		}

		public void clear() {
			values = null;
		}
	}
}
