
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
	public abstract class ListItemWidget : cave.ui.VerticalBoxWidget, cave.ui.TitledWidget, cave.ui.DisplayAwareWidget
	{
		protected cave.ui.VerticalBoxWidget list = null;
		private int widgetListSpacing = 0;
		private int widgetListMargin = 0;

		public ListItemWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetListMargin = 0;
			widgetListSpacing = context.getHeightValue("1mm");
		}

		public virtual string getWidgetTitle() {
			return(null);
		}

		public virtual void onWidgetDisplayed() {
			onGetData();
		}

		public override void initializeWidget() {
			base.initializeWidget();
			var shw = getSubHeaderWidget();
			if(shw != null) {
				addWidget(shw);
			}
			setWidgetMargin(context.getHeightValue("1mm"));
			setWidgetSpacing(context.getHeightValue("1mm"));
			list = cave.ui.VerticalBoxWidget.forContext(context, widgetListMargin, widgetListSpacing);
			addWidget((Windows.UI.Xaml.UIElement)cave.ui.VerticalScrollerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)list), 1.00);
		}

		public void onGetData() {
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)list);
			startDataQuery((System.Collections.Generic.List<cape.DynamicMap> response) => {
				if(!(response != null) || (cape.Vector.getSize(response) < 1)) {
					onNoDataReceived();
					return;
				}
				onDataReceived(response);
			});
		}

		public virtual void onNoDataReceived() {
			list.addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)cave.ui.LabelWidget.forText(context, "No data found"), 0.50, 0.50));
		}

		public virtual void onDataReceived(System.Collections.Generic.List<cape.DynamicMap> data) {
			if(data != null) {
				var n = 0;
				var m = data.Count;
				for(n = 0 ; n < m ; n++) {
					var record = data[n];
					if(record != null) {
						var widget = createWidgetForRecord(record);
						if(widget != null) {
							list.addWidget(widget);
						}
					}
				}
			}
		}

		public virtual Windows.UI.Xaml.UIElement getSubHeaderWidget() {
			return(null);
		}

		public virtual Windows.UI.Xaml.UIElement createWidgetForRecord(cape.DynamicMap record) {
			return(null);
		}

		public abstract void startDataQuery(System.Action<System.Collections.Generic.List<cape.DynamicMap>> callback);

		public int getWidgetListSpacing() {
			return(widgetListSpacing);
		}

		public cave.ui.ListItemWidget setWidgetListSpacing(int v) {
			widgetListSpacing = v;
			return(this);
		}

		public int getWidgetListMargin() {
			return(widgetListMargin);
		}

		public cave.ui.ListItemWidget setWidgetListMargin(int v) {
			widgetListMargin = v;
			return(this);
		}
	}
}
