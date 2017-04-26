
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
	public class RichTextBlockParagraph : capex.text.RichTextParagraph
	{
		public RichTextBlockParagraph() : base() {
		}

		private string id = null;
		private string text = null;

		public override string toMarkup() {
			var sb = new cape.StringBuilder();
			string delim = null;
			if(cape.String.isEmpty(id)) {
				delim = "-- ";
			}
			else {
				delim = ("-- " + id) + " --";
			}
			sb.append(delim);
			sb.append('\n');
			if(!(object.Equals(text, null))) {
				sb.append(text);
				if(cape.String.endsWith(text, "\n") == false) {
					sb.append('\n');
				}
			}
			sb.append(delim);
			return(sb.toString());
		}

		public override string toText() {
			return(text);
		}

		public override cape.DynamicMap toJson() {
			var map = new cape.DynamicMap();
			map.set("type", (object)"block");
			map.set("id", (object)id);
			map.set("text", (object)text);
			return(map);
		}

		public override string toHtml(capex.text.RichTextDocumentReferenceResolver refs) {
			var ids = "";
			if(cape.String.isEmpty(id) == false) {
				ids = " " + capex.text.HTMLString.sanitize(id);
			}
			var content = capex.text.RichTextStyledParagraph.forString(text);
			return(((("<div class=\"block" + ids) + "\">") + content.toHtml(refs)) + "</div>");
		}

		public string getId() {
			return(id);
		}

		public capex.text.RichTextBlockParagraph setId(string v) {
			id = v;
			return(this);
		}

		public string getText() {
			return(text);
		}

		public capex.text.RichTextBlockParagraph setText(string v) {
			text = v;
			return(this);
		}
	}
}
