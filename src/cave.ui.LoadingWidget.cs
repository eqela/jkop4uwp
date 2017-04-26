
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
	public class LoadingWidget : cave.ui.LayerWidget
	{
		public LoadingWidget(cave.GuiApplicationContext ctx) : base(ctx) {
		}

		private static string displayText = null;
		private static cave.Image displayImage = null;

		public static void initializeWithText(string text) {
			cave.ui.LoadingWidget.displayText = text;
			cave.ui.LoadingWidget.displayImage = null;
		}

		public static void initializeWithImage(cave.Image image) {
			cave.ui.LoadingWidget.displayText = null;
			cave.ui.LoadingWidget.displayImage = image;
		}

		public static cave.ui.LoadingWidget openPopup(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget) {
			var v = new cave.ui.LoadingWidget(context);
			if(v.showPopup(widget) == false) {
				v = null;
			}
			return(v);
		}

		public static cave.ui.LoadingWidget closePopup(cave.ui.LoadingWidget widget) {
			if(widget != null) {
				widget.stop();
				cave.ui.Widget.removeFromParent((Windows.UI.Xaml.UIElement)widget);
			}
			return(null);
		}

		private Windows.UI.Xaml.UIElement loading = null;
		private cave.ui.WidgetAnimation animation = null;

		public override void initializeWidget() {
			base.initializeWidget();
			var background = cave.ui.CanvasWidget.forColor(context, cave.Color.forRGBADouble((double)0, (double)0, (double)0, 0.80));
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)background, () => {
				;
			});
			addWidget((Windows.UI.Xaml.UIElement)background);
			if(cave.ui.LoadingWidget.displayImage != null) {
				var img = cave.ui.ImageWidget.forImage(context, cave.ui.LoadingWidget.displayImage);
				img.setWidgetImageHeight(context.getHeightValue("20mm"));
				loading = (Windows.UI.Xaml.UIElement)img;
			}
			else {
				var text = cave.ui.LoadingWidget.displayText;
				if(cape.String.isEmpty(text)) {
					text = "Loading ..";
				}
				var lt = cave.ui.LabelWidget.forText(context, text);
				lt.setWidgetTextColor(cave.Color.white());
				lt.setWidgetFontSize((double)context.getHeightValue("3mm"));
				loading = (Windows.UI.Xaml.UIElement)lt;
			}
			addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(context, loading, 0.50, 0.50));
			start();
		}

		public void start() {
			animation = cave.ui.WidgetAnimation.forDuration(context, (long)1000);
			animation.addFadeOutIn(loading);
			animation.setShouldLoop(true);
			animation.start();
		}

		public void stop() {
			if(animation != null) {
				animation.setShouldStop(true);
				animation = null;
			}
		}

		public bool showPopup(Windows.UI.Xaml.UIElement target) {
			var topmost = cave.ui.LayerWidget.findTopMostLayerWidget(target);
			if(topmost == null) {
				return(false);
			}
			topmost.addWidget((Windows.UI.Xaml.UIElement)this);
			return(true);
		}
	}
}
