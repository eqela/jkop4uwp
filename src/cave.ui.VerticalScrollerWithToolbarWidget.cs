
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
	public class VerticalScrollerWithToolbarWidget : cave.ui.LayerWidget
	{
		public VerticalScrollerWithToolbarWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public VerticalScrollerWithToolbarWidget(cave.GuiApplicationContext context) : base(context) {
		}

		private bool autohideToolbar = false;
		private Windows.UI.Xaml.UIElement widgetContent = null;
		private cave.ui.ToolbarWidget widgetToolbar = null;
		private bool shown = true;

		public override void initializeWidget() {
			base.initializeWidget();
			var scroller = cave.ui.VerticalScrollerWidget.forWidget(context, widgetContent);
			scroller.setOnScrolledHandler((int direction) => {
				if(autohideToolbar) {
					if(direction == 0 && shown == true) {
						hideToolbar();
						shown = false;
					}
					else if(direction == 1 && shown == false) {
						showToolbar();
						shown = true;
					}
				}
			});
			if(autohideToolbar) {
				addWidget((Windows.UI.Xaml.UIElement)scroller);
				var align = new cave.ui.AlignWidget(context);
				align.addWidget((Windows.UI.Xaml.UIElement)widgetToolbar, (double)0, 1.00, true);
				addWidget((Windows.UI.Xaml.UIElement)align);
			}
			else {
				var vbox = cave.ui.VerticalBoxWidget.forContext(context);
				vbox.addWidget((Windows.UI.Xaml.UIElement)scroller, 1.00);
				vbox.addWidget((Windows.UI.Xaml.UIElement)widgetToolbar);
				addWidget((Windows.UI.Xaml.UIElement)vbox);
			}
		}

		public void showToolbar() {
			var sy = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)widgetToolbar);
			var y = cave.ui.Widget.getY((Windows.UI.Xaml.UIElement)widgetToolbar);
			var targety = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this) - sy;
			var anim = cave.ui.WidgetAnimation.forDuration(context, (long)250);
			anim.addCallback((double completion) => {
				var dy = (int)(y - completion * sy);
				if(dy < targety) {
					dy = targety;
				}
				cave.ui.Widget.move((Windows.UI.Xaml.UIElement)widgetToolbar, 0, dy);
			});
			anim.setEndListener(() => {
				shown = true;
			});
			anim.start();
		}

		public void hideToolbar() {
			var sy = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)widgetToolbar);
			var y = cave.ui.Widget.getY((Windows.UI.Xaml.UIElement)widgetToolbar);
			var targety = cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this);
			var anim = cave.ui.WidgetAnimation.forDuration(context, (long)250);
			anim.addCallback((double completion) => {
				var dy = (int)(y + completion * sy);
				if(dy > targety) {
					dy = targety;
				}
				cave.ui.Widget.move((Windows.UI.Xaml.UIElement)widgetToolbar, 0, dy);
			});
			anim.setEndListener(() => {
				shown = false;
			});
			anim.start();
		}

		public bool getAutohideToolbar() {
			return(autohideToolbar);
		}

		public cave.ui.VerticalScrollerWithToolbarWidget setAutohideToolbar(bool v) {
			autohideToolbar = v;
			return(this);
		}

		public Windows.UI.Xaml.UIElement getWidgetContent() {
			return(widgetContent);
		}

		public cave.ui.VerticalScrollerWithToolbarWidget setWidgetContent(Windows.UI.Xaml.UIElement v) {
			widgetContent = v;
			return(this);
		}

		public cave.ui.ToolbarWidget getWidgetToolbar() {
			return(widgetToolbar);
		}

		public cave.ui.VerticalScrollerWithToolbarWidget setWidgetToolbar(cave.ui.ToolbarWidget v) {
			widgetToolbar = v;
			return(this);
		}
	}
}
