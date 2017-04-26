
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

namespace cave.ui
{
	public class SignatureWidget : cave.ui.CanvasWidget
	{
		private cave.Color strokeColor = null;
		private float strokeWidth = 0.00f;

		public SignatureWidget(cave.GuiApplicationContext ctx) : base(ctx) {
			setStrokeColor(cave.Color.black());
			setStrokeWidth((float)2.00);
		}

		public void setStrokeColor(cave.Color color) {
			strokeColor = color;
			System.Diagnostics.Debug.WriteLine("[cave.ui.SignatureWidget.setStrokeColor] (SignatureWidget.sling:231:2): Not implemented.");
		}

		public void setStrokeWidth(float width) {
			strokeWidth = width;
			System.Diagnostics.Debug.WriteLine("[cave.ui.SignatureWidget.setStrokeWidth] (SignatureWidget.sling:244:2): Not implemented.");
		}

		public void clear() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.SignatureWidget.clear] (SignatureWidget.sling:264:2): Not implemented.");
		}

		public cave.Image getSignatureAsImage() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.SignatureWidget.getSignatureAsImage] (SignatureWidget.sling:286:2): Not implemented.");
			return(null);
		}
	}
}
