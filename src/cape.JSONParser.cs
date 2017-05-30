
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
	public class JSONParser
	{
		private class NullObject
		{
			public NullObject() {
			}
		}

		public static object parse(byte[] buffer) {
			if(buffer == null) {
				return(null);
			}
			return(new cape.JSONParser(cape.String.forUTF8Buffer(buffer)).acceptObject());
		}

		public static object parse(string str) {
			if(cape.String.isEmpty(str)) {
				return(null);
			}
			return(new cape.JSONParser(str).acceptObject());
		}

		public static object parse(cape.File file) {
			if(file == null) {
				return(null);
			}
			return(cape.JSONParser.parse(file.getContentsString("UTF-8")));
		}

		private cape.CharacterIterator iterator = null;

		private JSONParser(string str) {
			iterator = (cape.CharacterIterator)new cape.CharacterIteratorForString(str);
			iterator.moveToNextChar();
		}

		private void skipSpaces() {
			while(true) {
				if(iterator.hasEnded()) {
					break;
				}
				var c = iterator.getCurrentChar();
				if(c == ' ' || c == '\t' || c == '\r' || c == '\n') {
					iterator.moveToNextChar();
					continue;
				}
				break;
			}
		}

		private bool acceptChar(char c) {
			skipSpaces();
			if(iterator.getCurrentChar() == c) {
				iterator.moveToNextChar();
				return(true);
			}
			return(false);
		}

		private string acceptString() {
			skipSpaces();
			var ss = iterator.getCurrentChar();
			if(ss != '\'' && ss != '\"') {
				return(null);
			}
			var sb = new cape.StringBuilder();
			while(true) {
				var c = iterator.getNextChar();
				if(c == ss) {
					iterator.moveToNextChar();
					break;
				}
				if(c == '\\') {
					c = iterator.getNextChar();
				}
				sb.append(c);
			}
			return(sb.toString());
		}

		private cape.BooleanObject acceptBoolean() {
			skipSpaces();
			var ss = iterator.getCurrentChar();
			if(ss != 't' && ss != 'f') {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append(ss);
			var li = 5;
			if(ss == 't') {
				li = 4;
			}
			var btc = 0;
			while(true) {
				var c = iterator.getNextChar();
				btc++;
				if(c != 'a' && c != 'l' && c != 's' && c != 'e' && c != 'r' && c != 'u') {
					iterator.moveToNextChar();
					btc++;
					break;
				}
				sb.append(c);
				if(sb.count() == li) {
					iterator.moveToNextChar();
					btc++;
					break;
				}
			}
			var v = sb.toString();
			if(li == 4 && object.Equals("true", v)) {
				return(cape.Boolean.asObject(true));
			}
			if(li == 5 && object.Equals("false", v)) {
				return(cape.Boolean.asObject(false));
			}
			var i = 0;
			while(i < btc) {
				iterator.moveToPreviousChar();
				i++;
			}
			return(null);
		}

		private object acceptNumber() {
			skipSpaces();
			var ss = iterator.getCurrentChar();
			if(ss != '-' && ss != '+' && ss != '.' && (ss < '0' || ss > '9')) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append(ss);
			while(true) {
				var c = iterator.getNextChar();
				if(c != '.' && (c < '0' || c > '9')) {
					break;
				}
				sb.append(c);
			}
			var s = sb.toString();
			if(cape.String.getIndexOf(s, '.') > -1) {
				return((object)cape.Double.asObject(cape.Double.asDouble((object)s)));
			}
			return((object)cape.Integer.asObject(cape.Integer.asInteger(s)));
		}

		private cape.JSONParser.NullObject acceptNull() {
			skipSpaces();
			var ss = iterator.getCurrentChar();
			if(ss != 'n') {
				return(null);
			}
			var sb = new cape.StringBuilder();
			sb.append(ss);
			var btc = 0;
			while(true) {
				var c = iterator.getNextChar();
				btc++;
				if(c != 'u' && c != 'l') {
					iterator.moveToNextChar();
					btc++;
					break;
				}
				sb.append(c);
				if(sb.count() == 4) {
					iterator.moveToNextChar();
					btc++;
					break;
				}
			}
			if(object.Equals("null", sb.toString())) {
				return(new cape.JSONParser.NullObject());
			}
			var i = 0;
			while(i < btc) {
				iterator.moveToPreviousChar();
				i++;
			}
			return(null);
		}

		private object acceptObject() {
			if(acceptChar('[')) {
				var v = new cape.DynamicVector();
				while(true) {
					if(acceptChar(']')) {
						break;
					}
					var o = acceptObject();
					if(o == null) {
						return(null);
					}
					v.append(o);
					acceptChar(',');
				}
				return((object)v);
			}
			if(acceptChar('{')) {
				var v1 = new cape.DynamicMap();
				while(true) {
					if(acceptChar('}')) {
						break;
					}
					var key = acceptString();
					if(object.Equals(key, null)) {
						return(null);
					}
					if(acceptChar(':') == false) {
						return(null);
					}
					var val = acceptObject();
					if(val == null) {
						return(null);
					}
					if(val is cape.JSONParser.NullObject) {
						v1.set(key, null);
					}
					else {
						v1.set(key, val);
					}
					acceptChar(',');
				}
				return((object)v1);
			}
			var s = acceptString();
			if(!(object.Equals(s, null))) {
				return((object)s);
			}
			var b = acceptBoolean();
			if(b != null) {
				return((object)b);
			}
			var n = acceptNull();
			if(n != null) {
				return((object)n);
			}
			var v2 = acceptNumber();
			if(v2 != null) {
				return(v2);
			}
			return(null);
		}
	}
}
