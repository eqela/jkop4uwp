
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
	public class FileInputWidget
	{
		public static cave.ui.FileInputWidget forType(cave.GuiApplicationContext context, string type) {
			var v = new cave.ui.FileInputWidget(context);
			v.setWidgetType(type);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetType = null;
		private System.Action widgetListener = null;

		public FileInputWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
		}

		public void setWidgetType(string type) {
			widgetType = type;
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileInputWidget.setWidgetType] (FileInputWidget.sling:80:2): Not implemented");
		}

		public string getWidgetType() {
			return(widgetType);
		}

		public void setWidgetListener(string @event, System.Action listener) {
			widgetListener = listener;
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileInputWidget.setWidgetListener] (FileInputWidget.sling:96:2): Not implemented");
		}

		public string getWidgetFileName() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileInputWidget.getWidgetFileName] (FileInputWidget.sling:109:2): Not implemented");
			return(null);
		}

		public string getWidgetFileMimeType() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileInputWidget.getWidgetFileMimeType] (FileInputWidget.sling:116:1): Not implemented");
			return(null);
		}

		public byte[] getWidgetFileContents() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileInputWidget.getWidgetFileContents] (FileInputWidget.sling:122:1): Not implemented");
			return(null);
		}
	}
}
