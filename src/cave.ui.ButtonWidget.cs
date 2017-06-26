
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
	public class ButtonWidget : Windows.UI.Xaml.Controls.Button
	{
		public ButtonWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.ButtonWidget forText(cave.GuiApplicationContext context, string text, System.Action handler) {
			var v = new cave.ui.ButtonWidget(context);
			v.setWidgetText(text);
			v.setWidgetClickHandler(handler);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetText = null;
		private cave.Color widgetTextColor = null;
		private cave.Color widgetBackgroundColor = null;
		private cave.Image widgetIcon = null;
		private System.Action widgetClickHandler = null;
		private string widgetFont = null;
		private double widgetFontSize = 0.00;

		public ButtonWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
		}

		public void setWidgetText(string text) {
			widgetText = text;
			this.Content = text;
		}

		public string getWidgetText() {
			return(widgetText);
		}

		public void setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ButtonWidget.setWidgetTextColor] (ButtonWidget.sling:154:2): Not implemented");
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public void setWidgetBackgroundColor(cave.Color color) {
			widgetBackgroundColor = color;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ButtonWidget.setWidgetBackgroundColor] (ButtonWidget.sling:199:2): Not implemented");
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public void onWidgetClicked() {
			if(widgetClickHandler != null) {
				widgetClickHandler();
			}
		}

		public void setWidgetClickHandler(System.Action handler) {
			widgetClickHandler = handler;
			this.Click += (object sender, Windows.UI.Xaml.RoutedEventArgs e) => {
				handler();
			};
		}

		public void setWidgetIcon(cave.Image icon) {
			widgetIcon = icon;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ButtonWidget.setWidgetIcon] (ButtonWidget.sling:268:2): Not implemented");
		}

		public cave.Image getWidgetIcon() {
			return(widgetIcon);
		}

		public void setWidgetFont(string font) {
			widgetFont = font;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ButtonWidget.setWidgetFont] (ButtonWidget.sling:284:2): Not implemented");
		}

		public void setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ButtonWidget.setWidgetFontSize] (ButtonWidget.sling:300:2): Not implemented");
		}
	}
}
