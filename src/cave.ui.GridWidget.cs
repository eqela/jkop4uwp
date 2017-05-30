
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
	public class GridWidget : cave.ui.CustomContainerWidget
	{
		public GridWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public GridWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public static cave.ui.GridWidget forContext(cave.GuiApplicationContext context) {
			return(new cave.ui.GridWidget(context));
		}

		private int widgetCellSize = -1;
		private int minimumCols = 2;
		private int maximumCols = 0;
		private int widgetSpacing = 0;
		private int widgetMargin = 0;

		public override void computeWidgetLayout(int widthConstraint) {
			var rows = 0;
			var cols = 0;
			var children = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)this);
			if(children == null) {
				return;
			}
			var childCount = cape.Vector.getSize(children);
			if(childCount < 1) {
				return;
			}
			var wcs = widgetCellSize;
			if(wcs < 1) {
				wcs = context.getWidthValue("25mm");
			}
			var adjustWcs = false;
			var mywidth = widthConstraint - widgetMargin - widgetMargin;
			if(widthConstraint < 0) {
				cols = childCount;
			}
			else {
				cols = (int)cape.Math.floor((double)((mywidth + widgetSpacing) / (wcs + widgetSpacing)));
				if(minimumCols > 0 && cols < minimumCols) {
					cols = minimumCols;
					adjustWcs = true;
				}
				else if(childCount >= cols) {
					adjustWcs = true;
				}
			}
			if(adjustWcs) {
				wcs = (mywidth + widgetSpacing) / cols - widgetSpacing;
			}
			if(maximumCols > 0 && cols > maximumCols) {
				cols = maximumCols;
			}
			rows = (int)cape.Math.floor((double)(childCount / cols));
			if(childCount % cols > 0) {
				rows++;
			}
			cave.ui.Widget.setLayoutSize((Windows.UI.Xaml.UIElement)this, widgetMargin + cols * wcs + (cols - 1) * widgetSpacing + widgetMargin, widgetMargin + rows * wcs + (rows - 1) * widgetSpacing + widgetMargin);
			var cx = 0;
			var x = widgetMargin;
			var y = widgetMargin;
			if(children != null) {
				var n = 0;
				var m = children.Count;
				for(n = 0 ; n < m ; n++) {
					var child = children[n];
					if(child != null) {
						cave.ui.Widget.move(child, x, y);
						cave.ui.Widget.layout(child, wcs);
						cave.ui.Widget.resizeHeight(child, wcs);
						x += wcs;
						x += widgetSpacing;
						cx++;
						if(cx >= cols) {
							cx = 0;
							y += wcs;
							y += widgetSpacing;
							x = widgetMargin;
						}
					}
				}
			}
		}

		public int getWidgetCellSize() {
			return(widgetCellSize);
		}

		public cave.ui.GridWidget setWidgetCellSize(int v) {
			widgetCellSize = v;
			return(this);
		}

		public int getMinimumCols() {
			return(minimumCols);
		}

		public cave.ui.GridWidget setMinimumCols(int v) {
			minimumCols = v;
			return(this);
		}

		public int getMaximumCols() {
			return(maximumCols);
		}

		public cave.ui.GridWidget setMaximumCols(int v) {
			maximumCols = v;
			return(this);
		}

		public int getWidgetSpacing() {
			return(widgetSpacing);
		}

		public cave.ui.GridWidget setWidgetSpacing(int v) {
			widgetSpacing = v;
			return(this);
		}

		public int getWidgetMargin() {
			return(widgetMargin);
		}

		public cave.ui.GridWidget setWidgetMargin(int v) {
			widgetMargin = v;
			return(this);
		}
	}
}
