
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
	public class AlignWidget : cave.ui.CustomContainerWidget
	{
		private class MyChildEntry
		{
			public MyChildEntry() {
			}

			public Windows.UI.Xaml.UIElement widget = null;
			public double alignX = 0.00;
			public double alignY = 0.00;
			public bool maximizeWidth = false;
		}

		public static cave.ui.AlignWidget forWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget, double alignX = 0.50, double alignY = 0.50, int margin = 0) {
			var v = new cave.ui.AlignWidget(context);
			v.widgetMarginLeft = margin;
			v.widgetMarginRight = margin;
			v.widgetMarginTop = margin;
			v.widgetMarginBottom = margin;
			v.addWidget(widget, alignX, alignY);
			return(v);
		}

		private System.Collections.Generic.List<cave.ui.AlignWidget.MyChildEntry> children = null;
		private int widgetMarginLeft = 0;
		private int widgetMarginRight = 0;
		private int widgetMarginTop = 0;
		private int widgetMarginBottom = 0;

		public AlignWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			children = new System.Collections.Generic.List<cave.ui.AlignWidget.MyChildEntry>();
		}

		public cave.ui.AlignWidget setWidgetMargin(int margin) {
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

		public cave.ui.AlignWidget setWidgetMarginLeft(int value) {
			widgetMarginLeft = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginRight() {
			return(widgetMarginRight);
		}

		public cave.ui.AlignWidget setWidgetMarginRight(int value) {
			widgetMarginRight = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginTop() {
			return(widgetMarginTop);
		}

		public cave.ui.AlignWidget setWidgetMarginTop(int value) {
			widgetMarginTop = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public int getWidgetMarginBottom() {
			return(widgetMarginBottom);
		}

		public cave.ui.AlignWidget setWidgetMarginBottom(int value) {
			widgetMarginBottom = value;
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			updateChildWidgetLocations();
		}

		private void updateChildWidgetLocations() {
			if(children != null) {
				var n = 0;
				var m = children.Count;
				for(n = 0 ; n < m ; n++) {
					var child = children[n];
					if(child != null) {
						handleEntry(child);
					}
				}
			}
		}

		public override void computeWidgetLayout(int widthConstraint) {
			var wc = -1;
			if(widthConstraint >= 0) {
				wc = (widthConstraint - widgetMarginLeft) - widgetMarginRight;
				if(wc < 0) {
					wc = 0;
				}
			}
			var mw = 0;
			var mh = 0;
			if(children != null) {
				var n = 0;
				var m = children.Count;
				for(n = 0 ; n < m ; n++) {
					var child = children[n];
					if(child != null) {
						var widget = child.widget;
						if(child.maximizeWidth) {
							cave.ui.Widget.layout(widget, widthConstraint);
						}
						else {
							cave.ui.Widget.layout(widget, -1);
						}
						var cw = cave.ui.Widget.getWidth(widget);
						if((wc >= 0) && (cw > wc)) {
							cave.ui.Widget.layout(widget, wc);
							cw = cave.ui.Widget.getWidth(widget);
						}
						if(cw > mw) {
							mw = cw;
						}
						var ch = cave.ui.Widget.getHeight(widget);
						if(ch > mh) {
							mh = ch;
						}
					}
				}
			}
			var mywidth = (mw + widgetMarginLeft) + widgetMarginRight;
			if(widthConstraint >= 0) {
				mywidth = widthConstraint;
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, mywidth, (mh + widgetMarginTop) + widgetMarginBottom);
			updateChildWidgetLocations();
		}

		private void handleEntry(cave.ui.AlignWidget.MyChildEntry entry) {
			var w = (cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this) - widgetMarginLeft) - widgetMarginRight;
			var h = (cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this) - widgetMarginTop) - widgetMarginBottom;
			var cw = cave.ui.Widget.getWidth(entry.widget);
			var ch = cave.ui.Widget.getHeight(entry.widget);
			if((cw > w) || (ch > h)) {
				if(cw > w) {
					cw = w;
				}
				if(ch > h) {
					ch = h;
				}
			}
			var dx = (int)(widgetMarginLeft + ((w - cw) * entry.alignX));
			var dy = (int)(widgetMarginTop + ((h - ch) * entry.alignY));
			cave.ui.Widget.move(entry.widget, dx, dy);
		}

		public override void onChildWidgetRemoved(Windows.UI.Xaml.UIElement widget) {
			base.onChildWidgetRemoved(widget);
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

		public override cave.ui.CustomContainerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			return((cave.ui.CustomContainerWidget)addWidget(widget, 0.50, 0.50));
		}

		public cave.ui.AlignWidget addWidget(Windows.UI.Xaml.UIElement widget, double alignX, double alignY, bool maximizeWidth = false) {
			var ee = new cave.ui.AlignWidget.MyChildEntry();
			ee.widget = widget;
			ee.alignX = alignX;
			ee.alignY = alignY;
			ee.maximizeWidth = maximizeWidth;
			children.Add(ee);
			cave.ui.Widget.addChild((Windows.UI.Xaml.UIElement)this, widget);
			if(hasSize()) {
				handleEntry(ee);
			}
			return(this);
		}

		public void setAlignForIndex(int index, double alignX, double alignY) {
			var child = cape.Vector.get(children, index);
			if(child == null) {
				return;
			}
			if((child.alignX != alignX) || (child.alignY != alignY)) {
				child.alignX = alignX;
				child.alignY = alignY;
				cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			}
		}
	}
}
