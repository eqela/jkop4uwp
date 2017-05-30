
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
	public class LayerWidget : cave.ui.CustomContainerWidget
	{
		public LayerWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public LayerWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public static cave.ui.LayerWidget findTopMostLayerWidget(Windows.UI.Xaml.UIElement widget) {
			cave.ui.LayerWidget v = null;
			var pp = widget;
			while(pp != null) {
				if(pp is cave.ui.LayerWidget) {
					v = (cave.ui.LayerWidget)pp;
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			return(v);
		}

		public static cave.ui.LayerWidget forContext(cave.GuiApplicationContext context) {
			return(new cave.ui.LayerWidget(context));
		}

		public static cave.ui.LayerWidget forMargin(cave.GuiApplicationContext context, int margin) {
			var v = new cave.ui.LayerWidget(context);
			v.setWidgetMargin(margin);
			return(v);
		}

		public static cave.ui.LayerWidget forWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget, int margin = 0) {
			var v = new cave.ui.LayerWidget(context);
			v.setWidgetMargin(margin);
			v.addWidget(widget);
			return(v);
		}

		public static cave.ui.LayerWidget forWidgetAndWidth(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget, int width) {
			var v = new cave.ui.LayerWidget(context);
			v.addWidget(widget);
			v.setWidgetWidthRequest(width);
			return(v);
		}

		public static cave.ui.LayerWidget forWidgets(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement[] widgets, int margin = 0) {
			var v = new cave.ui.LayerWidget(context);
			v.setWidgetMargin(margin);
			if(widgets != null) {
				var n = 0;
				var m = widgets.Length;
				for(n = 0 ; n < m ; n++) {
					var widget = widgets[n];
					if(widget != null) {
						v.addWidget(widget);
					}
				}
			}
			return(v);
		}

		protected int widgetMarginLeft = 0;
		protected int widgetMarginRight = 0;
		protected int widgetMarginTop = 0;
		protected int widgetMarginBottom = 0;
		private int widgetWidthRequest = 0;
		private int widgetHeightRequest = 0;
		private int widgetMinimumHeightRequest = 0;
		private int widgetMaximumWidthRequest = 0;

		public cave.ui.LayerWidget setWidgetMaximumWidthRequest(int width) {
			widgetMaximumWidthRequest = width;
			if(cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this) > width) {
				cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			}
			return(this);
		}

		public int getWidgetMaximumWidthRequest() {
			return(widgetMaximumWidthRequest);
		}

		public cave.ui.LayerWidget setWidgetWidthRequest(int request) {
			widgetWidthRequest = request;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetWidthRequest() {
			return(widgetWidthRequest);
		}

		public cave.ui.LayerWidget setWidgetHeightRequest(int request) {
			widgetHeightRequest = request;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetHeightRequest() {
			return(widgetHeightRequest);
		}

		public cave.ui.LayerWidget setWidgetMinimumHeightRequest(int request) {
			widgetMinimumHeightRequest = request;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMinimumHeightRequest() {
			return(widgetMinimumHeightRequest);
		}

		public cave.ui.LayerWidget setWidgetMargin(int margin) {
			widgetMarginLeft = margin;
			widgetMarginRight = margin;
			widgetMarginTop = margin;
			widgetMarginBottom = margin;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginLeft() {
			return(widgetMarginLeft);
		}

		public cave.ui.LayerWidget setWidgetMarginLeft(int value) {
			widgetMarginLeft = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginRight() {
			return(widgetMarginRight);
		}

		public cave.ui.LayerWidget setWidgetMarginRight(int value) {
			widgetMarginRight = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginTop() {
			return(widgetMarginTop);
		}

		public cave.ui.LayerWidget setWidgetMarginTop(int value) {
			widgetMarginTop = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginBottom() {
			return(widgetMarginBottom);
		}

		public cave.ui.LayerWidget setWidgetMarginBottom(int value) {
			widgetMarginBottom = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			var mh = height - widgetMarginTop - widgetMarginBottom;
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.resizeHeight(child, mh);
					}
				}
			}
		}

		public override void computeWidgetLayout(int widthConstraint) {
			var wc = widthConstraint;
			if(wc < 0 && widgetWidthRequest > 0) {
				wc = widgetWidthRequest;
			}
			if(wc >= 0) {
				wc = wc - widgetMarginLeft - widgetMarginRight;
				if(wc < 0) {
					wc = 0;
				}
			}
			var mw = 0;
			var mh = 0;
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.layout(child, wc);
						cave.ui.Widget.move(child, widgetMarginLeft, widgetMarginTop);
						var cw = cave.ui.Widget.getWidth(child);
						if(wc < 0 && widgetMaximumWidthRequest > 0 && cw + widgetMarginLeft + widgetMarginRight > widgetMaximumWidthRequest) {
							cave.ui.Widget.layout(child, widgetMaximumWidthRequest - widgetMarginLeft - widgetMarginRight);
							cw = cave.ui.Widget.getWidth(child);
						}
						var ch = cave.ui.Widget.getHeight(child);
						if(cw > mw) {
							mw = cw;
						}
						if(ch > mh) {
							mh = ch;
						}
					}
				}
			}
			var fw = widthConstraint;
			if(fw < 0) {
				fw = mw + widgetMarginLeft + widgetMarginRight;
			}
			var fh = mh + widgetMarginTop + widgetMarginBottom;
			if(widgetHeightRequest > 0) {
				fh = widgetHeightRequest;
			}
			if(widgetMinimumHeightRequest > 0 && fh < widgetMinimumHeightRequest) {
				fh = widgetMinimumHeightRequest;
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, fw, fh);
			mw = cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this) - widgetMarginLeft - widgetMarginRight;
			mh = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this) - widgetMarginTop - widgetMarginBottom;
			var array2 = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array2 != null) {
				var n2 = 0;
				var m2 = array2.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var child1 = array2[n2];
					if(child1 != null) {
						if(wc < 0) {
							cave.ui.Widget.layout(child1, mw);
						}
						cave.ui.Widget.resizeHeight(child1, mh);
					}
				}
			}
		}

		public void removeAllChildren() {
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)this);
		}

		public Windows.UI.Xaml.UIElement getChildWidget(int index) {
			var children = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(!(children != null)) {
				return(null);
			}
			return(cape.Vector.get(children, index));
		}
	}
}
