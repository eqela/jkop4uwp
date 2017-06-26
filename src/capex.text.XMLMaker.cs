
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

namespace capex.text {
	public class XMLMaker : cape.StringObject
	{
		public class CData : cape.StringObject
		{
			private string text = null;
			private bool simple = false;
			private bool singleLine = false;

			public CData(string t) {
				text = t;
			}

			public virtual string toString() {
				var sb = new cape.StringBuilder();
				if(simple) {
					sb.append(text);
				}
				else {
					sb.append("<![CDATA[");
					sb.append(text);
					sb.append("]]>");
				}
				return(sb.toString());
			}

			public string getText() {
				return(text);
			}

			public capex.text.XMLMaker.CData setText(string v) {
				text = v;
				return(this);
			}

			public bool getSimple() {
				return(simple);
			}

			public capex.text.XMLMaker.CData setSimple(bool v) {
				simple = v;
				return(this);
			}

			public bool getSingleLine() {
				return(singleLine);
			}

			public capex.text.XMLMaker.CData setSingleLine(bool v) {
				singleLine = v;
				return(this);
			}
		}

		public class Element : capex.text.XMLMaker.StartElement
		{
			public Element(string name) : base(name) {
				setSingle(true);
			}
		}

		public class EndElement : cape.StringObject
		{
			private string name = null;

			public EndElement(string n) {
				name = n;
			}

			public virtual string toString() {
				return("</" + getName() + ">");
			}

			public string getName() {
				return(name);
			}

			public capex.text.XMLMaker.EndElement setName(string v) {
				name = v;
				return(this);
			}
		}

		public class StartElement : cape.StringObject
		{
			private string name = null;
			private System.Collections.Generic.Dictionary<string,string> attributes = null;
			private bool single = false;
			private bool singleLine = false;

			public StartElement(string n) {
				name = n;
				attributes = new System.Collections.Generic.Dictionary<string,string>();
			}

			public capex.text.XMLMaker.StartElement attribute(string key, string value) {
				cape.Map.setValue(attributes, key, value);
				return(this);
			}

			private string escapeAttribute(string str) {
				if(object.Equals(str, null)) {
					return(str);
				}
				var sb = new cape.StringBuilder();
				var array = cape.String.toCharArray(str);
				if(array != null) {
					var n = 0;
					var m = array.Length;
					for(n = 0 ; n < m ; n++) {
						var c = array[n];
						if(c == '&') {
							sb.append("&amp;");
						}
						else if(c == '\"') {
							sb.append("&quot;");
						}
						else if(c == '<') {
							sb.append("&lt;");
						}
						else if(c == '>') {
							sb.append("&gt;");
						}
						else {
							sb.append(c);
						}
					}
				}
				return(sb.toString());
			}

