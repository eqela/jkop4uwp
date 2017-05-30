
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
	public class KeyEvent
	{
		public KeyEvent() {
		}

		public const int ACTION_NONE = 0;
		public const int ACTION_DOWN = 1;
		public const int ACTION_UP = 2;
		public const int KEY_NONE = 0;
		public const int KEY_SPACE = 1;
		public const int KEY_ENTER = 2;
		public const int KEY_TAB = 3;
		public const int KEY_ESCAPE = 4;
		public const int KEY_BACKSPACE = 5;
		public const int KEY_SHIFT = 6;
		public const int KEY_CONTROL = 7;
		public const int KEY_ALT = 8;
		public const int KEY_CAPSLOCK = 9;
		public const int KEY_NUMLOCK = 10;
		public const int KEY_LEFT = 11;
		public const int KEY_UP = 12;
		public const int KEY_RIGHT = 13;
		public const int KEY_DOWN = 14;
		public const int KEY_INSERT = 15;
		public const int KEY_DELETE = 16;
		public const int KEY_HOME = 17;
		public const int KEY_END = 18;
		public const int KEY_PAGEUP = 19;
		public const int KEY_PAGEDOWN = 20;
		public const int KEY_F1 = 21;
		public const int KEY_F2 = 22;
		public const int KEY_F3 = 23;
		public const int KEY_F4 = 24;
		public const int KEY_F5 = 25;
		public const int KEY_F6 = 26;
		public const int KEY_F7 = 27;
		public const int KEY_F8 = 28;
		public const int KEY_F9 = 29;
		public const int KEY_F10 = 30;
		public const int KEY_F11 = 31;
		public const int KEY_F12 = 32;
		public const int KEY_SUPER = 33;
		public const int KEY_BACK = 34;
		private int action = 0;
		private int keyCode = 0;
		private string stringValue = null;
		private bool shift = false;
		private bool control = false;
		private bool command = false;
		private bool alt = false;
		public bool isConsumed = false;

		public void consume() {
			isConsumed = true;
		}

		public bool isKeyPress(int key) {
			if(action == cave.KeyEvent.ACTION_DOWN && keyCode == key) {
				return(true);
			}
			return(false);
		}

		public bool isKey(int key) {
			if(keyCode == key) {
				return(true);
			}
			return(false);
		}

		public bool isString(string value) {
			if(object.Equals(value, stringValue)) {
				return(true);
			}
			return(false);
		}

		public bool isCharacter(char value) {
			if(!(object.Equals(stringValue, null)) && cape.String.getChar(stringValue, 0) == value) {
				return(true);
			}
			return(false);
		}

		public void clear() {
			action = 0;
			keyCode = 0;
			stringValue = null;
			isConsumed = false;
		}

		public int getAction() {
			return(action);
		}

		public cave.KeyEvent setAction(int v) {
			action = v;
			return(this);
		}

		public int getKeyCode() {
			return(keyCode);
		}

		public cave.KeyEvent setKeyCode(int v) {
			keyCode = v;
			return(this);
		}

		public string getStringValue() {
			return(stringValue);
		}

		public cave.KeyEvent setStringValue(string v) {
			stringValue = v;
			return(this);
		}

		public bool getShift() {
			return(shift);
		}

		public cave.KeyEvent setShift(bool v) {
			shift = v;
			return(this);
		}

		public bool getControl() {
			return(control);
		}

		public cave.KeyEvent setControl(bool v) {
			control = v;
			return(this);
		}

		public bool getCommand() {
			return(command);
		}

		public cave.KeyEvent setCommand(bool v) {
			command = v;
			return(this);
		}

		public bool getAlt() {
			return(alt);
		}

		public cave.KeyEvent setAlt(bool v) {
			alt = v;
			return(this);
		}
	}
}
