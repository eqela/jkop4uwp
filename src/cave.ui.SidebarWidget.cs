
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
	public class SidebarWidget : cave.ui.LayerWidget
	{
		public SidebarWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public SidebarWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public static cave.ui.SidebarWidget forItems(cave.GuiApplicationContext ctx, System.Collections.Generic.List<Windows.UI.Xaml.UIElement> items, cave.Color color = null) {
			var v = new cave.ui.SidebarWidget(ctx);
			v.setWidgetBackgroundColor(color);
			v.setWidgetItems(items);
			return(v);
		}

		private cave.Color widgetBackgroundColor = null;
		private cave.Color defaultActionItemWidgetBackgroundColor = null;
		private cave.Color defaultActionItemWidgetTextColor = null;
		private cave.Color defaultLabelItemWidgetBackgroundColor = null;
		private cave.Color defaultLabelItemWidgetTextColor = null;
		private System.Collections.Generic.List<Windows.UI.Xaml.UIElement> widgetItems = null;
		private bool widgetScrollEnabled = true;

		public void addToWidgetItems(Windows.UI.Xaml.UIElement widget) {
			if(widget == null) {
				return;
			}
			if(widgetItems == null) {
				widgetItems = new System.Collections.Generic.List<Windows.UI.Xaml.UIElement>();
			}
			widgetItems.Add(widget);
		}

		public cave.Color determineBackgroundColor() {
			var wc = widgetBackgroundColor;
			if(wc == null) {
				wc = cave.Color.white();
			}
			return(wc);
		}

		private cave.Color determineTextColor(cave.Color backgroundColor, cave.Color textColor, cave.Color defaultTextColor, cave.Color lowerBackgroundColor) {
			var tc = textColor;
			if(tc == null) {
				tc = defaultTextColor;
			}
			if(tc == null) {
				var cc = lowerBackgroundColor;
				if(cc == null) {
					cc = determineBackgroundColor();
				}
				if(cc.isDarkColor()) {
					tc = cave.Color.white();
				}
				else {
					tc = cave.Color.black();
				}
			}
			return(tc);
		}

		public Windows.UI.Xaml.UIElement addLabelItem(string text, bool bold = true, cave.Color backgroundColor = null, cave.Color textColor = null) {
			var v = new cave.ui.LayerWidget(context);
			var bgc = backgroundColor;
			if(bgc == null) {
				bgc = defaultLabelItemWidgetBackgroundColor;
			}
			var tc = determineTextColor(backgroundColor, textColor, defaultLabelItemWidgetTextColor, bgc);
			if(bgc != null) {
				v.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc));
			}
			var mm3 = context.getHeightValue("3mm");
			var lbl = cave.ui.LabelWidget.forText(context, text);
			lbl.setWidgetFontSize((double)mm3);
			lbl.setWidgetTextColor(tc);
			lbl.setWidgetFontBold(bold);
			v.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lbl, mm3));
			addToWidgetItems((Windows.UI.Xaml.UIElement)v);
			return((Windows.UI.Xaml.UIElement)this);
		}

		public Windows.UI.Xaml.UIElement addActionItem(string text, System.Action handler, bool bold = false, cave.Color backgroundColor = null, cave.Color textColor = null) {
			var v = new cave.ui.LayerWidget(context);
			var bgc = backgroundColor;
			if(bgc == null) {
				bgc = defaultActionItemWidgetBackgroundColor;
			}
			var tc = determineTextColor(backgroundColor, textColor, defaultActionItemWidgetTextColor, bgc);
			if(bgc != null) {
				v.addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc));
			}
			var mm3 = context.getHeightValue("3mm");
			var lbl = cave.ui.LabelWidget.forText(context, text);
			lbl.setWidgetFontSize((double)mm3);
			lbl.setWidgetTextColor(tc);
			lbl.setWidgetFontBold(bold);
			v.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lbl, mm3));
			if(handler != null) {
				cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)v, handler);
			}
			addToWidgetItems((Windows.UI.Xaml.UIElement)v);
			return((Windows.UI.Xaml.UIElement)this);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			var wc = determineBackgroundColor();
			addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, wc));
			var vbox = cave.ui.VerticalBoxWidget.forContext(context, 0);
			if(cape.Vector.isEmpty(widgetItems) == false) {
				if(widgetItems != null) {
					var n = 0;
					var m = widgetItems.Count;
					for(n = 0 ; n < m ; n++) {
						var item = widgetItems[n];
						if(item != null) {
							vbox.addWidget(item);
						}
					}
				}
			}
			var tml = new cave.ui.TopMarginLayerWidget(context);
			tml.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidgetAndWidth(context, (Windows.UI.Xaml.UIElement)vbox, context.getWidthValue("50mm")));
			applyScroller((Windows.UI.Xaml.UIElement)tml);
		}

		private void applyScroller(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			if(!widgetScrollEnabled) {
				addWidget(widget);
			}
			else {
				var scroller = cave.ui.VerticalScrollerWidget.forWidget(context, widget);
				scroller.setWidgetScrollBarDisabled(true);
				addWidget((Windows.UI.Xaml.UIElement)scroller);
			}
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.SidebarWidget setWidgetBackgroundColor(cave.Color v) {
			widgetBackgroundColor = v;
			return(this);
		}

		public cave.Color getDefaultActionItemWidgetBackgroundColor() {
			return(defaultActionItemWidgetBackgroundColor);
		}

		public cave.ui.SidebarWidget setDefaultActionItemWidgetBackgroundColor(cave.Color v) {
			defaultActionItemWidgetBackgroundColor = v;
			return(this);
		}

		public cave.Color getDefaultActionItemWidgetTextColor() {
			return(defaultActionItemWidgetTextColor);
		}

		public cave.ui.SidebarWidget setDefaultActionItemWidgetTextColor(cave.Color v) {
			defaultActionItemWidgetTextColor = v;
			return(this);
		}

		public cave.Color getDefaultLabelItemWidgetBackgroundColor() {
			return(defaultLabelItemWidgetBackgroundColor);
		}

		public cave.ui.SidebarWidget setDefaultLabelItemWidgetBackgroundColor(cave.Color v) {
			defaultLabelItemWidgetBackgroundColor = v;
			return(this);
		}

		public cave.Color getDefaultLabelItemWidgetTextColor() {
			return(defaultLabelItemWidgetTextColor);
		}

		public cave.ui.SidebarWidget setDefaultLabelItemWidgetTextColor(cave.Color v) {
			defaultLabelItemWidgetTextColor = v;
			return(this);
		}

		public System.Collections.Generic.List<Windows.UI.Xaml.UIElement> getWidgetItems() {
			return(widgetItems);
		}

		public cave.ui.SidebarWidget setWidgetItems(System.Collections.Generic.List<Windows.UI.Xaml.UIElement> v) {
			widgetItems = v;
			return(this);
		}

		public bool getWidgetScrollEnabled() {
			return(widgetScrollEnabled);
		}

		public cave.ui.SidebarWidget setWidgetScrollEnabled(bool v) {
			widgetScrollEnabled = v;
			return(this);
		}
	}
}
