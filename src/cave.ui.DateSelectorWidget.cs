
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
	public class DateSelectorWidget : cave.ui.LayerWidget, cave.ui.WidgetWithValue
	{
		public static new cave.ui.DateSelectorWidget forContext(cave.GuiApplicationContext context) {
			var v = new cave.ui.DateSelectorWidget(context);
			return(v);
		}

		public DateSelectorWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			widgetContext = ctx;
		}

		private cave.GuiApplicationContext widgetContext = null;
		private cave.ui.SelectWidget dayBox = null;
		private cave.ui.SelectWidget monthBox = null;
		private cave.ui.SelectWidget yearBox = null;
		private string value = null;
		private int skipYears = 0;

		public override void initializeWidget() {
			base.initializeWidget();
			dayBox = cave.ui.SelectWidget.forStringList(context, new object[] {
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7",
				"8",
				"9",
				"10",
				"11",
				"12",
				"13",
				"14",
				"15",
				"16",
				"17",
				"18",
				"19",
				"20",
				"21",
				"22",
				"23",
				"24",
				"25",
				"26",
				"27",
				"28",
				"29",
				"30",
				"31"
			});
			monthBox = cave.ui.SelectWidget.forKeyValueStrings(context, new object[] {
				"1:January",
				"2:February",
				"3:March",
				"4:April",
				"5:May",
				"6:June",
				"7:July",
				"8:August",
				"9:September",
				"10:October",
				"11:November",
				"12:December"
			});
			var cy = cape.DateTime.forNow().getYear();
			if(cy < 1) {
				cy = 2016;
			}
			cy -= skipYears;
			var yearOptions = new cape.KeyValueList<string, string>();
			for(var i = cy ; i >= 1920 ; i--) {
				var str = cape.String.forInteger(i);
				yearOptions.add((string)str, (string)str);
			}
			yearBox = cave.ui.SelectWidget.forKeyValueList(context, yearOptions);
			var box = cave.ui.HorizontalBoxWidget.forContext(context);
			box.setWidgetSpacing(context.getHeightValue("1mm"));
			box.addWidget((Windows.UI.Xaml.UIElement)dayBox, (double)1);
			box.addWidget((Windows.UI.Xaml.UIElement)monthBox, (double)2);
			box.addWidget((Windows.UI.Xaml.UIElement)yearBox, (double)1);
			addWidget((Windows.UI.Xaml.UIElement)box);
			applyValueToWidgets();
		}

		private void applyValueToWidgets() {
			if(((dayBox == null) || (monthBox == null)) || (yearBox == null)) {
				return;
			}
			if(object.Equals(value, null)) {
				return;
			}
			var year = cape.String.getSubString(value, 0, 4);
			var month = cape.String.getSubString(value, 4, 2);
			var day = cape.String.getSubString(value, 6, 2);
			if(cape.String.startsWith(day, "0")) {
				day = cape.String.getSubString(day, 1);
			}
			if(cape.String.startsWith(month, "0")) {
				month = cape.String.getSubString(month, 1);
			}
			yearBox.setWidgetValue((object)year);
			monthBox.setWidgetValue((object)month);
			dayBox.setWidgetValue((object)day);
		}

		private void getValueFromWidgets() {
			if(((dayBox == null) || (monthBox == null)) || (yearBox == null)) {
				return;
			}
			var year = cape.String.asString(yearBox.getWidgetValue());
			var month = cape.String.asString(monthBox.getWidgetValue());
			var day = cape.String.asString(dayBox.getWidgetValue());
			var sb = new cape.StringBuilder();
			sb.append(year);
			if(cape.String.getLength(month) == 1) {
				sb.append('0');
			}
			sb.append(month);
			if(cape.String.getLength(day) == 1) {
				sb.append('0');
			}
			sb.append(day);
			value = sb.toString();
		}

		public virtual void setWidgetValue(object value) {
			this.value = value as string;
			applyValueToWidgets();
		}

		public virtual object getWidgetValue() {
			getValueFromWidgets();
			return((object)value);
		}

		public int getSkipYears() {
			return(skipYears);
		}

		public cave.ui.DateSelectorWidget setSkipYears(int v) {
			skipYears = v;
			return(this);
		}
	}
}