			public virtual string toString() {
				var sb = new cape.StringBuilder();
				sb.append('<');
				sb.append(name);
				System.Collections.Generic.List<string> keys = cape.Map.getKeys(attributes);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n];
						if(key != null) {
							sb.append(' ');
							sb.append(key);
							sb.append('=');
							sb.append('\"');
							var val = escapeAttribute(cape.Map.getValue(attributes, key));
							sb.append(val);
							sb.append('\"');
						}
					}
				}
				if(single) {
					sb.append(' ');
					sb.append('/');
				}
				sb.append('>');
				return(sb.toString());
			}

			public string getName() {
				return(name);
			}

			public capex.text.XMLMaker.StartElement setName(string v) {
				name = v;
				return(this);
			}

			public System.Collections.Generic.Dictionary<string,string> getAttributes() {
				return(attributes);
			}

			public capex.text.XMLMaker.StartElement setAttributes(System.Collections.Generic.Dictionary<string,string> v) {
				attributes = v;
				return(this);
			}

			public bool getSingle() {
				return(single);
			}

			public capex.text.XMLMaker.StartElement setSingle(bool v) {
				single = v;
				return(this);
			}

			public bool getSingleLine() {
				return(singleLine);
			}

			public capex.text.XMLMaker.StartElement setSingleLine(bool v) {
				singleLine = v;
				return(this);
			}
		}

		public class CustomXML
		{
			private string @string = null;

			public CustomXML(string s) {
				@string = s;
			}

			public string getString() {
				return(@string);
			}

			public capex.text.XMLMaker.CustomXML setString(string v) {
				@string = v;
				return(this);
			}
		}

		private System.Collections.Generic.List<object> elements = null;
		private string customHeader = null;
		private bool singleLine = false;
		private string header = null;

		public XMLMaker() {
			elements = new System.Collections.Generic.List<object>();
			header = "<?xml version=\"1.0\" encoding=\"utf-8\"?>";
		}

		public capex.text.XMLMaker start(string element, bool singleLine = false) {
			add((object)new capex.text.XMLMaker.StartElement(element).setSingleLine(singleLine));
			return(this);
		}

		public capex.text.XMLMaker start(string element, string k1, string v1, bool singleLine = false) {
			var v = new capex.text.XMLMaker.StartElement(element);
			v.setSingleLine(singleLine);
			if(!(object.Equals(k1, null))) {
				v.attribute(k1, v1);
			}
			add((object)v);
			return(this);
		}

		public capex.text.XMLMaker start(string element, System.Collections.Generic.Dictionary<object,object> attrs, bool singleLine = false) {
			var v = new capex.text.XMLMaker.StartElement(element);
			v.setSingleLine(singleLine);
			if(attrs != null) {
				System.Collections.Generic.List<object> keys = cape.Map.getKeys(attrs);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n] as string;
						if(key != null) {
							var val = attrs[key];
							v.attribute(key, cape.String.asString(val));
						}
					}
				}
			}
			add((object)v);
			return(this);
		}

		public capex.text.XMLMaker element(string element, System.Collections.Generic.Dictionary<object,object> attrs) {
			var v = new capex.text.XMLMaker.Element(element);
			if(attrs != null) {
				System.Collections.Generic.List<object> keys = cape.Map.getKeys(attrs);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n] as string;
						if(key != null) {
							var val = attrs[key];
							v.attribute(key, cape.String.asString(val));
						}
					}
				}
			}
			add((object)v);
			return(this);
		}

		public capex.text.XMLMaker end(string element) {
			add((object)new capex.text.XMLMaker.EndElement(element));
			return(this);
		}

		public capex.text.XMLMaker text(string element) {
			add((object)element);
			return(this);
		}

		public capex.text.XMLMaker cdata(string element) {
			add((object)new capex.text.XMLMaker.CData(element));
			return(this);
		}

		public capex.text.XMLMaker textElement(string element, string text, System.Collections.Generic.Dictionary<object,object> attrs = null) {
			var se = new capex.text.XMLMaker.StartElement(element);
			se.setSingleLine(true);
			if(attrs != null) {
				System.Collections.Generic.List<object> keys = cape.Map.getKeys(attrs);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n] as string;
						if(key != null) {
							var val = attrs[key];
							se.attribute(key, cape.String.asString(val));
						}
					}
				}
			}
			add((object)se);
			add((object)text);
			add((object)new capex.text.XMLMaker.EndElement(element));
			return(this);
		}

		public capex.text.XMLMaker add(object o) {
			if(o != null) {
				cape.Vector.append(elements, o);
			}
			return(this);
		}

		private void append(cape.StringBuilder sb, int level, string str, bool noIndent, bool noNewLine) {
			var n = 0;
			if(singleLine == false && noIndent == false) {
				for(n = 0 ; n < level ; n++) {
					sb.append(' ');
					sb.append(' ');
				}
			}
			sb.append(str);
			if(singleLine == false && noNewLine == false) {
				sb.append('\n');
			}
		}

		private string escapeString(string str) {
			var sb = new cape.StringBuilder();
			if(!(object.Equals(str, null))) {
				var array = cape.String.toCharArray(str);
				if(array != null) {
					var n = 0;
					var m = array.Length;
					for(n = 0 ; n < m ; n++) {
						var c = array[n];
						if(c == '\"') {
							sb.append("&quot;");
						}
						else if(c == '\'') {
							sb.append("&apos;");
						}
						else if(c == '<') {
							sb.append("&lt;");
						}
						else if(c == '>') {
							sb.append("&gt;");
						}
						else if(c == '&') {
							sb.append("&amp;");
						}
						else {
							sb.append(c);
						}
					}
				}
			}
			return(sb.toString());
		}

		public virtual string toString() {
			var sb = new cape.StringBuilder();
			var level = 0;
			if(!(object.Equals(header, null))) {
				append(sb, level, header, false, false);
			}
			if(!(object.Equals(customHeader, null))) {
				sb.append(customHeader);
			}
			var singleLine = false;
			if(elements != null) {
				var n = 0;
				var m = elements.Count;
				for(n = 0 ; n < m ; n++) {
					var o = elements[n];
					if(o != null) {
						if(o is capex.text.XMLMaker.Element) {
							append(sb, level, ((capex.text.XMLMaker.Element)o).toString(), singleLine, singleLine);
						}
						else if(o is capex.text.XMLMaker.StartElement) {
							singleLine = ((capex.text.XMLMaker.StartElement)o).getSingleLine();
							append(sb, level, ((capex.text.XMLMaker.StartElement)o).toString(), false, singleLine);
							if(((capex.text.XMLMaker.StartElement)o).getSingle() == false) {
								level++;
							}
						}
						else if(o is capex.text.XMLMaker.EndElement) {
							level--;
							append(sb, level, ((capex.text.XMLMaker.EndElement)o).toString(), singleLine, false);
							singleLine = false;
						}
						else if(o is capex.text.XMLMaker.CustomXML) {
							append(sb, level, ((capex.text.XMLMaker.CustomXML)o).getString(), singleLine, singleLine);
						}
						else if(o is string) {
							append(sb, level, escapeString((string)o), singleLine, singleLine);
						}
						else if(o is capex.text.XMLMaker.CData) {
							append(sb, level, ((capex.text.XMLMaker.CData)o).toString(), singleLine, ((capex.text.XMLMaker.CData)o).getSingleLine());
						}
					}
				}
			}
			return(sb.toString());
		}

		public System.Collections.Generic.List<object> getElements() {
			return(elements);
		}

		public capex.text.XMLMaker setElements(System.Collections.Generic.List<object> v) {
			elements = v;
			return(this);
		}

		public string getCustomHeader() {
			return(customHeader);
		}

		public capex.text.XMLMaker setCustomHeader(string v) {
			customHeader = v;
			return(this);
		}

		public bool getSingleLine() {
			return(singleLine);
		}

		public capex.text.XMLMaker setSingleLine(bool v) {
			singleLine = v;
			return(this);
		}

		public string getHeader() {
			return(header);
		}

		public capex.text.XMLMaker setHeader(string v) {
			header = v;
			return(this);
		}
	}
}
