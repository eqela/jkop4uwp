
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
	public class SplashScreenWidget : cave.ui.LayerWidget
	{
		public SplashScreenWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private class Slide
		{
			public Slide() {
			}

			public string resource = null;
			public int delay = 0;
		}

		private cave.Color backgroundColor = null;
		private System.Collections.Generic.List<cave.ui.SplashScreenWidget.Slide> slides = null;
		private System.Action doneHandler = null;
		private int currentSlide = -1;
		private cave.ui.ImageWidget currentImageWidget = null;
		private string imageWidgetWidth = "80mm";
		private string margin = "5mm";

		public SplashScreenWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			slides = new System.Collections.Generic.List<cave.ui.SplashScreenWidget.Slide>();
		}

		public void addSlide(string resource, int delay) {
			var slide = new cave.ui.SplashScreenWidget.Slide();
			slide.resource = resource;
			slide.delay = delay;
			slides.Add(slide);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			if(backgroundColor != null) {
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, backgroundColor));
			}
			nextImage();
		}

		public void nextImage() {
			currentSlide++;
			var slide = cape.Vector.get(slides, currentSlide);
			if(slide == null) {
				var anim = cave.ui.WidgetAnimation.forDuration(context, (long)1000);
				anim.addFadeOut((Windows.UI.Xaml.UIElement)currentImageWidget, true);
				anim.setEndListener(() => {
					onEnded();
				});
				anim.start();
				return;
			}
			var imageWidget = cave.ui.ImageWidget.forImageResource(context, slide.resource);
			cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)imageWidget, (double)0);
			imageWidget.setWidgetImageWidth(context.getWidthValue(imageWidgetWidth));
			var align = cave.ui.AlignWidget.forWidget(context, (Windows.UI.Xaml.UIElement)imageWidget, 0.50, 0.50);
			var sz = context.getWidthValue(margin);
			if(sz > 0) {
				align.setWidgetMargin(sz);
			}
			addWidget((Windows.UI.Xaml.UIElement)align);
			var anim1 = cave.ui.WidgetAnimation.forDuration(context, (long)1000);
			anim1.addCrossFade((Windows.UI.Xaml.UIElement)currentImageWidget, (Windows.UI.Xaml.UIElement)imageWidget, true);
			anim1.start();
			currentImageWidget = imageWidget;
			context.startTimer((long)slide.delay, () => {
				nextImage();
			});
		}

		public void onEnded() {
			if(doneHandler != null) {
				doneHandler();
			}
		}

		public cave.Color getBackgroundColor() {
			return(backgroundColor);
		}

		public cave.ui.SplashScreenWidget setBackgroundColor(cave.Color v) {
			backgroundColor = v;
			return(this);
		}

		public System.Action getDoneHandler() {
			return(doneHandler);
		}

		public cave.ui.SplashScreenWidget setDoneHandler(System.Action v) {
			doneHandler = v;
			return(this);
		}

		public string getImageWidgetWidth() {
			return(imageWidgetWidth);
		}

		public cave.ui.SplashScreenWidget setImageWidgetWidth(string v) {
			imageWidgetWidth = v;
			return(this);
		}

		public string getMargin() {
			return(margin);
		}

		public cave.ui.SplashScreenWidget setMargin(string v) {
			margin = v;
			return(this);
		}
	}
}
