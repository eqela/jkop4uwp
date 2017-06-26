
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
	public class VerticalBoxWidget : cave.ui.CustomContainerWidget
	{
		public VerticalBoxWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private class MyChildEntry
		{
			public MyChildEntry() {
			}

			public Windows.UI.Xaml.UIElement widget = null;
			public double weight = 0.00;
		}

		public static cave.ui.VerticalBoxWidget forContext(cave.GuiApplicationContext context, int widgetMargin = 0, int widgetSpacing = 0) {
			var v = new cave.ui.VerticalBoxWidget(context);
			v.widgetMarginLeft = widgetMargin;
			v.widgetMarginRight = widgetMargin;
			v.widgetMarginTop = widgetMargin;
			v.widgetMarginBottom = widgetMargin;
			v.widgetSpacing = widgetSpacing;
			return(v);
		}

		private System.Collections.Generic.List<cave.ui.VerticalBoxWidget.MyChildEntry> children = null;
		private int widgetSpacing = 0;
		protected int widgetMarginLeft = 0;
		protected int widgetMarginRight = 0;
		protected int widgetMarginTop = 0;
		protected int widgetMarginBottom = 0;
		private int widgetWidthRequest = 0;
		private int widgetMaximumWidthRequest = 0;

		public VerticalBoxWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			children = new System.Collections.Generic.List<cave.ui.VerticalBoxWidget.MyChildEntry>();
		}

		public cave.ui.VerticalBoxWidget setWidgetMargin(int margin) {
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

		public cave.ui.VerticalBoxWidget setWidgetMarginLeft(int value) {
			widgetMarginLeft = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginRight() {
			return(widgetMarginRight);
		}

		public cave.ui.VerticalBoxWidget setWidgetMarginRight(int value) {
			widgetMarginRight = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginTop() {
			return(widgetMarginTop);
		}

		public cave.ui.VerticalBoxWidget setWidgetMarginTop(int value) {
			widgetMarginTop = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginBottom() {
			return(widgetMarginBottom);
		}

		public cave.ui.VerticalBoxWidget setWidgetMarginBottom(int value) {
			widgetMarginBottom = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.VerticalBoxWidget setWidgetWidthRequest(int request) {
			widgetWidthRequest = request;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetWidthRequest() {
			return(widgetWidthRequest);
		}

		public cave.ui.VerticalBoxWidget setWidgetMaximumWidthRequest(int width) {
			widgetMaximumWidthRequest = width;
			if(cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this) > width) {
				cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			}
			return(this);
		}

		public int getWidgetMaximumWidthRequest() {
			return(widgetMaximumWidthRequest);
		}

		public override void computeWidgetLayout(int widthConstraint) {
			var wc = -1;
			if(widthConstraint < 0 && widgetWidthRequest > 0) {
				wc = widgetWidthRequest - widgetMarginLeft - widgetMarginRight;
			}
			if(wc < 0 && widthConstraint >= 0) {
				wc = widthConstraint - widgetMarginLeft - widgetMarginRight;
				if(wc < 0) {
					wc = 0;
				}
			}
			var widest = 0;
			var childCount = 0;
			var y = widgetMarginTop;
			if(wc < 0) {
				if(children != null) {
					var n = 0;
					var m = children.Count;
					for(n = 0 ; n < m ; n++) {
						var child = children[n];
						if(child != null) {
							cave.ui.Widget.layout(child.widget, -1);
							var ww = cave.ui.Widget.getWidth(child.widget);
							if(ww > wc) {
								wc = ww;
							}
						}
					}
				}
			}
			if(children != null) {
				var n2 = 0;
				var m2 = children.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var child1 = children[n2];
					if(child1 != null) {
						if(childCount > 0) {
							y += widgetSpacing;
						}
						childCount++;
						cave.ui.Widget.layout(child1.widget, wc);
						cave.ui.Widget.move(child1.widget, widgetMarginLeft, y);
						var ww1 = cave.ui.Widget.getWidth(child1.widget);
						if(wc < 0 && widgetMaximumWidthRequest > 0 && ww1 + widgetMarginLeft + widgetMarginRight > widgetMaximumWidthRequest) {
							cave.ui.Widget.layout(child1.widget, widgetMaximumWidthRequest - widgetMarginLeft - widgetMarginRight);
							ww1 = cave.ui.Widget.getWidth(child1.widget);
						}
						if(ww1 > widest) {
							widest = ww1;
						}
						y += cave.ui.Widget.getHeight(child1.widget);
					}
				}
			}
			y += widgetMarginBottom;
			var mywidth = widest + widgetMarginLeft + widgetMarginRight;
			if(widthConstraint >= 0) {
				mywidth = widthConstraint;
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, mywidth, y);
			onWidgetHeightChanged(y);
		}

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			var totalWeight = 0.00;
			var availableHeight = height - widgetMarginTop - widgetMarginBottom;
			var childCount = 0;
			if(children != null) {
				var n = 0;
				var m = children.Count;
				for(n = 0 ; n < m ; n++) {
					var child = children[n];
					if(child != null) {
						childCount++;
						if(child.weight > 0.00) {
							totalWeight += child.weight;
						}
						else {
							availableHeight -= cave.ui.Widget.getHeight(child.widget);
						}
					}
				}
			}
			if(childCount > 1 && widgetSpacing > 0) {
				availableHeight -= (childCount - 1) * widgetSpacing;
			}
			if(availableHeight < 0) {
				availableHeight = 0;
			}
			var y = widgetMarginTop;
			if(children != null) {
				var n2 = 0;
				var m2 = children.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var child1 = children[n2];
					if(child1 != null) {
						cave.ui.Widget.move(child1.widget, widgetMarginLeft, y);
						if(child1.weight > 0.00) {
							var hh = (int)(availableHeight * child1.weight / totalWeight);
							cave.ui.Widget.resizeHeight(child1.widget, hh);
						}
						var hh1 = cave.ui.Widget.getHeight(child1.widget);
						y += hh1;
						y += widgetSpacing;
					}
				}
			}
		}

		public override void onChildWidgetRemoved(Windows.UI.Xaml.UIElement widget) {
			if(widget != null) {
				if(children != null) {
					var n = 0;
					var m = children.Count;
					for(n = 0 ; n < m ; n++) {
						var child = children[n];
						if(child != null) {
							if(child.widget == widget) {
								cape.Vector.removeValue(children, child);
								break;
							}
						}
					}
				}
			}
			base.onChildWidgetRemoved(widget);
		}

		public override cave.ui.CustomContainerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			return((cave.ui.CustomContainerWidget)addWidget(widget, 0.00));
		}

		public void removeAllChildren() {
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)this);
		}

		public cave.ui.VerticalBoxWidget addWidget(Windows.UI.Xaml.UIElement widget, double weight) {
			if(widget != null) {
				var ee = new cave.ui.VerticalBoxWidget.MyChildEntry();
				ee.widget = widget;
				ee.weight = weight;
				children.Add(ee);
				cave.ui.Widget.addChild((Windows.UI.Xaml.UIElement)this, widget);
			}
			return(this);
		}

		public int getWidgetSpacing() {
			return(widgetSpacing);
		}

		public cave.ui.VerticalBoxWidget setWidgetSpacing(int v) {
			widgetSpacing = v;
			return(this);
		}
	}
}
