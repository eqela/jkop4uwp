
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
	public class FileInfo
	{
		public FileInfo() {
		}

		public const int FILE_TYPE_UNKNOWN = 0;
		public const int FILE_TYPE_FILE = 1;
		public const int FILE_TYPE_DIR = 2;

		public static cape.FileInfo forFile(cape.File file) {
			if(file == null) {
				return(new cape.FileInfo());
			}
			return(file.stat());
		}

		private cape.File file = null;
		private int size = 0;
		private int createTime = 0;
		private int accessTime = 0;
		private int modifyTime = 0;
		private int ownerUser = 0;
		private int ownerGroup = 0;
		private int mode = 0;
		private bool executable = false;
		private int type = 0;
		private bool link = false;

		public bool isFile() {
			if(type == cape.FileInfo.FILE_TYPE_FILE) {
				return(true);
			}
			return(false);
		}

		public bool isLink() {
			return(link);
		}

		public bool isDirectory() {
			if(type == cape.FileInfo.FILE_TYPE_DIR) {
				return(true);
			}
			return(false);
		}

		public bool exists() {
			return((isFile() || isDirectory()) || isLink());
		}

		public cape.File getFile() {
			return(file);
		}

		public cape.FileInfo setFile(cape.File v) {
			file = v;
			return(this);
		}

		public int getSize() {
			return(size);
		}

		public cape.FileInfo setSize(int v) {
			size = v;
			return(this);
		}

		public int getCreateTime() {
			return(createTime);
		}

		public cape.FileInfo setCreateTime(int v) {
			createTime = v;
			return(this);
		}

		public int getAccessTime() {
			return(accessTime);
		}

		public cape.FileInfo setAccessTime(int v) {
			accessTime = v;
			return(this);
		}

		public int getModifyTime() {
			return(modifyTime);
		}

		public cape.FileInfo setModifyTime(int v) {
			modifyTime = v;
			return(this);
		}

		public int getOwnerUser() {
			return(ownerUser);
		}

		public cape.FileInfo setOwnerUser(int v) {
			ownerUser = v;
			return(this);
		}

		public int getOwnerGroup() {
			return(ownerGroup);
		}

		public cape.FileInfo setOwnerGroup(int v) {
			ownerGroup = v;
			return(this);
		}

		public int getMode() {
			return(mode);
		}

		public cape.FileInfo setMode(int v) {
			mode = v;
			return(this);
		}

		public bool getExecutable() {
			return(executable);
		}

		public cape.FileInfo setExecutable(bool v) {
			executable = v;
			return(this);
		}

		public int getType() {
			return(type);
		}

		public cape.FileInfo setType(int v) {
			type = v;
			return(this);
		}

		public bool getLink() {
			return(link);
		}

		public cape.FileInfo setLink(bool v) {
			link = v;
			return(this);
		}
	}
}
