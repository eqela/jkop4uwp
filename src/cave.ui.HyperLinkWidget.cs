
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
	public class HyperLinkWidget : Windows.UI.Xaml.Controls.HyperlinkButton
	{
		public HyperLinkWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.HyperLinkWidget forText(cave.GuiApplicationContext context, string text, System.Action handler) {
			var v = new cave.ui.HyperLinkWidget(context);
			v.setWidgetText(text);
			v.setWidgetClickHandler(handler);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetText = null;
		private cave.Color widgetTextColor = null;
		private double widgetFontSize = 0.00;
		private System.Action widgetClickHandler = null;
		private string widgetUrl = null;

		public HyperLinkWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			setWidgetTextColor(cave.Color.forRGB(0, 0, 255));
			setWidgetFontSize((double)context.getHeightValue("3mm"));
		}

		public void setWidgetText(string text) {
			widgetText = text;
			System.Diagnostics.Debug.WriteLine("[cave.ui.HyperLinkWidget.setWidgetText] (HyperLinkWidget.sling:123:2): Not implemented");
		}

		public string getWidgetText() {
			return(widgetText);
		}

		public void setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			System.Diagnostics.Debug.WriteLine("[cave.ui.HyperLinkWidget.setWidgetTextColor] (HyperLinkWidget.sling:163:2): Not implemented");
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public void setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			System.Diagnostics.Debug.WriteLine("[cave.ui.HyperLinkWidget.setWidgetFontSize] (HyperLinkWidget.sling:189:2): Not implemented");
		}

		public double getFontSize() {
			return(widgetFontSize);
		}

		public void setWidgetClickHandler(System.Action handler) {
			widgetClickHandler = handler;
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)this, handler);
		}

		public void setWidgetUrl(string url) {
			widgetUrl = url;
		}

		public string getWidgetUrl() {
			return(widgetUrl);
		}
	}
}
