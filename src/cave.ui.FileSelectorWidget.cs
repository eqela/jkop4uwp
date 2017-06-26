
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
	public class FileSelectorWidget
	{
		public FileSelectorWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		private cave.GuiApplicationContext context = null;

		public FileSelectorWidget(cave.GuiApplicationContext context) {
			this.context = context;
		}

		public void openFileDialog(Windows.UI.Xaml.UIElement widget, string type, System.Action<byte[], string, string, cape.Error> callback) {
			var cb = callback;
			System.Diagnostics.Debug.WriteLine("[cave.ui.FileSelectorWidget.openFileDialog] (FileSelectorWidget.sling:172:2): Not implemented");
			if(cb != null) {
				cb(null, null, null, cape.Error.forCode("notSupported"));
			}
		}
	}
}
