
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

namespace cape
{
	public class FileFinder : cape.Iterator<cape.File>
	{
		public static cape.FileFinder forRoot(cape.File root) {
			return(new cape.FileFinder().setRoot(root));
		}

		private class Pattern
		{
			public Pattern() {
			}

			private string pattern = null;
			private string suffix = null;
			private string prefix = null;

			public cape.FileFinder.Pattern setPattern(string pattern) {
				this.pattern = pattern;
				if(pattern != null) {
					if(cape.String.startsWith(pattern, "*")) {
						suffix = cape.String.getSubString(pattern, 1);
					}
					if(cape.String.endsWith(pattern, "*")) {
						prefix = cape.String.getSubString(pattern, 0, cape.String.getLength(pattern) - 1);
					}
				}
				return(this);
			}

			public bool match(string check) {
				if(!(check != null)) {
					return(false);
				}
				if(object.Equals(pattern, check)) {
					return(true);
				}
				if(suffix != null && cape.String.endsWith(check, suffix)) {
					return(true);
				}
				if(prefix != null && cape.String.startsWith(check, prefix)) {
					return(true);
				}
				return(false);
			}
		}

		private cape.File root = null;
		private System.Collections.Generic.List<cape.FileFinder.Pattern> patterns = null;
		private System.Collections.Generic.List<cape.FileFinder.Pattern> excludePatterns = null;
		private cape.Stack<cape.Iterator<cape.File>> stack = null;
		private bool includeMatchingDirectories = false;
		private bool includeDirectories = false;

		public FileFinder() {
			patterns = new System.Collections.Generic.List<cape.FileFinder.Pattern>();
			excludePatterns = new System.Collections.Generic.List<cape.FileFinder.Pattern>();
		}

		public cape.FileFinder setRoot(cape.File root) {
			this.root = root;
			stack = null;
			return(this);
		}

		public cape.FileFinder addPattern(string pattern) {
			patterns.Add(new cape.FileFinder.Pattern().setPattern(pattern));
			return(this);
		}

		public cape.FileFinder addExcludePattern(string pattern) {
			excludePatterns.Add(new cape.FileFinder.Pattern().setPattern(pattern));
			return(this);
		}

		public bool matchPattern(cape.File file) {
			if(!(file != null)) {
				return(false);
			}
			if(cape.Vector.getSize(patterns) < 1) {
				return(true);
			}
			var filename = file.baseName();
			if(patterns != null) {
				var n = 0;
				var m = patterns.Count;
				for(n = 0 ; n < m ; n++) {
					var pattern = patterns[n];
					if(pattern != null) {
						if(pattern.match(filename)) {
							return(true);
						}
					}
				}
			}
			return(false);
		}

		public bool matchExcludePattern(cape.File file) {
			if(!(file != null)) {
				return(false);
			}
			if(cape.Vector.getSize(excludePatterns) < 1) {
				return(false);
			}
			var filename = file.baseName();
			if(excludePatterns != null) {
				var n = 0;
				var m = excludePatterns.Count;
				for(n = 0 ; n < m ; n++) {
					var pattern = excludePatterns[n];
					if(pattern != null) {
						if(pattern.match(filename)) {
							return(true);
						}
					}
				}
			}
			return(false);
		}

		public virtual cape.File next() {
			while(true) {
				if(!(stack != null)) {
					if(!(root != null)) {
						break;
					}
					var es = root.entries();
					root = null;
					if(!(es != null)) {
						break;
					}
					stack = new cape.Stack<cape.Iterator<cape.File>>();
					stack.push((cape.Iterator<cape.File>)es);
				}
				var entries = stack.peek();
				if(!(entries != null)) {
					stack = null;
					break;
				}
				var e = entries.next();
				if(!(e != null)) {
					stack.pop();
				}
				else if(matchExcludePattern(e)) {
					;
				}
				else if(e.isFile()) {
					if(matchPattern(e)) {
						return(e);
					}
				}
				else if(includeMatchingDirectories && e.isDirectory() && matchPattern(e)) {
					return(e);
				}
				else if(e.isDirectory() && e.isLink() == false) {
					var ees = e.entries();
					if(ees != null) {
						stack.push((cape.Iterator<cape.File>)ees);
					}
					if(includeDirectories) {
						return(e);
					}
				}
			}
			return(null);
		}

		public bool getIncludeMatchingDirectories() {
			return(includeMatchingDirectories);
		}

		public cape.FileFinder setIncludeMatchingDirectories(bool v) {
			includeMatchingDirectories = v;
			return(this);
		}

		public bool getIncludeDirectories() {
			return(includeDirectories);
		}

		public cape.FileFinder setIncludeDirectories(bool v) {
			includeDirectories = v;
			return(this);
		}
	}
}
