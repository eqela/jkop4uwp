
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

namespace capex.text
{
	public class XMLParser
	{
		public XMLParser() {
		}

		public static cape.DynamicMap parseAsTreeObject(string xml, bool ignoreWhiteSpace = true) {
			cape.DynamicMap root = null;
			var stack = new cape.Stack<cape.DynamicMap>();
			var pp = capex.text.XMLParser.forString(xml);
			pp.setIgnoreWhiteSpace(ignoreWhiteSpace);
			while(true) {
				var o = pp.next();
				if(o == null) {
					break;
				}
				if(o is capex.text.XMLParser.StartElement) {
					var name = ((capex.text.XMLParser.StartElement)o).getName();
					if(root == null && object.Equals(name, "?xml")) {
						continue;
					}
					var nn = new cape.DynamicMap();
					nn.set("name", (object)name);
					nn.set("attributes", (object)((capex.text.XMLParser.StartElement)o).getParams());
					if(root == null) {
						root = nn;
						stack.push((cape.DynamicMap)nn);
					}
					else {
						var current = stack.peek();
						if(current == null) {
							current = root;
						}
						var children = current.getDynamicVector("children");
						if(children == null) {
							children = new cape.DynamicVector();
							current.set("children", (object)children);
						}
						children.append((object)nn);
						stack.push((cape.DynamicMap)nn);
					}
				}
				else if(o is capex.text.XMLParser.EndElement) {
					stack.pop();
				}
				else if(o is capex.text.XMLParser.CharacterData) {
					var current1 = stack.peek();
					if(current1 != null) {
						var children1 = current1.getDynamicVector("children");
						if(children1 == null) {
							children1 = new cape.DynamicVector();
							current1.set("children", (object)children1);
						}
						children1.append((object)((capex.text.XMLParser.CharacterData)o).getData());
					}
				}
			}
			return(root);
		}

		public class Value
		{
			public Value() {
			}

			private int position = 0;

			public int getPosition() {
				return(position);
			}

			public capex.text.XMLParser.Value setPosition(int v) {
				position = v;
				return(this);
			}
		}

		public class StartElement : capex.text.XMLParser.Value
		{
			public StartElement() : base() {
			}

			private string name = null;
			private cape.DynamicMap @params = null;

			public string getParam(string pname) {
				if(!(@params != null)) {
					return(null);
				}
				return(@params.getString(pname));
			}

			public string getName() {
				return(name);
			}

			public capex.text.XMLParser.StartElement setName(string v) {
				name = v;
				return(this);
			}

			public cape.DynamicMap getParams() {
				return(@params);
			}

			public capex.text.XMLParser.StartElement setParams(cape.DynamicMap v) {
				@params = v;
				return(this);
			}
		}

		public class EndElement : capex.text.XMLParser.Value
		{
			public EndElement() : base() {
			}

			private string name = null;

			public string getName() {
				return(name);
			}

			public capex.text.XMLParser.EndElement setName(string v) {
				name = v;
				return(this);
			}
		}

		public class CharacterData : capex.text.XMLParser.Value
		{
			public CharacterData() : base() {
			}

			private string data = null;

			public string getData() {
				return(data);
			}

			public capex.text.XMLParser.CharacterData setData(string v) {
				data = v;
				return(this);
			}
		}

		public class Comment : capex.text.XMLParser.Value
		{
			public Comment() : base() {
			}

			private string text = null;

			public string getText() {
				return(text);
			}

			public capex.text.XMLParser.Comment setText(string v) {
				text = v;
				return(this);
			}
		}

		private cape.CharacterIterator it = null;
		private capex.text.XMLParser.Value nextQueue = null;
		private string cdataStart = "![CDATA[";
		private string commentStart = "!--";
		private cape.StringBuilder tag = null;
		private cape.StringBuilder def = null;
		private cape.StringBuilder cdata = null;
		private cape.StringBuilder comment = null;
		private bool ignoreWhiteSpace = false;
		private int currentPosition = 0;

