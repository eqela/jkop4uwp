
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
	public class ScreenForWidget : Windows.UI.Xaml.Controls.Page, cave.ScreenWithContext
	{
		public static cave.ui.ScreenForWidget findScreenForWidget(Windows.UI.Xaml.UIElement widget) {
			return(cave.ui.Widget.findScreen(widget) as cave.ui.ScreenForWidget);
		}

		protected cave.GuiApplicationContext context = null;
		private Windows.UI.Xaml.UIElement myWidget = null;

		public ScreenForWidget() {
			initialize();
		}

		protected override void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
		{
			base.OnNavigatedTo(e);
			var rootFrame = Windows.UI.Xaml.Window.Current.Content as Windows.UI.Xaml.Controls.Frame;
			if(rootFrame == null) {
				return;
			}
			var navm = Windows.UI.Core.SystemNavigationManager.GetForCurrentView();
			if(rootFrame.CanGoBack) {
				navm.AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Visible;
				navm.BackRequested += (sender, be) => {
					if(rootFrame.CanGoBack && be.Handled == false) {
						be.Handled = true;
						rootFrame.GoBack();
					}
				};
			}
			else {
				navm.AppViewBackButtonVisibility = Windows.UI.Core.AppViewBackButtonVisibility.Collapsed;
			}
		}

		private cave.KeyEvent keyEvent = null;

		public void onBackKeyPressEvent() {
			if(!(keyEvent != null)) {
				keyEvent = new cave.KeyEvent();
			}
			keyEvent.clear();
			keyEvent.setAction(cave.KeyEvent.ACTION_DOWN);
			keyEvent.setKeyCode(cave.KeyEvent.KEY_BACK);
			deliverKeyEventToWidget(keyEvent, getWidget());
		}

		public void deliverKeyEventToWidget(cave.KeyEvent @event, Windows.UI.Xaml.UIElement widget) {
			if(!(widget != null)) {
				return;
			}
			var array = cave.ui.Widget.getChildren(widget);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var child = array[n];
					if(child != null) {
						deliverKeyEventToWidget(@event, child);
						if(@event.isConsumed) {
							return;
						}
					}
				}
			}
			var kl = widget as cave.KeyListener;
			if(kl != null) {
				kl.onKeyEvent(@event);
				if(@event.isConsumed) {
					return;
				}
			}
		}

		public void unlockOrientation() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.ScreenForWidget.unlockOrientation] (ScreenForWidget.sling:301:2): Not implemented");
		}

		public void lockToLandscapeOrientation() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.ScreenForWidget.lockToLandscapeOrientation] (ScreenForWidget.sling:316:2): Not implemented");
		}

		public void lockToPortraitOrientation() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.ScreenForWidget.lockToPortraitOrientation] (ScreenForWidget.sling:331:2): Not implemented");
		}

		public virtual void setContext(cave.GuiApplicationContext context) {
			this.context = context;
		}

		public virtual cave.GuiApplicationContext getContext() {
			return(context);
		}

		private cave.GuiApplicationContext createContext() {
			cave.GuiApplicationContext v = null;
			v = (cave.GuiApplicationContext)new cave.GuiApplicationContextForUWP();
			return(v);
		}

		public virtual void onPrepareScreen() {
		}

		public virtual void initialize() {
			if(context == null) {
				context = createContext();
			}
		}

		public virtual void cleanup() {
		}

		public Windows.UI.Xaml.UIElement getWidget() {
			return(myWidget);
		}

		public void setWidget(cave.ui.CustomContainerWidget widget) {
			if(!!(myWidget != null)) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.ScreenForWidget.setWidget] (ScreenForWidget.sling:406:2): Multiple calls to setWidget()");
				return;
			}
			if(!(widget != null)) {
				return;
			}
			myWidget = (Windows.UI.Xaml.UIElement)widget;
			widget.setAllowResize(false);
			this.Content = widget;
			widget.tryInitializeWidget();
			cave.ui.Widget.onWidgetAddedToParent((Windows.UI.Xaml.UIElement)widget);
			cave.ui.Widget.notifyOnAddedToScreen((Windows.UI.Xaml.UIElement)widget, this);
		}
	}
}
