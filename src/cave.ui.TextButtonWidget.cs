
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
	public class TextButtonWidget : cave.ui.LayerWidget
	{
		public TextButtonWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.TextButtonWidget forText(cave.GuiApplicationContext context, string text, System.Action handler = null) {
			var v = new cave.ui.TextButtonWidget(context);
			v.setWidgetText(text);
			if(handler != null) {
				v.setWidgetClickHandler(handler);
			}
			return(v);
		}

		public TextButtonWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetContext = ctx;
			setWidgetStyle("TextButtonWidget");
		}

		private cave.GuiApplicationContext widgetContext = null;
		private System.Action widgetClickHandler = null;
		private string widgetText = null;
		private double widgetRoundingRadius = 0.00;
		private cave.Color widgetBackgroundColor = null;
		private cave.Color widgetPressedBackgroundColor = null;
		private cave.Color widgetTextColor = null;
		private int widgetFontSize = 0;
		private string widgetFontFamily = "Arial";
		private string widgetFontResource = null;
		private int widgetPadding = -1;
		private int widgetPaddingHorizontal = -1;

		public cave.ui.TextButtonWidget setWidgetClickHandler(System.Action handler) {
			widgetClickHandler = handler;
			return(this);
		}

		public System.Action getWidgetClickHandler() {
			return(widgetClickHandler);
		}

		public cave.ui.TextButtonWidget setWidgetStyle(string style) {
			widgetRoundingRadius = (double)context.getStylePixels(style, "roundingRadius");
			widgetBackgroundColor = context.getStyleColor(style, "backgroundColor");
			widgetPressedBackgroundColor = context.getStyleColor(style, "pressedColor");
			widgetTextColor = context.getStyleColor(style, "textColor");
			widgetFontSize = context.getStylePixels(style, "fontSize");
			widgetFontFamily = context.getStyleString(style, "fontFamily");
			widgetPadding = context.getStylePixels(style, "padding");
			return(this);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			var bgc = widgetBackgroundColor;
			if(!(bgc != null)) {
				bgc = cave.Color.forRGBDouble(0.60, 0.60, 0.60);
			}
			var canvas = cave.ui.CanvasWidget.forColor(context, bgc);
			canvas.setWidgetRoundingRadius(widgetRoundingRadius);
			addWidget((Windows.UI.Xaml.UIElement)canvas);
			var pbgc = widgetPressedBackgroundColor;
			if(!(pbgc != null)) {
				pbgc = createDefaultPressColor(bgc);
			}
			var cvn = canvas;
			var c1 = bgc;
			var c2 = pbgc;
			var thisWidget = this;
			cave.ui.Widget.setWidgetPointerHandlers((Windows.UI.Xaml.UIElement)this, (double x, double y) => {
				cvn.setWidgetColor(c2);
			}, null, (double x2, double y2) => {
				if(widgetClickHandler != null && x2 <= cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)thisWidget) && y2 <= cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)thisWidget) && x2 >= 0 && y2 >= 0) {
					widgetClickHandler();
				}
				cvn.setWidgetColor(c1);
			});
			var fgc = widgetTextColor;
			if(!(fgc != null)) {
				if(bgc.isLightColor()) {
					fgc = cave.Color.forRGB(0, 0, 0);
				}
				else {
					fgc = cave.Color.forRGB(255, 255, 255);
				}
			}
			var padding = this.widgetPadding;
			if(padding < 0) {
				padding = context.getHeightValue("2mm");
			}
			var label = cave.ui.LabelWidget.forText(context, widgetText);
			label.setWidgetTextColor(fgc);
			if(widgetFontResource != null) {
				label.setWidgetFontResource(widgetFontResource);
			}
			else {
				label.setWidgetFontFamily(widgetFontFamily);
			}
			if(widgetFontSize > 0) {
				label.setWidgetFontSize((double)widgetFontSize);
			}
			var aw = cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)label, 0.50, 0.50, padding);
			if(widgetPaddingHorizontal >= 0) {
				aw.setWidgetMarginLeft(widgetPaddingHorizontal);
				aw.setWidgetMarginRight(widgetPaddingHorizontal);
			}
			addWidget((Windows.UI.Xaml.UIElement)aw);
		}

		private cave.Color createDefaultPressColor(cave.Color bg) {
			if(!(bg != null)) {
				return(null);
			}
			var r = (int)(bg.getRedInt() * (1 - 0.25));
			var g = (int)(bg.getGreenInt() * (1 - 0.25));
			var b = (int)(bg.getBlueInt() * (1 - 0.25));
			return(cave.Color.forRGB(r, g, b));
		}

		public string getWidgetText() {
			return(widgetText);
		}

		public cave.ui.TextButtonWidget setWidgetText(string v) {
			widgetText = v;
			return(this);
		}

		public double getWidgetRoundingRadius() {
			return(widgetRoundingRadius);
		}

		public cave.ui.TextButtonWidget setWidgetRoundingRadius(double v) {
			widgetRoundingRadius = v;
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.TextButtonWidget setWidgetBackgroundColor(cave.Color v) {
			widgetBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetPressedBackgroundColor() {
			return(widgetPressedBackgroundColor);
		}

		public cave.ui.TextButtonWidget setWidgetPressedBackgroundColor(cave.Color v) {
			widgetPressedBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public cave.ui.TextButtonWidget setWidgetTextColor(cave.Color v) {
			widgetTextColor = v;
			return(this);
		}

		public int getWidgetFontSize() {
			return(widgetFontSize);
		}

		public cave.ui.TextButtonWidget setWidgetFontSize(int v) {
			widgetFontSize = v;
			return(this);
		}

		public string getWidgetFontFamily() {
			return(widgetFontFamily);
		}

		public cave.ui.TextButtonWidget setWidgetFontFamily(string v) {
			widgetFontFamily = v;
			return(this);
		}

		public string getWidgetFontResource() {
			return(widgetFontResource);
		}

		public cave.ui.TextButtonWidget setWidgetFontResource(string v) {
			widgetFontResource = v;
			return(this);
		}

		public int getWidgetPadding() {
			return(widgetPadding);
		}

		public cave.ui.TextButtonWidget setWidgetPadding(int v) {
			widgetPadding = v;
			return(this);
		}

		public int getWidgetPaddingHorizontal() {
			return(widgetPaddingHorizontal);
		}

		public cave.ui.TextButtonWidget setWidgetPaddingHorizontal(int v) {
			widgetPaddingHorizontal = v;
			return(this);
		}
	}
}
