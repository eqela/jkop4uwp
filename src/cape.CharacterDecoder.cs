
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

namespace cape
{
	public abstract class CharacterDecoder : cape.CharacterIterator
	{
		public CharacterDecoder() {
		}

		public const int UTF8 = 0;
		public const int ASCII = 1;
		public const int UCS2 = 2;
		private int encoding = 0;
		private char current = (char)0;
		private int currentSize = 0;
		private bool ended = false;

		public void copyTo(cape.CharacterDecoder o) {
			o.encoding = encoding;
			o.current = current;
			o.currentSize = currentSize;
			o.ended = ended;
		}

		public virtual bool moveToPreviousByte() {
			return(false);
		}

		public virtual bool moveToNextByte() {
			return(false);
		}

		public virtual int getCurrentByte() {
			return(0);
		}

		public cape.CharacterDecoder setEncoding(string ee) {
			if(cape.String.equalsIgnoreCase(ee, "UTF8") || cape.String.equalsIgnoreCase(ee, "UTF-8")) {
				encoding = cape.CharacterDecoder.UTF8;
				currentSize = 1;
				return(this);
			}
			if(cape.String.equalsIgnoreCase(ee, "ASCII")) {
				encoding = cape.CharacterDecoder.ASCII;
				currentSize = 1;
				return(this);
			}
			if(cape.String.equalsIgnoreCase(ee, "UCS2") || cape.String.equalsIgnoreCase(ee, "UCS-2")) {
				encoding = cape.CharacterDecoder.UCS2;
				currentSize = 2;
				return(this);
			}
			return(null);
		}

		public virtual bool moveToPreviousChar() {
			var cs = currentSize;
			if(cs > 1) {
				var n = 0;
				for(n = 0 ; n < cs - 1 ; n++) {
					if(moveToPreviousByte() == false) {
						return(false);
					}
				}
			}
			var v = doMoveToPreviousChar();
			if(v == false && cs > 1) {
				var n1 = 0;
				for(n1 = 0 ; n1 < cs - 1 ; n1++) {
					moveToNextByte();
				}
			}
			if(v && ended) {
				ended = false;
			}
			return(v);
		}

		public bool doMoveToPreviousChar() {
			if(encoding == cape.CharacterDecoder.UTF8) {
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var c2 = (int)getCurrentByte();
				if(c2 <= 127) {
					current = (char)c2;
					currentSize = 1;
					return(true);
				}
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var c1 = (int)getCurrentByte();
				if((c1 & 192) == 192) {
					if(moveToNextByte() == false) {
						return(false);
					}
					var v = (int)((c1 & 31) << 6);
					v += (int)(c2 & 63);
					current = (char)v;
					currentSize = 2;
					return(true);
				}
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var c0 = (int)getCurrentByte();
				if((c0 & 224) == 224) {
					if(moveToNextByte() == false) {
						return(false);
					}
					if(moveToNextByte() == false) {
						return(false);
					}
					var v1 = (int)((c0 & 15) << 12);
					v1 += (int)((c1 & 63) << 6);
					v1 += (int)(c2 & 63);
					current = (char)v1;
					currentSize = 3;
					return(true);
				}
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var cm1 = (int)getCurrentByte();
				if((cm1 & 240) == 240) {
					if(moveToNextByte() == false) {
						return(false);
					}
					if(moveToNextByte() == false) {
						return(false);
					}
					if(moveToNextByte() == false) {
						return(false);
					}
					var v2 = (int)((cm1 & 7) << 18);
					v2 += (int)((c0 & 63) << 12);
					v2 += (int)((c1 & 63) << 6);
					v2 += (int)(c2 & 63);
					current = (char)v2;
					currentSize = 4;
					return(true);
				}
				moveToNextByte();
				moveToNextByte();
				moveToNextByte();
				moveToNextByte();
				return(false);
			}
			if(encoding == cape.CharacterDecoder.ASCII) {
				if(moveToPreviousByte() == false) {
					return(false);
				}
				current = (char)getCurrentByte();
				return(true);
			}
			if(encoding == cape.CharacterDecoder.UCS2) {
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var c11 = (int)getCurrentByte();
				if(moveToPreviousByte() == false) {
					return(false);
				}
				var c01 = (int)getCurrentByte();
				if(moveToNextByte() == false) {
					return(false);
				}
				current = (char)(c01 << 8 & c11);
				return(true);
			}
			return(false);
		}

		public virtual bool moveToNextChar() {
			var v = doMoveToNextChar();
			if(v == false) {
				current = (char)0;
				ended = true;
			}
			return(v);
		}

		public bool doMoveToNextChar() {
			if(encoding == cape.CharacterDecoder.UTF8) {
				if(moveToNextByte() == false) {
					return(false);
				}
				var b1 = getCurrentByte();
				var v = -1;
				if((b1 & 240) == 240) {
					v = (int)((b1 & 7) << 18);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b2 = getCurrentByte();
					v += (int)((b2 & 63) << 12);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b3 = getCurrentByte();
					v += (int)((b3 & 63) << 6);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b4 = getCurrentByte();
					v += (int)(b4 & 63);
					currentSize = 4;
				}
				else if((b1 & 224) == 224) {
					v = (int)((b1 & 15) << 12);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b21 = getCurrentByte();
					v += (int)((b21 & 63) << 6);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b31 = getCurrentByte();
					v += (int)(b31 & 63);
					currentSize = 3;
				}
				else if((b1 & 192) == 192) {
					v = (int)((b1 & 31) << 6);
					if(moveToNextByte() == false) {
						return(false);
					}
					var b22 = getCurrentByte();
					v += (int)(b22 & 63);
					currentSize = 2;
				}
				else if(b1 <= 127) {
					v = (int)b1;
					currentSize = 1;
				}
				else {
					return(false);
				}
				current = (char)v;
				return(true);
			}
			if(encoding == cape.CharacterDecoder.ASCII) {
				if(moveToNextByte() == false) {
					return(false);
				}
				current = (char)getCurrentByte();
				return(true);
			}
			if(encoding == cape.CharacterDecoder.UCS2) {
				if(moveToNextByte() == false) {
					return(false);
				}
				var c0 = (int)getCurrentByte();
				if(moveToNextByte() == false) {
					return(false);
				}
				var c1 = (int)getCurrentByte();
				current = (char)(c0 << 8 & c1);
				return(true);
			}
			return(false);
		}

		public virtual char getCurrentChar() {
			return(current);
		}

		public virtual char getNextChar() {
			if(moveToNextChar() == false) {
				return((char)0);
			}
			return(current);
		}

		public virtual bool hasEnded() {
			return(ended);
		}
	}
}
