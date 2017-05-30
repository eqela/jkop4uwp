
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

namespace capex.image
{
	public class PixelRegionBuffer
	{
		public PixelRegionBuffer() {
		}

		private capex.image.RGBAPixelIntegerBuffer src = null;
		private int rangew = 0;
		private int rangeh = 0;
		private byte[] cache = null;

		public static capex.image.PixelRegionBuffer forRgbaPixels(capex.image.RGBAPixelIntegerBuffer src, int w, int h) {
			var v = new capex.image.PixelRegionBuffer();
			v.src = src;
			v.rangew = w;
			v.rangeh = h;
			return(v);
		}

		public int getStride() {
			return(rangew * 4);
		}

		public byte[] getBufferRegion(int x, int y, bool newbuffer = false) {
			if(cache == null && newbuffer == false) {
				cache = new byte[rangew * rangeh * 4];
			}
			var v = cache;
			if(newbuffer) {
				v = new byte[rangew * rangeh * 4];
			}
			var p = v;
			if(p == null) {
				return(null);
			}
			var i = 0;
			var j = 0;
			for(i = 0 ; i < rangeh ; i++) {
				for(j = 0 ; j < rangew ; j++) {
					var pix = src.getRgbaPixel(x + j, y + i);
					cape.Buffer.setByte(p, (long)((i * rangew + j) * 4 + 0), (byte)pix[0]);
					cape.Buffer.setByte(p, (long)((i * rangew + j) * 4 + 1), (byte)pix[1]);
					cape.Buffer.setByte(p, (long)((i * rangew + j) * 4 + 2), (byte)pix[2]);
					cape.Buffer.setByte(p, (long)((i * rangew + j) * 4 + 3), (byte)pix[3]);
				}
			}
			return(v);
		}
	}
}
