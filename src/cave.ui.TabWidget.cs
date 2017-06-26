
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
	public class TabWidget : cave.ui.LayerWidget
	{
		public TabWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public TabWidget(cave.GuiApplicationContext context) : base(context) {
			var thisWidget = (dynamic)this;
			var widget = new cave.ui.VerticalBoxWidget(context);
			tabHeaders = new cave.ui.HorizontalBoxWidget(context);
			widget.addWidget((Windows.UI.Xaml.UIElement)tabHeaders);
			var widget2 = new cave.ui.LayerWidget(context);
			background = new cave.ui.CanvasWidget(context);
			background.setWidgetColor(cave.Color.white());
			widget2.addWidget((Windows.UI.Xaml.UIElement)background);
			content = new cave.ui.LayerWidget(context);
			widget2.addWidget((Windows.UI.Xaml.UIElement)content);
			widget.addWidget((Windows.UI.Xaml.UIElement)widget2, 1.00);
			addWidget((Windows.UI.Xaml.UIElement)widget);
		}

		private class TabHeaderWidget : cave.ui.LayerWidget
		{
			public TabHeaderWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
			}

			public TabHeaderWidget(cave.GuiApplicationContext context) : base(context) {
				var thisWidget = (dynamic)this;
				headerBackground = new cave.ui.CanvasWidget(context);
				addWidget((Windows.UI.Xaml.UIElement)headerBackground);
				headerPadding = new cave.ui.LayerWidget(context);
				headerTitle = new cave.ui.LabelWidget(context);
				headerTitle.setWidgetText("Tab Header");
				headerPadding.addWidget((Windows.UI.Xaml.UIElement)headerTitle);
				addWidget((Windows.UI.Xaml.UIElement)headerPadding);
			}

			private int index = 0;
			private cave.ui.TabWidget container = null;

			public override void initializeWidget() {
				base.initializeWidget();
				cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)this, () => {
					container.updateSelectedTab(index);
				});
			}

			public void setHeaderMargin(int tabHeaderPadding) {
				headerPadding.setWidgetMargin(tabHeaderPadding);
			}

			public void setHeaderTitle(string text) {
				headerTitle.setWidgetText(text);
			}

			public void setHeaderBackground(cave.Color color) {
				headerBackground.setWidgetColor(color);
			}

			public void setHeaderTextColor(cave.Color color) {
				headerTitle.setWidgetTextColor(color);
			}

			cave.ui.CanvasWidget headerBackground = null;
			cave.ui.LayerWidget headerPadding = null;
			cave.ui.LabelWidget headerTitle = null;

			public int getIndex() {
				return(index);
			}

			public cave.ui.TabWidget.TabHeaderWidget setIndex(int v) {
				index = v;
				return(this);
			}

			public cave.ui.TabWidget getContainer() {
				return(container);
			}

			public cave.ui.TabWidget.TabHeaderWidget setContainer(cave.ui.TabWidget v) {
				container = v;
				return(this);
			}
		}

		private System.Collections.Generic.List<cape.DynamicMap> widgetTabContent = null;
		private cave.Color widgetSelectedTabBackgroundColor = null;
		private cave.Color widgetSelectedTabTextColor = null;
		private cave.Color widgetUnselectedTabBackgroundColor = null;
		private cave.Color widgetUnselectedTabTextColor = null;
		private int widgetTabHeaderTitleMargin = 0;
		private System.Action<int, Windows.UI.Xaml.UIElement> widgetOnTabChangeListener = null;

		public override void initializeWidget() {
			base.initializeWidget();
			if(!(widgetTabContent != null)) {
				return;
			}
			for(var i = 0 ; i < cape.Vector.getSize(widgetTabContent) ; i++) {
				var d = cape.Vector.get(widgetTabContent, i);
				if(!(d != null)) {
					continue;
				}
				var h = new cave.ui.TabWidget.TabHeaderWidget(context);
				h.setHeaderMargin(widgetTabHeaderTitleMargin);
				h.setContainer(this);
				h.setIndex(i);
				h.setHeaderTitle(d.getString("header"));
				tabHeaders.addWidget((Windows.UI.Xaml.UIElement)h, 1.00);
			}
			updateSelectedTab(0);
		}

		public void updateSelectedTab(int idx) {
			var w = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)tabHeaders);
			for(var i = 0 ; i < w.Count ; i++) {
				if(i == idx) {
					if(!(widgetSelectedTabBackgroundColor != null)) {
						widgetSelectedTabBackgroundColor = cave.Color.white();
					}
					if(!(widgetSelectedTabTextColor != null)) {
						widgetSelectedTabTextColor = cave.Color.black();
					}
					(w[idx] as cave.ui.TabWidget.TabHeaderWidget).setHeaderBackground(widgetSelectedTabBackgroundColor);
					(w[idx] as cave.ui.TabWidget.TabHeaderWidget).setHeaderTextColor(widgetSelectedTabTextColor);
					continue;
				}
				if(!(widgetUnselectedTabBackgroundColor != null)) {
					widgetUnselectedTabBackgroundColor = cave.Color.forRGB(128, 128, 128);
				}
				if(!(widgetUnselectedTabTextColor != null)) {
					widgetUnselectedTabTextColor = cave.Color.forRGB(80, 80, 80);
				}
				(w[i] as cave.ui.TabWidget.TabHeaderWidget).setHeaderBackground(widgetUnselectedTabBackgroundColor);
				(w[i] as cave.ui.TabWidget.TabHeaderWidget).setHeaderTextColor(widgetUnselectedTabTextColor);
			}
			updateContent(idx);
		}

		private void updateContent(int idx) {
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)content);
			var v = cape.Vector.get(widgetTabContent, idx);
			if(!(v != null)) {
				return;
			}
			var w = v.get("widget") as Windows.UI.Xaml.UIElement;
			if(!(w != null)) {
				return;
			}
			content.addWidget(w);
			if(widgetOnTabChangeListener != null) {
				widgetOnTabChangeListener(idx, w);
			}
		}

		public void setWidgetSelectedTab(int index) {
			if(index < widgetTabContent.Count) {
				updateSelectedTab(index);
			}
		}

		cave.ui.HorizontalBoxWidget tabHeaders = null;
		cave.ui.CanvasWidget background = null;
		cave.ui.LayerWidget content = null;

		public System.Collections.Generic.List<cape.DynamicMap> getWidgetTabContent() {
			return(widgetTabContent);
		}

		public cave.ui.TabWidget setWidgetTabContent(System.Collections.Generic.List<cape.DynamicMap> v) {
			widgetTabContent = v;
			return(this);
		}

		public cave.Color getWidgetSelectedTabBackgroundColor() {
			return(widgetSelectedTabBackgroundColor);
		}

		public cave.ui.TabWidget setWidgetSelectedTabBackgroundColor(cave.Color v) {
			widgetSelectedTabBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetSelectedTabTextColor() {
			return(widgetSelectedTabTextColor);
		}

		public cave.ui.TabWidget setWidgetSelectedTabTextColor(cave.Color v) {
			widgetSelectedTabTextColor = v;
			return(this);
		}

		public cave.Color getWidgetUnselectedTabBackgroundColor() {
			return(widgetUnselectedTabBackgroundColor);
		}

		public cave.ui.TabWidget setWidgetUnselectedTabBackgroundColor(cave.Color v) {
			widgetUnselectedTabBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetUnselectedTabTextColor() {
			return(widgetUnselectedTabTextColor);
		}

		public cave.ui.TabWidget setWidgetUnselectedTabTextColor(cave.Color v) {
			widgetUnselectedTabTextColor = v;
			return(this);
		}

		public int getWidgetTabHeaderTitleMargin() {
			return(widgetTabHeaderTitleMargin);
		}

		public cave.ui.TabWidget setWidgetTabHeaderTitleMargin(int v) {
			widgetTabHeaderTitleMargin = v;
			return(this);
		}

		public System.Action<int, Windows.UI.Xaml.UIElement> getWidgetOnTabChangeListener() {
			return(widgetOnTabChangeListener);
		}

		public cave.ui.TabWidget setWidgetOnTabChangeListener(System.Action<int, Windows.UI.Xaml.UIElement> v) {
			widgetOnTabChangeListener = v;
			return(this);
		}
	}
}
