
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
	public class PopupWidget : cave.ui.LayerWidget
	{
		public PopupWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.PopupWidget forContentWidget(cave.GuiApplicationContext context, Windows.UI.Xaml.UIElement widget) {
			var v = new cave.ui.PopupWidget(context);
			v.setWidgetContent(widget);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private cave.ui.CanvasWidget widgetContainerBackgroundColor = null;
		private Windows.UI.Xaml.UIElement widgetContent = null;
		private int animationDestY = 0;
		private System.Action popupAnimationEndHandler = null;
		private bool widgetModal = true;

		public PopupWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetContext = ctx;
		}

		public void setWidgetContainerBackgroundColor(cave.Color color) {
			if(color != null) {
				widgetContainerBackgroundColor = cave.ui.CanvasWidget.forColor(widgetContext, color);
			}
		}

		public void setWidgetContent(Windows.UI.Xaml.UIElement widget) {
			if(widget != null) {
				widgetContent = widget;
			}
		}

		public cave.ui.CanvasWidget getWidgetContainerBackgroundColor() {
			return(widgetContainerBackgroundColor);
		}

		public Windows.UI.Xaml.UIElement getWidgetContent() {
			return(widgetContent);
		}

		public override void initializeWidget() {
			base.initializeWidget();
			if(widgetContainerBackgroundColor == null) {
				widgetContainerBackgroundColor = cave.ui.CanvasWidget.forColor(widgetContext, cave.Color.forRGBADouble((double)0, (double)0, (double)0, 0.80));
				cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)widgetContainerBackgroundColor, () => {
					if(!widgetModal) {
						hidePopup();
					}
				});
			}
			addWidget((Windows.UI.Xaml.UIElement)widgetContainerBackgroundColor);
			if(widgetContent != null) {
				addWidget((Windows.UI.Xaml.UIElement)cave.ui.AlignWidget.forWidget(widgetContext, widgetContent, 0.50, 0.50));
			}
		}

		public override void onWidgetHeightChanged(int height) {
			base.onWidgetHeightChanged(height);
			animationDestY = cave.ui.Widget.getY(widgetContent);
		}

		public override void computeWidgetLayout(int widthConstraint) {
			base.computeWidgetLayout(widthConstraint);
			animationDestY = cave.ui.Widget.getY(widgetContent);
		}

		public virtual void showPopup(Windows.UI.Xaml.UIElement widget) {
			cave.ui.LayerWidget parentLayer = null;
			var parent = widget;
			while(parent != null) {
				if(parent is cave.ui.LayerWidget) {
					parentLayer = (cave.ui.LayerWidget)parent;
				}
				parent = cave.ui.Widget.getParent(parent);
			}
			if(parentLayer == null) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.PopupWidget.showPopup] (PopupWidget.sling:126:3): No LayerWidget was found in the widget tree. Cannot show popup!");
				return;
			}
			parentLayer.addWidget((Windows.UI.Xaml.UIElement)this);
			cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)widgetContainerBackgroundColor, (double)0);
			cave.ui.Widget.setAlpha(widgetContent, (double)0);
			animationDestY = cave.ui.Widget.getY(widgetContent);
			var ay = context.getHeightValue("3mm");
			cave.ui.Widget.move(widgetContent, cave.ui.Widget.getX(widgetContent), (int)(animationDestY + ay));
			var anim = cave.ui.WidgetAnimation.forDuration(context, (long)300);
			anim.addCallback((double completion) => {
				var bgf = completion * 1.50;
				if(bgf > 1.00) {
					bgf = 1.00;
				}
				cave.ui.Widget.setAlpha((Windows.UI.Xaml.UIElement)widgetContainerBackgroundColor, bgf);
				cave.ui.Widget.setAlpha(widgetContent, completion);
				cave.ui.Widget.move(widgetContent, cave.ui.Widget.getX(widgetContent), (int)(animationDestY + (1.00 - completion) * ay));
			});
			anim.setEndListener(() => {
				if(popupAnimationEndHandler != null) {
					popupAnimationEndHandler();
				}
			});
			anim.start();
		}

		public virtual void hidePopup() {
			var anim = cave.ui.WidgetAnimation.forDuration(context, (long)300);
			anim.addFadeOut((Windows.UI.Xaml.UIElement)this, true);
			anim.start();
		}

		public System.Action getPopupAnimationEndHandler() {
			return(popupAnimationEndHandler);
		}

		public cave.ui.PopupWidget setPopupAnimationEndHandler(System.Action v) {
			popupAnimationEndHandler = v;
			return(this);
		}

		public bool getWidgetModal() {
			return(widgetModal);
		}

		public cave.ui.PopupWidget setWidgetModal(bool v) {
			widgetModal = v;
			return(this);
		}
	}
}
