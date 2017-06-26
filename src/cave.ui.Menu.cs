
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
	public class Menu
	{
		public Menu() {
		}

		public class Entry
		{
			public Entry() {
			}

			public string title = null;
			public System.Action handler = null;
		}

		private System.Collections.Generic.List<cave.ui.Menu.Entry> entries = null;

		public void addEntry(cave.ui.Menu.Entry entry) {
			if(!(entry != null)) {
				return;
			}
			if(!(entries != null)) {
				entries = new System.Collections.Generic.List<cave.ui.Menu.Entry>();
			}
			entries.Add(entry);
		}

		public void addEntry(string title, System.Action handler) {
			var e = new cave.ui.Menu.Entry();
			e.title = title;
			e.handler = handler;
			addEntry(e);
		}

		public System.Collections.Generic.List<cave.ui.Menu.Entry> getEntries() {
			return(entries);
		}

		public cave.ui.Menu setEntries(System.Collections.Generic.List<cave.ui.Menu.Entry> v) {
			entries = v;
			return(this);
		}
	}
}
