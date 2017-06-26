
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

using System;

namespace cave {
	public class GuiApplicationContextForUWP : cave.GuiApplicationContext
	{
		public GuiApplicationContextForUWP() : base() {
		}

		private static cave.GuiApplicationContextForUWP instance = null;

		public static cave.GuiApplicationContextForUWP getInstance() {
			if(!(cave.GuiApplicationContextForUWP.instance != null)) {
				cave.GuiApplicationContextForUWP.instance = new cave.GuiApplicationContextForUWP();
			}
			return(cave.GuiApplicationContextForUWP.instance);
		}

		public override cape.File getApplicationDataDirectory() {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getApplicationDataDirectory");
			return(null);
		}

		public override cave.Image getResourceImage(string id) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getResourceImage");
			return(null);
		}

		public override cave.Image getImageForBuffer(byte[] buffer, string mimeType) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getImageForBuffer");
			return(null);
		}

		public override void showMessageDialog(string title, string message) {
			var dlg = new Windows.UI.Xaml.Controls.ContentDialog() {
				Title = title,
				Content = message,
				SecondaryButtonText = "OK"
			};
			dlg.ShowAsync();
		}

		public override async void showMessageDialog(string title, string message, System.Action callback) {
			var dlg = new Windows.UI.Xaml.Controls.ContentDialog() {
				Title = title,
				Content = message,
				SecondaryButtonText = "OK"
			};
			await dlg.ShowAsync();
			if(callback != null) {
				callback();
			}
		}

		public override void showConfirmDialog(string title, string message, System.Action okcallback, System.Action cancelcallback) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showConfirmDialog");
		}

		public override void showErrorDialog(string message) {
			var dlg = new Windows.UI.Xaml.Controls.ContentDialog() {
				Title = "ERROR",
				Content = message,
				SecondaryButtonText = "OK"
			};
			dlg.ShowAsync();
		}

		public override async void showErrorDialog(string message, System.Action callback) {
			var dlg = new Windows.UI.Xaml.Controls.ContentDialog() {
				Title = "ERROR",
				Content = message,
				SecondaryButtonText = "OK"
			};
			await dlg.ShowAsync();
			if(callback != null) {
				callback();
			}
		}

		public override int getScreenTopMargin() {
			return(0);
		}

		public override int getScreenWidth() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().ScreenWidthInRawPixels);
		}

		public override int getScreenHeight() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().ScreenHeightInRawPixels);
		}

		public override int getScreenDensity() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi);
		}

		public override int getHeightValue(string spec) {
			return(cave.Length.asPoints(spec, getScreenDensity()));
		}

		public override int getWidthValue(string spec) {
			return(cave.Length.asPoints(spec, getScreenDensity()));
		}

		public override void startTimer(long timeout, System.Action callback) {
			if(!(callback != null)) {
				return;
			}
			var dt = new Windows.UI.Xaml.DispatcherTimer();
			dt.Interval = new System.TimeSpan(0, 0, 0, 0, (int)timeout);
			dt.Tick += (object sender, object e) => {
				callback();
				dt.Stop();
			};
			dt.Start();
		}
	}
}
