
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
	public interface GuiApplicationContext : cape.ApplicationContext
	{
		cave.Image getResourceImage(string id);
		cave.Image getImageForBuffer(byte[] buffer, string mimeType);
		void showMessageDialog(string title, string message);
		void showMessageDialog(string title, string message, System.Action callback);
		void showConfirmDialog(string title, string message, System.Action okcallback, System.Action cancelcallback);
		void showErrorDialog(string message);
		void showErrorDialog(string message, System.Action callback);
		int getScreenTopMargin();
		int getScreenWidth();
		int getScreenHeight();
		int getScreenDensity();
		int getHeightValue(string spec);
		int getWidthValue(string spec);
		void startTimer(long timeout, System.Action callback);
	}
}
