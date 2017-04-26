
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
	public class FontDescription
	{
		public static cave.FontDescription forDefault() {
			return(new cave.FontDescription());
		}

		public cave.FontDescription forFile(cape.File file, cave.Length size = null) {
			var v = new cave.FontDescription();
			v.setFile(file);
			if(size != null) {
				v.setSize(size);
			}
			return(v);
		}

		public cave.FontDescription forName(string name, cave.Length size = null) {
			var v = new cave.FontDescription();
			v.setName(name);
			if(size != null) {
				v.setSize(size);
			}
			return(v);
		}

		private cape.File file = null;
		private string name = null;
		private bool bold = false;
		private bool italic = false;
		private bool underline = false;
		private cave.Length size = null;

		public FontDescription() {
			file = null;
			name = "Sans";
			size = cave.Length.forMicroMeters((double)2500);
			bold = false;
			italic = false;
			underline = false;
		}

		public cave.FontDescription dup() {
			var v = new cave.FontDescription();
			v.file = file;
			v.name = name;
			v.bold = bold;
			v.italic = italic;
			v.underline = underline;
			v.size = size;
			return(v);
		}

		public cape.File getFile() {
			return(file);
		}

		public cave.FontDescription setFile(cape.File v) {
			file = v;
			return(this);
		}

		public string getName() {
			return(name);
		}

		public cave.FontDescription setName(string v) {
			name = v;
			return(this);
		}

		public bool getBold() {
			return(bold);
		}

		public cave.FontDescription setBold(bool v) {
			bold = v;
			return(this);
		}

		public bool getItalic() {
			return(italic);
		}

		public cave.FontDescription setItalic(bool v) {
			italic = v;
			return(this);
		}

		public bool getUnderline() {
			return(underline);
		}

		public cave.FontDescription setUnderline(bool v) {
			underline = v;
			return(this);
		}

		public cave.Length getSize() {
			return(size);
		}

		public cave.FontDescription setSize(cave.Length v) {
			size = v;
			return(this);
		}
	}
}
