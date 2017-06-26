
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
	public class TextAreaWidget : Windows.UI.Xaml.Controls.TextBox, cave.ui.WidgetWithValue
	{
		public TextAreaWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.TextAreaWidget forPlaceholder(cave.GuiApplicationContext context, string placeholder, int rows = 1) {
			var v = new cave.ui.TextAreaWidget(context);
			v.setWidgetPlaceholder(placeholder);
			v.setWidgetRows(rows);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetPlaceholder = null;
		private int widgetPaddingLeft = 0;
		private int widgetPaddingTop = 0;
		private int widgetPaddingRight = 0;
		private int widgetPaddingBottom = 0;
		private int widgetRows = 0;
		private cave.Color widgetTextColor = null;
		private cave.Color widgetBackgroundColor = null;
		private string widgetFontFamily = null;
		private string widgetFontResource = null;
		private double widgetFontSize = 0.00;
		private System.Action widgetTextChangeHandler = null;

		public TextAreaWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			setWidgetStyle("TextAreaWidget");
		}

		public cave.ui.TextAreaWidget setWidgetStyle(string style) {
			widgetFontFamily = widgetContext.getStyleString(style, "fontFamily");
			if(cape.String.isEmpty(widgetFontFamily)) {
				widgetFontFamily = "Arial";
			}
			widgetFontSize = (double)widgetContext.getStylePixels(style, "fontSize");
			if(widgetFontSize < 1.00) {
				widgetFontSize = (double)widgetContext.getHeightValue("3mm");
			}
			widgetTextColor = widgetContext.getStyleColor(style, "textColor");
			widgetBackgroundColor = widgetContext.getStyleColor(style, "backgroundColor");
			setWidgetPadding(widgetContext.getStylePixels(style, "padding"));
			updateWidgetFont();
			updateWidgetColors();
			return(this);
		}

		public void configureMonospaceFont() {
			setWidgetFontFamily("monospace");
		}

		private void updateWidgetColors() {
			var textColor = widgetTextColor;
			if(!(textColor != null)) {
				if(widgetBackgroundColor != null) {
					if(widgetBackgroundColor.isLightColor()) {
						textColor = cave.Color.black();
					}
					else {
						textColor = cave.Color.white();
					}
				}
				else {
					textColor = cave.Color.black();
				}
			}
		}

		private void updateWidgetFont() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.updateWidgetFont] (TextAreaWidget.sling:281:2): Not implemented");
		}

		public cave.ui.TextAreaWidget setWidgetFontFamily(string family) {
			widgetFontFamily = family;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetFontResource(string res) {
			widgetFontResource = res;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			updateWidgetColors();
			return(this);
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public cave.ui.TextAreaWidget setWidgetBackgroundColor(cave.Color color) {
			widgetBackgroundColor = color;
			updateWidgetColors();
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.TextAreaWidget setWidgetRows(int row) {
			this.widgetRows = row;
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.setWidgetRows] (TextAreaWidget.sling:343:2): Not implemented");
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetText(string text) {
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.setWidgetText] (TextAreaWidget.sling:367:2): Not implemented");
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetPlaceholder(string placeholder) {
			widgetPlaceholder = placeholder;
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.setWidgetPlaceholder] (TextAreaWidget.sling:390:2): Not implemented");
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextAreaWidget setWidgetPadding(int padding) {
			return(setWidgetPadding(padding, padding, padding, padding));
		}

		public cave.ui.TextAreaWidget setWidgetPadding(int lr, int tb) {
			return(setWidgetPadding(lr, tb, lr, tb));
		}

		public cave.ui.TextAreaWidget setWidgetPadding(int l, int t, int r, int b) {
			if(l < 0 || t < 0 || r < 0 || b < 0) {
				return(this);
			}
			if(widgetPaddingLeft == l && widgetPaddingTop == t && widgetPaddingRight == r && widgetPaddingBottom == b) {
				return(this);
			}
			widgetPaddingLeft = l;
			widgetPaddingTop = t;
			widgetPaddingRight = r;
			widgetPaddingBottom = b;
			updateWidgetPadding();
			return(this);
		}

		private void updateWidgetPadding() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.updateWidgetPadding] (TextAreaWidget.sling:433:2): Not implemented");
		}

		public string getWidgetText() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextAreaWidget.getWidgetText] (TextAreaWidget.sling:449:2): Not implemented");
			return(null);
		}

		public string getWidgetPlaceholder() {
			return(widgetPlaceholder);
		}

		public virtual void setWidgetValue(object value) {
			setWidgetText(cape.String.asString(value));
		}

		public virtual object getWidgetValue() {
			return((object)getWidgetText());
		}

		private void onChangeListener() {
			if(widgetTextChangeHandler != null) {
				widgetTextChangeHandler();
			}
		}

		public System.Action getWidgetTextChangeHandler() {
			return(widgetTextChangeHandler);
		}

		public cave.ui.TextAreaWidget setWidgetTextChangeHandler(System.Action v) {
			widgetTextChangeHandler = v;
			return(this);
		}
	}
}
