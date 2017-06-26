
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

namespace capex.util {
	public class PaperConfiguration
	{
		public PaperConfiguration() {
		}

		public class Size
		{
			public double width = 0.00;
			public double height = 0.00;

			public Size() {
				width = 0.00;
				height = 0.00;
			}

			public Size(double w, double h) {
				width = w;
				height = h;
			}

			public double getHeight() {
				return(height);
			}

			public double getWidth() {
				return(width);
			}
		}

		public static capex.util.PaperConfiguration forDefault() {
			return(capex.util.PaperConfiguration.forA4Portrait());
		}

		public static capex.util.PaperConfiguration forA4Portrait() {
			var v = new capex.util.PaperConfiguration();
			v.setSize(capex.util.PaperSize.forValue(capex.util.PaperSize.A4));
			v.setOrientation(capex.util.PaperOrientation.forValue(capex.util.PaperOrientation.PORTRAIT));
			return(v);
		}

		public static capex.util.PaperConfiguration forA4Landscape() {
			var v = new capex.util.PaperConfiguration();
			v.setSize(capex.util.PaperSize.forValue(capex.util.PaperSize.A4));
			v.setOrientation(capex.util.PaperOrientation.forValue(capex.util.PaperOrientation.LANDSCAPE));
			return(v);
		}

		private capex.util.PaperSize size = null;
		private capex.util.PaperOrientation orientation = null;

		public capex.util.PaperConfiguration.Size getSizeInches() {
			var sz = getRawSizeInches();
			if(capex.util.PaperOrientation.matches(orientation, capex.util.PaperOrientation.LANDSCAPE)) {
				return(new capex.util.PaperConfiguration.Size(sz.getHeight(), sz.getWidth()));
			}
			return(sz);
		}

		public capex.util.PaperConfiguration.Size getRawSizeInches() {
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.LETTER)) {
				return(new capex.util.PaperConfiguration.Size(8.50, (double)11));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.LEGAL)) {
				return(new capex.util.PaperConfiguration.Size(8.50, (double)14));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.A3)) {
				return(new capex.util.PaperConfiguration.Size(11.70, 16.50));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.A4)) {
				return(new capex.util.PaperConfiguration.Size(8.27, 11.70));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.A5)) {
				return(new capex.util.PaperConfiguration.Size(5.80, 8.30));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.B4)) {
				return(new capex.util.PaperConfiguration.Size(9.80, 13.90));
			}
			if(capex.util.PaperSize.matches(size, capex.util.PaperSize.B5)) {
				return(new capex.util.PaperConfiguration.Size(6.90, 9.80));
			}
			return(new capex.util.PaperConfiguration.Size(8.27, 11.70));
		}

		public capex.util.PaperConfiguration.Size getSizeDots(int dpi) {
			var szi = getSizeInches();
			return(new capex.util.PaperConfiguration.Size(szi.getWidth() * dpi, szi.getHeight() * dpi));
		}

		public capex.util.PaperSize getSize() {
			return(size);
		}

		public capex.util.PaperConfiguration setSize(capex.util.PaperSize v) {
			size = v;
			return(this);
		}

		public capex.util.PaperOrientation getOrientation() {
			return(orientation);
		}

		public capex.util.PaperConfiguration setOrientation(capex.util.PaperOrientation v) {
			orientation = v;
			return(this);
		}
	}
}
