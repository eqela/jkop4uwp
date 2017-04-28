
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
	public class CustomContainerWidget : Windows.UI.Xaml.Controls.Panel, cave.ui.WidgetWithLayout, cave.ui.HeightAwareWidget
	{
		public CustomContainerWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		protected cave.GuiApplicationContext context = null;
		private bool allowResize = true;
		private double lastWidth = -1.00;
		private double lastHeight = -1.00;

		protected override Windows.Foundation.Size ArrangeOverride(Windows.Foundation.Size finalSize)
		{
			foreach (Windows.UI.Xaml.UIElement child in Children) {
				Widget.arrangeWidget(child);
			}
			return(finalSize);
		}
		protected override Windows.Foundation.Size MeasureOverride(Windows.Foundation.Size availableSize)
		{
			var wi = Widget.getMyWidgetInfo(this);
			if(Widget.isRootWidget(this) == false) {
				return(new Windows.Foundation.Size(wi.w, wi.h));
			}
			double wint, hint;
			if(System.Double.IsPositiveInfinity(availableSize.Width)) {
				wint = -1.0;
			}
			else {
				wint = availableSize.Width;
			}
			if(System.Double.IsPositiveInfinity(availableSize.Height)) {
				hint = -1.0;
			}
			else {
				hint = availableSize.Height;
			}
			if(wint != lastWidth || hint != lastHeight) {
				if(wint >= 0) {
					wi.w = (float)wint;
				}
				if(hint >= 0) {
					wi.h = (float)hint;
				}
				Widget.layout(this, (int)System.Math.Ceiling(wint));
				if(hint >= 0) {
					Widget.resizeHeight(this, (int)System.Math.Ceiling(hint));
				}
				onWidgetHeightChanged((int)wi.h);
				lastWidth = wint;
				lastHeight = hint;
			}
			return(new Windows.Foundation.Size(wi.w, wi.h));
		}

		private bool initialized = false;
		private bool widgetChanged = true;
		private int lastWidthConstraint = -2;
		private int lastLayoutWidth = -1;

		public CustomContainerWidget(cave.GuiApplicationContext ctx) {
			context = ctx;
			togglePointerEventHandling(false);
		}

		public void togglePointerEventHandling(bool active) {
			if(active) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.CustomContainerWidget.togglePointerEventHandling] (CustomContainerWidget.sling:178:3): Not implemented");
			}
			else {
				System.Diagnostics.Debug.WriteLine("[cave.ui.CustomContainerWidget.togglePointerEventHandling] (CustomContainerWidget.sling:192:3): Not implemented");
			}
		}

		public void onNativelyResized() {
			if(cave.ui.Widget.isRootWidget((Windows.UI.Xaml.UIElement)this)) {
				cave.ui.Widget.layout((Windows.UI.Xaml.UIElement)this, cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this));
				onWidgetHeightChanged(cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this));
			}
		}

		public bool hasSize() {
			if((cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this) > 0) || (cave.ui.Widget.getHeight((Windows.UI.Xaml.UIElement)this) > 0)) {
				return(true);
			}
			return(false);
		}

		public virtual void tryInitializeWidget() {
			if(initialized) {
				return;
			}
			setInitialized(true);
			initializeWidget();
		}

		public virtual void initializeWidget() {
		}

		public virtual cave.ui.CustomContainerWidget addWidget(Windows.UI.Xaml.UIElement widget) {
			cave.ui.Widget.addChild((Windows.UI.Xaml.UIElement)this, widget);
			return(this);
		}

		public virtual void onChildWidgetAdded(Windows.UI.Xaml.UIElement widget) {
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}

		public virtual void onChildWidgetRemoved(Windows.UI.Xaml.UIElement widget) {
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}

		public virtual void onWidgetAddedToParent() {
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}

		public virtual void onWidgetRemovedFromParent() {
		}

		public virtual void onWidgetHeightChanged(int height) {
		}

		public virtual void computeWidgetLayout(int widthConstraint) {
		}

		public virtual bool layoutWidget(int widthConstraint, bool force) {
			if((force || widgetChanged) || (widthConstraint != lastWidthConstraint)) {
				if(((force == false) && (widgetChanged == false)) && ((widthConstraint >= 0) && (widthConstraint == lastLayoutWidth))) {
					;
				}
				else {
					widgetChanged = false;
					computeWidgetLayout(widthConstraint);
					lastWidthConstraint = widthConstraint;
					lastLayoutWidth = cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this);
				}
			}
			return(true);
		}

		private bool isLayoutScheduled = false;

		public void scheduleLayout() {
			if(isLayoutScheduled) {
				return;
			}
			isLayoutScheduled = true;
			context.startTimer((long)0, () => {
				executeLayout();
			});
		}

		public void executeLayout() {
			isLayoutScheduled = false;
			var ww = cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this);
			if((ww == 0) && allowResize) {
				ww = -1;
			}
			cave.ui.Widget.layout((Windows.UI.Xaml.UIElement)this, ww);
		}

		public bool getAllowResize() {
			return(allowResize);
		}

		public cave.ui.CustomContainerWidget setAllowResize(bool v) {
			allowResize = v;
			return(this);
		}

		public bool getInitialized() {
			return(initialized);
		}

		public cave.ui.CustomContainerWidget setInitialized(bool v) {
			initialized = v;
			return(this);
		}

		public bool getWidgetChanged() {
			return(widgetChanged);
		}

		public cave.ui.CustomContainerWidget setWidgetChanged(bool v) {
			widgetChanged = v;
			return(this);
		}
	}
}
