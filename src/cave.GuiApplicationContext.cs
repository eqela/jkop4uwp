
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

namespace cave {
	public abstract class GuiApplicationContext : cape.ApplicationContext
	{
		public GuiApplicationContext() {
		}

		private System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<object,object>> styles = null;

		public void setStyle(string id, System.Collections.Generic.Dictionary<object,object> style) {
			if(!(styles != null)) {
				styles = new System.Collections.Generic.Dictionary<string,System.Collections.Generic.Dictionary<object,object>>();
			}
			styles[id] = style;
		}

		public System.Collections.Generic.Dictionary<object,object> getStyle(string id) {
			if(!(styles != null)) {
				return(null);
			}
			if(!(id != null)) {
				return(null);
			}
			return(styles[id]);
		}

		public object getStyleObject(string id, string style) {
			var ss = cape.Map.getValue(styles, id);
			if(!(ss != null)) {
				return(null);
			}
			return(cape.Map.getValue(ss, style));
		}

		public string getStyleString(string id, string style) {
			return(cape.String.asString(getStyleObject(id, style)));
		}

		public cave.Color getStyleColor(string id, string style) {
			return(cave.Color.asColor(getStyleObject(id, style)));
		}

		public int getStylePixels(string id, string style) {
			return(getHeightValue(getStyleString(id, style)));
		}

		public abstract cape.File getApplicationDataDirectory();
		public abstract cave.Image getResourceImage(string id);
		public abstract cave.Image getImageForBuffer(byte[] buffer, string mimeType);
		public abstract void showMessageDialog(string title, string message);
		public abstract void showMessageDialog(string title, string message, System.Action callback);
		public abstract void showConfirmDialog(string title, string message, System.Action okcallback, System.Action cancelcallback);
		public abstract void showErrorDialog(string message);
		public abstract void showErrorDialog(string message, System.Action callback);
		public abstract int getScreenTopMargin();
		public abstract int getScreenWidth();
		public abstract int getScreenHeight();
		public abstract int getScreenDensity();
		public abstract int getHeightValue(string spec);
		public abstract int getWidthValue(string spec);
		public abstract void startTimer(long timeout, System.Action callback);

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
	}
}
