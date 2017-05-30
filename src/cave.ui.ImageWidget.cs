
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
	public class ImageWidget : Windows.UI.Xaml.Controls.Control, cave.ui.WidgetWithLayout
	{
		public ImageWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.ImageWidget forImage(cave.GuiApplicationContext context, cave.Image image) {
			var v = new cave.ui.ImageWidget(context);
			v.setWidgetImage(image);
			return(v);
		}

		public static cave.ui.ImageWidget forImageResource(cave.GuiApplicationContext context, string resName) {
			var v = new cave.ui.ImageWidget(context);
			v.setWidgetImageResource(resName);
			return(v);
		}

		public const int STRETCH = 0;
		public const int FIT = 1;
		public const int FILL = 2;
		private cave.GuiApplicationContext widgetContext = null;
		private cave.Image widgetImage = null;
		private int widgetImageWidth = 0;
		private int widgetImageHeight = 0;
		private int widgetImageScaleMethod = 0;

		public ImageWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			setWidgetImageScaleMethod(cave.ui.ImageWidget.STRETCH);
		}

		public void setWidgetImageScaleMethod(int method) {
			widgetImageScaleMethod = method;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ImageWidget.setWidgetImageScaleMethod] (ImageWidget.sling:148:2): Not implemented");
		}

		public void setWidgetImage(cave.Image image) {
			widgetImage = image;
			System.Diagnostics.Debug.WriteLine("[cave.ui.ImageWidget.setWidgetImage] (ImageWidget.sling:186:2): Not implemented");
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}

		public void setWidgetImageResource(string resName) {
			cave.Image img = null;
			if(cape.String.isEmpty(resName) == false && widgetContext != null) {
				img = widgetContext.getResourceImage(resName);
				if(img == null) {
					cape.Log.error((cape.LoggingContext)widgetContext, "Failed to get resource image: `" + resName + "'");
				}
			}
			setWidgetImage(img);
		}

		public virtual bool layoutWidget(int widthConstraint, bool force) {
			if(widgetImage == null) {
				cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, widgetImageWidth, widgetImageHeight);
				return(true);
			}
			if(widthConstraint < 0 && widgetImageWidth < 1 && widgetImageHeight < 1) {
				return(false);
			}
			var width = -1;
			var height = -1;
			if(widgetImageWidth > 0 && widgetImageHeight > 0) {
				width = widgetImageWidth;
				height = widgetImageHeight;
			}
			else if(widgetImageWidth > 0) {
				width = widgetImageWidth;
			}
			else if(widgetImageHeight > 0) {
				height = widgetImageHeight;
			}
			else {
				width = widthConstraint;
			}
			if(width > 0 && widthConstraint >= 0 && width > widthConstraint) {
				width = widthConstraint;
				height = -1;
			}
			if(height < 0) {
				height = widgetImage.getPixelHeight() * width / widgetImage.getPixelWidth();
			}
			if(width < 0) {
				width = widgetImage.getPixelWidth() * height / widgetImage.getPixelHeight();
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, width, height);
			return(true);
		}

		public int getWidgetImageWidth() {
			return(widgetImageWidth);
		}

		public cave.ui.ImageWidget setWidgetImageWidth(int v) {
			widgetImageWidth = v;
			return(this);
		}

		public int getWidgetImageHeight() {
			return(widgetImageHeight);
		}

		public cave.ui.ImageWidget setWidgetImageHeight(int v) {
			widgetImageHeight = v;
			return(this);
		}
	}
}
