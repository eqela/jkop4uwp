
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
	public class PopupMenu
	{
		public PopupMenu() {
		}

		private class MyContainer : cave.ui.CustomContainerWidget
		{
			public MyContainer(cave.GuiApplicationContext ctx) : base(ctx) {
			}

			public MyContainer() : base() {
			}

			private Windows.UI.Xaml.UIElement widget = null;

			public override void onWidgetHeightChanged(int height) {
				var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var child = array[n];
						if(child != null) {
							cave.ui.Widget.move(child, cave.ui.Widget.getAbsoluteX(widget), cave.ui.Widget.getAbsoluteY(widget));
						}
					}
				}
			}

			public override void computeWidgetLayout(int widthConstraint) {
				var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var child = array[n];
						if(child != null) {
							cave.ui.Widget.layout(child, widthConstraint);
							cave.ui.Widget.move(child, cave.ui.Widget.getAbsoluteX(widget), cave.ui.Widget.getAbsoluteY(widget));
						}
					}
				}
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, widthConstraint, cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this));
			}

			public Windows.UI.Xaml.UIElement getWidget() {
				return(widget);
			}

			public cave.ui.PopupMenu.MyContainer setWidget(Windows.UI.Xaml.UIElement v) {
				widget = v;
				return(this);
			}
		}

		public static void showBelow(cave.GuiApplicationContext ctx, Windows.UI.Xaml.UIElement w, cave.ui.Menu menu) {
			if(!(w != null)) {
				return;
			}
			if(!(menu != null)) {
				return;
			}
			var widget = w;
			var context = ctx;
			var pm = new Windows.UI.Xaml.Controls.MenuFlyout();
			var array = menu.getEntries();
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var entry = array[n];
					if(entry != null) {
						var i = new Windows.UI.Xaml.Controls.MenuFlyoutItem();
						i.Text = entry.title;
						i.Click += (sender, e) => {
							entry.handler();
						};
						pm.Items.Add(i);
					}
				}
			}
			pm.ShowAt(widget, new Windows.Foundation.Point(0, Widget.getHeight(widget)));
		}
	}
}
