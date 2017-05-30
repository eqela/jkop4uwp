
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

namespace capex.text
{
	public class RichTextImageParagraph : capex.text.RichTextParagraph
	{
		public RichTextImageParagraph() : base() {
		}

		private string filename = null;
		private int width = 100;

		public override string toMarkup() {
			if(width >= 100) {
				return("@image " + filename + "\n");
			}
			if(width >= 75) {
				return("@image75 " + filename + "\n");
			}
			if(width >= 50) {
				return("@image50 " + filename + "\n");
			}
			return("@image25 " + filename + "\n");
		}

		public override string toText() {
			return("[image:" + filename + "]\n");
		}

		public override cape.DynamicMap toJson() {
			var v = new cape.DynamicMap();
			v.set("type", (object)"image");
			v.set("width", width);
			v.set("filename", (object)filename);
			return(v);
		}

		public override string toHtml(capex.text.RichTextDocumentReferenceResolver refs) {
			var sb = new cape.StringBuilder();
			if(width >= 100) {
				sb.append("<div class=\"img100\">");
			}
			else if(width >= 75) {
				sb.append("<div class=\"img75\">");
			}
			else if(width >= 50) {
				sb.append("<div class=\"img50\">");
			}
			else {
				sb.append("<div class=\"img25\">");
			}
			sb.append("<img src=\"" + capex.text.HTMLString.sanitize(filename) + "\" />");
			sb.append("</div>\n");
			return(sb.toString());
		}

		public string getFilename() {
			return(filename);
		}

		public capex.text.RichTextImageParagraph setFilename(string v) {
			filename = v;
			return(this);
		}

		public int getWidth() {
			return(width);
		}

		public capex.text.RichTextImageParagraph setWidth(int v) {
			width = v;
			return(this);
		}
	}
}
