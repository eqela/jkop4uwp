
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
	public class Widget
	{
		public class MyWidgetInfo
		{
			public MyWidgetInfo() {
			}

			public float x = (float)0.00;
			public float y = (float)0.00;
			public float w = (float)0.00;
			public float h = (float)0.00;
		}

		public static readonly Windows.UI.Xaml.DependencyProperty WidgetProperty = Windows.UI.Xaml.DependencyProperty.RegisterAttached(
		"Widget",
		typeof(MyWidgetInfo),
		typeof(Widget),
		new Windows.UI.Xaml.PropertyMetadata(null)
		);

		public static cave.ui.Widget.MyWidgetInfo getMyWidgetInfo(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(null);
			}
			var v = widget.GetValue(WidgetProperty) as cave.ui.Widget.MyWidgetInfo;
			if(!(v != null)) {
				v = new cave.ui.Widget.MyWidgetInfo();
				widget.SetValue(WidgetProperty, v);
			}
			return(v);
		}

		public static void arrangeWidget(Windows.UI.Xaml.UIElement widget) {
			var wi = cave.ui.Widget.getMyWidgetInfo(widget);
			widget.Arrange(new Windows.Foundation.Rect(wi.x, wi.y, wi.w, wi.h));
		}

		public static void onWidgetAddedToParent(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			if(widget is cave.ui.CustomContainerWidget) {
				((cave.ui.CustomContainerWidget)widget).onWidgetAddedToParent();
			}
		}

		public static void onWidgetRemovedFromParent(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			if(widget is cave.ui.CustomContainerWidget) {
				((cave.ui.CustomContainerWidget)widget).onWidgetRemovedFromParent();
			}
		}

		public static void notifyOnAddedToScreen(Windows.UI.Xaml.UIElement widget, cave.ui.ScreenForWidget screen) {
			var array = cave.ui.Widget.getChildren(widget);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.notifyOnAddedToScreen(child, screen);
					}
				}
			}
			if(widget is cave.ui.ScreenAwareWidget) {
				((cave.ui.ScreenAwareWidget)widget).onWidgetAddedToScreen(screen);
			}
		}

		public static void notifyOnRemovedFromScreen(Windows.UI.Xaml.UIElement widget, cave.ui.ScreenForWidget screen) {
			if(widget is cave.ui.ScreenAwareWidget) {
				((cave.ui.ScreenAwareWidget)widget).onWidgetRemovedFromScreen(screen);
			}
			var array = cave.ui.Widget.getChildren(widget);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						cave.ui.Widget.notifyOnRemovedFromScreen(child, screen);
					}
				}
			}
		}

		public static void addChild(Windows.UI.Xaml.UIElement parent, Windows.UI.Xaml.UIElement child) {
			if(!(parent != null)) {
				return;
			}
			if(!(child != null)) {
				return;
			}
			var ccw = child as cave.ui.CustomContainerWidget;
			if(ccw != null) {
				ccw.tryInitializeWidget();
			}
			if(parent is Windows.UI.Xaml.Controls.Panel) {
				var panel = (Windows.UI.Xaml.Controls.Panel)parent;
				panel.Children.Add(child);
			}
			else if(parent is Windows.UI.Xaml.Controls.UserControl) {
				var uc = (Windows.UI.Xaml.Controls.UserControl)parent;
				var ucc = uc.Content as Windows.UI.Xaml.Controls.ContentControl;
				if(ucc != null) {
					ucc.Content = child;
				}
			}
			else {
				System.Diagnostics.Debug.WriteLine("[cave.ui.Widget.addChild] (Widget.sling:179:3): Unsupported parent type when adding a child widget");
			}
			var pp = parent as cave.ui.CustomContainerWidget;
			if(pp != null) {
				pp.onChildWidgetAdded(child);
			}
			cave.ui.Widget.onWidgetAddedToParent(child);
			var screen = cave.ui.ScreenForWidget.findScreenForWidget(child);
			if(screen != null) {
				cave.ui.Widget.notifyOnAddedToScreen(child, screen);
			}
		}

		public static Windows.UI.Xaml.UIElement removeFromParent(Windows.UI.Xaml.UIElement child) {
			if(!(child != null)) {
				return(null);
			}
			var parentWidget = cave.ui.Widget.getParent(child);
			if(!(parentWidget != null)) {
				return(null);
			}
			var pp = parentWidget as cave.ui.CustomContainerWidget;
			System.Diagnostics.Debug.WriteLine("[cave.ui.Widget.removeFromParent] (Widget.sling:215:2): Not implemented.");
			if(pp != null) {
				pp.onChildWidgetRemoved(child);
			}
			var screen = cave.ui.ScreenForWidget.findScreenForWidget(parentWidget);
			if(screen != null) {
				cave.ui.Widget.notifyOnRemovedFromScreen(child, screen);
			}
			cave.ui.Widget.onWidgetRemovedFromParent(child);
			return(null);
		}

		public static bool hasParent(Windows.UI.Xaml.UIElement widget) {
			if(!(cave.ui.Widget.getParent(widget) != null)) {
				return(false);
			}
			return(true);
		}

		public static Windows.UI.Xaml.UIElement getParent(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(null);
			}
			var fwe = widget as Windows.UI.Xaml.FrameworkElement;
			if(!(fwe != null)) {
				return(null);
			}
			Windows.UI.Xaml.UIElement v = null;
			v = fwe.Parent as Windows.UI.Xaml.UIElement;
			return(v);
		}

		public static System.Collections.Generic.List<Windows.UI.Xaml.UIElement> getChildren(Windows.UI.Xaml.UIElement widget) {
			var cc = widget as Windows.UI.Xaml.Controls.Panel;
			if(!(cc != null)) {
				return(null);
			}
			var v = new System.Collections.Generic.List<Windows.UI.Xaml.UIElement>();
			foreach(var child in cc.Children) {
				v.Add(child);
			}
			return(v);
		}

		public static int getX(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(0);
			}
			var v = 0;
			v = (int)cave.ui.Widget.getMyWidgetInfo(widget).x;
			return(v);
		}

		public static int getY(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(0);
			}
			var v = 0;
			v = (int)cave.ui.Widget.getMyWidgetInfo(widget).y;
			return(v);
		}

		public static int getWidth(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(0);
			}
			var v = 0;
			v = (int)cape.Math.ceil((double)cave.ui.Widget.getMyWidgetInfo(widget).w);
			return(v);
		}

		public static int getHeight(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return(0);
			}
			var v = 0;
			v = (int)cape.Math.ceil((double)cave.ui.Widget.getMyWidgetInfo(widget).h);
			return(v);
		}

		public static void move(Windows.UI.Xaml.UIElement widget, int x, int y) {
			var wi = cave.ui.Widget.getMyWidgetInfo(widget);
			wi.x = (float)x;
			wi.y = (float)y;
			widget.UpdateLayout();
		}

		public static bool isRootWidget(Windows.UI.Xaml.UIElement widget) {
			var cw = widget as cave.ui.CustomContainerWidget;
			if(!(cw != null)) {
				return(false);
			}
			var pp = cave.ui.Widget.getParent((Windows.UI.Xaml.UIElement)cw);
			if(!(pp != null)) {
				return(true);
			}
			if(pp is cave.ui.WidgetWithLayout) {
				return(false);
			}
			return(true);
		}

		public static Windows.UI.Xaml.Controls.Page findScreen(Windows.UI.Xaml.UIElement widget) {
			var pp = widget;
			while(pp != null) {
				if(pp is Windows.UI.Xaml.Controls.Page) {
					return((Windows.UI.Xaml.Controls.Page)pp);
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			return(null);
		}

		public static cave.ui.CustomContainerWidget findRootWidget(Windows.UI.Xaml.UIElement widget) {
			var v = widget;
			while(true) {
				if(!(v != null)) {
					break;
				}
				if(cave.ui.Widget.isRootWidget(v)) {
					return(v as cave.ui.CustomContainerWidget);
				}
				v = cave.ui.Widget.getParent(v);
			}
			return(null);
		}

		public static bool setLayoutSize(Windows.UI.Xaml.UIElement widget, int widthValue, int heightValue) {
			if(cave.ui.Widget.isRootWidget(widget)) {
				var ccw = widget as cave.ui.CustomContainerWidget;
				if(ccw != null && ccw.getAllowResize() == false) {
					return(false);
				}
			}
			var width = widthValue;
			if(width < 0) {
				width = 0;
			}
			var height = heightValue;
			if(height < 0) {
				height = 0;
			}
			if(cave.ui.Widget.getWidth(widget) == width && cave.ui.Widget.getHeight(widget) == height) {
				return(false);
			}
			var wi = cave.ui.Widget.getMyWidgetInfo(widget);
			wi.w = (float)width;
			wi.h = (float)height;
			var fwe = widget as Windows.UI.Xaml.FrameworkElement;
			if(fwe != null) {
				fwe.Width = (double)width;
				fwe.Height = (double)height;
			}
			if(widget is cave.ui.ResizeAwareWidget) {
				((cave.ui.ResizeAwareWidget)widget).onWidgetResized();
			}
			return(true);
		}

		public static bool resizeHeight(Windows.UI.Xaml.UIElement widget, int height) {
			if(!cave.ui.Widget.setLayoutSize(widget, cave.ui.Widget.getWidth(widget), height)) {
				return(false);
			}
			if(widget is cave.ui.HeightAwareWidget) {
				((cave.ui.HeightAwareWidget)widget).onWidgetHeightChanged(height);
			}
			return(true);
		}

		public static void layout(Windows.UI.Xaml.UIElement widget, int widthConstraint, bool force = false) {
			if(!(widget != null)) {
				return;
			}
			var done = false;
			if(widget is cave.ui.WidgetWithLayout) {
				done = ((cave.ui.WidgetWithLayout)widget).layoutWidget(widthConstraint, force);
			}
			if(!done) {
				var srw = 0;
				var srh = 0;
				Windows.Foundation.Size available;
				available.Height = float.PositiveInfinity;
				if(widthConstraint >= 0) {
					available.Width = (float)widthConstraint;
				}
				else {
					available.Width = float.PositiveInfinity;
				}
				widget.Measure(available);
				srw = (int)System.Math.Ceiling(widget.DesiredSize.Width);
				srh = (int)System.Math.Ceiling(widget.DesiredSize.Height);
				widget.InvalidateMeasure();
				if(widthConstraint >= 0 && srw < widthConstraint) {
					srw = widthConstraint;
				}
				cave.ui.Widget.setLayoutSize(widget, srw, srh);
			}
		}

		private static Windows.UI.Xaml.Input.PointerEventHandler existingWidgetClickHandler = null;

		public static void setWidgetClickHandler(Windows.UI.Xaml.UIElement widget, System.Action handler) {
			if(widget is cave.ui.CustomContainerWidget) {
				if(!(handler != null)) {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(false);
				}
				else {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(true);
				}
			}
			if(cave.ui.Widget.existingWidgetClickHandler != null) {
				widget.PointerReleased -= existingWidgetClickHandler;
				cave.ui.Widget.existingWidgetClickHandler = null;
			}
			if(handler != null) {
				existingWidgetClickHandler = (object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e) => {
					handler();
				};
				widget.PointerReleased += existingWidgetClickHandler;
			}
		}

		public static void setWidgetLongClickHandler(Windows.UI.Xaml.UIElement widget, System.Action handler) {
			if(widget is cave.ui.CustomContainerWidget) {
				if(!(handler != null)) {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(false);
				}
				else {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(true);
				}
			}
			System.Diagnostics.Debug.WriteLine("[cave.ui.Widget.setWidgetLongClickHandler] (Widget.sling:884:2): Not implemented");
		}

		public static void setWidgetPointerHandlers(Windows.UI.Xaml.UIElement widget, System.Action<double, double> onStartHandler = null, System.Action<double, double> onTouchHandler = null, System.Action<double, double> onEndHandler = null) {
			if(widget is cave.ui.CustomContainerWidget) {
				if(onStartHandler == null && onTouchHandler == null && onEndHandler == null) {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(false);
				}
				else {
					((cave.ui.CustomContainerWidget)widget).togglePointerEventHandling(true);
				}
			}
			System.Diagnostics.Debug.WriteLine("[cave.ui.Widget.setWidgetPointerHandlers] (Widget.sling:968:2): Not implemented");
		}

		public static void removeChildrenOf(Windows.UI.Xaml.UIElement widget) {
			var children = cave.ui.Widget.getChildren(widget);
			if(children != null) {
				var n = 0;
				var m = children.Count;
				for(n = 0 ; n < m ; n++) {
					var child = children[n];
					if(child != null) {
						cave.ui.Widget.removeFromParent(child);
					}
				}
			}
		}

		public static void onChanged(Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			var ccw = widget as cave.ui.CustomContainerWidget;
			if(ccw != null && ccw.getWidgetChanged()) {
				return;
			}
			if(cave.ui.Widget.isRootWidget(widget)) {
				((cave.ui.CustomContainerWidget)widget).scheduleLayout();
			}
			else {
				var pp = cave.ui.Widget.getParent(widget) as Windows.UI.Xaml.UIElement;
				if(pp != null) {
					cave.ui.Widget.onChanged(pp);
				}
				else {
					var root = cave.ui.Widget.findRootWidget(widget);
					if(root != null) {
						root.scheduleLayout();
					}
				}
			}
			if(ccw != null) {
				ccw.setWidgetChanged(true);
			}
		}

		public static void setAlpha(Windows.UI.Xaml.UIElement widget, double alpha) {
			if(!(widget != null)) {
				return;
			}
			System.Diagnostics.Debug.WriteLine("[cave.ui.Widget.setAlpha] (Widget.sling:1020:2): Not implemented");
		}
	}
}
