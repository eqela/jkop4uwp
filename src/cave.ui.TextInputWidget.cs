
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
	public class TextInputWidget : Windows.UI.Xaml.Controls.TextBox, cave.ui.WidgetWithValue
	{
		public TextInputWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.TextInputWidget forType(cave.GuiApplicationContext context, int type, string placeholder) {
			var v = new cave.ui.TextInputWidget(context);
			v.setWidgetType(type);
			v.setWidgetPlaceholder(placeholder);
			return(v);
		}

		public const int TYPE_DEFAULT = 0;
		public const int TYPE_NONASSISTED = 1;
		public const int TYPE_NAME = 2;
		public const int TYPE_EMAIL_ADDRESS = 3;
		public const int TYPE_URL = 4;
		public const int TYPE_PHONE_NUMBER = 5;
		public const int TYPE_PASSWORD = 6;
		public const int TYPE_INTEGER = 7;
		public const int TYPE_FLOAT = 8;
		public const int TYPE_STREET_ADDRESS = 9;
		public const int ALIGN_LEFT = 0;
		public const int ALIGN_RIGHT = 1;
		private cave.GuiApplicationContext widgetContext = null;
		private int widgetType = 0;
		private string widgetPlaceholder = null;
		private string widgetText = null;
		private int widgetTextAlign = 0;
		private int widgetPaddingLeft = 0;
		private int widgetPaddingTop = 0;
		private int widgetPaddingRight = 0;
		private int widgetPaddingBottom = 0;
		private string widgetFontFamily = null;
		private string widgetFontResource = null;
		private double widgetFontSize = 0.00;
		private cave.Color widgetTextColor = null;
		private cave.Color widgetBackgroundColor = null;
		private bool widgetIsEnabled = false;
		private System.Action widgetTextChangeHandler = null;
		private System.Action widgetOnFocusHandler = null;
		private System.Action widgetOnLoseFocusHandler = null;

		public TextInputWidget(cave.GuiApplicationContext context) {
			this.Background = cave.Color.forRGBA(0,0,0,0).toBrush();
			this.Padding = new Windows.UI.Xaml.Thickness(0,0,0,0);
			this.BorderThickness = new Windows.UI.Xaml.Thickness(0,0,0,0);
			this.MinHeight = 0.0;
			this.GotFocus += (object sender, Windows.UI.Xaml.RoutedEventArgs e) => {
				onFocusListener();
			};
			this.LostFocus += (object sender, Windows.UI.Xaml.RoutedEventArgs e) => {
				onLoseFocusListener();
			};
			widgetContext = context;
			setWidgetStyle("TextInputWidget");
		}

		public cave.ui.TextInputWidget setWidgetStyle(string style) {
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

		public cave.ui.TextInputWidget setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			updateWidgetColors();
			return(this);
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public cave.ui.TextInputWidget setWidgetBackgroundColor(cave.Color color) {
			widgetBackgroundColor = color;
			updateWidgetColors();
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		private void updateWidgetColors() {
			var textColor = widgetTextColor;
			if(textColor == null) {
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
			this.Foreground = textColor.toBrush();
		}

		public cave.ui.TextInputWidget setWidgetType(int type) {
			widgetType = type;
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextInputWidget.setWidgetType] (TextInputWidget.sling:447:2): Not implemented");
			return(this);
		}

		public int getWidgetType() {
			return(widgetType);
		}

		public cave.ui.TextInputWidget setWidgetText(string text) {
			widgetText = text;
			this.Text = text;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public string getWidgetText() {
			return(this.Text);
		}

		public cave.ui.TextInputWidget setWidgetPlaceholder(string placeholder) {
			widgetPlaceholder = placeholder;
			this.PlaceholderText = placeholder;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public string getWidgetPlaceholder() {
			return(widgetPlaceholder);
		}

		public void setWidgetPadding(int padding) {
			setWidgetPadding(padding, padding, padding, padding);
		}

		public void setWidgetPadding(int x, int y) {
			setWidgetPadding(x, y, x, y);
		}

		public cave.ui.TextInputWidget setWidgetPadding(int l, int t, int r, int b) {
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
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		private void updateWidgetPadding() {
			var thickness = new Windows.UI.Xaml.Thickness();
			thickness.Left = (double)widgetPaddingLeft;
			thickness.Top = (double)widgetPaddingTop;
			thickness.Right = (double)widgetPaddingRight;
			thickness.Bottom = (double)widgetPaddingBottom;
			this.Padding = thickness;
		}

		public cave.ui.TextInputWidget setWidgetFontFamily(string family) {
			widgetFontFamily = family;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextInputWidget setWidgetFontResource(string res) {
			widgetFontResource = res;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.TextInputWidget setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		private void updateWidgetFont() {
			this.FontSize = (double)widgetFontSize;
			this.FontFamily = new Windows.UI.Xaml.Media.FontFamily(widgetFontFamily);
		}

		public void setWidgetIsEnabled(bool v) {
			widgetIsEnabled = v;
			this.IsReadOnly = !v;
		}

		public bool getWidgetIsEnabled() {
			return(widgetIsEnabled);
		}

		public virtual void setWidgetValue(object value) {
			var v = cape.String.asString(value);
			if(!(v != null)) {
				v = "";
			}
			setWidgetText(v);
		}

		public virtual object getWidgetValue() {
			return((object)getWidgetText());
		}

		private void onChangeListener() {
			if(widgetTextChangeHandler != null) {
				widgetTextChangeHandler();
			}
		}

		private void onFocusListener() {
			if(widgetOnFocusHandler != null) {
				widgetOnFocusHandler();
			}
		}

		private void onLoseFocusListener() {
			if(widgetOnLoseFocusHandler != null) {
				widgetOnLoseFocusHandler();
			}
		}

		public cave.ui.TextInputWidget setWidgetTextAlign(int align) {
			widgetTextAlign = align;
			System.Diagnostics.Debug.WriteLine("[cave.ui.TextInputWidget.setWidgetTextAlign] (TextInputWidget.sling:739:2): Not implemented");
			return(this);
		}

		public int getWidgetTextAlign() {
			return(widgetTextAlign);
		}

		public System.Action getWidgetTextChangeHandler() {
			return(widgetTextChangeHandler);
		}

		public cave.ui.TextInputWidget setWidgetTextChangeHandler(System.Action v) {
			widgetTextChangeHandler = v;
			return(this);
		}

		public System.Action getWidgetOnFocusHandler() {
			return(widgetOnFocusHandler);
		}

		public cave.ui.TextInputWidget setWidgetOnFocusHandler(System.Action v) {
			widgetOnFocusHandler = v;
			return(this);
		}

		public System.Action getWidgetOnLoseFocusHandler() {
			return(widgetOnLoseFocusHandler);
		}

		public cave.ui.TextInputWidget setWidgetOnLoseFocusHandler(System.Action v) {
			widgetOnLoseFocusHandler = v;
			return(this);
		}
	}
}
