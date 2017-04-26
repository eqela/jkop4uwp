
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
	public class SwitcherLayerWidget : cave.ui.CustomContainerWidget
	{
		public SwitcherLayerWidget(cave.GuiApplicationContext ctx) : base(ctx) {
		}

		public static cave.ui.SwitcherLayerWidget findTopMostLayerWidget(Windows.UI.Xaml.UIElement widget) {
			cave.ui.SwitcherLayerWidget v = null;
			var pp = widget;
			while(pp != null) {
				if(pp is cave.ui.SwitcherLayerWidget) {
					v = (cave.ui.SwitcherLayerWidget)pp;
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			return(v);
		}

		public static cave.ui.SwitcherLayerWidget forMargin(cave.GuiApplicationContext context, int margin) {
			var v = new cave.ui.SwitcherLayerWidget(context);
			v.margin = margin;
			return(v);
		}

		public static cave.ui.SwitcherLayerWidget forWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget, int margin = 0) {
			var v = new cave.ui.SwitcherLayerWidget(context);
			v.margin = margin;
			v.addWidget(widget);
			return(v);
		}

		public static cave.ui.SwitcherLayerWidget forWidgets(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement[] widgets, int margin = 0) {
			var v = new cave.ui.SwitcherLayerWidget(context);
			v.margin = margin;
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

		private int margin = 0;

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			var children = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(children != null) {
				var topmost = cape.Vector.get(children, cape.Vector.getSize(children) - 1);
				if(topmost != null) {
					cave.ui.Widget.resizeHeight(topmost, (height - margin) - margin);
				}
			}
		}

		public override void computeWidgetLayout(int widthConstraint) {
			var children = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(children == null) {
				return;
			}
			var childCount = cape.Vector.getSize(children);
			var wc = -1;
			if(widthConstraint >= 0) {
				wc = (widthConstraint - margin) - margin;
				if(wc < 0) {
					wc = 0;
				}
			}
			var mw = 0;
			var mh = 0;
			var n = 0;
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n2 = 0;
				var m = array.Count;
				for(n2 = 0 ; n2 < m ; n2++) {
					var child = array[n2];
					if(child != null) {
						if(n == (childCount - 1)) {
							cave.ui.Widget.layout(child, wc);
							mw = cave.ui.Widget.getWidth(child);
							mh = cave.ui.Widget.getHeight(child);
							cave.ui.Widget.move(child, margin, margin);
						}
						else {
							var ww = cave.ui.Widget.getWidth(child);
							cave.ui.Widget.move(child, -ww, margin);
						}
						n++;
					}
				}
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, (mw + margin) + margin, (mh + margin) + margin);
		}

		public void removeAllChildren() {
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)this);
		}

		public Windows.UI.Xaml.UIElement getChildWidget(int index) {
			var children = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(children == null) {
				return(null);
			}
			return(cape.Vector.get(children, index));
		}
	}
}
