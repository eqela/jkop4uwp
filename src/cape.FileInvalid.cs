
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
	public class FileInvalid : cape.FileAdapter
	{
		public FileInvalid() : base() {
		}

		public override cape.File entry(string name) {
			return((cape.File)new cape.FileInvalid());
		}

		public override bool makeExecutable() {
			return(false);
		}

		public override bool move(cape.File dest, bool replace) {
			return(false);
		}

		public override bool rename(string newname, bool replace) {
			return(false);
		}

		public override bool touch() {
			return(false);
		}

		public override cape.FileReader read() {
			return(null);
		}

		public override cape.FileWriter write() {
			return(null);
		}

		public override cape.FileWriter append() {
			return(null);
		}

		public override cape.FileInfo stat() {
			return(null);
		}

		public override bool exists() {
			return(false);
		}

		public override bool isExecutable() {
			return(false);
		}

		public override bool createFifo() {
			return(false);
		}

		public override bool createDirectory() {
			return(false);
		}

		public override bool createDirectoryRecursive() {
			return(false);
		}

		public override bool removeDirectory() {
			return(false);
		}

		public override string getPath() {
			return(null);
		}

		public override bool isSame(cape.File file) {
			return(false);
		}

		public override bool remove() {
			return(false);
		}

		public override bool removeRecursive() {
			return(false);
		}

		public override int compareModificationTime(cape.File file) {
			return(0);
		}

		public override string directoryName() {
			return(null);
		}

		public override string baseName() {
			return(null);
		}

		public override bool isIdentical(cape.File file) {
			return(false);
		}

		public override byte[] getContentsBuffer() {
			return(null);
		}

		public override string getContentsString(string encoding) {
			return(null);
		}

		public override bool setContentsBuffer(byte[] buffer) {
			return(false);
		}

		public override bool setContentsString(string str, string encoding) {
			return(false);
		}

		public override bool isNewerThan(cape.File bf) {
			return(false);
		}

		public override bool isOlderThan(cape.File bf) {
			return(false);
		}

		public override bool writeFromReader(cape.Reader reader, bool append) {
			return(false);
		}

		public override cape.Iterator<cape.File> entries() {
			return(null);
		}
	}
}
