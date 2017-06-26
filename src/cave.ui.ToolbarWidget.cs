
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
	public class ToolbarWidget : cave.ui.LayerWidget
	{
		public ToolbarWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public ToolbarWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public static cave.ui.ToolbarWidget forItems(cave.GuiApplicationContext ctx, System.Collections.Generic.List<Windows.UI.Xaml.UIElement> items, cave.Color color = null) {
			var v = new cave.ui.ToolbarWidget(ctx);
			v.setWidgetItems(items);
			v.setWidgetBackgroundColor(color);
			return(v);
		}

		private cave.Color defaultActionItemWidgetBackgroundColor = null;
		private cave.Color defaultActionItemWidgetTextColor = null;
		private cave.Color widgetBackgroundColor = null;
		private System.Collections.Generic.List<Windows.UI.Xaml.UIElement> widgetItems = null;
		private cave.ui.LayerWidget overlayWidget = null;

		public void addToWidgetItems(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			if(!(widgetItems != null)) {
				widgetItems = new System.Collections.Generic.List<Windows.UI.Xaml.UIElement>();
			}
			widgetItems.Add(widget);
		}

		public cave.Color determineBackgroundColor() {
			var wc = widgetBackgroundColor;
			if(!(wc != null)) {
				wc = cave.Color.white();
			}
			return(wc);
		}

		private cave.Color determineTextColor(cave.Color backgroundColor, cave.Color textColor, cave.Color defaultTextColor) {
			var tc = textColor;
			if(!(tc != null)) {
				tc = defaultTextColor;
			}
			if(!(tc != null)) {
				var cc = determineBackgroundColor();
				if(cc.isDarkColor()) {
					tc = cave.Color.white();
				}
				else {
					tc = cave.Color.black();
				}
			}
			return(tc);
		}

		public Windows.UI.Xaml.UIElement addActionItem(string text, string resName, System.Action handler, cave.Color textColor = null) {
			var iconWidget = cave.ui.ImageWidget.forImageResource(context, resName);
			iconWidget.setWidgetImageScaleMethod(cave.ui.ImageWidget.FIT);
			iconWidget.setWidgetImageHeight(context.getHeightValue("5mm"));
			iconWidget.setWidgetImageWidth(context.getWidthValue("5mm"));
			var tc = determineTextColor(widgetBackgroundColor, textColor, defaultActionItemWidgetTextColor);
			var mm2 = context.getHeightValue("2mm");
			var lbl = cave.ui.LabelWidget.forText(context, text);
			lbl.setWidgetFontSize((double)mm2);
			lbl.setWidgetTextColor(tc);
			var vbox = cave.ui.VerticalBoxWidget.forContext(context, 0, context.getHeightValue("1mm"));
			vbox.addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)iconWidget));
			vbox.addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lbl));
			if(handler != null) {
				cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)vbox, handler);
			}
			addToWidgetItems((Windows.UI.Xaml.UIElement)vbox);
			return((Windows.UI.Xaml.UIElement)this);
		}

		public void addOverlay(Windows.UI.Xaml.UIElement widget) {
			overlayWidget.addWidget(widget);
		}

		public bool removeOverlay() {
			if(!(overlayWidget != null)) {
				return(false);
			}
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)overlayWidget);
			return(true);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			overlayWidget = new cave.ui.LayerWidget(context);
			var bgc = widgetBackgroundColor;
			if(!(bgc != null)) {
				bgc = defaultActionItemWidgetBackgroundColor;
			}
			if(bgc != null) {
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc));
			}
			var hbox = cave.ui.HorizontalBoxWidget.forContext(context, 0);
			if(widgetItems != null) {
				var n = 0;
				var m = widgetItems.Count;
				for(n = 0 ; n < m ; n++) {
					var w = widgetItems[n];
					if(w != null) {
						hbox.addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, w), 1.00);
					}
				}
			}
			addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)hbox, context.getHeightValue("1mm")));
			addWidget((Windows.UI.Xaml.UIElement)overlayWidget);
		}

		public cave.Color getDefaultActionItemWidgetBackgroundColor() {
			return(defaultActionItemWidgetBackgroundColor);
		}

		public cave.ui.ToolbarWidget setDefaultActionItemWidgetBackgroundColor(cave.Color v) {
			defaultActionItemWidgetBackgroundColor = v;
			return(this);
		}

		public cave.Color getDefaultActionItemWidgetTextColor() {
			return(defaultActionItemWidgetTextColor);
		}

		public cave.ui.ToolbarWidget setDefaultActionItemWidgetTextColor(cave.Color v) {
			defaultActionItemWidgetTextColor = v;
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.ToolbarWidget setWidgetBackgroundColor(cave.Color v) {
			widgetBackgroundColor = v;
			return(this);
		}

		public System.Collections.Generic.List<Windows.UI.Xaml.UIElement> getWidgetItems() {
			return(widgetItems);
		}

		public cave.ui.ToolbarWidget setWidgetItems(System.Collections.Generic.List<Windows.UI.Xaml.UIElement> v) {
			widgetItems = v;
			return(this);
		}
	}
}
