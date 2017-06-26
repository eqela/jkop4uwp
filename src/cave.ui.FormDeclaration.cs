
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

namespace cave.ui {
	public class FormDeclaration
	{
		public static cave.ui.FormDeclaration forDescription(string desc) {
			var v = new cave.ui.FormDeclaration();
			if(!v.parseDescription(desc)) {
				return(null);
			}
			return(v);
		}

		public class Element
		{
			public Element() {
			}

			public static cave.ui.FormDeclaration.Element forType(string type) {
				var v = new cave.ui.FormDeclaration.Element();
				v.setProperty("type", (object)type);
				return(v);
			}

			public static cave.ui.FormDeclaration.Element forProperties(cape.DynamicMap properties) {
				var v = new cave.ui.FormDeclaration.Element();
				v.setProperties(properties);
				return(v);
			}

			private cape.DynamicMap properties = null;
			private System.Collections.Generic.List<cave.ui.FormDeclaration.Element> children = null;

			public string getId() {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getString("id"));
			}

			public string getType() {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getString("type"));
			}

			public string getLabel() {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getString("label"));
			}

			public bool isType(string type) {
				if(!(properties != null)) {
					return(false);
				}
				if(!(type != null)) {
					return(false);
				}
				if(object.Equals(properties.getString("type"), type)) {
					return(true);
				}
				return(false);
			}

			public void setProperty(string key, object value) {
				if(!(key != null)) {
					return;
				}
				if(!(properties != null)) {
					properties = new cape.DynamicMap();
				}
				properties.set(key, value);
			}

			public void setProperty(string key, int value) {
				setProperty(key, (object)cape.String.forInteger(value));
			}

			public void setProperty(string key, double value) {
				setProperty(key, (object)cape.String.forDouble(value));
			}

			public void setProperty(string key, bool value) {
				setProperty(key, (object)cape.String.forBoolean(value));
			}

			public object getPropertyObject(string key) {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.get(key));
			}

			public cape.DynamicVector getPropertyDynamicVector(string key) {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getDynamicVector(key));
			}

			public System.Collections.Generic.List<object> getPropertyVector(string key) {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getVector(key));
			}

			public string getPropertyString(string key) {
				if(!(properties != null)) {
					return(null);
				}
				return(properties.getString(key));
			}

			public int getPropertyInteger(string key) {
				if(!(properties != null)) {
					return(0);
				}
				return(properties.getInteger(key));
			}

			public double getPropertyDouble(string key) {
				if(!(properties != null)) {
					return(0.00);
				}
				return(properties.getDouble(key));
			}

			public bool getPropertyBoolean(string key) {
				if(!(properties != null)) {
					return(false);
				}
				return(properties.getBoolean(key));
			}

			public void addToChildren(cave.ui.FormDeclaration.Element element) {
				if(!(element != null)) {
					return;
				}
				if(!(children != null)) {
					children = new System.Collections.Generic.List<cave.ui.FormDeclaration.Element>();
				}
				children.Add(element);
			}

			public cape.DynamicMap getProperties() {
				return(properties);
			}

			public cave.ui.FormDeclaration.Element setProperties(cape.DynamicMap v) {
				properties = v;
				return(this);
			}

			public System.Collections.Generic.List<cave.ui.FormDeclaration.Element> getChildren() {
				return(children);
			}

			public cave.ui.FormDeclaration.Element setChildren(System.Collections.Generic.List<cave.ui.FormDeclaration.Element> v) {
				children = v;
				return(this);
			}
		}

		private cave.ui.FormDeclaration.Element root = null;
		private cape.Stack<cave.ui.FormDeclaration.Element> stack = null;

		public FormDeclaration() {
			clear();
		}

		public void clear() {
			stack = new cape.Stack<cave.ui.FormDeclaration.Element>();
			root = startVerticalContainer();
		}

		public cave.ui.FormDeclaration.Element getRoot() {
			return(root);
		}

		public cave.ui.FormDeclaration.Element addElement(cave.ui.FormDeclaration.Element element) {
			var current = stack.peek();
			if(current != null) {
				current.addToChildren(element);
			}
			return(element);
		}

		public cave.ui.FormDeclaration.Element startVerticalContainer() {
			var v = cave.ui.FormDeclaration.Element.forType("VerticalContainer");
			addElement(v);
			stack.push((cave.ui.FormDeclaration.Element)v);
			return(v);
		}

		public cave.ui.FormDeclaration.Element endVerticalContainer() {
			var cc = stack.peek();
			if(cc != null && cc.isType("VerticalContainer")) {
				stack.pop();
			}
			return(cc);
		}

		public cave.ui.FormDeclaration.Element startHorizontalContainer() {
			var v = cave.ui.FormDeclaration.Element.forType("HorizontalContainer");
			addElement(v);
			stack.push((cave.ui.FormDeclaration.Element)v);
			return(v);
		}

		public cave.ui.FormDeclaration.Element endHorizontalContainer() {
			var cc = stack.peek();
			if(cc != null && cc.isType("HorizontalContainer")) {
				stack.pop();
			}
			return(cc);
		}

		public cave.ui.FormDeclaration.Element startGroup(string id, string title, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("GroupContainer");
			v.setProperty("id", (object)id);
			v.setProperty("title", (object)title);
			v.setProperty("description", (object)description);
			addElement(v);
			stack.push((cave.ui.FormDeclaration.Element)v);
			return(v);
		}

		public cave.ui.FormDeclaration.Element endGroup() {
			var cc = stack.peek();
			if(cc != null && cc.isType("GroupContainer")) {
				stack.pop();
			}
			return(cc);
		}

		public cave.ui.FormDeclaration.Element startTab(string id, string label) {
			var v = cave.ui.FormDeclaration.Element.forType("TabContainer");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			addElement(v);
			stack.push((cave.ui.FormDeclaration.Element)v);
			return(v);
		}

		public cave.ui.FormDeclaration.Element endTab() {
			var cc = stack.peek();
			if(cc != null && cc.isType("TabContainer")) {
				stack.pop();
			}
			return(cc);
		}

		public cave.ui.FormDeclaration.Element addTextInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("TextInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addRawTextInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("RawTextInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addPasswordInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("PasswordInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addNameInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("NameInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addEmailAddressInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("EmailAddressInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addPhoneNumberInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("PhoneNumberInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addStreetAddressInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("StreetAddressInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addMultipleChoiceInput(string id, string label, string description, string[] values) {
			var vvs = new cape.KeyValueList<string, string>();
			if(values != null) {
				var n = 0;
				var m = values.Length;
				for(n = 0 ; n < m ; n++) {
					var value = values[n];
					if(value != null) {
						var comps = cape.String.split(value, ':', 2);
						var kk = cape.Vector.get(comps, 0);
						var vv = cape.Vector.get(comps, 1);
						if(object.Equals(vv, null)) {
							vv = kk;
						}
						vvs.add((string)kk, (string)vv);
					}
				}
			}
			return(addMultipleChoiceInput(id, label, description, vvs));
		}

		public cave.ui.FormDeclaration.Element addMultipleChoiceInput(string id, string label, string description, cape.KeyValueList<string, string> values) {
			var v = cave.ui.FormDeclaration.Element.forType("MultipleChoiceInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			var vv = new cape.DynamicVector();
			var it = values.iterate();
			while(it != null) {
				var kvp = it.next();
				if(!(kvp != null)) {
					break;
				}
				var m = new cape.DynamicMap();
				m.set("key", (object)kvp.key);
				m.set("value", (object)kvp.value);
				vv.append((object)m);
			}
			v.setProperty("values", (object)vv);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addDateInput(string id, string label, string description, int skipYears) {
			var v = cave.ui.FormDeclaration.Element.forType("DateInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("skipYears", skipYears);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addPhotoCaptureInput(string id, string label, string description, cave.Image defImage) {
			var v = cave.ui.FormDeclaration.Element.forType("PhotoCaptureInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("defaultImage", (object)defImage);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addCodeInput(string id, string label, string description, int rows) {
			var v = cave.ui.FormDeclaration.Element.forType("CodeInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("rows", rows);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addTextAreaInput(string id, string label, string description, int rows) {
			var v = cave.ui.FormDeclaration.Element.forType("TextAreaInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("rows", rows);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addStaticTextElement(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("StaticTextElement");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addRadioGroupInput(string id, string label, string description, string groupname, System.Collections.Generic.List<string> items) {
			var v = cave.ui.FormDeclaration.Element.forType("RadioGroupInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("groupName", (object)groupname);
			v.setProperty("items", (object)cape.DynamicVector.forStringVector(items));
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addCheckBoxInput(string id, string label, string description, bool isChecked) {
			var v = cave.ui.FormDeclaration.Element.forType("CheckBoxInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			v.setProperty("isChecked", isChecked);
			return(addElement(v));
		}

		public cave.ui.FormDeclaration.Element addStringListInput(string id, string label, string description) {
			var v = cave.ui.FormDeclaration.Element.forType("StringListInput");
			v.setProperty("id", (object)id);
			v.setProperty("label", (object)label);
			v.setProperty("description", (object)description);
			return(addElement(v));
		}

		public bool addFieldsTo(System.Collections.Generic.List<object> fields, cave.ui.FormDeclaration.Element element) {
			if(!(fields != null)) {
				return(false);
			}
			if(fields != null) {
				var n = 0;
				var m = fields.Count;
				for(n = 0 ; n < m ; n++) {
					var field = fields[n] as cape.DynamicMap;
					if(field != null) {
						var e = cave.ui.FormDeclaration.Element.forProperties(field);
						var childFields = e.getPropertyVector("fields");
						if(childFields != null) {
							addFieldsTo(childFields, e);
						}
						element.addToChildren(e);
					}
				}
			}
			return(true);
		}

		public bool parseDescription(string desc) {
			clear();
			var data = cape.JSONParser.parse(desc) as cape.DynamicMap;
			if(!(data != null)) {
				return(false);
			}
			if(!addFieldsTo(data.getVector("fields"), root)) {
				return(false);
			}
			return(true);
		}
	}
}
