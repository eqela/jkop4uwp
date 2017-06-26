
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

namespace capex.image {
	public class ImageFilter
	{
		public ImageFilter() {
		}

		public const int RESIZE_TYPE_BILINEAR = 0;
		public const int RESIZE_TYPE_BICUBIC = 1;

		public static capex.image.BitmapBuffer resizeImage(capex.image.BitmapBuffer bmpbuf, int nw = -1, int nh = -1, int type = 0) {
			if(bmpbuf == null) {
				return(null);
			}
			if(type == capex.image.ImageFilter.RESIZE_TYPE_BICUBIC) {
				return(capex.image.ImageResizer.resizeBicubic(bmpbuf, nw, nh));
			}
			return(capex.image.ImageResizer.resizeBilinear(bmpbuf, nw, nh));
		}

		public static capex.image.BitmapBuffer filterGreyscale(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			return(capex.image.GreyscaleImage.createGreyscale(bmpbuf));
		}

		public static capex.image.BitmapBuffer filterRedSepia(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			return(capex.image.GreyscaleImage.createRedSepia(bmpbuf));
		}

		public static capex.image.BitmapBuffer filterBlur(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			var array = new double[] {
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				1.00,
				1.00,
				1.00,
				0.00,
				1.00,
				1.00,
				1.00,
				1.00,
				1.00,
				0.00,
				1.00,
				1.00,
				1.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00
			};
			return(capex.image.ImageFilterUtil.createForArrayFilter(bmpbuf, array, 5, 5, 1.00 / 13.00));
		}

		public static capex.image.BitmapBuffer filterSharpen(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			var array = new double[] {
				-1.00,
				-1.00,
				-1.00,
				-1.00,
				9.00,
				-1.00,
				-1.00,
				-1.00,
				-1.00
			};
			return(capex.image.ImageFilterUtil.createForArrayFilter(bmpbuf, array, 3, 3));
		}

		public static capex.image.BitmapBuffer filterEmboss(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			var array = new double[] {
				-2.00,
				-1.00,
				0.00,
				-1.00,
				1.00,
				1.00,
				0.00,
				1.00,
				2.00
			};
			return(capex.image.ImageFilterUtil.createForArrayFilter(bmpbuf, array, 3, 3, 2.00, 0.00));
		}

		public static capex.image.BitmapBuffer filterMotionBlur(capex.image.BitmapBuffer bmpbuf) {
			if(bmpbuf == null) {
				return(null);
			}
			var array = new double[] {
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			};
			return(capex.image.ImageFilterUtil.createForArrayFilter(bmpbuf, array, 9, 9, 1.00 / 9.00));
		}
	}
}
