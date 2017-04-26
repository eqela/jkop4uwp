
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

namespace cave.ui
{
	public class FormWidget : cave.ui.LayerWidget
	{
		private class Action
		{
			public Action() {
			}

			public string label = null;
			public System.Action handler = null;
		}

		private class MyStringListInputWidget : cave.ui.TextInputWidget
		{
			public MyStringListInputWidget(cave.GuiApplicationContext context) : base(context) {
			}

			public override void setWidgetValue(object value) {
				var vv = value as cape.DynamicVector;
				if(vv == null) {
					return;
				}
				var sb = new cape.StringBuilder();
				var array = vv.toVector();
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var v = array[n] as string;
						if(v != null) {
							if(sb.count() > 0) {
								sb.append(", ");
							}
							sb.append(v);
						}
					}
				}
				setWidgetText(sb.toString());
			}

			public override object getWidgetValue() {
				var v = new cape.DynamicVector();
				var array = cape.String.split(getWidgetText(), ',');
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var @string = array[n];
						if(@string != null) {
							v.append((object)cape.String.strip(@string));
						}
					}
				}
				return((object)v);
			}
		}

		private class StaticTextWidget : cave.ui.LayerWidget, cave.ui.WidgetWithValue
		{
			public static cave.ui.FormWidget.StaticTextWidget forText(cave.GuiApplicationContext context, string text) {
				var v = new cave.ui.FormWidget.StaticTextWidget(context);
				v.setWidgetValue((object)text);
				return(v);
			}

			public StaticTextWidget(cave.GuiApplicationContext context) : base(context) {
			}

			private cave.Color backgroundColor = null;
			private cave.Color textColor = null;
			private cave.ui.LabelWidget label = null;
			private string labeltext = null;

			public override void initializeWidget() {
				base.initializeWidget();
				label = cave.ui.LabelWidget.forText(context, labeltext);
				var color = textColor;
				if(color == null) {
					if(backgroundColor.isLightColor()) {
						color = cave.Color.forRGB(0, 0, 0);
					}
					else {
						color = cave.Color.forRGB(255, 255, 255);
					}
				}
				label.setWidgetTextColor(color);
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, backgroundColor));
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)label, context.getHeightValue("1500um")));
			}

			public virtual void setWidgetValue(object value) {
				labeltext = cape.String.asString(value);
				if(label != null) {
					label.setWidgetText(labeltext);
				}
			}

			public virtual object getWidgetValue() {
				if(label == null) {
					return(null);
				}
				return((object)label.getWidgetText());
			}

			public cave.Color getBackgroundColor() {
				return(backgroundColor);
			}

			public cave.ui.FormWidget.StaticTextWidget setBackgroundColor(cave.Color v) {
				backgroundColor = v;
				return(this);
			}

			public cave.Color getTextColor() {
				return(textColor);
			}

			public cave.ui.FormWidget.StaticTextWidget setTextColor(cave.Color v) {
				textColor = v;
				return(this);
			}
		}

		public static cave.ui.FormWidget forDeclaration(cave.GuiApplicationContext context, cave.ui.FormDeclaration form) {
			var v = new cave.ui.FormWidget(context);
			v.setFormDeclaration(form);
			return(v);
		}

		private cave.ui.FormDeclaration formDeclaration = null;
		private System.Collections.Generic.Dictionary<string,Windows.UI.Xaml.UIElement> fieldsById = null;
		private System.Collections.Generic.List<cave.ui.FormWidget.Action> actions = null;
		private int elementSpacing = 0;
		private int formMargin = 0;
		private cave.ui.AlignWidget alignWidget = null;
		private bool enableFieldLabels = true;
		private int formWidth = 0;
		private int fieldLabelFontSize = 0;
		private string fieldLabelFontFamily = null;
		private cave.ui.LayerWidget customFooterWidget = null;
		private cape.DynamicMap queueData = null;
		private cave.Color widgetBackgroundColor = null;
		private bool enableScrolling = true;
		private bool fillContainerWidget = false;
		private cape.DynamicMap preservedFormData = null;
		private System.Collections.Generic.Dictionary<string,System.Action> actionHandlers = null;

		public FormWidget(cave.GuiApplicationContext context) : base(context) {
			fieldsById = new System.Collections.Generic.Dictionary<string,Windows.UI.Xaml.UIElement>();
			formMargin = context.getHeightValue("1mm");
			formWidth = context.getWidthValue("120mm");
			fieldLabelFontSize = context.getHeightValue("2000um");
			fieldLabelFontFamily = "Arial";
			elementSpacing = formMargin;
			customFooterWidget = new cave.ui.LayerWidget(context);
			widgetBackgroundColor = cave.Color.forString("#EEEEEE");
		}

		public cave.ui.LayerWidget getCustomFooterWidget() {
			return(customFooterWidget);
		}

		public virtual cave.ui.FormDeclaration getFormDeclaration() {
			return(formDeclaration);
		}

		public cave.ui.FormWidget setFormDeclaration(cave.ui.FormDeclaration value) {
			formDeclaration = value;
			return(this);
		}

		public virtual void addActions() {
		}

		public void addAction(string label, System.Action handler) {
			if(!(label != null)) {
				return;
			}
			if(!(actions != null)) {
				actions = new System.Collections.Generic.List<cave.ui.FormWidget.Action>();
			}
			var v = new cave.ui.FormWidget.Action();
			v.label = label;
			v.handler = handler;
			actions.Add(v);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForTextInputWidget(cave.ui.TextInputWidget widget, bool allowReplace) {
			widget.setWidgetBackgroundColor(cave.Color.white());
			widget.setWidgetPadding(context.getHeightValue("1500um"));
			widget.setWidgetFontSize((double)context.getHeightValue("3000um"));
			return((Windows.UI.Xaml.UIElement)widget);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForTextButtonWidget(cave.ui.TextButtonWidget widget, bool allowReplace) {
			widget.setWidgetBackgroundColor(cave.Color.forString("blue"));
			cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)widget, 0.90);
			return((Windows.UI.Xaml.UIElement)widget);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForSelectWidget(cave.ui.SelectWidget widget, bool allowReplace) {
			widget.setWidgetBackgroundColor(cave.Color.white());
			widget.setWidgetPadding(context.getHeightValue("1500um"));
			widget.setWidgetFontSize((double)context.getHeightValue("3000um"));
			return((Windows.UI.Xaml.UIElement)widget);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForTextAreaWidget(cave.ui.TextAreaWidget widget, bool allowReplace) {
			widget.setWidgetBackgroundColor(cave.Color.white());
			widget.setWidgetPadding(context.getHeightValue("1500um"));
			widget.setWidgetFontSize((double)context.getHeightValue("3000um"));
			return((Windows.UI.Xaml.UIElement)widget);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForDateSelectorWidget(cave.ui.DateSelectorWidget widget, bool allowReplace) {
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)widget);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						setStyleForWidget(child, false);
					}
				}
			}
			return((Windows.UI.Xaml.UIElement)widget);
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForRadioButtonGroupWidget(cave.ui.RadioButtonGroupWidget widget, bool allowReplace) {
			if(!allowReplace) {
				return((Windows.UI.Xaml.UIElement)widget);
			}
			return((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)widget, context.getHeightValue("1500um")));
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForCheckBoxWidget(cave.ui.CheckBoxWidget widget, bool allowReplace) {
			if(!allowReplace) {
				return((Windows.UI.Xaml.UIElement)widget);
			}
			return((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)widget, context.getHeightValue("1500um")));
		}

		public virtual Windows.UI.Xaml.UIElement setStyleForWidget(Windows.UI.Xaml.UIElement widget, bool allowReplace) {
			if(widget is cave.ui.TextInputWidget) {
				return(setStyleForTextInputWidget((cave.ui.TextInputWidget)widget, allowReplace));
			}
			if(widget is cave.ui.TextButtonWidget) {
				return(setStyleForTextButtonWidget((cave.ui.TextButtonWidget)widget, allowReplace));
			}
			if(widget is cave.ui.SelectWidget) {
				return(setStyleForSelectWidget((cave.ui.SelectWidget)widget, allowReplace));
			}
			if(widget is cave.ui.TextAreaWidget) {
				return(setStyleForTextAreaWidget((cave.ui.TextAreaWidget)widget, allowReplace));
			}
			if(widget is cave.ui.DateSelectorWidget) {
				return(setStyleForDateSelectorWidget((cave.ui.DateSelectorWidget)widget, allowReplace));
			}
			if(widget is cave.ui.RadioButtonGroupWidget) {
				return(setStyleForRadioButtonGroupWidget((cave.ui.RadioButtonGroupWidget)widget, allowReplace));
			}
			if(widget is cave.ui.CheckBoxWidget) {
				return(setStyleForCheckBoxWidget((cave.ui.CheckBoxWidget)widget, allowReplace));
			}
			var array = cave.ui.Widget.getChildren(widget);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						setStyleForWidget(child, false);
					}
				}
			}
			return(widget);
		}

		public string asPlaceHolder(string str) {
			if(enableFieldLabels) {
				return(null);
			}
			return(str);
		}

		public virtual cave.Color getBackgroundColorForElement(cave.ui.FormDeclaration.Element element) {
			if(element.isType("GroupContainer")) {
				return(cave.Color.black());
			}
			return(cave.Color.white());
		}

		public virtual cave.Color getForegroundColorForElement(cave.ui.FormDeclaration.Element element) {
			return(null);
		}

		public cave.Color getAdjustedForegroundColor(cave.ui.FormDeclaration.Element element, cave.Color backgroundColor) {
			var v = getForegroundColorForElement(element);
			if(v != null) {
				return(v);
			}
			if(!(backgroundColor != null)) {
				return(cave.Color.black());
			}
			if(backgroundColor.isWhite()) {
				return(cave.Color.forRGB(100, 100, 100));
			}
			if(backgroundColor.isDarkColor()) {
				return(cave.Color.white());
			}
			return(cave.Color.black());
		}

		public void setActionHandlers(System.Collections.Generic.Dictionary<object,object> handlers) {
			if(!(handlers != null)) {
				return;
			}
			actionHandlers = new System.Collections.Generic.Dictionary<string,System.Action>();
			cape.Iterator<object> keys = cape.Map.iterateKeys(handlers);
			if(!(keys != null)) {
				return;
			}
			while(true) {
				var key = keys.next();
				if(!(key != null)) {
					break;
				}
				if(key is string) {
					actionHandlers[(string)key] = (System.Action)cape.Map.get(handlers, key);
				}
			}
		}

		public void setActionHandler(string actionName, System.Action handler) {
			if(!(actionName != null)) {
				return;
			}
			if(!(handler != null)) {
				return;
			}
			if(!(actionHandlers != null)) {
				actionHandlers = new System.Collections.Generic.Dictionary<string,System.Action>();
			}
			actionHandlers[actionName] = handler;
		}

		public System.Action getActionHandler(string actionName) {
			if(!(actionHandlers != null)) {
				return(null);
			}
			if(!(actionName != null)) {
				return(null);
			}
			return(cape.Map.get(actionHandlers, actionName));
		}

		public virtual Windows.UI.Xaml.UIElement createWidgetForElement(cave.ui.FormDeclaration.Element element) {
			if(!(element != null)) {
				return(null);
			}
			if(element.isType("TextInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_DEFAULT, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("TextButton")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextButtonWidget.forText(context, element.getPropertyString("text"), getActionHandler(element.getPropertyString("action"))));
			}
			if(element.isType("RawTextInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_NONASSISTED, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("PasswordInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_PASSWORD, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("NameInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_NAME, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("EmailAddressInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_EMAIL_ADDRESS, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("PhoneNumberInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_PHONE_NUMBER, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("StreetAddressInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextInputWidget.forType(context, cave.ui.TextInputWidget.TYPE_STREET_ADDRESS, asPlaceHolder(element.getLabel())));
			}
			if(element.isType("TextAreaInput")) {
				return((Windows.UI.Xaml.UIElement)cave.ui.TextAreaWidget.forPlaceholder(context, asPlaceHolder(element.getLabel()), element.getPropertyInteger("rows")));
			}
			if(element.isType("CodeInput")) {
				var v = cave.ui.TextAreaWidget.forPlaceholder(context, asPlaceHolder(element.getLabel()), element.getPropertyInteger("rows"));
				v.configureMonospaceFont();
				return((Windows.UI.Xaml.UIElement)v);
			}
			if(element.isType("StaticTextElement")) {
				var st = cave.ui.FormWidget.StaticTextWidget.forText(context, element.getLabel());
				var bgc = getBackgroundColorForElement(element);
				var fgc = getAdjustedForegroundColor(element, bgc);
				st.setBackgroundColor(bgc);
				st.setTextColor(fgc);
				return((Windows.UI.Xaml.UIElement)st);
			}
			if(element.isType("RadioGroupInput")) {
				var items = element.getPropertyDynamicVector("items");
				if(!(items != null)) {
					items = new cape.DynamicVector();
				}
				return((Windows.UI.Xaml.UIElement)cave.ui.RadioButtonGroupWidget.forGroup(context, element.getPropertyString("groupName"), items.toVectorOfStrings()));
			}
			if(element.isType("MultipleChoiceInput")) {
				var kvl = new cape.KeyValueList<string, string>();
				var values = element.getPropertyVector("values");
				if(values != null) {
					var n = 0;
					var m = values.Count;
					for(n = 0 ; n < m ; n++) {
						var value = values[n] as cape.DynamicMap;
						if(value != null) {
							var key = value.getString("key");
							var val = value.getString("value");
							if(key != null) {
								kvl.add((string)key, (string)val);
							}
						}
					}
				}
				return((Windows.UI.Xaml.UIElement)cave.ui.SelectWidget.forKeyValueList(context, kvl));
			}
			if(element.isType("CheckBoxInput")) {
				var cbx = cave.ui.CheckBoxWidget.forText(context, element.getPropertyString("text"));
				cbx.setWidgetChecked(element.getPropertyBoolean("isChecked"));
				return((Windows.UI.Xaml.UIElement)cbx);
			}
			if(element.isType("DateInput")) {
				var v1 = cave.ui.DateSelectorWidget.forContext(context);
				v1.setSkipYears(element.getPropertyInteger("skipYears"));
				return((Windows.UI.Xaml.UIElement)v1);
			}
			if(element.isType("StringListInput")) {
				var v2 = new cave.ui.FormWidget.MyStringListInputWidget(context);
				v2.setWidgetPlaceholder(element.getLabel());
				return((Windows.UI.Xaml.UIElement)v2);
			}
			cave.ui.CustomContainerWidget container = null;
			if(element.isType("VerticalContainer")) {
				var vb = cave.ui.VerticalBoxWidget.forContext(context);
				if(formWidth > 0) {
					vb.setWidgetWidthRequest(formWidth);
				}
				vb.setWidgetSpacing(elementSpacing);
				container = (cave.ui.CustomContainerWidget)vb;
			}
			else if(element.isType("HorizontalContainer")) {
				var hb = cave.ui.HorizontalBoxWidget.forContext(context);
				hb.setWidgetSpacing(elementSpacing);
				container = (cave.ui.CustomContainerWidget)hb;
			}
			else if(element.isType("GroupContainer")) {
				var vb1 = cave.ui.VerticalBoxWidget.forContext(context);
				if(formWidth > 0) {
					vb1.setWidgetWidthRequest(formWidth);
				}
				vb1.setWidgetSpacing(elementSpacing);
				var wlayer = cave.ui.LayerWidget.forContext(context);
				var bgc1 = getBackgroundColorForElement(element);
				wlayer.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc1));
				var groupLabel = cave.ui.LabelWidget.forText(context, element.getPropertyString("title"));
				groupLabel.setWidgetTextColor(getAdjustedForegroundColor(element, bgc1));
				wlayer.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)groupLabel, context.getHeightValue("2mm")));
				vb1.addWidget((Windows.UI.Xaml.UIElement)wlayer);
				container = (cave.ui.CustomContainerWidget)vb1;
			}
			if(!(container != null)) {
				System.Diagnostics.Debug.WriteLine(("Unsupported form declaration container encountered: `" + element.getType()) + "'");
				return(null);
			}
			var array = element.getChildren();
			if(array != null) {
				var n2 = 0;
				var m2 = array.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var child = array[n2];
					if(child != null) {
						var ww = createAndRegisterWidget(child);
						if(!(ww != null)) {
							continue;
						}
						var label = child.getLabel();
						if(enableFieldLabels && !cape.String.isEmpty(label)) {
							var wlayer1 = cave.ui.LayerWidget.forContext(context);
							var bgc2 = getBackgroundColorForElement(child);
							wlayer1.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc2));
							var wbox = cave.ui.VerticalBoxWidget.forContext(context);
							var lw = cave.ui.LabelWidget.forText(context, label);
							lw.setWidgetTextColor(getAdjustedForegroundColor(child, bgc2));
							lw.setWidgetFontSize((double)fieldLabelFontSize);
							lw.setWidgetFontFamily(fieldLabelFontFamily);
							var ss = context.getHeightValue("1mm");
							wbox.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lw).setWidgetMarginLeft(ss).setWidgetMarginRight(ss).setWidgetMarginTop(ss));
							wbox.addWidget(ww, child.getPropertyDouble("weight"));
							wlayer1.addWidget((Windows.UI.Xaml.UIElement)wbox);
							addToContainerWithWeight(container, (Windows.UI.Xaml.UIElement)wlayer1, child.getPropertyDouble("weight"));
						}
						else {
							addToContainerWithWeight(container, ww, child.getPropertyDouble("weight"));
						}
					}
				}
			}
			return((Windows.UI.Xaml.UIElement)container);
		}

		public void addToContainerWithWeight(cave.ui.CustomContainerWidget container, Windows.UI.Xaml.UIElement child, double weight) {
			if(weight <= 0.00) {
				container.addWidget(child);
			}
			else if(container is cave.ui.HorizontalBoxWidget) {
				((cave.ui.HorizontalBoxWidget)container).addWidget(child, weight);
			}
			else if(container is cave.ui.VerticalBoxWidget) {
				((cave.ui.VerticalBoxWidget)container).addWidget(child, weight);
			}
			else {
				System.Diagnostics.Debug.WriteLine("[cave.ui.FormWidget.addToContainerWithWeight] (FormWidget.sling:489:2): Tried to add a widget with weight to a container that is not a box. Ignoring weight.");
				container.addWidget(child);
			}
		}

		public Windows.UI.Xaml.UIElement createAndRegisterWidget(cave.ui.FormDeclaration.Element element) {
			var v = createWidgetForElement(element);
			if(!(v != null)) {
				return(null);
			}
			var vv = setStyleForWidget(v, true);
			var id = element.getId();
			if(!cape.String.isEmpty(id)) {
				fieldsById[id] = v;
			}
			return(vv);
		}

		public override void computeWidgetLayout(int widthConstraint) {
			if(alignWidget != null) {
				if(widthConstraint >= context.getWidthValue("120mm")) {
					alignWidget.setAlignForIndex(0, 0.50, 0.50);
				}
				else {
					alignWidget.setAlignForIndex(0, 0.50, (double)0);
				}
			}
			base.computeWidgetLayout(widthConstraint);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			var declaration = getFormDeclaration();
			if(!(declaration != null)) {
				return;
			}
			var root = declaration.getRoot();
			if(!(root != null)) {
				return;
			}
			if(widgetBackgroundColor != null) {
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, widgetBackgroundColor));
			}
			var box = cave.ui.VerticalBoxWidget.forContext(context);
			box.setWidgetMargin(formMargin);
			box.setWidgetSpacing(formMargin);
			var topWidget = createAndRegisterWidget(root);
			if(topWidget != null) {
				box.addWidget(topWidget, 1.00);
			}
			if(queueData != null) {
				setFormData(queueData);
			}
			if(!(actions != null)) {
				addActions();
			}
			if(actions != null) {
				var hbox = cave.ui.HorizontalBoxWidget.forContext(context);
				hbox.setWidgetSpacing(context.getHeightValue("1mm"));
				if(actions != null) {
					var n = 0;
					var m = actions.Count;
					for(n = 0 ; n < m ; n++) {
						var action = actions[n];
						if(action != null) {
							var button = cave.ui.TextButtonWidget.forText(context, action.label, action.handler);
							var bb = setStyleForTextButtonWidget(button, true);
							hbox.addWidget(bb, (double)1);
						}
					}
				}
				box.addWidget((Windows.UI.Xaml.UIElement)hbox);
			}
			box.addWidget((Windows.UI.Xaml.UIElement)customFooterWidget);
			Windows.UI.Xaml.UIElement finalWidget = null;
			if(fillContainerWidget) {
				finalWidget = (Windows.UI.Xaml.UIElement)box;
			}
			else {
				alignWidget = cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)box, 0.50, 0.50, 0);
				finalWidget = (Windows.UI.Xaml.UIElement)alignWidget;
			}
			if(enableScrolling) {
				var scroller = cave.ui.VerticalScrollerWidget.forWidget(context, finalWidget);
				addWidget((Windows.UI.Xaml.UIElement)scroller);
			}
			else {
				addWidget(finalWidget);
			}
		}

		public void setFormData(cape.DynamicMap data, bool preserveUnknownValues = false) {
			if(cape.Map.count(fieldsById) < 1) {
				queueData = data;
			}
			else {
				System.Collections.Generic.List<string> keys = cape.Map.getKeys(fieldsById);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n];
						if(key != null) {
							object value = null;
							if(data != null) {
								value = data.get(key);
							}
							setFieldData(key, value);
						}
					}
				}
				if(preserveUnknownValues && (data != null)) {
					preservedFormData = data.duplicateMap();
				}
			}
		}

		public void setFieldData(string id, object value) {
			var widget = cape.Map.get(fieldsById, id) as cave.ui.WidgetWithValue;
			if(!(widget != null)) {
				return;
			}
			widget.setWidgetValue(value);
		}

		public virtual void getFormDataTo(cape.DynamicMap data) {
			if(!(data != null)) {
				return;
			}
			if(preservedFormData != null) {
				var keys = preservedFormData.getKeys();
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n];
						if(key != null) {
							data.set(key, preservedFormData.get(key));
						}
					}
				}
			}
			System.Collections.Generic.List<string> keys1 = cape.Map.getKeys(fieldsById);
			if(keys1 != null) {
				var n2 = 0;
				var m2 = keys1.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var key1 = keys1[n2];
					if(key1 != null) {
						var widget = cape.Map.get(fieldsById, key1) as cave.ui.WidgetWithValue;
						if(!(widget != null)) {
							continue;
						}
						data.set(key1, widget.getWidgetValue());
					}
				}
			}
		}

		public cape.DynamicMap getFormData() {
			var v = new cape.DynamicMap();
			getFormDataTo(v);
			return(v);
		}

		public void clearFormData() {
			var clearData = new cape.DynamicMap();
			System.Collections.Generic.List<string> keys = cape.Map.getKeys(fieldsById);
			if(keys != null) {
				var n = 0;
				var m = keys.Count;
				for(n = 0 ; n < m ; n++) {
					var key = keys[n];
					if(key != null) {
						clearData.set(key, null);
					}
				}
			}
			setFormData(clearData);
		}

		public Windows.UI.Xaml.UIElement getElementAsWidget(string id) {
			if(!(id != null)) {
				return(null);
			}
			var widget = cape.Map.get(fieldsById, id);
			if(!(widget != null)) {
				return(null);
			}
			return(widget);
		}

		public int getElementSpacing() {
			return(elementSpacing);
		}

		public cave.ui.FormWidget setElementSpacing(int v) {
			elementSpacing = v;
			return(this);
		}

		public int getFormMargin() {
			return(formMargin);
		}

		public cave.ui.FormWidget setFormMargin(int v) {
			formMargin = v;
			return(this);
		}

		public bool getEnableFieldLabels() {
			return(enableFieldLabels);
		}

		public cave.ui.FormWidget setEnableFieldLabels(bool v) {
			enableFieldLabels = v;
			return(this);
		}

		public int getFormWidth() {
			return(formWidth);
		}

		public cave.ui.FormWidget setFormWidth(int v) {
			formWidth = v;
			return(this);
		}

		public int getFieldLabelFontSize() {
			return(fieldLabelFontSize);
		}

		public cave.ui.FormWidget setFieldLabelFontSize(int v) {
			fieldLabelFontSize = v;
			return(this);
		}

		public string getFieldLabelFontFamily() {
			return(fieldLabelFontFamily);
		}

		public cave.ui.FormWidget setFieldLabelFontFamily(string v) {
			fieldLabelFontFamily = v;
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.FormWidget setWidgetBackgroundColor(cave.Color v) {
			widgetBackgroundColor = v;
			return(this);
		}

		public bool getEnableScrolling() {
			return(enableScrolling);
		}

		public cave.ui.FormWidget setEnableScrolling(bool v) {
			enableScrolling = v;
			return(this);
		}

		public bool getFillContainerWidget() {
			return(fillContainerWidget);
		}

		public cave.ui.FormWidget setFillContainerWidget(bool v) {
			fillContainerWidget = v;
			return(this);
		}
	}
}
