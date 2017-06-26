
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
	public class ImageButtonWidget : cave.ui.LayerWidget
	{
		public ImageButtonWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.ImageButtonWidget forImage(cave.GuiApplicationContext context, cave.Image image, System.Action handler) {
			var v = new cave.ui.ImageButtonWidget(context);
			v.setWidgetImage(image);
			v.setWidgetClickHandler(handler);
			return(v);
		}

		public static cave.ui.ImageButtonWidget forImageResource(cave.GuiApplicationContext context, string resName, System.Action handler) {
			var v = new cave.ui.ImageButtonWidget(context);
			v.setWidgetImageResource(resName);
			v.setWidgetClickHandler(handler);
			return(v);
		}

		public ImageButtonWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetContext = ctx;
		}

		private cave.GuiApplicationContext widgetContext = null;
		private cave.Image widgetImage = null;
		private string widgetImageResource = null;
		private System.Action widgetClickHandler = null;
		private int widgetImageScale = 0;
		private int widgetButtonWidth = 0;
		private int widgetButtonHeight = 0;
		private cave.ui.ImageWidget imageWidget = null;

		public cave.ui.ImageButtonWidget setWidgetImage(cave.Image image) {
			widgetImage = image;
			widgetImageResource = null;
			updateImageWidget();
			return(this);
		}

		public cave.ui.ImageButtonWidget setWidgetImageResource(string resName) {
			widgetImageResource = resName;
			widgetImage = null;
			updateImageWidget();
			return(this);
		}

		public cave.ui.ImageButtonWidget setWidgetClickHandler(System.Action handler) {
			widgetClickHandler = handler;
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)this, handler);
			return(this);
		}

		private void updateImageWidget() {
			if(imageWidget == null) {
				return;
			}
			if(widgetImage != null) {
				imageWidget.setWidgetImage(widgetImage);
			}
			else {
				imageWidget.setWidgetImageResource(widgetImageResource);
			}
		}

		public override void initializeWidget() {
			base.initializeWidget();
			imageWidget = new cave.ui.ImageWidget(context);
			imageWidget.setWidgetImageScaleMethod(widgetImageScale);
			imageWidget.setWidgetImageWidth(widgetButtonWidth);
			imageWidget.setWidgetImageHeight(widgetButtonHeight);
			addWidget((Windows.UI.Xaml.UIElement)imageWidget);
			updateImageWidget();
		}

		public int getWidgetImageScale() {
			return(widgetImageScale);
		}

		public cave.ui.ImageButtonWidget setWidgetImageScale(int v) {
			widgetImageScale = v;
			return(this);
		}

		public int getWidgetButtonWidth() {
			return(widgetButtonWidth);
		}

		public cave.ui.ImageButtonWidget setWidgetButtonWidth(int v) {
			widgetButtonWidth = v;
			return(this);
		}

		public int getWidgetButtonHeight() {
			return(widgetButtonHeight);
		}

		public cave.ui.ImageButtonWidget setWidgetButtonHeight(int v) {
			widgetButtonHeight = v;
			return(this);
		}
	}
}
