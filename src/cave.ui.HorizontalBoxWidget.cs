
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
	public class HorizontalBoxWidget : cave.ui.CustomContainerWidget
	{
		public HorizontalBoxWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private class MyChildEntry
		{
			public MyChildEntry() {
			}

			public Windows.UI.Xaml.UIElement widget = null;
			public double weight = 0.00;
		}

		public static cave.ui.HorizontalBoxWidget forContext(cave.GuiApplicationContext context, int widgetMargin = 0, int widgetSpacing = 0) {
			var v = new cave.ui.HorizontalBoxWidget(context);
			v.widgetMarginLeft = widgetMargin;
			v.widgetMarginRight = widgetMargin;
			v.widgetMarginTop = widgetMargin;
			v.widgetMarginBottom = widgetMargin;
			v.widgetSpacing = widgetSpacing;
			return(v);
		}

		private System.Collections.Generic.List<cave.ui.HorizontalBoxWidget.MyChildEntry> children = null;
		private int widgetSpacing = 0;
		protected int widgetMarginLeft = 0;
		protected int widgetMarginRight = 0;
		protected int widgetMarginTop = 0;
		protected int widgetMarginBottom = 0;
		private int fixedWidgetHeight = 0;

		public HorizontalBoxWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			children = new System.Collections.Generic.List<cave.ui.HorizontalBoxWidget.MyChildEntry>();
		}

		public cave.ui.HorizontalBoxWidget setWidgetMargin(int margin) {
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

		public cave.ui.HorizontalBoxWidget setWidgetMarginLeft(int value) {
			widgetMarginLeft = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginRight() {
			return(widgetMarginRight);
		}

		public cave.ui.HorizontalBoxWidget setWidgetMarginRight(int value) {
			widgetMarginRight = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginTop() {
			return(widgetMarginTop);
		}

		public cave.ui.HorizontalBoxWidget setWidgetMarginTop(int value) {
			widgetMarginTop = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginBottom() {
			return(widgetMarginBottom);
		}

		public cave.ui.HorizontalBoxWidget setWidgetMarginBottom(int value) {
			widgetMarginBottom = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public override void computeWidgetLayout(int widthConstraint) {
			var totalWeight = 0.00;
			var highest = 0;
			var availableWidth = (widthConstraint - widgetMarginLeft) - widgetMarginRight;
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
							cave.ui.Widget.layout(child.widget, -1);
							availableWidth -= cave.ui.Widget.getWidth(child.widget);
							var height = cave.ui.Widget.getHeight(child.widget);
							if(height > highest) {
								highest = height;
							}
						}
					}
				}
			}
			if((childCount > 1) && (widgetSpacing > 0)) {
				availableWidth -= (childCount - 1) * widgetSpacing;
			}
			if(children != null) {
				var n2 = 0;
				var m2 = children.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var child1 = children[n2];
					if(child1 != null) {
						if(child1.weight > 0.00) {
							var ww = (int)((availableWidth * child1.weight) / totalWeight);
							cave.ui.Widget.layout(child1.widget, ww);
							var height1 = cave.ui.Widget.getHeight(child1.widget);
							if(height1 > highest) {
								highest = height1;
							}
						}
					}
				}
			}
			var realHighest = highest;
			highest += widgetMarginTop + widgetMarginBottom;
			if(fixedWidgetHeight > 0) {
				highest = fixedWidgetHeight;
			}
			if(widthConstraint < 0) {
				var totalWidth = widthConstraint - availableWidth;
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, totalWidth, highest);
			}
			else {
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, widthConstraint, highest);
			}
			if(availableWidth < 0) {
				availableWidth = 0;
			}
			var x = widgetMarginLeft;
			if(children != null) {
				var n3 = 0;
				var m3 = children.Count;
				for(n3 = 0 ; n3 < m3 ; n3++) {
					var child2 = children[n3];
					if(child2 != null) {
						var ww1 = 0;
						if(child2.weight > 0.00) {
							ww1 = (int)((availableWidth * child2.weight) / totalWeight);
							cave.ui.Widget.move(child2.widget, x, widgetMarginTop);
							cave.ui.Widget.layout(child2.widget, ww1);
							cave.ui.Widget.resizeHeight(child2.widget, realHighest);
						}
						else {
							ww1 = cave.ui.Widget.getWidth(child2.widget);
							cave.ui.Widget.move(child2.widget, x, widgetMarginTop);
							cave.ui.Widget.layout(child2.widget, ww1);
							cave.ui.Widget.resizeHeight(child2.widget, realHighest);
						}
						x += ww1;
						x += widgetSpacing;
					}
				}
			}
		}

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.resizeHeight(child, (height - widgetMarginTop) - widgetMarginBottom);
					}
				}
			}
		}

		public override void onChildWidgetRemoved(Windows.UI.Xaml.UIElement widget) {
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
			base.onChildWidgetRemoved(widget);
		}

		public override cave.ui.CustomContainerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			return((cave.ui.CustomContainerWidget)addWidget(widget, 0.00));
		}

		public cave.ui.HorizontalBoxWidget addWidget(Windows.UI.Xaml.UIElement widget, double weight) {
			if(widget != null) {
				var ee = new cave.ui.HorizontalBoxWidget.MyChildEntry();
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

		public cave.ui.HorizontalBoxWidget setWidgetSpacing(int v) {
			widgetSpacing = v;
			return(this);
		}

		public int getFixedWidgetHeight() {
			return(fixedWidgetHeight);
		}

		public cave.ui.HorizontalBoxWidget setFixedWidgetHeight(int v) {
			fixedWidgetHeight = v;
			return(this);
		}
	}
}
