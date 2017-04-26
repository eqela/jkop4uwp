
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
	/// <summary>
	/// The Buffer class provides convenience methods for dealing with data buffers
	/// (arbitrary sequences of bytes).
	/// </summary>

	public class Buffer
	{
		public Buffer() {
		}

		private class MyBufferObject : cape.BufferObject
		{
			public MyBufferObject() {
			}

			private byte[] buffer = null;

			public virtual byte[] toBuffer() {
				return(buffer);
			}

			public byte[] getBuffer() {
				return(buffer);
			}

			public cape.Buffer.MyBufferObject setBuffer(byte[] v) {
				buffer = v;
				return(this);
			}
		}

		/// <summary>
		/// Returns the given array as a BufferObject (which is an object type) that can be
		/// used wherever an object or a class instance is required.
		/// </summary>

		public static cape.BufferObject asObject(byte[] buffer) {
			var v = new cape.Buffer.MyBufferObject();
			v.setBuffer(buffer);
			return((cape.BufferObject)v);
		}

		public static byte[] asBuffer(object obj) {
			if(obj == null) {
				return(null);
			}
			if(obj is cape.BufferObject) {
				return(((cape.BufferObject)obj).toBuffer());
			}
			return(null);
		}

		public static byte[] forInt8Array(sbyte[] buf) {
			return((byte[])(System.Array)buf);
		}

		public static sbyte[] toInt8Array(byte[] buf) {
			return((sbyte[])(System.Array)buf);
		}

		public static byte[] getSubBuffer(byte[] buffer, long offset, long size = (long)-1, bool alwaysNewBuffer = false) {
			if(((alwaysNewBuffer == false) && (offset == 0)) && (size < 0)) {
				return(buffer);
			}
			var bsz = cape.Buffer.getSize(buffer);
			var sz = size;
			if(sz < 0) {
				sz = bsz - offset;
			}
			if(((alwaysNewBuffer == false) && (offset == 0)) && (sz == bsz)) {
				return(buffer);
			}
			if(sz < 1) {
				return(null);
			}
			var v = new byte[sz];
			cape.Buffer.copyFrom(v, buffer, offset, (long)0, sz);
			return(v);
		}

		public static byte getInt8(byte[] buffer, long offset) {
			return(buffer[offset]);
		}

		static ushort reverse16(ushort value) {
			return((ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8));
		}
		static uint reverse32(uint value) {
			return((uint)((value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8 |
			(value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24));
		}

		public static void copyFrom(byte[] array, byte[] src, long soffset, long doffset, long size) {
			System.Array.Copy(src, (int)soffset, array, (int)doffset, (int)size);
		}

		public static ushort getInt16LE(byte[] buffer, long offset) {
			var v = (ushort)0;
			v = (ushort)System.BitConverter.ToInt16(buffer, (int)offset);
			if(System.BitConverter.IsLittleEndian == false) {
				v = reverse16(v);
			}
			return(v);
		}

		public static ushort getInt16BE(byte[] buffer, long offset) {
			var v = (ushort)0;
			v = (ushort)System.BitConverter.ToInt16(buffer, (int)offset);
			if(System.BitConverter.IsLittleEndian) {
				v = reverse16(v);
			}
			return(v);
		}

		public static uint getInt32LE(byte[] buffer, long offset) {
			var v = (uint)0;
			v = (uint)System.BitConverter.ToInt32(buffer, (int)offset);
			if(System.BitConverter.IsLittleEndian == false) {
				v = reverse32(v);
			}
			return(v);
		}

		public static uint getInt32BE(byte[] buffer, long offset) {
			var v = (uint)0;
			v = (uint)System.BitConverter.ToInt32(buffer, (int)offset);
			if(System.BitConverter.IsLittleEndian) {
				v = reverse32(v);
			}
			return(v);
		}

		public static long getSize(byte[] buffer) {
			if(buffer == null) {
				return((long)0);
			}
			return((long)(buffer.Length));
		}

		public static byte getByte(byte[] buffer, long offset) {
			return(cape.Buffer.getInt8(buffer, offset));
		}

		public static void setByte(byte[] buffer, long offset, byte value) {
			buffer[offset] = value;
		}

		public static byte[] allocate(long size) {
			return(new byte[size]);
		}

		public static byte[] resize(byte[] buffer, long newSize) {
			if(buffer == null) {
				return(cape.Buffer.allocate(newSize));
			}
			System.Array.Resize(ref buffer, (int)newSize);
			return(buffer);
		}

		public static byte[] append(byte[] original, byte[] toAppend, long size = (long)-1) {
			if((toAppend == null) || (size == 0)) {
				return(original);
			}
			var sz = size;
			var os = cape.Buffer.getSize(original);
			var oas = cape.Buffer.getSize(toAppend);
			if(sz >= 0) {
				oas = sz;
			}
			var nl = os + oas;
			var nb = cape.Buffer.resize(original, nl);
			cape.Buffer.copyFrom(nb, toAppend, (long)0, os, oas);
			return(nb);
		}

		public static byte[] forHexString(string str) {
			if((object.Equals(str, null)) || ((cape.String.getLength(str) % 2) != 0)) {
				return(null);
			}
			cape.StringBuilder sb = null;
			var b = cape.Buffer.allocate((long)(cape.String.getLength(str) / 2));
			var n = 0;
			var it = cape.String.iterate(str);
			while(it != null) {
				var c = it.getNextChar();
				if(c < 1) {
					break;
				}
				if(sb == null) {
					sb = new cape.StringBuilder();
				}
				if((((c >= 'a') && (c <= 'f')) || ((c >= 'A') && (c <= 'F'))) || ((c >= '0') && (c <= '9'))) {
					sb.append(c);
					if(sb.count() == 2) {
						cape.Buffer.setByte(b, (long)n++, (byte)cape.String.toIntegerFromHex(sb.toString()));
						sb.clear();
					}
				}
				else {
					return(null);
				}
			}
			return(b);
		}

		public static byte[] readFrom(cape.Reader reader) {
			if(reader == null) {
				return(null);
			}
			byte[] v = null;
			var tmp = new byte[1024];
			while(true) {
				var r = reader.read(tmp);
				if(r < 1) {
					break;
				}
				v = cape.Buffer.append(v, tmp, (long)r);
				if(v == null) {
					break;
				}
			}
			return(v);
		}
	}
}