		public static capex.text.XMLParser forFile(cape.File file) {
			if(!(file != null)) {
				return(null);
			}
			var reader = file.read();
			if(!(reader != null)) {
				return(null);
			}
			var v = new capex.text.XMLParser();
			v.it = (cape.CharacterIterator)new cape.CharacterIteratorForReader((cape.Reader)reader);
			return(v);
		}

		public static capex.text.XMLParser forString(string @string) {
			if(!(@string != null)) {
				return(null);
			}
			var v = new capex.text.XMLParser();
			v.it = (cape.CharacterIterator)new cape.CharacterIteratorForString(@string);
			return(v);
		}

		public static capex.text.XMLParser forIterator(cape.CharacterIterator it) {
			if(!(it != null)) {
				return(null);
			}
			var v = new capex.text.XMLParser();
			v.it = it;
			return(v);
		}

		private capex.text.XMLParser.Value onTagString(string tagstring, int pos) {
			if(cape.String.charAt(tagstring, 0) == '/') {
				var v = new capex.text.XMLParser.EndElement();
				v.setPosition(pos);
				v.setName(cape.String.subString(tagstring, 1));
				return((capex.text.XMLParser.Value)v);
			}
			var element = new cape.StringBuilder();
			var @params = new cape.DynamicMap();
			var it = new cape.CharacterIteratorForString(tagstring);
			var c = ' ';
			while((c = it.getNextChar()) > 0) {
				if(c == ' ' || c == '\t' || c == '\n' || c == '\r' || c == '/') {
					if(element.count() > 0) {
						break;
					}
				}
				else {
					element.append(c);
				}
			}
			while(c > 0 && c != '/') {
				var pname = new cape.StringBuilder();
				var pval = new cape.StringBuilder();
				while(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
					c = it.getNextChar();
				}
				while(c > 0 && c != ' ' && c != '\t' && c != '\n' && c != '\r' && c != '=') {
					pname.append(c);
					c = it.getNextChar();
				}
				while(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
					c = it.getNextChar();
				}
				if(c != '=') {
					;
				}
				else {
					c = it.getNextChar();
					while(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
						c = it.getNextChar();
					}
					if(c != '\"') {
						;
						while(c > 0 && c != ' ' && c != '\t' && c != '\n' && c != '\r') {
							pval.append(c);
							c = it.getNextChar();
						}
						while(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
							c = it.getNextChar();
						}
					}
					else {
						c = it.getNextChar();
						while(c > 0 && c != '\"') {
							pval.append(c);
							c = it.getNextChar();
						}
						if(c != '\"') {
							;
						}
						else {
							c = it.getNextChar();
						}
						while(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
							c = it.getNextChar();
						}
					}
				}
				var pnamestr = pname.toString();
				var pvalstr = pval.toString();
				@params.set(pnamestr, (object)pvalstr);
			}
			var els = element.toString();
			if(c == '/') {
				var e = new capex.text.XMLParser.EndElement();
				e.setName(els);
				e.setPosition(pos);
				nextQueue = (capex.text.XMLParser.Value)e;
			}
			var v1 = new capex.text.XMLParser.StartElement();
			v1.setPosition(pos);
			v1.setName(els);
			v1.setParams(@params);
			return((capex.text.XMLParser.Value)v1);
		}

		private bool isOnlyWhiteSpace(string str) {
			if(object.Equals(str, null)) {
				return(true);
			}
			var array = cape.String.toCharArray(str);
			if(array != null) {
				var n = 0;
				var m = array.Length;
				for(n = 0 ; n < m ; n++) {
					var c = array[n];
					if(c == ' ' || c == '\t' || c == '\n' || c == '\r') {
						;
					}
					else {
						return(false);
					}
				}
			}
			return(true);
		}

