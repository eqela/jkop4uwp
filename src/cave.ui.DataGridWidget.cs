
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
	public class DataGridWidget : cave.ui.LayerWidget
	{
		public DataGridWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private class DataGridRowWidget : cave.ui.LayerWidget
		{
			public DataGridRowWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
			}

			private int widgetGridWidth = 0;
			private int widgetCellPadding = 0;
			private bool widgetIsEditable = false;
			private cave.ui.CanvasWidget widgetBackground = null;
			private cave.ui.LayerWidget widgetMain = null;
			private cave.ui.HorizontalBoxWidget widgetCellContainer = null;

			public DataGridRowWidget(cave.GuiApplicationContext ctx) : base(ctx) {
				addWidget((Windows.UI.Xaml.UIElement)(widgetBackground = cave.ui.CanvasWidget.forColor(context, cave.Color.black())));
				addWidget((Windows.UI.Xaml.UIElement)(widgetMain = new cave.ui.LayerWidget(ctx)));
			}

			public void setWidgetBackgroundColor(cave.Color color) {
				var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)widgetCellContainer);
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var cell = array[n] as cave.ui.DataGridWidget.CellWidget;
						if(cell != null) {
							cell.setWidgetCellBackgroundColor(color);
						}
					}
				}
			}

			public void reload(object[] data, System.Collections.Generic.List<cave.ui.DataGridWidget.Column> widgetColumns, bool isEditable = false) {
				var n = 0;
				var c = data.Length;
				widgetCellContainer = new cave.ui.HorizontalBoxWidget(context);
				widgetCellContainer.setWidgetSpacing(widgetGridWidth);
				widgetMain.addWidget((Windows.UI.Xaml.UIElement)widgetCellContainer);
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
							cave.ui.DataGridWidget.CellWidget cell = null;
							if(isEditable) {
								cell = cave.ui.DataGridWidget.CellWidget.forEditableCell(context, str, widgetCellPadding);
							}
							else {
								cell = cave.ui.DataGridWidget.CellWidget.forStaticCell(context, str, widgetCellPadding);
							}
							cell.setWidgetKey(column.key);
							cell.setWidgetCellTextColor(cave.Color.black());
							widgetCellContainer.addWidget((Windows.UI.Xaml.UIElement)cell, column.weight);
							n++;
						}
					}
				}
			}

			public void collectRowDataTo(cape.DynamicVector dv) {
				if(!(dv != null)) {
					return;
				}
				var dm = new cape.DynamicMap();
				var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)widgetCellContainer);
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var cell = array[n] as cave.ui.DataGridWidget.CellWidget;
						if(cell != null) {
							dm.set(cell.getWidgetKey(), (object)cell.getWidgetText());
						}
					}
				}
				dv.append((object)dm);
			}

			public int getWidgetGridWidth() {
				return(widgetGridWidth);
			}

			public cave.ui.DataGridWidget.DataGridRowWidget setWidgetGridWidth(int v) {
				widgetGridWidth = v;
				return(this);
			}

			public int getWidgetCellPadding() {
				return(widgetCellPadding);
			}

			public cave.ui.DataGridWidget.DataGridRowWidget setWidgetCellPadding(int v) {
				widgetCellPadding = v;
				return(this);
			}

			public bool getWidgetIsEditable() {
				return(widgetIsEditable);
			}

			public cave.ui.DataGridWidget.DataGridRowWidget setWidgetIsEditable(bool v) {
				widgetIsEditable = v;
				return(this);
			}
		}

		private class CellWidget : cave.ui.LayerWidget
		{
			public CellWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
			}

			public CellWidget(cave.GuiApplicationContext context) : base(context) {
				var thisWidget = (dynamic)this;
				cellBackground = new cave.ui.CanvasWidget(context);
				cellBackground.setWidgetColor(cave.Color.white());
				addWidget((Windows.UI.Xaml.UIElement)cellBackground);
				cellTextCon = new cave.ui.LayerWidget(context);
				addWidget((Windows.UI.Xaml.UIElement)cellTextCon);
			}

			public static cave.ui.DataGridWidget.CellWidget forEditableCell(cave.GuiApplicationContext ctx, string text, int padding = 0) {
				var v = new cave.ui.DataGridWidget.CellWidget(ctx);
				v.setWidgetText(text);
				v.setWidgetCellPadding(padding);
				v.setWidgetIsEditable(true);
				return(v);
			}

			public static cave.ui.DataGridWidget.CellWidget forStaticCell(cave.GuiApplicationContext ctx, string text, int padding = 0) {
				var v = new cave.ui.DataGridWidget.CellWidget(ctx);
				v.setWidgetText(text);
				v.setWidgetCellPadding(padding);
				v.setWidgetIsEditable(false);
				return(v);
			}

			private string widgetText = null;
			private bool widgetIsEditable = false;
			private string widgetKey = null;
			private cave.Color widgetTextColor = null;
			private Windows.UI.Xaml.UIElement widget = null;

			public override void initializeWidget() {
				base.initializeWidget();
				if(widgetIsEditable) {
					widget = (Windows.UI.Xaml.UIElement)new cave.ui.TextInputWidget(context);
					((cave.ui.TextInputWidget)widget).setWidgetText(widgetText);
					((cave.ui.TextInputWidget)widget).setWidgetTextColor(widgetTextColor);
					cellTextCon.addWidget(widget);
				}
				else {
					widget = (Windows.UI.Xaml.UIElement)cave.ui.LabelWidget.forText(context, widgetText);
					((cave.ui.LabelWidget)widget).setWidgetTextColor(widgetTextColor);
					cellTextCon.addWidget(widget);
				}
			}

			public void setWidgetCellPadding(int padding) {
				cellTextCon.setWidgetMargin(padding);
			}

			public void setWidgetCellBackgroundColor(cave.Color color) {
				cellBackground.setWidgetColor(color);
			}

			public void setWidgetCellTextColor(cave.Color color) {
				widgetTextColor = color;
				if(!(widget != null)) {
					return;
				}
				if(widget is cave.ui.TextInputWidget) {
					((cave.ui.TextInputWidget)widget).setWidgetTextColor(color);
				}
				else {
					((cave.ui.LabelWidget)widget).setWidgetTextColor(color);
				}
			}

			public string getWidgetText() {
				var v = "";
				if(widget is cave.ui.TextInputWidget) {
					v = ((cave.ui.TextInputWidget)widget).getWidgetText();
				}
				else {
					v = ((cave.ui.LabelWidget)widget).getWidgetText();
				}
				return(v);
			}

			cave.ui.CanvasWidget cellBackground = null;
			cave.ui.LayerWidget cellTextCon = null;

			public cave.ui.DataGridWidget.CellWidget setWidgetText(string v) {
				widgetText = v;
				return(this);
			}

			public bool getWidgetIsEditable() {
				return(widgetIsEditable);
			}

			public cave.ui.DataGridWidget.CellWidget setWidgetIsEditable(bool v) {
				widgetIsEditable = v;
				return(this);
			}

			public string getWidgetKey() {
				return(widgetKey);
			}

			public cave.ui.DataGridWidget.CellWidget setWidgetKey(string v) {
				widgetKey = v;
				return(this);
			}
		}

		private class Column
		{
			public Column() {
			}

			public string name = null;
			public double weight = 0.00;
			public string key = null;
		}

		private cave.ui.CanvasWidget widgetBackground = null;
		private cave.ui.CanvasWidget widgetGridBackground = null;
		private cave.ui.VerticalBoxWidget widgetGrid = null;
		private cave.ui.VerticalBoxWidget widgetDataBox = null;
		private cave.ui.HorizontalBoxWidget widgetHeaderRow = null;
		private System.Collections.Generic.List<cave.ui.DataGridWidget.Column> widgetColumns = null;
		private int widgetGridWidth = 0;
		private cave.ui.DataGridWidget.DataGridRowWidget selectedRow = null;
		private cave.Color widgetHeaderBackgroundColor = null;
		private cave.Color widgetHeaderForegroundColor = null;
		private cave.Color widgetDataBackgroundColor = null;
		private cave.Color widgetDataForegroundColor = null;
		private cave.Color widgetSelectedDataBackgroundColor = null;
		private cave.Color widgetSelectedDataForegroundColor = null;
		private int widgetCellPadding = 0;
		private bool widgetIsEditable = false;

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
			setWidgetSelectedDataForegroundColor(cave.Color.white());
			setWidgetSelectedDataBackgroundColor(cave.Color.instance("#428AFF"));
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

		public void addColumn(string name, string key, double weight = 1.00) {
			if(!(widgetColumns != null)) {
				widgetColumns = new System.Collections.Generic.List<cave.ui.DataGridWidget.Column>();
			}
			var c = new cave.ui.DataGridWidget.Column();
			c.name = name;
			c.key = key;
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

		public void addRow(object[] data, System.Action clickHandler = null, System.Action doubleClickHandler = null) {
			var row = new cave.ui.DataGridWidget.DataGridRowWidget(context);
			row.setWidgetGridWidth(widgetGridWidth);
			row.setWidgetCellPadding(widgetCellPadding);
			row.reload(data, widgetColumns, widgetIsEditable);
			widgetDataBox.addWidget((Windows.UI.Xaml.UIElement)row);
			var r = row;
			var h = clickHandler;
			var d = doubleClickHandler;
			cave.ui.Widget.setWidgetClickHandler((Windows.UI.Xaml.UIElement)row, () => {
				if(selectedRow != null) {
					selectedRow.setWidgetBackgroundColor(cave.Color.white());
				}
				r.setWidgetBackgroundColor(widgetSelectedDataBackgroundColor);
				selectedRow = r;
				if(h != null) {
					h();
				}
			});
			cave.ui.Widget.setWidgetDoubleClickHandler((Windows.UI.Xaml.UIElement)row, () => {
				if(selectedRow != null) {
					selectedRow.setWidgetBackgroundColor(cave.Color.white());
				}
				r.setWidgetBackgroundColor(widgetSelectedDataBackgroundColor);
				selectedRow = r;
				if(d != null) {
					d();
				}
			});
		}

		public cape.DynamicVector getGridData() {
			var dv = new cape.DynamicVector();
			var array = cave.ui.Widget.getChildren((Windows.UI.Xaml.UIElement)widgetDataBox);
			if(array != null) {
				var n = 0;
				var m = array.Count;
				for(n = 0 ; n < m ; n++) {
					var dgr = array[n] as cave.ui.DataGridWidget.DataGridRowWidget;
					if(dgr != null) {
						dgr.collectRowDataTo(dv);
					}
				}
			}
			return(dv);
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

		public cave.Color getWidgetSelectedDataBackgroundColor() {
			return(widgetSelectedDataBackgroundColor);
		}

		public cave.ui.DataGridWidget setWidgetSelectedDataBackgroundColor(cave.Color v) {
			widgetSelectedDataBackgroundColor = v;
			return(this);
		}

		public cave.Color getWidgetSelectedDataForegroundColor() {
			return(widgetSelectedDataForegroundColor);
		}

		public cave.ui.DataGridWidget setWidgetSelectedDataForegroundColor(cave.Color v) {
			widgetSelectedDataForegroundColor = v;
			return(this);
		}

		public int getWidgetCellPadding() {
			return(widgetCellPadding);
		}

		public cave.ui.DataGridWidget setWidgetCellPadding(int v) {
			widgetCellPadding = v;
			return(this);
		}

		public bool getWidgetIsEditable() {
			return(widgetIsEditable);
		}

		public cave.ui.DataGridWidget setWidgetIsEditable(bool v) {
			widgetIsEditable = v;
			return(this);
		}
	}
}
