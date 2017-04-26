
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

namespace cave
{
	public class GuiApplicationContextForUWP : cave.GuiApplicationContext
	{
		public GuiApplicationContextForUWP() {
		}

		public virtual void logError(string message) {
			System.Diagnostics.Debug.WriteLine("[ERROR] " + message);
		}

		public virtual void logWarning(string message) {
			System.Diagnostics.Debug.WriteLine("[WARNING] " + message);
		}

		public virtual void logInfo(string message) {
			System.Diagnostics.Debug.WriteLine("[INFO] " + message);
		}

		public virtual void logDebug(string message) {
			System.Diagnostics.Debug.WriteLine("[DEBUG] " + message);
		}

		public virtual cape.File getApplicationDataDirectory() {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getApplicationDataDirectory");
			return(null);
		}

		public virtual cave.Image getResourceImage(string id) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getResourceImage");
			return(null);
		}

		public virtual cave.Image getImageForBuffer(byte[] buffer, string mimeType) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: getImageForBuffer");
			return(null);
		}

		public virtual void showMessageDialog(string title, string message) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showMessageDialog");
		}

		public virtual void showMessageDialog(string title, string message, System.Action callback) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showMessageDialog");
		}

		public virtual void showConfirmDialog(string title, string message, System.Action okcallback, System.Action cancelcallback) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showConfirmDialog");
		}

		public virtual void showErrorDialog(string message) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showErrorDialog");
		}

		public virtual void showErrorDialog(string message, System.Action callback) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cave.GuiApplicationContextForUWP :: showErrorDialog");
		}

		public virtual int getScreenTopMargin() {
			return(0);
		}

		public virtual int getScreenWidth() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().ScreenWidthInRawPixels);
		}

		public virtual int getScreenHeight() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().ScreenHeightInRawPixels);
		}

		public virtual int getScreenDensity() {
			return((int)Windows.Graphics.Display.DisplayInformation.GetForCurrentView().LogicalDpi);
		}

		public virtual int getHeightValue(string spec) {
			return(cave.Length.asPoints(spec, getScreenDensity()));
		}

		public virtual int getWidthValue(string spec) {
			return(cave.Length.asPoints(spec, getScreenDensity()));
		}

		public virtual void startTimer(long timeout, System.Action callback) {
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
