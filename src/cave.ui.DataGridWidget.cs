
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
	public class DataGridWidget : cave.ui.LayerWidget
	{
		public DataGridWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private class Column
		{
			public Column() {
			}

			public string name = null;
			public double weight = 0.00;
		}

		private cave.ui.CanvasWidget widgetBackground = null;
		private cave.ui.CanvasWidget widgetGridBackground = null;
		private cave.ui.VerticalBoxWidget widgetGrid = null;
		private cave.ui.VerticalBoxWidget widgetDataBox = null;
		private cave.ui.HorizontalBoxWidget widgetHeaderRow = null;
		private System.Collections.Generic.List<cave.ui.DataGridWidget.Column> widgetColumns = null;
		private int widgetGridWidth = 0;
		private cave.Color widgetHeaderBackgroundColor = null;
		private cave.Color widgetHeaderForegroundColor = null;
		private cave.Color widgetDataBackgroundColor = null;
		private cave.Color widgetDataForegroundColor = null;
		private int widgetCellPadding = 0;

		public DataGridWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			addWidget((Windows.UI.Xaml.UIElement)(widgetBackground = new cave.ui.CanvasWidget(ctx)));
			var db = new cave.ui.VerticalBoxWidget(ctx);
			widgetGrid = db;
			db.addWidget((Windows.UI.Xaml.UIElement)(widgetHeaderRow = new cave.ui.HorizontalBoxWidget(ctx)));
			db.addWidget((Windows.UI.Xaml.UIElement)(widgetDataBox = new cave.ui.VerticalBoxWidget(ctx)));
			var dblayer = new cave.ui.LayerWidget(ctx);
			dblayer.addWidget((Windows.UI.Xaml.UIElement)(widgetGridBackground = new cave.ui.CanvasWidget(ctx)));
			dblayer.addWidget((Windows.UI.Xaml.UIElement)db);
			var dblb = new cave.ui.VerticalBoxWidget(ctx);
			dblb.addWidget((Windows.UI.Xaml.UIElement)dblayer);
			addWidget((Windows.UI.Xaml.UIElement)dblb);
			setWidgetHeaderForegroundColor(cave.Color.black());
			setWidgetDataBackgroundColor(cave.Color.white());
			setWidgetDataForegroundColor(cave.Color.black());
			setWidgetBackgroundColor(cave.Color.white());
			setWidgetGridColor(cave.Color.black());
			setWidgetHeaderBackgroundColor(cave.Color.instance("#AAAAAA"));
			setWidgetBackgroundColor(cave.Color.white());
			setWidgetGridWidth(ctx.getHeightValue("500um"));
			setWidgetCellPadding(ctx.getHeightValue("1mm"));
		}

		public void setWidgetBackgroundColor(cave.Color color) {
			widgetBackground.setWidgetColor(color);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackground.getWidgetColor());
		}

		public void setWidgetGridColor(cave.Color color) {
			widgetGridBackground.setWidgetColor(color);
		}

		public cave.Color getWidgetGridColor() {
			return(widgetGridBackground.getWidgetColor());
		}

		public void setWidgetGridWidth(int width) {
			widgetGridWidth = width;
			widgetGrid.setWidgetMargin(width);
			widgetGrid.setWidgetSpacing(width);
			widgetDataBox.setWidgetSpacing(width);
			widgetHeaderRow.setWidgetSpacing(width);
		}

		public void deleteAllColumns() {
			widgetColumns = null;
			deleteWidgetHeaderRow();
			deleteAllRows();
		}

		public void addColumn(string name, double weight = 1.00) {
			if(!(widgetColumns != null)) {
				widgetColumns = new System.Collections.Generic.List<cave.ui.DataGridWidget.Column>();
			}
			var c = new cave.ui.DataGridWidget.Column();
			c.name = name;
			c.weight = weight;
			widgetColumns.Add(c);
		}

		public void addWidgetHeaderRow() {
			widgetHeaderRow.removeAllChildren();
			if(widgetColumns != null) {
				var n = 0;
				var m = widgetColumns.Count;
				for(n = 0 ; n < m ; n++) {
					var column = widgetColumns[n];
					if(column != null) {
						var str = column.name;
						if(!(str != null)) {
							str = "";
						}
						var lbl = cave.ui.LabelWidget.forText(context, str);
						lbl.setWidgetTextColor(widgetHeaderForegroundColor);
						lbl.setWidgetFontBold(true);
						var cc = cave.ui.CanvasWidget.forColor(context, widgetHeaderBackgroundColor);
						var ll = new cave.ui.LayerWidget(context);
						ll.addWidget((Windows.UI.Xaml.UIElement)cc);
						if(widgetCellPadding > 0) {
							ll.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lbl, widgetCellPadding));
						}
						else {
							ll.addWidget((Windows.UI.Xaml.UIElement)lbl);
						}
						widgetHeaderRow.addWidget((Windows.UI.Xaml.UIElement)ll, column.weight);
					}
				}
			}
		}

		public void deleteWidgetHeaderRow() {
			widgetHeaderRow.removeAllChildren();
		}

		public void deleteAllRows() {
			widgetDataBox.removeAllChildren();
		}

		public void addRow(object[] data, System.Action clickHandler = null) {
			var n = 0;
			var c = data.Length;
			var row = new cave.ui.HorizontalBoxWidget(context);
			row.setWidgetSpacing(widgetGridWidth);
			widgetDataBox.addWidget((Windows.UI.Xaml.UIElement)row);
			if(widgetColumns != null) {
				var n2 = 0;
				var m = widgetColumns.Count;
				for(n2 = 0 ; n2 < m ; n2++) {
					var column = widgetColumns[n2];
					if(column != null) {
						if(n >= c) {
							continue;
						}
						var dd = data[n];
						var str = cape.String.asString(dd);
						if(!(str != null)) {
							str = "";
						}
						var lbl = cave.ui.LabelWidget.forText(context, str);
						lbl.setWidgetFontBold(true);
						lbl.setWidgetTextColor(widgetDataForegroundColor);
						var cc = cave.ui.CanvasWidget.forColor(context, widgetDataBackgroundColor);
						var ll = new cave.ui.LayerWidget(context);
						ll.addWidget((Windows.UI.Xaml.UIElement)cc);
						if(widgetCellPadding > 0) {
							ll.addWidget((Windows.UI.Xaml.UIElement)cave.ui.LayerWidget.forWidget(context, (Windows.UI.Xaml.UIElement)lbl, widgetCellPadding));
						}
						else {
							ll.addWidget((Windows.UI.Xaml.UIElement)lbl);
						}
						row.addWidget((Windows.UI.Xaml.UIElement)ll, column.weight);
						n++;
					}
				}
			}
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)row, clickHandler);
		}

		public cave.Color getWidgetHeaderBackgroundColor() {
			return(widgetHeaderBackgroundColor);
		}

		public cave.ui.DataGridWidget setWidgetHeaderBackgroundColor(cave.Color v) {
			widgetHeaderBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetHeaderForegroundColor() {
			return(widgetHeaderForegroundColor);
		}

		public cave.ui.DataGridWidget setWidgetHeaderForegroundColor(cave.Color v) {
			widgetHeaderForegroundColor = v;
			return(this);
		}

		public cave.Color getWidgetDataBackgroundColor() {
			return(widgetDataBackgroundColor);
		}

		public cave.ui.DataGridWidget setWidgetDataBackgroundColor(cave.Color v) {
			widgetDataBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetDataForegroundColor() {
			return(widgetDataForegroundColor);
		}

		public cave.ui.DataGridWidget setWidgetDataForegroundColor(cave.Color v) {
			widgetDataForegroundColor = v;
			return(this);
		}

		public int getWidgetCellPadding() {
			return(widgetCellPadding);
		}

		public cave.ui.DataGridWidget setWidgetCellPadding(int v) {
			widgetCellPadding = v;
			return(this);
		}
	}
}
