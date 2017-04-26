
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
	public class ActionBarWidget : cave.ui.LayerWidget, cave.ui.ScreenAwareWidget
	{
		public ActionBarWidget(cave.GuiApplicationContext ctx) : base(ctx) {
		}

		private cave.Color widgetBackgroundColor = null;
		private cave.Color widgetTextColor = null;
		private int widgetMenuItemSpacing = 0;
		private string widgetTitle = null;
		private string widgetLeftIconResource = null;
		private System.Action widgetLeftAction = null;
		private string widgetRightIconResource = null;
		private System.Action widgetRightAction = null;
		private cave.ui.AlignWidget widgetTitleContainer = null;
		private cave.ui.LabelWidget label = null;
		private cave.ui.ImageButtonWidget leftButton = null;
		private cave.ui.ImageButtonWidget rightButton = null;
		private cave.ui.HorizontalBoxWidget box = null;
		private cave.ui.HorizontalBoxWidget menuItems = null;
		private cave.ui.LayerWidget overlayWidget = null;
		private cave.ui.CanvasWidget canvas = null;

		public cave.ui.ActionBarWidget setWidgetTitle(string value) {
			widgetTitle = value;
			if(label != null) {
				label.setWidgetText(widgetTitle);
			}
			return(this);
		}

		public void setWidgetTitleAlignment(double align) {
			if(widgetTitleContainer != null) {
				widgetTitleContainer.setAlignForIndex(0, align, 0.50);
			}
		}

		public void setActionBarMargin(int margin) {
			if(box != null) {
				box.setWidgetMargin(margin);
			}
		}

		public cave.ui.LabelWidget getWidgetTitleLabel() {
			return(label);
		}

		public string getWidgetTitle() {
			return(widgetTitle);
		}

		public void configureLeftButton(string iconResource, System.Action action) {
			widgetLeftIconResource = iconResource;
			widgetLeftAction = action;
			updateLeftButton();
		}

		public void configureRightButton(string iconResource, System.Action action) {
			widgetRightIconResource = iconResource;
			widgetRightAction = action;
			updateRightButton();
		}

		private void updateLeftButton() {
			if(leftButton == null) {
				return;
			}
			if(cape.String.isEmpty(widgetLeftIconResource) == false) {
				leftButton.setWidgetImageResource(widgetLeftIconResource);
				leftButton.setWidgetClickHandler(widgetLeftAction);
			}
			else {
				leftButton.setWidgetImageResource(null);
				leftButton.setWidgetClickHandler(null);
			}
		}

		private void updateRightButton() {
			if(rightButton == null) {
				return;
			}
			if(cape.String.isEmpty(widgetRightIconResource) == false) {
				rightButton.setWidgetImageResource(widgetRightIconResource);
				rightButton.setWidgetClickHandler(widgetRightAction);
			}
			else {
				rightButton.setWidgetImageResource(null);
				rightButton.setWidgetClickHandler(null);
			}
		}

		public cave.Color getWidgetTextColor() {
			var wtc = widgetTextColor;
			if(wtc == null) {
				wtc = cave.Color.forRGB(255, 255, 255);
			}
			return(wtc);
		}

		public virtual void onWidgetAddedToScreen(cave.ui.ScreenForWidget screen) {
		}

		public virtual void onWidgetRemovedFromScreen(cave.ui.ScreenForWidget screen) {
		}

		public void configureMenuItems(System.Collections.Generic.List<Windows.UI.Xaml.UIElement> items) {
			if(!(items != null)) {
				return;
			}
			if(!(menuItems != null)) {
				return;
			}
			if(items != null) {
				var n = 0;
				var m = items.Count;
				for(n = 0 ; n < m ; n++) {
					var widget = items[n];
					if(widget != null) {
						menuItems.addWidget(widget);
					}
				}
			}
		}

		public void setActionBarBackgroundColor(cave.Color color) {
			if(canvas != null) {
				canvas.setWidgetColor(color);
			}
		}

		public void addOverlay(Windows.UI.Xaml.UIElement widget) {
			if(overlayWidget != null) {
				overlayWidget.addWidget(widget);
			}
		}

		public bool removeOverlay() {
			if(!(overlayWidget != null)) {
				return(false);
			}
			cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)overlayWidget);
			return(true);
		}

		public void clearMenuItems() {
			if(menuItems != null) {
				cave.ui.Widget.removeChildrenOf((Windows.UI.Xaml.UIElement)menuItems);
			}
		}

		public override void initializeWidget() {
			base.initializeWidget();
			var bgc = widgetBackgroundColor;
			if(bgc != null) {
				canvas = cave.ui.CanvasWidget.forColor(context, bgc);
				addWidget((Windows.UI.Xaml.UIElement)canvas);
			}
			var tml = new cave.ui.TopMarginLayerWidget(context);
			label = cave.ui.LabelWidget.forText(context, widgetTitle);
			label.setWidgetFontFamily("Arial");
			var wtc = getWidgetTextColor();
			label.setWidgetTextColor(wtc);
			box = cave.ui.HorizontalBoxWidget.forContext(context);
			box.setWidgetMargin(context.getWidthValue("1mm"));
			box.setWidgetSpacing(context.getWidthValue("1mm"));
			leftButton = new cave.ui.ImageButtonWidget(context);
			leftButton.setWidgetButtonHeight(context.getHeightValue("6mm"));
			box.addWidget((Windows.UI.Xaml.UIElement)leftButton);
			updateLeftButton();
			widgetTitleContainer = cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)label, 0.50, 0.50);
			box.addWidget((Windows.UI.Xaml.UIElement)widgetTitleContainer, 1.00);
			var spacing = widgetMenuItemSpacing;
			if(spacing < 0) {
				spacing = context.getWidthValue("1mm");
			}
			menuItems = cave.ui.HorizontalBoxWidget.forContext(context, context.getWidthValue("1mm"), spacing);
			box.addWidget((Windows.UI.Xaml.UIElement)menuItems);
			rightButton = new cave.ui.ImageButtonWidget(context);
			rightButton.setWidgetButtonHeight(context.getHeightValue("6mm"));
			box.addWidget((Windows.UI.Xaml.UIElement)rightButton);
			updateRightButton();
			tml.addWidget((Windows.UI.Xaml.UIElement)box);
			overlayWidget = new cave.ui.LayerWidget(context);
			tml.addWidget((Windows.UI.Xaml.UIElement)overlayWidget);
			addWidget((Windows.UI.Xaml.UIElement)tml);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.ui.ActionBarWidget setWidgetBackgroundColor(cave.Color v) {
			widgetBackgroundColor = v;
			return(this);
		}

		public cave.ui.ActionBarWidget setWidgetTextColor(cave.Color v) {
			widgetTextColor = v;
			return(this);
		}

		public int getWidgetMenuItemSpacing() {
			return(widgetMenuItemSpacing);
		}

		public cave.ui.ActionBarWidget setWidgetMenuItemSpacing(int v) {
			widgetMenuItemSpacing = v;
			return(this);
		}
	}
}
