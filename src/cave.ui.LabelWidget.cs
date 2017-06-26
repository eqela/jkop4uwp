
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
	public class LabelWidget : Windows.UI.Xaml.Controls.UserControl
	{
		public LabelWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public const int ALIGN_LEFT = 0;
		public const int ALIGN_CENTER = 1;
		public const int ALIGN_RIGHT = 2;
		public const int ALIGN_JUSTIFY = 3;

		public static cave.ui.LabelWidget forText(cave.GuiApplicationContext context, string text) {
			var v = new cave.ui.LabelWidget(context);
			v.setWidgetText(text);
			return(v);
		}

		private Windows.UI.Xaml.Controls.TextBlock textBlock = null;
		private cave.GuiApplicationContext widgetContext = null;
		private string widgetText = null;
		private cave.Color widgetTextColor = null;
		private double widgetFontSize = 0.00;
		private bool widgetFontBold = false;
		private string widgetFontFamily = null;
		private string widgetFontResource = null;
		private int widgetTextAlign = 0;

		public LabelWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			setWidgetStyle("LabelWidget");
			textBlock = new Windows.UI.Xaml.Controls.TextBlock();
			this.Content = textBlock;
		}

		public cave.ui.LabelWidget setWidgetText(string text) {
			widgetText = text;
			if(!(widgetText != null)) {
				widgetText = "";
			}
			textBlock.Text = widgetText;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public string getWidgetText() {
			return(widgetText);
		}

		public cave.ui.LabelWidget setWidgetTextAlign(int align) {
			widgetTextAlign = align;
			if(align == cave.ui.LabelWidget.ALIGN_LEFT) {
				textBlock.TextAlignment = Windows.UI.Xaml.TextAlignment.Left;
			}
			else if(align == cave.ui.LabelWidget.ALIGN_CENTER) {
				textBlock.TextAlignment = Windows.UI.Xaml.TextAlignment.Center;
			}
			else if(align == cave.ui.LabelWidget.ALIGN_RIGHT) {
				textBlock.TextAlignment = Windows.UI.Xaml.TextAlignment.Right;
			}
			else if(align == cave.ui.LabelWidget.ALIGN_JUSTIFY) {
				textBlock.TextAlignment = Windows.UI.Xaml.TextAlignment.Justify;
			}
			else {
				textBlock.TextAlignment = Windows.UI.Xaml.TextAlignment.Left;
			}
			return(this);
		}

		public int getWidgetTextAlign() {
			return(widgetTextAlign);
		}

		public cave.ui.LabelWidget setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			updateWidgetFont();
			return(this);
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public cave.ui.LabelWidget setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			updateWidgetFont();
			return(this);
		}

		public double getFontSize() {
			return(widgetFontSize);
		}

		public cave.ui.LabelWidget setWidgetFontBold(bool bold) {
			widgetFontBold = bold;
			updateWidgetFont();
			return(this);
		}

		public cave.ui.LabelWidget setWidgetFontFamily(string font) {
			widgetFontFamily = font;
			updateWidgetFont();
			return(this);
		}

		public cave.ui.LabelWidget setWidgetFontResource(string res) {
			widgetFontResource = res;
			updateWidgetFont();
			return(this);
		}

		public cave.ui.LabelWidget setWidgetStyle(string style) {
			widgetFontFamily = widgetContext.getStyleString(style, "fontFamily");
			if(cape.String.isEmpty(widgetFontFamily)) {
				widgetFontFamily = "Arial";
			}
			widgetTextColor = widgetContext.getStyleColor(style, "textColor");
			if(!(widgetTextColor != null)) {
				widgetTextColor = cave.Color.forRGB(0, 0, 0);
			}
			widgetFontSize = (double)widgetContext.getStylePixels(style, "fontSize");
			if(widgetFontSize < 1.00) {
				widgetFontSize = (double)widgetContext.getHeightValue("3mm");
			}
			widgetFontBold = cape.Boolean.asBoolean((object)widgetContext.getStyleString(style, "fontBold"));
			updateWidgetFont();
			return(this);
		}

		private void updateWidgetFont() {
			var fs = widgetFontSize;
			if(fs < 1.00) {
				fs = 1.00;
			}
			this.FontSize = (double)fs;
			if(widgetFontFamily != null) {
				this.FontFamily = new Windows.UI.Xaml.Media.FontFamily(widgetFontFamily);
			}
			else {
				this.FontFamily = Windows.UI.Xaml.Media.FontFamily.XamlAutoFontFamily;
			}
			if(widgetFontBold) {
				this.FontWeight = Windows.UI.Text.FontWeights.Bold;
			}
			else {
				this.FontWeight = Windows.UI.Text.FontWeights.Normal;
			}
			if(widgetTextColor != null) {
				this.Foreground = widgetTextColor.toBrush();
			}
			else {
				this.Foreground = null;
			}
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}
	}
}
