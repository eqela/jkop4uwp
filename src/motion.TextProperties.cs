
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

namespace motion {
	public class TextProperties
	{
		public static motion.TextProperties forText(string tt) {
			var v = new motion.TextProperties();
			v.setText(tt);
			return(v);
		}

		public TextProperties() {
			textColor = cave.Color.black();
		}

		private string text = null;
		private cave.Color textColor = null;
		private cave.Color outlineColor = null;
		private cave.Color backgroundColor = null;
		private string fontFamily = null;
		private string fontResource = null;
		private cape.File fontFile = null;
		private bool fontIsItalic = false;
		private bool fontIsBold = false;
		private double fontSizeRelative = 0.00;
		private double fontSizeAbsolute = 1.00;
		private string fontSizeDescription = null;

		public string getText() {
			return(text);
		}

		public motion.TextProperties setText(string v) {
			text = v;
			return(this);
		}

		public cave.Color getTextColor() {
			return(textColor);
		}

		public motion.TextProperties setTextColor(cave.Color v) {
			textColor = v;
			return(this);
		}

		public cave.Color getOutlineColor() {
			return(outlineColor);
		}

		public motion.TextProperties setOutlineColor(cave.Color v) {
			outlineColor = v;
			return(this);
		}

		public cave.Color getBackgroundColor() {
			return(backgroundColor);
		}

		public motion.TextProperties setBackgroundColor(cave.Color v) {
			backgroundColor = v;
			return(this);
		}

		public string getFontFamily() {
			return(fontFamily);
		}

		public motion.TextProperties setFontFamily(string v) {
			fontFamily = v;
			return(this);
		}

		public string getFontResource() {
			return(fontResource);
		}

		public motion.TextProperties setFontResource(string v) {
			fontResource = v;
			return(this);
		}

		public cape.File getFontFile() {
			return(fontFile);
		}

		public motion.TextProperties setFontFile(cape.File v) {
			fontFile = v;
			return(this);
		}

		public bool getFontIsItalic() {
			return(fontIsItalic);
		}

		public motion.TextProperties setFontIsItalic(bool v) {
			fontIsItalic = v;
			return(this);
		}

		public bool getFontIsBold() {
			return(fontIsBold);
		}

		public motion.TextProperties setFontIsBold(bool v) {
			fontIsBold = v;
			return(this);
		}

		public double getFontSizeRelative() {
			return(fontSizeRelative);
		}

		public motion.TextProperties setFontSizeRelative(double v) {
			fontSizeRelative = v;
			return(this);
		}

		public double getFontSizeAbsolute() {
			return(fontSizeAbsolute);
		}

		public motion.TextProperties setFontSizeAbsolute(double v) {
			fontSizeAbsolute = v;
			return(this);
		}

		public string getFontSizeDescription() {
			return(fontSizeDescription);
		}

		public motion.TextProperties setFontSizeDescription(string v) {
			fontSizeDescription = v;
			return(this);
		}
	}
}
