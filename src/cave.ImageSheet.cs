
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
	public class ImageSheet
	{
		public ImageSheet() {
		}

		private cave.Image sheet = null;
		private int cols = -1;
		private int rows = -1;
		private int sourceSkipX = 0;
		private int sourceSkipY = 0;
		private int sourceImageWidth = -1;
		private int sourceImageHeight = -1;
		private int maxImages = -1;

		public System.Collections.Generic.List<cave.Image> toImages(int resizeToWidth = -1, int resizeToHeight = -1) {
			if(!(sheet != null)) {
				return(null);
			}
			var cols = this.cols;
			var rows = this.rows;
			var fwidth = sourceImageWidth;
			if(fwidth < 1) {
				fwidth = (sheet.getPixelWidth() - sourceSkipX) / cols;
			}
			else {
				cols = (sheet.getPixelWidth() - sourceSkipX) / fwidth;
			}
			var fheight = sourceImageHeight;
			if(fheight < 1) {
				fheight = (sheet.getPixelHeight() - sourceSkipY) / rows;
			}
			else {
				rows = (sheet.getPixelHeight() - sourceSkipY) / fheight;
			}
			var frames = new System.Collections.Generic.List<cave.Image>();
			var x = 0;
			var y = 0;
			for(y = 0 ; y < rows ; y++) {
				for(x = 0 ; x < cols ; x++) {
					var img = sheet.crop(x * fwidth, y * fheight, fwidth, fheight);
					if(resizeToWidth > 0) {
						img = img.scaleToSize(resizeToWidth, resizeToHeight);
					}
					frames.Add(img);
					if(maxImages > 0 && cape.Vector.getSize(frames) >= maxImages) {
						return(frames);
					}
				}
			}
			return(frames);
		}

		public cave.Image getSheet() {
			return(sheet);
		}

		public cave.ImageSheet setSheet(cave.Image v) {
			sheet = v;
			return(this);
		}

		public int getCols() {
			return(cols);
		}

		public cave.ImageSheet setCols(int v) {
			cols = v;
			return(this);
		}

		public int getRows() {
			return(rows);
		}

		public cave.ImageSheet setRows(int v) {
			rows = v;
			return(this);
		}

		public int getSourceSkipX() {
			return(sourceSkipX);
		}

		public cave.ImageSheet setSourceSkipX(int v) {
			sourceSkipX = v;
			return(this);
		}

		public int getSourceSkipY() {
			return(sourceSkipY);
		}

		public cave.ImageSheet setSourceSkipY(int v) {
			sourceSkipY = v;
			return(this);
		}

		public int getSourceImageWidth() {
			return(sourceImageWidth);
		}

		public cave.ImageSheet setSourceImageWidth(int v) {
			sourceImageWidth = v;
			return(this);
		}

		public int getSourceImageHeight() {
			return(sourceImageHeight);
		}

		public cave.ImageSheet setSourceImageHeight(int v) {
			sourceImageHeight = v;
			return(this);
		}

		public int getMaxImages() {
			return(maxImages);
		}

		public cave.ImageSheet setMaxImages(int v) {
			maxImages = v;
			return(this);
		}
	}
}
