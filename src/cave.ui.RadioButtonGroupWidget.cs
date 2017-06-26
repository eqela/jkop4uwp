
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
	public class RadioButtonGroupWidget : Windows.UI.Xaml.Controls.Control, cave.ui.WidgetWithValue
	{
		public RadioButtonGroupWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.RadioButtonGroupWidget forGroup(cave.GuiApplicationContext context, string group, System.Collections.Generic.List<string> items) {
			var v = new cave.ui.RadioButtonGroupWidget(context);
			v.setWidgetName(group);
			for(var i = 0 ; i < cape.Vector.getSize(items) ; i++) {
				v.addWidgetItem(cape.Vector.get(items, i), i);
			}
			return(v);
		}

		public const int HORIZONTAL = 0;
		public const int VERTICAL = 1;
		private cave.GuiApplicationContext widgetContext = null;
		private string widgetName = null;
		private string widgetSelectedValue = null;
		private System.Collections.Generic.List<string> widgetItems = null;
		private System.Action widgetChangeHandler = null;

		public RadioButtonGroupWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
		}

		public void addWidgetItem(string text, int index) {
			if(widgetItems == null) {
				widgetItems = new System.Collections.Generic.List<string>();
			}
			widgetItems.Add(text);
			System.Diagnostics.Debug.WriteLine("[cave.ui.RadioButtonGroupWidget.addWidgetItem] (RadioButtonGroupWidget.sling:147:2): Not implemented");
		}

		public void setWidgetSelectedValue(int indx) {
			System.Diagnostics.Debug.WriteLine("[cave.ui.RadioButtonGroupWidget.setWidgetSelectedValue] (RadioButtonGroupWidget.sling:182:2): Not implemented");
		}

		public void setWidgetName(string name) {
			widgetName = name;
		}

		public void setWidgetOrientation(int orientation) {
			if(orientation == cave.ui.RadioButtonGroupWidget.HORIZONTAL) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.RadioButtonGroupWidget.setWidgetOrientation] (RadioButtonGroupWidget.sling:212:3): Not implemented");
			}
			else if(orientation == cave.ui.RadioButtonGroupWidget.VERTICAL) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.RadioButtonGroupWidget.setWidgetOrientation] (RadioButtonGroupWidget.sling:229:3): Not implemented");
			}
		}

		public string getWidgetSelectedValue() {
			return(widgetSelectedValue);
		}

		public void onChangeSelectedItem() {
			if(widgetChangeHandler != null) {
				widgetChangeHandler();
			}
		}

		public virtual void setWidgetValue(object value) {
			var io = value as cape.IntegerObject;
			if(io != null) {
				setWidgetSelectedValue(io.toInteger());
			}
			else {
				setWidgetSelectedValue(-1);
			}
		}

		public virtual object getWidgetValue() {
			return((object)getWidgetSelectedValue());
		}

		public System.Action getWidgetChangeHandler() {
			return(widgetChangeHandler);
		}

		public cave.ui.RadioButtonGroupWidget setWidgetChangeHandler(System.Action v) {
			widgetChangeHandler = v;
			return(this);
		}
	}
}
