
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
	public class RichTextContentParagraph : capex.text.RichTextParagraph
	{
		public RichTextContentParagraph() : base() {
		}

		private string contentId = null;

		public override string toMarkup() {
			return("@content " + contentId + "\n");
		}

		public override string toText() {
			return("[content:" + contentId + "]\n");
		}

		public override cape.DynamicMap toJson() {
			var map = new cape.DynamicMap();
			map.set("type", (object)"content");
			map.set("id", (object)contentId);
			return(map);
		}

		public override string toHtml(capex.text.RichTextDocumentReferenceResolver refs) {
			string cc = null;
			if(refs != null && !(object.Equals(contentId, null))) {
				cc = refs.getContentString(contentId);
			}
			if(object.Equals(cc, null)) {
				cc = "";
			}
			return(cc);
		}

		public string getContentId() {
			return(contentId);
		}

		public capex.text.RichTextContentParagraph setContentId(string v) {
			contentId = v;
			return(this);
		}
	}
}
