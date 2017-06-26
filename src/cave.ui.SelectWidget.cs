
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
	public class SelectWidget : Windows.UI.Xaml.Controls.ComboBox, cave.ui.WidgetWithValue
	{
		public SelectWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.SelectWidget forKeyValueList(cave.GuiApplicationContext context, cape.KeyValueList<string, string> options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsKeyValueList(options);
			return(v);
		}

		public static cave.ui.SelectWidget forKeyValueStrings(cave.GuiApplicationContext context, object[] options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsKeyValueStrings(options);
			return(v);
		}

		public static cave.ui.SelectWidget forKeyValueStrings(cave.GuiApplicationContext context, System.Collections.Generic.List<string> options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsKeyValueStrings(options);
			return(v);
		}

		public static cave.ui.SelectWidget forStringList(cave.GuiApplicationContext context, object[] options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsStringList(options);
			return(v);
		}

		public static cave.ui.SelectWidget forStringList(cave.GuiApplicationContext context, System.Collections.Generic.List<string> options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsStringList(options);
			return(v);
		}

		public static cave.ui.SelectWidget forStringList(cave.GuiApplicationContext context, cape.DynamicVector options) {
			var v = new cave.ui.SelectWidget(context);
			v.setWidgetItemsAsStringList(options);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private cape.KeyValueList<string, string> widgetItems = null;
		private System.Action widgetValueChangeHandler = null;
		private cave.Color widgetBackgroundColor = null;
		private cave.Color widgetTextColor = null;
		private int widgetPadding = 0;
		private string widgetFontFamily = null;
		private string widgetFontResource = null;
		private double widgetFontSize = 0.00;

		public SelectWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
			widgetFontFamily = "Arial";
			widgetFontSize = (double)context.getHeightValue("3mm");
			updateWidgetAppearance();
		}

		public cave.ui.SelectWidget setWidgetFontFamily(string family) {
			widgetFontFamily = family;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.SelectWidget setWidgetFontResource(string res) {
			widgetFontResource = res;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		public cave.ui.SelectWidget setWidgetFontSize(double fontSize) {
			widgetFontSize = fontSize;
			updateWidgetFont();
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
			return(this);
		}

		private void updateWidgetFont() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.SelectWidget.updateWidgetFont] (SelectWidget.sling:310:2): Not implemented");
		}

		public cave.ui.SelectWidget setWidgetPadding(int value) {
			widgetPadding = value;
			updateWidgetAppearance();
			return(this);
		}

		public int getWidgetPadding() {
			return(widgetPadding);
		}

		public cave.ui.SelectWidget setWidgetTextColor(cave.Color color) {
			widgetTextColor = color;
			updateWidgetAppearance();
			return(this);
		}

		public cave.Color getWidgetTextColor() {
			return(widgetTextColor);
		}

		public cave.ui.SelectWidget setWidgetBackgroundColor(cave.Color color) {
			widgetBackgroundColor = color;
			updateWidgetAppearance();
			return(this);
		}

		public cave.Color getWidgetBackgroundColor() {
			return(widgetBackgroundColor);
		}

		public cave.Color getActualWidgetTextColor() {
			var textColor = widgetTextColor;
			if(textColor == null) {
				if(widgetBackgroundColor != null) {
					if(widgetBackgroundColor.isLightColor()) {
						textColor = cave.Color.black();
					}
					else {
						textColor = cave.Color.white();
					}
				}
				else {
					textColor = cave.Color.black();
				}
			}
			return(textColor);
		}

		private void updateWidgetAppearance() {
			var textColor = getActualWidgetTextColor();
			System.Diagnostics.Debug.WriteLine("[cave.ui.SelectWidget.updateWidgetAppearance] (SelectWidget.sling:423:2): Not implemented");
			cave.ui.Widget.onChanged((Windows.UI.Xaml.UIElement)this);
		}

		public void setWidgetItemsAsKeyValueList(cape.KeyValueList<string, string> items) {
			var selectedIndex = getSelectedWidgetIndex();
			widgetItems = items;
			System.Diagnostics.Debug.WriteLine("[cave.ui.SelectWidget.setWidgetItemsAsKeyValueList] (SelectWidget.sling:484:2): Not implemented.");
			setSelectedWidgetIndex(selectedIndex);
		}

		public void setWidgetItemsAsKeyValueStrings(System.Collections.Generic.List<string> options) {
			var list = new cape.KeyValueList<string, string>();
			if(options != null) {
				if(options != null) {
					var n = 0;
					var m = options.Count;
					for(n = 0 ; n < m ; n++) {
						var option = options[n];
						if(option != null) {
							var comps = cape.String.split(option, ':', 2);
							var kk = cape.Vector.get(comps, 0);
							var vv = cape.Vector.get(comps, 1);
							if(object.Equals(vv, null)) {
								vv = kk;
							}
							list.add((string)kk, (string)vv);
						}
					}
				}
			}
			setWidgetItemsAsKeyValueList(list);
		}

		public void setWidgetItemsAsKeyValueStrings(object[] options) {
			var list = new cape.KeyValueList<string, string>();
			if(options != null) {
				if(options != null) {
					var n = 0;
					var m = options.Length;
					for(n = 0 ; n < m ; n++) {
						var option = options[n] as string;
						if(option != null) {
							var comps = cape.String.split(option, ':', 2);
							var kk = cape.Vector.get(comps, 0);
							var vv = cape.Vector.get(comps, 1);
							if(object.Equals(vv, null)) {
								vv = kk;
							}
							list.add((string)kk, (string)vv);
						}
					}
				}
			}
			setWidgetItemsAsKeyValueList(list);
		}

		public void setWidgetItemsAsStringList(cape.DynamicVector options) {
			var list = new cape.KeyValueList<string, string>();
			if(options != null) {
				var array = options.toVector();
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var option = array[n] as string;
						if(option != null) {
							list.add((string)option, (string)option);
						}
					}
				}
			}
			setWidgetItemsAsKeyValueList(list);
		}

		public void setWidgetItemsAsStringList(System.Collections.Generic.List<string> options) {
			var list = new cape.KeyValueList<string, string>();
			if(options != null) {
				if(options != null) {
					var n = 0;
					var m = options.Count;
					for(n = 0 ; n < m ; n++) {
						var option = options[n];
						if(option != null) {
							list.add((string)option, (string)option);
						}
					}
				}
			}
			setWidgetItemsAsKeyValueList(list);
		}

		public void setWidgetItemsAsStringList(object[] options) {
			var list = new cape.KeyValueList<string, string>();
			if(options != null) {
				if(options != null) {
					var n = 0;
					var m = options.Length;
					for(n = 0 ; n < m ; n++) {
						var option = options[n] as string;
						if(option != null) {
							list.add((string)option, (string)option);
						}
					}
				}
			}
			setWidgetItemsAsKeyValueList(list);
		}

		public int adjustSelectedWidgetItemIndex(int index) {
			if(widgetItems == null || widgetItems.count() < 1) {
				return(-1);
			}
			if(index < 0) {
				return(0);
			}
			if(index >= widgetItems.count()) {
				return(widgetItems.count() - 1);
			}
			return(index);
		}

		private int findIndexForWidgetValue(string id) {
			var v = -1;
			if(widgetItems != null) {
				var n = 0;
				var it = widgetItems.iterate();
				while(it != null) {
					var item = it.next();
					if(item == null) {
						break;
					}
					if(object.Equals(item.key, id)) {
						v = n;
						break;
					}
					n++;
				}
			}
			return(v);
		}

		private int getWidgetItemCount() {
			if(widgetItems == null) {
				return(0);
			}
			return(widgetItems.count());
		}

		private string getWidgetTextForIndex(int index) {
			if(widgetItems == null) {
				return(null);
			}
			return(widgetItems.getValue(index));
		}

		public int getSelectedWidgetIndex() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.SelectWidget.getSelectedWidgetIndex] (SelectWidget.sling:619:2): Not implemented");
			return(-1);
		}

		public void setSelectedWidgetIndex(int index) {
			var v = adjustSelectedWidgetItemIndex(index);
			System.Diagnostics.Debug.WriteLine("[cave.ui.SelectWidget.setSelectedWidgetIndex] (SelectWidget.sling:651:2): Not implemented");
		}

		public void setSelectedWidgetValue(string id) {
			setSelectedWidgetIndex(findIndexForWidgetValue(id));
		}

		public string getSelectedWidgetValue() {
			var index = getSelectedWidgetIndex();
			if(widgetItems == null || index < 0) {
				return(null);
			}
			return(widgetItems.getKey(index));
		}

		public virtual void setWidgetValue(object value) {
			setSelectedWidgetValue(cape.String.asString(value));
		}

		public virtual object getWidgetValue() {
			return((object)getSelectedWidgetValue());
		}

		public void setWidgetValueChangeHandler(System.Action handler) {
			widgetValueChangeHandler = handler;
		}

		public void onWidgetSelectionChanged() {
			if(widgetValueChangeHandler != null) {
				widgetValueChangeHandler();
			}
		}
	}
}
