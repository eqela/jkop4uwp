
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
	public class WebWidget
	{
		public WebWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.WebWidget forUrl(cave.GuiApplicationContext context, string url) {
			var v = new cave.ui.WebWidget(context);
			v.setWidgetUrl(url);
			return(v);
		}

		public static cave.ui.WebWidget forHtmlString(cave.GuiApplicationContext context, string html) {
			var v = new cave.ui.WebWidget(context);
			v.setWidgetHtmlString(html);
			return(v);
		}

		private cave.GuiApplicationContext widgetContext = null;
		private string widgetUrl = null;
		private string widgetHtmlString = null;
		private System.Func<string, bool> customUrlHandler = null;

		public WebWidget(cave.GuiApplicationContext context) {
			widgetContext = context;
		}

		public bool overrideWidgetUrlLoading(string url) {
			if(customUrlHandler != null) {
				return(customUrlHandler(url));
			}
			return(false);
		}

		public cave.ui.WebWidget setWidgetHtmlString(string html) {
			this.widgetHtmlString = html;
			this.widgetUrl = null;
			updateWidgetContent();
			return(this);
		}

		public string getWidgetHtmlString() {
			return(widgetHtmlString);
		}

		public cave.ui.WebWidget setWidgetUrl(string url) {
			this.widgetUrl = url;
			this.widgetHtmlString = null;
			updateWidgetContent();
			return(this);
		}

		public string getWidgetUrl() {
			return(widgetUrl);
		}

		private void updateWidgetContent() {
			var url = widgetUrl;
			if(!(object.Equals(url, null))) {
				System.Diagnostics.Debug.WriteLine("[cave.ui.WebWidget.updateWidgetContent] (WebWidget.sling:159:3): Not implemented");
			}
			else {
				var htmlString = widgetHtmlString;
				System.Diagnostics.Debug.WriteLine("[cave.ui.WebWidget.updateWidgetContent] (WebWidget.sling:185:3): Not implemented");
			}
		}

		public System.Func<string, bool> getCustomUrlHandler() {
			return(customUrlHandler);
		}

		public cave.ui.WebWidget setCustomUrlHandler(System.Func<string, bool> v) {
			customUrlHandler = v;
			return(this);
		}
	}
}
