
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
	public class CheckBoxWidget : Windows.UI.Xaml.Controls.CheckBox, cave.ui.WidgetWithValue
	{
		public static cave.ui.CheckBoxWidget forText(cave.GuiApplicationContext context, string text) {
			var v = new cave.ui.CheckBoxWidget(context);
			v.setWidgetText(text);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetText = null;
		private cave.Color widgetTextColor = null;
		private System.Action widgetCheckHandler = null;

		public CheckBoxWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			widgetTextColor = cave.Color.black();
			setWidgetTextColor(widgetTextColor);
		}

		public void setWidgetText(string text) {
			widgetText = text;
			System.Diagnostics.Debug.WriteLine("[cave.ui.CheckBoxWidget.setWidgetText] (CheckBoxWidget.sling:120:2): Not implemented");
		}

		public string getWidgetText() {
			return(widgetText);
		}

		public void setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			System.Diagnostics.Debug.WriteLine("[cave.ui.CheckBoxWidget.setWidgetTextColor] (CheckBoxWidget.sling:144:2): Not implemented");
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public bool getWidgetChecked() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.CheckBoxWidget.getWidgetChecked] (CheckBoxWidget.sling:165:2): Not implemented.");
			return(false);
		}

		public void setWidgetChecked(bool x) {
			System.Diagnostics.Debug.WriteLine("[cave.ui.CheckBoxWidget.setWidgetChecked] (CheckBoxWidget.sling:194:2): Not implemented");
		}

		public virtual void setWidgetValue(object value) {
			setWidgetChecked(cape.Boolean.asBoolean(value));
		}

		public virtual object getWidgetValue() {
			return((object)cape.Boolean.asObject(getWidgetChecked()));
		}

		private void onCheckChangeListener() {
			if(widgetCheckHandler != null) {
				widgetCheckHandler();
			}
		}

		public System.Action getWidgetCheckHandler() {
			return(widgetCheckHandler);
		}

		public cave.ui.CheckBoxWidget setWidgetCheckHandler(System.Action v) {
			widgetCheckHandler = v;
			return(this);
		}
	}
}
