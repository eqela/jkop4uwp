
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
	public class CanvasWidget : Windows.UI.Xaml.Controls.UserControl, cave.ui.WidgetWithLayout
	{
		public static cave.ui.CanvasWidget forColor(cave.GuiApplicationContext context, cave.Color color) {
			var v = new cave.ui.CanvasWidget(context);
			v.setWidgetColor(color);
			return(v);
		}

		private Windows.UI.Xaml.Shapes.Rectangle rectangle = null;
		private cave.GuiApplicationContext widgetContext = null;
		private cave.Color widgetColor = null;
		private double widgetTopLeftRadius = 0.00;
		private double widgetTopRightRadius = 0.00;
		private double widgetBottomRightRadius = 0.00;
		private double widgetBottomLeftRadius = 0.00;
		private cave.Color widgetOutlineColor = null;
		private int widgetOutlineWidth = 0;

		public CanvasWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			widgetColor = cave.Color.black();
			widgetOutlineColor = cave.Color.black();
			rectangle = new Windows.UI.Xaml.Shapes.Rectangle();
			this.Content = rectangle;
		}

		public virtual bool layoutWidget(int widthConstraint, bool force) {
			var wc = widthConstraint;
			if(wc < 0) {
				wc = 0;
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, wc, 0);
			return(true);
		}

		public void setWidgetColor(cave.Color color) {
			widgetColor = color;
			rectangle.Fill = color.toBrush();
		}

		public cave.Color getWidgetColor() {
			return(widgetColor);
		}

		public void setWidgetOutlineColor(cave.Color color) {
			widgetOutlineColor = color;
			updateCanvas();
		}

		public cave.Color getWidgetOutlineColor() {
			return(widgetOutlineColor);
		}

		public void setWidgetOutlineWidth(int width) {
			widgetOutlineWidth = width;
			updateCanvas();
		}

		public int getWidgetOutlineWidth() {
			return(widgetOutlineWidth);
		}

		private void updateCanvas() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.CanvasWidget.updateCanvas] (CanvasWidget.sling:178:2): Not implemented.");
		}

		public double getWidgetTopLeftRadius() {
			return(widgetTopLeftRadius);
		}

		public double getWidgetTopRightRadius() {
			return(widgetTopRightRadius);
		}

		public double getWidgetBottomRightRadius() {
			return(widgetBottomRightRadius);
		}

		public double getWidgetBottomLeftRadius() {
			return(widgetBottomLeftRadius);
		}

		public void setWidgetRoundingRadius(double radius) {
			setWidgetRoundingRadius(radius, radius, radius, radius);
		}

		public void setWidgetRoundingRadius(double lradius, double rradius) {
			setWidgetRoundingRadius(lradius, rradius, rradius, lradius);
		}

		public void setWidgetRoundingRadius(double tlradius, double trradius, double brradius, double blradius) {
			widgetTopLeftRadius = tlradius;
			widgetTopRightRadius = trradius;
			widgetBottomRightRadius = brradius;
			widgetBottomLeftRadius = blradius;
			var isRounded = true;
			if((((widgetTopLeftRadius <= 0.00) && (widgetTopRightRadius <= 0.00)) && (widgetBottomRightRadius <= 0.00)) && (widgetBottomLeftRadius <= 0.00)) {
				isRounded = false;
			}
			System.Diagnostics.Debug.WriteLine("[cave.ui.CanvasWidget.setWidgetRoundingRadius] (CanvasWidget.sling:243:2): Not implemented");
		}
	}
}
