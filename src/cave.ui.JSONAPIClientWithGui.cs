
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
	public class JSONAPIClientWithGui : capex.web.JSONAPIClient
	{
		public JSONAPIClientWithGui() : base() {
		}

		private cave.GuiApplicationContext context = null;
		private Windows.UI.Xaml.UIElement parentWidget = null;
		private cave.ui.LoadingWidget loadingWidget = null;
		private int requestCounter = 0;

		public override void onStartSendRequest() {
			base.onStartSendRequest();
			if(loadingWidget == null) {
				loadingWidget = cave.ui.LoadingWidget.openPopup(context, parentWidget);
			}
			requestCounter++;
		}

		public override void onEndSendRequest() {
			base.onEndSendRequest();
			requestCounter--;
			if(requestCounter < 1) {
				loadingWidget = cave.ui.LoadingWidget.closePopup(loadingWidget);
			}
		}

		public override void onDefaultErrorHandler(cape.Error error) {
			if((error == null) || (context == null)) {
				return;
			}
			context.showErrorDialog(error.toString());
		}

		public cave.GuiApplicationContext getContext() {
			return(context);
		}

		public cave.ui.JSONAPIClientWithGui setContext(cave.GuiApplicationContext v) {
			context = v;
			return(this);
		}

		public Windows.UI.Xaml.UIElement getParentWidget() {
			return(parentWidget);
		}

		public cave.ui.JSONAPIClientWithGui setParentWidget(Windows.UI.Xaml.UIElement v) {
			parentWidget = v;
			return(this);
		}
	}
}
