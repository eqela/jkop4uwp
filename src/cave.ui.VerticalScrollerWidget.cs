
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
	public class VerticalScrollerWidget : Windows.UI.Xaml.Controls.UserControl, cave.ui.WidgetWithLayout, cave.ui.HeightAwareWidget
	{
		public VerticalScrollerWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.VerticalScrollerWidget forWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget) {
			var v = new cave.ui.VerticalScrollerWidget(context);
			v.addWidget(widget);
			return(v);
		}

		private System.Action<int> onScrolledHandler = null;
		private int layoutHeight = -1;
		private bool heightChanged = false;

		public VerticalScrollerWidget(cave.GuiApplicationContext context) {
			this.Content = new Windows.UI.Xaml.Controls.ScrollViewer();
		}

		public virtual void onWidgetHeightChanged(int height) {
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						if(height > layoutHeight) {
							cave.ui.Widget.resizeHeight(child, height);
						}
						else {
							cave.ui.Widget.resizeHeight(child, layoutHeight);
						}
					}
				}
			}
			heightChanged = true;
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
						cave.ui.Widget.layout(child, widthConstraint, heightChanged);
						var cw = cave.ui.Widget.getWidth(child);
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
			var mh2 = mh;
			var eh = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this);
			if(eh > 0 && mh2 > eh) {
				mh2 = eh;
			}
			heightChanged = false;
			layoutHeight = mh;
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, mw, mh2);
			return(true);
		}

		public cave.ui.VerticalScrollerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			cave.ui.Widget.addChild((Windows.UI.Xaml.UIElement)this, widget);
			return(this);
		}

		public System.Action<int> getOnScrolledHandler() {
			return(onScrolledHandler);
		}

		public cave.ui.VerticalScrollerWidget setOnScrolledHandler(System.Action<int> v) {
			onScrolledHandler = v;
			return(this);
		}
	}
}
