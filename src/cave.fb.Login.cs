
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

namespace cave.fb
{
	public class Login
	{
		public Login() {
		}

		public static cave.fb.Login forApplications(cave.GuiApplicationContext context, string webAppId) {
			var v = new cave.fb.Login();
			v.setContext(context);
			v.setWebAppId(webAppId);
			if(v.initialize() == false) {
				v = null;
			}
			return(v);
		}

		private cave.GuiApplicationContext context = null;
		private string webAppId = null;

		public bool initialize() {
			return(true);
		}

		public void execute(Windows.UI.Xaml.UIElement widget, System.Action<string, string, cape.Error> callback) {
			var cb = callback;
			System.Diagnostics.Debug.WriteLine("[cave.fb.Login.execute] (Login.sling:219:2): Not implemented");
			if(cb != null) {
				cb(null, null, cape.Error.forCode("not_supported"));
			}
		}

		public cave.GuiApplicationContext getContext() {
			return(context);
		}

		public cave.fb.Login setContext(cave.GuiApplicationContext v) {
			context = v;
			return(this);
		}

		public string getWebAppId() {
			return(webAppId);
		}

		public cave.fb.Login setWebAppId(string v) {
			webAppId = v;
			return(this);
		}
	}
}
