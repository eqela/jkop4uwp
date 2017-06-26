
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
	public class MenuBarWidget : cave.ui.LayerWidget
	{
		public MenuBarWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public MenuBarWidget(cave.GuiApplicationContext context) : base(context) {
			var thisWidget = (dynamic)this;
			var widget = new cave.ui.CanvasWidget(context);
			widget.setWidgetColor(cave.Color.forString("#BBBBBB"));
			addWidget((Windows.UI.Xaml.UIElement)widget);
			box = new cave.ui.HorizontalBoxWidget(context);
			box.setWidgetSpacing(context.getWidthValue("300um"));
			box.setWidgetMargin(context.getWidthValue("300um"));
			addWidget((Windows.UI.Xaml.UIElement)box);
		}

		public cave.ui.Menu addMenu(string title, cave.ui.Menu menu = null) {
			var v = menu;
			if(!(v != null)) {
				v = new cave.ui.Menu();
			}
			var m = v;
			var button = cave.ui.TextButtonWidget.forText(context, title);
			button.setWidgetPadding(context.getHeightValue("1mm"));
			button.setWidgetPaddingHorizontal(context.getWidthValue("3mm"));
			button.setWidgetBackgroundColor(cave.Color.forString("#BBBBBB"));
			button.setWidgetClickHandler(() => {
				cave.ui.PopupMenu.showBelow(context, (Windows.UI.Xaml.UIElement)button, m);
			});
			box.addWidget((Windows.UI.Xaml.UIElement)button);
			return(v);
		}

		cave.ui.HorizontalBoxWidget box = null;
	}
}
