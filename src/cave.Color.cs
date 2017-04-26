
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
	public class Color
	{
		private static cave.Color colorBlack = null;
		private static cave.Color colorWhite = null;

		public static cave.Color black() {
			if(!(cave.Color.colorBlack != null)) {
				cave.Color.colorBlack = cave.Color.instance("black");
			}
			return(cave.Color.colorBlack);
		}

		public static cave.Color white() {
			if(!(cave.Color.colorWhite != null)) {
				cave.Color.colorWhite = cave.Color.instance("white");
			}
			return(cave.Color.colorWhite);
		}

		public static cave.Color asColor(string o) {
			return(new cave.Color(o));
		}

		public static cave.Color asColor(object o) {
			if(!(o != null)) {
				return(null);
			}
			if(o is cave.Color) {
				return((cave.Color)o);
			}
			return(null);
		}

		public static cave.Color instance(string str = null) {
			if(object.Equals(str, "none")) {
				return(null);
			}
			var v = new cave.Color();
			if(!(object.Equals(str, null))) {
				if(v.parse(str) == false) {
					v = null;
				}
			}
			return(v);
		}

		public static cave.Color forString(string str) {
			if(object.Equals(str, "none")) {
				return(null);
			}
			var v = new cave.Color();
			if(!(object.Equals(str, null))) {
				if(v.parse(str) == false) {
					v = null;
				}
			}
			return(v);
		}

		public static cave.Color forRGBDouble(double r, double g, double b) {
			var v = new cave.Color();
			v.setRed(r);
			v.setGreen(g);
			v.setBlue(b);
			v.setAlpha(1.00);
			return(v);
		}

		public static cave.Color forRGBADouble(double r, double g, double b, double a) {
			var v = new cave.Color();
			v.setRed(r);
			v.setGreen(g);
			v.setBlue(b);
			v.setAlpha(a);
			return(v);
		}

		public static cave.Color forRGB(int r, int g, int b) {
			var v = new cave.Color();
			v.setRed((double)(r / 255.00));
			v.setGreen((double)(g / 255.00));
			v.setBlue((double)(b / 255.00));
			v.setAlpha(1.00);
			return(v);
		}

		public static cave.Color forRGBA(int r, int g, int b, int a) {
			var v = new cave.Color();
			v.setRed((double)(r / 255.00));
			v.setGreen((double)(g / 255.00));
			v.setBlue((double)(b / 255.00));
			v.setAlpha((double)(a / 255.00));
			return(v);
		}

		private double red = 0.00;
		private double green = 0.00;
		private double blue = 0.00;
		private double alpha = 0.00;

		public Color() {
		}

		public Color(string str) {
			parse(str);
		}

		public int getRedInt() {
			return((int)(red * 255));
		}

		public int getGreenInt() {
			return((int)(green * 255));
		}

		public int getBlueInt() {
			return((int)(blue * 255));
		}

		public int getAlphaInt() {
			return((int)(alpha * 255));
		}

		public bool isWhite() {
			if(((red + green) + blue) >= 3.00) {
				return(true);
			}
			return(false);
		}

		public bool isBlack() {
			if(((red + green) + blue) <= 0) {
				return(true);
			}
			return(false);
		}

		public bool isLightColor() {
			if(((red + green) + blue) >= 1.50) {
				return(true);
			}
			return(false);
		}

		public bool isDarkColor() {
			return(!isLightColor());
		}

		private int hexCharToInt(char c) {
			if((c >= '0') && (c <= '9')) {
				return(((int)c) - ((int)'0'));
			}
			if((c >= 'a') && (c <= 'f')) {
				return((10 + ((int)c)) - ((int)'a'));
			}
			if((c >= 'A') && (c <= 'F')) {
				return((10 + ((int)c)) - ((int)'A'));
			}
			return(0);
		}

		private int hexDigitToInt(string hx) {
			if(cape.String.isEmpty(hx)) {
				return(0);
			}
			var c0 = hexCharToInt(cape.String.charAt(hx, 0));
			if(cape.String.getLength(hx) < 2) {
				return(c0);
			}
			return((c0 * 16) + hexCharToInt(cape.String.charAt(hx, 1)));
		}

		public bool parse(string s) {
			if(object.Equals(s, null)) {
				red = 0.00;
				green = 0.00;
				blue = 0.00;
				alpha = 1.00;
				return(true);
			}
			var v = true;
			alpha = 1.00;
			if(cape.String.charAt(s, 0) == '#') {
				var slength = cape.String.getLength(s);
				if((slength == 7) || (slength == 9)) {
					red = (double)(hexDigitToInt(cape.String.getSubString(s, 1, 2)) / 255.00);
					green = (double)(hexDigitToInt(cape.String.getSubString(s, 3, 2)) / 255.00);
					blue = (double)(hexDigitToInt(cape.String.getSubString(s, 5, 2)) / 255.00);
					if(slength == 9) {
						alpha = (double)(hexDigitToInt(cape.String.getSubString(s, 7, 2)) / 255.00);
					}
					v = true;
				}
				else {
					red = green = blue = 0.00;
					v = false;
				}
			}
			else if(object.Equals(s, "black")) {
				red = 0.00;
				green = 0.00;
				blue = 0.00;
			}
			else if(object.Equals(s, "white")) {
				red = 1.00;
				green = 1.00;
				blue = 1.00;
			}
			else if(object.Equals(s, "red")) {
				red = 1.00;
				green = 0.00;
				blue = 0.00;
			}
			else if(object.Equals(s, "green")) {
				red = 0.00;
				green = 1.00;
				blue = 0.00;
			}
			else if(object.Equals(s, "blue")) {
				red = 0.00;
				green = 0.00;
				blue = 1.00;
			}
			else if(object.Equals(s, "lightred")) {
				red = 0.60;
				green = 0.40;
				blue = 0.40;
			}
			else if(object.Equals(s, "lightgreen")) {
				red = 0.40;
				green = 0.60;
				blue = 0.40;
			}
			else if(object.Equals(s, "lightblue")) {
				red = 0.40;
				green = 0.40;
				blue = 0.60;
			}
			else if(object.Equals(s, "yellow")) {
				red = 1.00;
				green = 1.00;
				blue = 0.00;
			}
			else if(object.Equals(s, "cyan")) {
				red = 0.00;
				green = 1.00;
				blue = 1.00;
			}
			else if(object.Equals(s, "orange")) {
				red = 1.00;
				green = 0.50;
				blue = 0.00;
			}
			else {
				v = false;
			}
			return(v);
		}

		private int decimalStringToInteger(string str) {
			if(cape.String.isEmpty(str)) {
				return(0);
			}
			var v = 0;
			var m = cape.String.getLength(str);
			var n = 0;
			for(n = 0 ; n < m ; n++) {
				var c = cape.String.charAt(str, n);
				if((c >= '0') && (c <= '9')) {
					v = v * 10;
					v += (int)(c - '0');
				}
				else {
					break;
				}
			}
			return(v);
		}

		public cave.Color dup(string arg = null) {
			var f = 1.00;
			if(!(object.Equals(arg, null))) {
				if(object.Equals(arg, "light")) {
					f = 1.20;
				}
				else if(object.Equals(arg, "dark")) {
					f = 0.80;
				}
				else if(cape.String.endsWith(arg, "%")) {
					f = ((double)decimalStringToInteger(arg)) / 100.00;
				}
			}
			var v = new cave.Color();
			if(f > 1.00) {
				v.setRed(red + ((1.00 - red) * (f - 1.00)));
				v.setGreen(green + ((1.00 - green) * (f - 1.00)));
				v.setBlue(blue + ((1.00 - blue) * (f - 1.00)));
			}
			else if(f < 1.00) {
				v.setRed(red * f);
				v.setGreen(green * f);
				v.setBlue(blue * f);
			}
			else {
				v.setRed(red);
				v.setGreen(green);
				v.setBlue(blue);
			}
			v.setAlpha(alpha);
			return(v);
		}

		public int toRGBAInt32() {
			var v = (int)0;
			v |= ((int)(((int)(red * 255)) & 255)) << 24;
			v |= ((int)(((int)(green * 255)) & 255)) << 16;
			v |= ((int)(((int)(blue * 255)) & 255)) << 8;
			v |= (int)(((int)(alpha * 255)) & 255);
			return(v);
		}

		public int toARGBInt32() {
			var v = (int)0;
			v |= ((int)(((int)(alpha * 255)) & 255)) << 24;
			v |= ((int)(((int)(red * 255)) & 255)) << 16;
			v |= ((int)(((int)(green * 255)) & 255)) << 8;
			v |= (int)(((int)(blue * 255)) & 255);
			return(v);
		}

		public string toString() {
			return(toRgbaString());
		}

		public string toRgbString() {
			var r = cape.String.forIntegerHex((int)(red * 255));
			var g = cape.String.forIntegerHex((int)(green * 255));
			var b = cape.String.forIntegerHex((int)(blue * 255));
			var sb = new cape.StringBuilder();
			sb.append("#");
			to2Digits(r, sb);
			to2Digits(g, sb);
			to2Digits(b, sb);
			return(sb.toString());
		}

		public string toRgbaString() {
			var a = cape.String.forIntegerHex((int)(alpha * 255));
			return(toRgbString() + a);
		}

		public void to2Digits(string val, cape.StringBuilder sb) {
			if(cape.String.getLength(val) == 1) {
				sb.append('0');
			}
			sb.append(val);
		}

		public Windows.UI.Xaml.Media.Brush toBrush() {
			var r = (byte)getRedInt();
			var g = (byte)getGreenInt();
			var b = (byte)getBlueInt();
			var a = (byte)getAlphaInt();
			Windows.UI.Xaml.Media.Brush v = null;
			v = new Windows.UI.Xaml.Media.SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
			return(v);
		}

		public double getRed() {
			return(red);
		}

		public cave.Color setRed(double v) {
			red = v;
			return(this);
		}

		public double getGreen() {
			return(green);
		}

		public cave.Color setGreen(double v) {
			green = v;
			return(this);
		}

		public double getBlue() {
			return(blue);
		}

		public cave.Color setBlue(double v) {
			blue = v;
			return(this);
		}

		public double getAlpha() {
			return(alpha);
		}

		public cave.Color setAlpha(double v) {
			alpha = v;
			return(this);
		}
	}
}