		private char getNextChar() {
			if(!(it != null)) {
				return('\0');
			}
			var v = it.getNextChar();
			if(v > 0) {
				currentPosition++;
			}
			return(v);
		}

		public void moveToPreviousChar() {
			if(!(it != null)) {
				return;
			}
			if(currentPosition > 0) {
				currentPosition--;
				it.moveToPreviousChar();
			}
		}

		public capex.text.XMLParser.Value peek() {
			if(nextQueue != null) {
				return(nextQueue);
			}
			nextQueue = next();
			return(nextQueue);
		}

		public capex.text.XMLParser.Value next() {
			if(nextQueue != null) {
				var v = nextQueue;
				nextQueue = null;
				return(v);
			}
			var pos = currentPosition;
			while(it.hasEnded() == false) {
				var cbp = currentPosition;
				var nxb = getNextChar();
				if(nxb < 1) {
					continue;
				}
				if(tag != null) {
					if(nxb == '>') {
						var ts = tag.toString();
						tag = null;
						return(onTagString(ts, pos));
					}
					tag.append(nxb);
					if(nxb == '[' && tag.count() == cape.String.getLength(cdataStart) && cape.String.equals(cdataStart, tag.toString())) {
						tag = null;
						cdata = new cape.StringBuilder();
					}
					else if(nxb == '-' && tag.count() == cape.String.getLength(commentStart) && cape.String.equals(commentStart, tag.toString())) {
						tag = null;
						comment = new cape.StringBuilder();
					}
				}
				else if(cdata != null) {
					var c0 = nxb;
					var c1 = ' ';
					var c2 = ' ';
					if(c0 == ']') {
						c1 = getNextChar();
						if(c1 == ']') {
							c2 = getNextChar();
							if(c2 == '>') {
								var dd = cdata.toString();
								cdata = null;
								var v1 = new capex.text.XMLParser.CharacterData();
								v1.setPosition(pos);
								v1.setData(dd);
								return((capex.text.XMLParser.Value)v1);
							}
							else {
								moveToPreviousChar();
								moveToPreviousChar();
								cdata.append(c0);
							}
						}
						else {
							moveToPreviousChar();
							cdata.append(c0);
						}
					}
					else {
						cdata.append(c0);
					}
				}
				else if(comment != null) {
					var c01 = nxb;
					var c11 = ' ';
					var c21 = ' ';
					if(c01 == '-') {
						c11 = getNextChar();
						if(c11 == '-') {
							c21 = getNextChar();
							if(c21 == '>') {
								var ct = comment.toString();
								comment = null;
								var v2 = new capex.text.XMLParser.Comment();
								v2.setPosition(pos);
								v2.setText(ct);
								return((capex.text.XMLParser.Value)v2);
							}
							else {
								moveToPreviousChar();
								moveToPreviousChar();
								comment.append(c01);
							}
						}
						else {
							moveToPreviousChar();
							comment.append(c01);
						}
					}
					else {
						comment.append(c01);
					}
				}
				else if(nxb == '<') {
					if(def != null) {
						var cd = def.toString();
						def = null;
						if(ignoreWhiteSpace && cd != null) {
							if(isOnlyWhiteSpace(cd)) {
								cd = null;
								pos = cbp;
							}
						}
						if(cd != null) {
							moveToPreviousChar();
							var v3 = new capex.text.XMLParser.CharacterData();
							v3.setPosition(pos);
							v3.setData(cd);
							return((capex.text.XMLParser.Value)v3);
						}
					}
					tag = new cape.StringBuilder();
				}
				else {
					if(def == null) {
						def = new cape.StringBuilder();
					}
					def.append(nxb);
				}
			}
			return(null);
		}

		public bool getIgnoreWhiteSpace() {
			return(ignoreWhiteSpace);
		}

		public capex.text.XMLParser setIgnoreWhiteSpace(bool v) {
			ignoreWhiteSpace = v;
			return(this);
		}
	}
}
