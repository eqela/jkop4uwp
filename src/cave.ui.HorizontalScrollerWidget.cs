
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
	public class HorizontalScrollerWidget : Windows.UI.Xaml.Controls.Control, cave.ui.WidgetWithLayout
	{
		public HorizontalScrollerWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.HorizontalScrollerWidget forWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget) {
			var v = new cave.ui.HorizontalScrollerWidget(context);
			v.addWidget(widget);
			return(v);
		}

		private System.Action<int> onScrolledHandler = null;
		private int layoutWidth = -1;

		public HorizontalScrollerWidget(cave.GuiApplicationContext context) {
		}

		public virtual bool layoutWidget(int widthConstraint, bool force) {
			var mw = 0;
			var mh = 0;
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.move(child, 0, 0);
						cave.ui.Widget.layout(child, -1);
						var childWidth = cave.ui.Widget.getWidth(child);
						var childHeight = cave.ui.Widget.getHeight(child);
						if(childWidth > mw) {
							mw = childWidth;
						}
						if(childHeight > mh) {
							mh = childHeight;
						}
					}
				}
			}
			layoutWidth = mw;
			if(widthConstraint < 0) {
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, mw, mh);
			}
			else {
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, widthConstraint, mh);
			}
			return(true);
		}

		public cave.ui.HorizontalScrollerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			cave.ui.Widget.addChild((Windows.UI.Xaml.UIElement)this, widget);
			return(this);
		}

		public System.Action<int> getOnScrolledHandler() {
			return(onScrolledHandler);
		}

		public cave.ui.HorizontalScrollerWidget setOnScrolledHandler(System.Action<int> v) {
			onScrolledHandler = v;
			return(this);
		}
	}
}
