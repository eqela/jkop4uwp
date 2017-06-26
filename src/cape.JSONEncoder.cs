
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
	public class JSONEncoder
	{
		public JSONEncoder() {
		}

		private bool isNewLine = true;
		private cape.StringBuilder mysb = null;

		private void print(string line, int indent, bool startline, bool endline, cape.StringBuilder sb, bool niceFormatting) {
			if(startline && isNewLine == false) {
				if(niceFormatting) {
					sb.append('\n');
				}
				isNewLine = true;
			}
			if(isNewLine && niceFormatting) {
				for(var n = 0 ; n < indent ; n++) {
					sb.append('\t');
				}
			}
			sb.append(line);
			if(endline) {
				if(niceFormatting) {
					sb.append('\n');
				}
				isNewLine = true;
			}
			else {
				isNewLine = false;
			}
		}

		private void encodeArray(object[] cc, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("[", indent, false, true, sb, niceFormatting);
			var first = true;
			if(cc != null) {
				var n = 0;
				var m = cc.Length;
				for(n = 0 ; n < m ; n++) {
					var o = cc[n];
					if(o != null) {
						if(first == false) {
							print(",", indent, false, true, sb, niceFormatting);
						}
						encodeObject(o, indent + 1, sb, niceFormatting);
						first = false;
					}
				}
			}
			print("]", indent, true, false, sb, niceFormatting);
		}

		private void encodeDynamicVector(cape.DynamicVector cc, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("[", indent, false, true, sb, niceFormatting);
			var first = true;
			var array = cc.toVector();
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var o = array[n];
					if(o != null) {
						if(first == false) {
							print(",", indent, false, true, sb, niceFormatting);
						}
						encodeObject(o, indent + 1, sb, niceFormatting);
						first = false;
					}
				}
			}
			print("]", indent, true, false, sb, niceFormatting);
		}

		private void encodeVector(System.Collections.Generic.List<object> cc, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("[", indent, false, true, sb, niceFormatting);
			var first = true;
			if(cc != null) {
				var n = 0;
				var m = cc.Count;
				for(n = 0 ; n < m ; n++) {
					var o = cc[n];
					if(o != null) {
						if(first == false) {
							print(",", indent, false, true, sb, niceFormatting);
						}
						encodeObject(o, indent + 1, sb, niceFormatting);
						first = false;
					}
				}
			}
			print("]", indent, true, false, sb, niceFormatting);
		}

		private void encodeMap(System.Collections.Generic.Dictionary<object,object> map, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("{", indent, false, true, sb, niceFormatting);
			var first = true;
			cape.Iterator<object> it = cape.Map.iterateKeys(map);
			while(it != null) {
				var keyo = it.next();
				if(!(keyo != null)) {
					break;
				}
				var key = cape.String.asString(keyo);
				if(!(key != null)) {
					continue;
				}
				if(!first) {
					print(",", indent, false, true, sb, niceFormatting);
				}
				encodeString(key, indent + 1, sb, niceFormatting);
				if(niceFormatting) {
					sb.append(" : ");
				}
				else {
					sb.append(':');
				}
				encodeObject(map[keyo], indent + 1, sb, niceFormatting);
				first = false;
			}
			print("}", indent, true, false, sb, niceFormatting);
		}

		private void encodeDynamicMap(cape.DynamicMap map, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("{", indent, false, true, sb, niceFormatting);
			var first = true;
			var it = map.iterateKeys();
			while(it != null) {
				var key = it.next();
				if(!(key != null)) {
					break;
				}
				if(first == false) {
					print(",", indent, false, true, sb, niceFormatting);
				}
				encodeString(key, indent + 1, sb, niceFormatting);
				if(niceFormatting) {
					sb.append(" : ");
				}
				else {
					sb.append(':');
				}
				encodeObject(map.get(key), indent + 1, sb, niceFormatting);
				first = false;
			}
			print("}", indent, true, false, sb, niceFormatting);
		}

		private void encodeKeyValueList(cape.KeyValueListForStrings list, int indent, cape.StringBuilder sb, bool niceFormatting) {
			print("{", indent, false, true, sb, niceFormatting);
			var first = true;
			cape.Iterator<cape.KeyValuePair<string, string>> it = list.iterate();
			while(it != null) {
				var pair = it.next();
				if(pair == null) {
					break;
				}
				if(first == false) {
					print(",", indent, false, true, sb, niceFormatting);
				}
				encodeString(pair.key, indent + 1, sb, niceFormatting);
				if(niceFormatting) {
					sb.append(" : ");
				}
				else {
					sb.append(':');
				}
				encodeString(pair.value, indent + 1, sb, niceFormatting);
				first = false;
			}
			print("}", indent, true, false, sb, niceFormatting);
		}

		private void encodeString(string s, int indent, cape.StringBuilder sb, bool niceFormatting) {
			if(mysb == null) {
				mysb = new cape.StringBuilder();
			}
			else {
				mysb.clear();
			}
			mysb.append('\"');
			if(cape.String.indexOf(s, '\"') >= 0 || cape.String.indexOf(s, '\\') >= 0) {
				var it = cape.String.iterate(s);
				while(true) {
					var c = it.getNextChar();
					if(c < 1) {
						break;
					}
					if(c == '\"') {
						mysb.append('\\');
					}
					else if(c == '\\') {
						mysb.append('\\');
					}
					mysb.append(c);
				}
			}
			else {
				mysb.append(s);
			}
			mysb.append('\"');
			print(mysb.toString(), indent, false, false, sb, niceFormatting);
		}

		public virtual void encodeObject(object o, int indent, cape.StringBuilder sb, bool niceFormatting) {
			if(!(o != null)) {
				encodeString("", indent, sb, niceFormatting);
			}
			else if(o is object[]) {
				encodeArray((object[])o, indent, sb, niceFormatting);
			}
			else if(o is System.Collections.Generic.List<object>) {
				encodeVector((System.Collections.Generic.List<object>)o, indent, sb, niceFormatting);
			}
			else if(o is System.Collections.Generic.Dictionary<object,object>) {
				encodeMap((System.Collections.Generic.Dictionary<object,object>)o, indent, sb, niceFormatting);
			}
			else if(o is byte[]) {
				encodeString(cape.Base64Encoder.encode((byte[])o), indent, sb, niceFormatting);
			}
			else if(o is string) {
				encodeString((string)o, indent, sb, niceFormatting);
			}
			else if(o is cape.DynamicMap) {
				encodeDynamicMap((cape.DynamicMap)o, indent, sb, niceFormatting);
			}
			else if(o is cape.DynamicVector) {
				encodeDynamicVector((cape.DynamicVector)o, indent, sb, niceFormatting);
			}
			else if(o is cape.KeyValueListForStrings) {
				encodeKeyValueList((cape.KeyValueListForStrings)o, indent, sb, niceFormatting);
			}
			else if(o is cape.StringObject) {
				encodeString(((cape.StringObject)o).toString(), indent, sb, niceFormatting);
			}
			else if(o is cape.BufferObject) {
				encodeString(cape.Base64Encoder.encode(((cape.BufferObject)o).toBuffer()), indent, sb, niceFormatting);
			}
			else if(o is cape.ArrayObject<object>) {
				object[] aa = ((cape.ArrayObject<object>)o).toArray();
				encodeArray(aa, indent, sb, niceFormatting);
			}
			else if(o is cape.VectorObject<object>) {
				System.Collections.Generic.List<object> vv = ((cape.VectorObject<object>)o).toVector();
				encodeVector(vv, indent, sb, niceFormatting);
			}
			else if(o is cape.IntegerObject || o is cape.LongIntegerObject || o is cape.BooleanObject || o is cape.DoubleObject || o is cape.CharacterObject) {
				encodeString(cape.String.asString(o), indent, sb, niceFormatting);
			}
			else {
				encodeString("", indent, sb, niceFormatting);
			}
		}

		public static string encode(object o, bool niceFormatting = true) {
			var sb = new cape.StringBuilder();
			new cape.JSONEncoder().encodeObject(o, 0, sb, niceFormatting);
			return(sb.toString());
		}

		public static void encodeStringToBuilder(string s, cape.StringBuilder sb) {
			sb.append('\"');
			if(cape.String.indexOf(s, '\"') >= 0 || cape.String.indexOf(s, '\\') >= 0) {
				var it = cape.String.iterate(s);
				while(true) {
					var c = it.getNextChar();
					if(c < 1) {
						break;
					}
					if(c == '\"') {
						sb.append('\\');
					}
					else if(c == '\\') {
						sb.append('\\');
					}
					sb.append(c);
				}
			}
			else {
				sb.append(s);
			}
			sb.append('\"');
		}
	}
}
