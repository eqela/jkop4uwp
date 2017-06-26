
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
	public class AsynchronousImageWidget : cave.ui.LayerWidget
	{
		public AsynchronousImageWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private Windows.UI.Xaml.UIElement overlay = null;
		private int widgetImageWidth = 0;
		private int widgetImageHeight = 0;
		private int widgetImageScaleMethod = 0;
		private cave.Image widgetPlaceholderImage = null;

		public AsynchronousImageWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public virtual void configureImageWidgetProperties(cave.ui.ImageWidget imageWidget) {
			imageWidget.setWidgetImageWidth(widgetImageWidth);
			imageWidget.setWidgetImageHeight(widgetImageHeight);
			imageWidget.setWidgetImageScaleMethod(widgetImageScaleMethod);
		}

		public cave.ui.ImageWidget onStartLoading(bool useOverlay = true) {
			removeAllChildren();
			var v = new cave.ui.ImageWidget(context);
			configureImageWidgetProperties(v);
			if(widgetPlaceholderImage != null) {
				v.setWidgetImage(widgetPlaceholderImage);
			}
			addWidget((Windows.UI.Xaml.UIElement)v);
			if(useOverlay) {
				overlay = (Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, cave.Color.forRGBA(0, 0, 0, 200));
				addWidget(overlay);
			}
			return(v);
		}

		public void onEndLoading() {
			if(overlay != null) {
				cave.ui.Widget.removeFromParent(overlay);
				overlay = null;
			}
		}

		public int getWidgetImageWidth() {
			return(widgetImageWidth);
		}

		public cave.ui.AsynchronousImageWidget setWidgetImageWidth(int v) {
			widgetImageWidth = v;
			return(this);
		}

		public int getWidgetImageHeight() {
			return(widgetImageHeight);
		}

		public cave.ui.AsynchronousImageWidget setWidgetImageHeight(int v) {
			widgetImageHeight = v;
			return(this);
		}

		public int getWidgetImageScaleMethod() {
			return(widgetImageScaleMethod);
		}

		public cave.ui.AsynchronousImageWidget setWidgetImageScaleMethod(int v) {
			widgetImageScaleMethod = v;
			return(this);
		}

		public cave.Image getWidgetPlaceholderImage() {
			return(widgetPlaceholderImage);
		}

		public cave.ui.AsynchronousImageWidget setWidgetPlaceholderImage(cave.Image v) {
			widgetPlaceholderImage = v;
			return(this);
		}
	}
}
