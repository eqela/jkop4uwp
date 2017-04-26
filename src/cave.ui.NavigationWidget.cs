
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
	public class NavigationWidget : cave.ui.LayerWidget, cave.ui.TitleContainerWidget, cave.KeyListener
	{
		public static bool switchToContainer(Windows.UI.Xaml.UIElement widget, Windows.UI.Xaml.UIElement newWidget) {
			cave.ui.NavigationWidget ng = null;
			var pp = cave.ui.Widget.getParent(widget);
			while(pp != null) {
				if(pp is cave.ui.NavigationWidget) {
					ng = (cave.ui.NavigationWidget)pp;
					break;
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			if(ng == null) {
				return(false);
			}
			return(ng.switchWidget(newWidget));
		}

		public static bool pushToContainer(Windows.UI.Xaml.UIElement widget, Windows.UI.Xaml.UIElement newWidget) {
			cave.ui.NavigationWidget ng = null;
			var pp = cave.ui.Widget.getParent(widget);
			while(pp != null) {
				if(pp is cave.ui.NavigationWidget) {
					ng = (cave.ui.NavigationWidget)pp;
					break;
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			if(ng == null) {
				return(false);
			}
			return(ng.pushWidget(newWidget));
		}

		public static Windows.UI.Xaml.UIElement popFromContainer(Windows.UI.Xaml.UIElement widget) {
			cave.ui.NavigationWidget ng = null;
			var pp = cave.ui.Widget.getParent(widget);
			while(pp != null) {
				if(pp is cave.ui.NavigationWidget) {
					ng = (cave.ui.NavigationWidget)pp;
					break;
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			if(ng == null) {
				return(null);
			}
			return(ng.popWidget());
		}

		public static cave.ui.NavigationWidget findNavigationWidget(Windows.UI.Xaml.UIElement widget) {
			var pp = cave.ui.Widget.getParent(widget);
			while(pp != null) {
				if(pp is cave.ui.NavigationWidget) {
					return((cave.ui.NavigationWidget)pp);
				}
				pp = cave.ui.Widget.getParent(pp);
			}
			return(null);
		}

		private cave.ui.SwitcherLayerWidget contentArea = null;
		protected cave.ui.ActionBarWidget actionBar = null;
		private cape.Stack<Windows.UI.Xaml.UIElement> widgetStack = null;
		private Windows.UI.Xaml.UIElement dimWidget = null;
		private Windows.UI.Xaml.UIElement sidebarWidget = null;
		private cave.ui.LayerWidget sidebarSlotLeft = null;
		private bool sidebarIsFixed = false;
		private bool sidebarIsDisplayed = false;
		private bool enableSidebar = true;
		private bool enableActionBar = true;
		private bool actionBarIsFloating = false;
		private cave.Color actionBarBackgroundColor = null;
		private cave.Color actionBarTextColor = null;
		private int actionBarMenuItemSpacing = 0;
		private cave.Color backgroundColor = null;
		private string backImageResourceName = "backarrow";
		private string burgerMenuImageResourceName = "burger";

		public NavigationWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetStack = new cape.Stack<Windows.UI.Xaml.UIElement>();
			actionBarBackgroundColor = cave.Color.black();
			actionBarTextColor = cave.Color.white();
		}

		public virtual void onKeyEvent(cave.KeyEvent @event) {
			if(@event.isKeyPress(cave.KeyEvent.KEY_BACK)) {
				if((widgetStack != null) && (widgetStack.getSize() > 1)) {
					if(popWidget() != null) {
						@event.consume();
					}
				}
			}
		}

		public virtual System.Collections.Generic.List<Windows.UI.Xaml.UIElement> getActionBarMenuItems() {
			return(null);
		}

		public virtual System.Action getMenuHandler() {
			return(null);
		}

		public virtual string getAppIconResource() {
			return(null);
		}

		public virtual System.Action getAppMenuHandler() {
			return(null);
		}

		public virtual Windows.UI.Xaml.UIElement createMainWidget() {
			return(null);
		}

		public void updateMenuButton() {
			if(actionBar == null) {
				return;
			}
			var handler = getMenuHandler();
			if((widgetStack != null) && (widgetStack.getSize() > 1)) {
				actionBar.configureLeftButton(backImageResourceName, () => {
					popWidget();
				});
			}
			else if(enableSidebar == false) {
				actionBar.configureLeftButton(null, null);
			}
			else if(handler == null) {
				if(sidebarIsFixed == false) {
					actionBar.configureLeftButton(burgerMenuImageResourceName, () => {
						displaySidebarWidget();
					});
				}
				else {
					actionBar.configureLeftButton(null, null);
				}
			}
			else {
				actionBar.configureLeftButton(burgerMenuImageResourceName, handler);
			}
		}

		public virtual Windows.UI.Xaml.UIElement createSidebarWidget() {
			return(null);
		}

		private void enableFixedSidebar() {
			if(((sidebarWidget == null) || (sidebarSlotLeft == null)) || sidebarIsFixed) {
				return;
			}
			hideSidebarWidget(false);
			sidebarIsFixed = true;
			sidebarSlotLeft.addWidget(sidebarWidget);
			updateMenuButton();
		}

		private void disableFixedSidebar() {
			if(sidebarIsDisplayed || (sidebarIsFixed == false)) {
				return;
			}
			cave.ui.Widget.removeFromParent(sidebarWidget);
			sidebarIsFixed = false;
			updateMenuButton();
		}

		private int updateSidebarWidthRequest(int sz) {
			var v = 0;
			if(((sidebarIsFixed == false) && sidebarIsDisplayed) && (sidebarWidget != null)) {
				var layer = cave.ui.Widget.getParent(sidebarWidget) as cave.ui.LayerWidget;
				if(layer != null) {
					v = (int)(0.80 * sz);
					layer.setWidgetMaximumWidthRequest(v);
				}
			}
			return(v);
		}

		public override void computeWidgetLayout(int widthConstraint) {
			if(widthConstraint > context.getWidthValue("200mm")) {
				enableFixedSidebar();
			}
			else {
				disableFixedSidebar();
			}
			base.computeWidgetLayout(widthConstraint);
		}

		public void displaySidebarWidget(bool animated = true) {
			if((sidebarIsFixed || sidebarIsDisplayed) || (sidebarWidget == null)) {
				return;
			}
			if(dimWidget == null) {
				dimWidget = (Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, cave.Color.forRGBADouble(0.00, 0.00, 0.00, 0.80));
			}
			addWidget(dimWidget);
			sidebarIsDisplayed = true;
			var box = new cave.ui.HorizontalBoxWidget(context);
			box.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, sidebarWidget));
			var filler = new cave.ui.LayerWidget(context);
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)filler, () => {
				hideSidebarWidget();
			});
			box.addWidget((Windows.UI.Xaml.UIElement)filler, (double)1);
			var sidebarWidthRequest = updateSidebarWidthRequest(cave.ui.Widget.getWidth((Windows.UI.Xaml.UIElement)this));
			addWidget((Windows.UI.Xaml.UIElement)box);
			if(animated) {
				cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)box, 0.00);
				var sx = -sidebarWidthRequest;
				cave.ui.Widget.move((Windows.UI.Xaml.UIElement)box, sx, cave.ui.Widget.getY((Windows.UI.Xaml.UIElement)box));
				cave.ui.Widget.setAlpha(dimWidget, 0.00);
				var anim = cave.ui.WidgetAnimation.forDuration(context, (long)250);
				anim.addCallback((double completion) => {
					var dx = (int)(sx + ((0 - sx) * completion));
					if(dx > 0) {
						dx = 0;
					}
					cave.ui.Widget.move((Windows.UI.Xaml.UIElement)box, dx, cave.ui.Widget.getY((Windows.UI.Xaml.UIElement)box));
					cave.ui.Widget.setAlpha(dimWidget, completion);
					cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)box, completion);
				});
				anim.start();
			}
		}

		public void hideSidebarWidget(bool animated = true) {
			if(sidebarIsDisplayed == false) {
				return;
			}
			sidebarIsDisplayed = false;
			var box = cave.ui.Widget.getParent(cave.ui.Widget.getParent(sidebarWidget));
			if(animated) {
				var fx = -cave.ui.Widget.getWidth(sidebarWidget);
				var anim = cave.ui.WidgetAnimation.forDuration(context, (long)250);
				anim.addCallback((double completion) => {
					var dx = (int)(fx * completion);
					cave.ui.Widget.move(box, dx, cave.ui.Widget.getY(box));
					cave.ui.Widget.setAlpha(dimWidget, 1.00 - completion);
				});
				anim.setEndListener(() => {
					cave.ui.Widget.removeFromParent(sidebarWidget);
					cave.ui.Widget.removeFromParent(box);
					cave.ui.Widget.removeFromParent(dimWidget);
				});
				anim.start();
			}
			else {
				cave.ui.Widget.removeFromParent(sidebarWidget);
				cave.ui.Widget.removeFromParent(box);
				cave.ui.Widget.removeFromParent(dimWidget);
			}
		}

		public virtual void createBackground() {
			var bgc = getBackgroundColor();
			if(bgc != null) {
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.CanvasWidget.forColor(context, bgc));
			}
		}

		public override void initializeWidget() {
			base.initializeWidget();
			createBackground();
			var mainContainer = cave.ui.VerticalBoxWidget.forContext(context);
			if(enableActionBar) {
				actionBar = new cave.ui.ActionBarWidget(context);
				actionBar.setWidgetBackgroundColor(actionBarBackgroundColor);
				actionBar.setWidgetTextColor(actionBarTextColor);
				actionBar.setWidgetMenuItemSpacing(actionBarMenuItemSpacing);
				var appIcon = getAppIconResource();
				if(cape.String.isEmpty(appIcon) == false) {
					actionBar.configureRightButton(appIcon, getAppMenuHandler());
				}
			}
			if((actionBar != null) && (actionBarIsFloating == false)) {
				mainContainer.addWidget((Windows.UI.Xaml.UIElement)actionBar);
			}
			contentArea = new cave.ui.SwitcherLayerWidget(context);
			if((actionBar != null) && (actionBarIsFloating == true)) {
				var ll = new cave.ui.LayerWidget(context);
				ll.addWidget((Windows.UI.Xaml.UIElement)contentArea);
				ll.addWidget((Windows.UI.Xaml.UIElement)cave.ui.VerticalBoxWidget.forContext(context).addWidget((Windows.UI.Xaml.UIElement)actionBar, 0.00).addWidget((Windows.UI.Xaml.UIElement)new cave.ui.CustomContainerWidget(context), 1.00));
				mainContainer.addWidget((Windows.UI.Xaml.UIElement)ll, 1.00);
			}
			else {
				mainContainer.addWidget((Windows.UI.Xaml.UIElement)contentArea, 1.00);
			}
			var superMainContainer = cave.ui.HorizontalBoxWidget.forContext(context);
			sidebarSlotLeft = new cave.ui.LayerWidget(context);
			superMainContainer.addWidget((Windows.UI.Xaml.UIElement)sidebarSlotLeft);
			superMainContainer.addWidget((Windows.UI.Xaml.UIElement)mainContainer, (double)1);
			addWidget((Windows.UI.Xaml.UIElement)superMainContainer);
			sidebarWidget = createSidebarWidget();
			updateMenuButton();
			var main = createMainWidget();
			if(main != null) {
				pushWidget(main);
			}
		}

		public virtual void updateWidgetTitle(string title) {
			if(actionBar != null) {
				actionBar.setWidgetTitle(title);
			}
		}

		private void onCurrentWidgetChanged() {
			if(contentArea == null) {
				return;
			}
			Windows.UI.Xaml.UIElement widget = null;
			var widgets = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)contentArea);
			if((widgets != null) && (cape.Vector.getSize(widgets) > 0)) {
				widget = cape.Vector.get(widgets, cape.Vector.getSize(widgets) - 1);
			}
			if((widget != null) && (widget is cave.ui.DisplayAwareWidget)) {
				((cave.ui.DisplayAwareWidget)widget).onWidgetDisplayed();
			}
			if((widget != null) && (widget is cave.ui.TitledWidget)) {
				updateWidgetTitle(((cave.ui.TitledWidget)widget).getWidgetTitle());
			}
			else {
				updateWidgetTitle("");
			}
			if((widget != null) && (widget is cave.ui.ActionBarControlWidget)) {
				if(!(actionBar != null)) {
					return;
				}
				actionBar.removeOverlay();
				var customBar = ((cave.ui.ActionBarControlWidget)widget).createActionBarOverlay(actionBar);
				if(customBar != null) {
					actionBar.addOverlay(customBar);
				}
				actionBar.clearMenuItems();
				var menuItems = ((cave.ui.ActionBarControlWidget)widget).getActionBarItems();
				if(menuItems != null) {
					actionBar.configureMenuItems(menuItems);
				}
				((cave.ui.ActionBarControlWidget)widget).setActionBarBackgroundColor(actionBar);
			}
			else {
				if(actionBar != null) {
					actionBar.removeOverlay();
					actionBar.clearMenuItems();
					actionBar.setActionBarBackgroundColor(actionBarBackgroundColor);
				}
				var menuItems1 = getActionBarMenuItems();
				if(menuItems1 != null) {
					actionBar.configureMenuItems(menuItems1);
				}
			}
			updateMenuButton();
		}

		public bool pushWidget(Windows.UI.Xaml.UIElement widget) {
			if((contentArea == null) || (widget == null)) {
				return(false);
			}
			widgetStack.push(widget);
			contentArea.addWidget(widget);
			onCurrentWidgetChanged();
			return(true);
		}

		public bool switchWidget(Windows.UI.Xaml.UIElement widget) {
			if(widget == null) {
				return(false);
			}
			popWidget();
			return(pushWidget(widget));
		}

		public Windows.UI.Xaml.UIElement popWidget() {
			if(contentArea == null) {
				return(null);
			}
			var topmost = widgetStack.pop();
			if(topmost == null) {
				return(null);
			}
			cave.ui.Widget.removeFromParent(topmost);
			onCurrentWidgetChanged();
			return(topmost);
		}

		public bool getEnableSidebar() {
			return(enableSidebar);
		}

		public cave.ui.NavigationWidget setEnableSidebar(bool v) {
			enableSidebar = v;
			return(this);
		}

		public bool getEnableActionBar() {
			return(enableActionBar);
		}

		public cave.ui.NavigationWidget setEnableActionBar(bool v) {
			enableActionBar = v;
			return(this);
		}

		public bool getActionBarIsFloating() {
			return(actionBarIsFloating);
		}

		public cave.ui.NavigationWidget setActionBarIsFloating(bool v) {
			actionBarIsFloating = v;
			return(this);
		}

		public cave.Color getActionBarBackgroundColor() {
			return(actionBarBackgroundColor);
		}

		public cave.ui.NavigationWidget setActionBarBackgroundColor(cave.Color v) {
			actionBarBackgroundColor = v;
			return(this);
		}

		public cave.Color getActionBarTextColor() {
			return(actionBarTextColor);
		}

		public cave.ui.NavigationWidget setActionBarTextColor(cave.Color v) {
			actionBarTextColor = v;
			return(this);
		}

		public int getActionBarMenuItemSpacing() {
			return(actionBarMenuItemSpacing);
		}

		public cave.ui.NavigationWidget setActionBarMenuItemSpacing(int v) {
			actionBarMenuItemSpacing = v;
			return(this);
		}

		public cave.Color getBackgroundColor() {
			return(backgroundColor);
		}

		public cave.ui.NavigationWidget setBackgroundColor(cave.Color v) {
			backgroundColor = v;
			return(this);
		}

		public string getBackImageResourceName() {
			return(backImageResourceName);
		}

		public cave.ui.NavigationWidget setBackImageResourceName(string v) {
			backImageResourceName = v;
			return(this);
		}

		public string getBurgerMenuImageResourceName() {
			return(burgerMenuImageResourceName);
		}

		public cave.ui.NavigationWidget setBurgerMenuImageResourceName(string v) {
			burgerMenuImageResourceName = v;
			return(this);
		}
	}
}
