
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
	public class RadioButtonWidget : cave.ui.LayerWidget, cave.ui.WidgetWithValue
	{
		public RadioButtonWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public RadioButtonWidget(cave.GuiApplicationContext context) : base(context) {
		}

		private class MyRadioButtonWidget : cave.ui.HorizontalBoxWidget
		{
			public MyRadioButtonWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
			}

			public MyRadioButtonWidget(cave.GuiApplicationContext context) : base(context) {
				var thisWidget = (dynamic)this;
				setWidgetSpacing(context.getHeightValue("2000um"));
				var widget = new cave.ui.AlignWidget(context);
				var widget2 = new cave.ui.LayerWidget(context);
				widget2.setWidgetHeightRequest(context.getHeightValue("4000um"));
				widget2.setWidgetWidthRequest(context.getHeightValue("4000um"));
				outline = new cave.ui.CanvasWidget(context);
				outline.setWidgetColor(cave.Color.black());
				outline.setWidgetRoundingRadius((double)context.getHeightValue("2000um"));
				widget2.addWidget((Windows.UI.Xaml.UIElement)outline);
				var widget3 = new cave.ui.LayerWidget(context);
				widget3.setWidgetMargin(context.getHeightValue("500um"));
				canvas = new cave.ui.CanvasWidget(context);
				widget3.addWidget((Windows.UI.Xaml.UIElement)canvas);
				widget2.addWidget((Windows.UI.Xaml.UIElement)widget3);
				widget.addWidget((Windows.UI.Xaml.UIElement)widget2);
				addWidget((Windows.UI.Xaml.UIElement)widget);
				label = new cave.ui.LabelWidget(context);
				addWidget((Windows.UI.Xaml.UIElement)label, 1.00);
			}

			private int widgetIndex = 0;
			private cave.ui.RadioButtonWidget widgetContainer = null;
			private cave.Color widgetColor = null;
			private string widgetText = null;
			private int widgetFontSize = 0;
			private string widgetFontResource = null;

			public override void initializeWidget() {
				base.initializeWidget();
				label.setWidgetText(widgetText);
				label.setWidgetFontSize((double)widgetFontSize);
				label.setWidgetFontResource(widgetFontResource);
				canvas.setWidgetColor(cave.Color.white());
				canvas.setWidgetRoundingRadius((double)context.getHeightValue("1500um"));
				cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)this, () => {
					widgetContainer.updateSelectedWidget(widgetIndex);
				});
			}

			public void onSelected() {
				if(!(widgetColor != null)) {
					canvas.setWidgetColor(cave.Color.black());
				}
				canvas.setWidgetColor(widgetColor);
				canvas.setWidgetRoundingRadius((double)context.getHeightValue("1500um"));
			}

			public void onDeSelected() {
				canvas.setWidgetColor(cave.Color.white());
				canvas.setWidgetRoundingRadius((double)context.getHeightValue("1500um"));
			}

			cave.ui.CanvasWidget outline = null;
			cave.ui.CanvasWidget canvas = null;
			cave.ui.LabelWidget label = null;

			public int getWidgetIndex() {
				return(widgetIndex);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetIndex(int v) {
				widgetIndex = v;
				return(this);
			}

			public cave.ui.RadioButtonWidget getWidgetContainer() {
				return(widgetContainer);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetContainer(cave.ui.RadioButtonWidget v) {
				widgetContainer = v;
				return(this);
			}

			public cave.Color getWidgetColor() {
				return(widgetColor);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetColor(cave.Color v) {
				widgetColor = v;
				return(this);
			}

			public string getWidgetText() {
				return(widgetText);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetText(string v) {
				widgetText = v;
				return(this);
			}

			public int getWidgetFontSize() {
				return(widgetFontSize);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetFontSize(int v) {
				widgetFontSize = v;
				return(this);
			}

			public string getWidgetFontResource() {
				return(widgetFontResource);
			}

			public cave.ui.RadioButtonWidget.MyRadioButtonWidget setWidgetFontResource(string v) {
				widgetFontResource = v;
				return(this);
			}
		}

		public static cave.ui.RadioButtonWidget forGroup(cave.GuiApplicationContext context, System.Collections.Generic.List<string> items) {
			var v = new cave.ui.RadioButtonWidget(context);
			v.setWidgetItems(items);
			return(v);
		}

		public const int HORIZONTAL = 0;
		public const int VERTICAL = 1;
		private int selectedWidgetIndex = 0;
		private System.Collections.Generic.List<cave.ui.RadioButtonWidget.MyRadioButtonWidget> vrb = null;
		private System.Collections.Generic.List<string> widgetItems = null;
		private int widgetFontSize = 0;
		private string widgetFontResource = null;
		private cave.Color widgetOnSelectedColor = null;
		private int widgetOrientation = cave.ui.RadioButtonWidget.VERTICAL;
		private System.Action<int> widgetClickHandler = null;

		public override void initializeWidget() {
			base.initializeWidget();
			vrb = new System.Collections.Generic.List<cave.ui.RadioButtonWidget.MyRadioButtonWidget>();
			cave.ui.CustomContainerWidget box = null;
			if(widgetOrientation == cave.ui.RadioButtonWidget.HORIZONTAL) {
				box = (cave.ui.CustomContainerWidget)cave.ui.HorizontalBoxWidget.forContext(context, context.getHeightValue("2500um"), context.getHeightValue("1500um"));
			}
			else {
				box = (cave.ui.CustomContainerWidget)cave.ui.VerticalBoxWidget.forContext(context, context.getHeightValue("2500um"), context.getHeightValue("1500um"));
			}
			for(var i = 0 ; i < cape.Vector.getSize(widgetItems) ; i++) {
				var d = cape.Vector.get(widgetItems, i);
				if(!(d != null)) {
					continue;
				}
				var rb = new cave.ui.RadioButtonWidget.MyRadioButtonWidget(context);
				rb.setWidgetText(d);
				rb.setWidgetIndex(i);
				rb.setWidgetContainer(this);
				rb.setWidgetFontSize(widgetFontSize);
				rb.setWidgetFontResource(widgetFontResource);
				rb.setWidgetColor(widgetOnSelectedColor);
				vrb.Add(rb);
				if(widgetOrientation == cave.ui.RadioButtonWidget.HORIZONTAL) {
					((cave.ui.HorizontalBoxWidget)box).addWidget((Windows.UI.Xaml.UIElement)rb, 1.00);
				}
				else {
					box.addWidget((Windows.UI.Xaml.UIElement)rb);
				}
			}
			addWidget((Windows.UI.Xaml.UIElement)box);
		}

		public virtual void setWidgetValue(object value) {
			setSelectWidgetValue(cape.String.asString(value));
		}

		public virtual object getWidgetValue() {
			var index = getSelectedWidgetIndex();
			if(!(widgetItems != null) || index < 0) {
				return(null);
			}
			return((object)cape.Vector.get(widgetItems, index));
		}

		public int getSelectedWidgetIndex() {
			return(selectedWidgetIndex);
		}

		public void setSelectWidgetValue(string selectedWidget) {
			updateSelectedWidget(findIndexForWidgetValue(selectedWidget));
		}

		public void updateSelectedWidget(int index) {
			if(selectedWidgetIndex > -1) {
				var widget = cape.Vector.get(vrb, selectedWidgetIndex);
				if(widget != null) {
					widget.onDeSelected();
				}
			}
			var ww = cape.Vector.get(vrb, index);
			if(ww != null) {
				ww.onSelected();
			}
			selectedWidgetIndex = index;
			if(widgetClickHandler != null) {
				widgetClickHandler(index);
			}
		}

		private int findIndexForWidgetValue(string id) {
			var v = -1;
			if(widgetItems != null) {
				var n = 0;
				cape.Iterator<string> it = cape.Vector.iterate(widgetItems);
				while(it != null) {
					var item = it.next();
					if(object.Equals(item, null)) {
						break;
					}
					if(object.Equals(item, id)) {
						v = n;
						break;
					}
					n++;
				}
			}
			return(v);
		}

		public System.Collections.Generic.List<string> getWidgetItems() {
			return(widgetItems);
		}

		public cave.ui.RadioButtonWidget setWidgetItems(System.Collections.Generic.List<string> v) {
			widgetItems = v;
			return(this);
		}

		public int getWidgetFontSize() {
			return(widgetFontSize);
		}

		public cave.ui.RadioButtonWidget setWidgetFontSize(int v) {
			widgetFontSize = v;
			return(this);
		}

		public string getWidgetFontResource() {
			return(widgetFontResource);
		}

		public cave.ui.RadioButtonWidget setWidgetFontResource(string v) {
			widgetFontResource = v;
			return(this);
		}

		public cave.Color getWidgetOnSelectedColor() {
			return(widgetOnSelectedColor);
		}

		public cave.ui.RadioButtonWidget setWidgetOnSelectedColor(cave.Color v) {
			widgetOnSelectedColor = v;
			return(this);
		}

		public int getWidgetOrientation() {
			return(widgetOrientation);
		}

		public cave.ui.RadioButtonWidget setWidgetOrientation(int v) {
			widgetOrientation = v;
			return(this);
		}

		public System.Action<int> getWidgetClickHandler() {
			return(widgetClickHandler);
		}

		public cave.ui.RadioButtonWidget setWidgetClickHandler(System.Action<int> v) {
			widgetClickHandler = v;
			return(this);
		}
	}
}
