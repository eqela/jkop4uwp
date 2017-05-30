
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
	internal class FileForDotNet : cape.FileAdapter
	{
		private class MyFileReader : cape.DotNetStreamReader
		{
			public MyFileReader() : base() {
			}

			private cape.FileForDotNet file = null;

			public bool initialize(string fileName) {
				if(!(fileName != null)) {
					return(false);
				}
				var v = true;
				try {
					setStream(System.IO.File.OpenRead(fileName));
				}
				catch(System.Exception e) {
					file.onError(e.ToString());
					setStream(null);
					v = false;
				}
				return(v);
			}

			public cape.FileForDotNet getFile() {
				return(file);
			}

			public cape.FileForDotNet.MyFileReader setFile(cape.FileForDotNet v) {
				file = v;
				return(this);
			}
		}

		private class MyFileWriter : cape.DotNetStreamWriter, cape.FileWriter
		{
			public MyFileWriter() : base() {
			}

			private bool append = false;
			private cape.FileForDotNet file = null;

			public bool initialize(string fileName) {
				if(!(fileName != null)) {
					return(false);
				}
				var v = true;
				try {
					if(append) {
						setStream(System.IO.File.Open(fileName, System.IO.FileMode.Append));
					}
					else {
						setStream(System.IO.File.Open(fileName, System.IO.FileMode.Create));
					}
				}
				catch(System.Exception e) {
					file.onError(e.ToString());
					setStream(null);
					v = false;
				}
				return(v);
			}

			public bool getAppend() {
				return(append);
			}

			public cape.FileForDotNet.MyFileWriter setAppend(bool v) {
				append = v;
				return(this);
			}

			public cape.FileForDotNet getFile() {
				return(file);
			}

			public cape.FileForDotNet.MyFileWriter setFile(cape.FileForDotNet v) {
				file = v;
				return(this);
			}
		}

		public static cape.File forPath(string path) {
			var v = new cape.FileForDotNet();
			v.setCompletePath(path);
			return((cape.File)v);
		}

		private string completePath = null;
		private bool isWindows = false;

		public FileForDotNet() {
			isWindows = cape.OS.isWindows();
		}

		public override cape.File withExtension(string ext) {
			if(!(ext != null)) {
				return((cape.File)this);
			}
			var v = new cape.FileForDotNet();
			if(completePath != null) {
				v.setCompletePathRaw(completePath + "." + ext);
			}
			return((cape.File)v);
		}

		public void setCompletePathRaw(string v) {
			completePath = v;
		}

		public void setCompletePath(string v) {
			if(cape.String.isEmpty(v)) {
				completePath = null;
				return;
			}
			var x = v;
			string delim = new System.String(System.IO.Path.DirectorySeparatorChar, 1);
			while(x.EndsWith(delim) && x.Length > 1) {
				x = x.Substring(0, x.Length-1);
			}
			if(cape.OS.isWindows()) {
				var l = cape.String.getLength(x);
				var c1 = cape.String.getChar(x, 1);
				if(l == 2 && c1 == ':') {
					completePath = x + "\\";
					return;
				}
				if(l == 3 && c1 == ':' && cape.String.getChar(x, 2) == '\\') {
					completePath = x;
					return;
				}
			}
			try {
				completePath = System.IO.Path.GetFullPath(x);
			}
			catch(System.Exception e) {
				onError(e.ToString());
				completePath = x;
			}
		}

		public override cape.File entry(string name) {
			onError(null);
			if(object.Equals(name, null) || cape.String.getLength(name) < 1) {
				return((cape.File)this);
			}
			var v = new cape.FileForDotNet();
			if(object.Equals(completePath, null)) {
				v.setCompletePath(name);
			}
			else {
				v.setCompletePathRaw(System.IO.Path.Combine(completePath, name));
			}
			return((cape.File)v);
		}

		public override cape.File getParent() {
			onError(null);
			string v = null;
			if(completePath != null) {
				var di = System.IO.Directory.GetParent(completePath);
				if(di != null) {
					v = di.FullName;
				}
			}
			if(v == null) {
				v = completePath;
			}
			var r = new cape.FileForDotNet();
			r.setCompletePathRaw(v);
			return((cape.File)r);
		}

		private class MyIterator : cape.Iterator<cape.File>
		{
			public MyIterator() {
			}

			private string completePath = null;
			private cape.FileForDotNet file = null;

			public System.Collections.IEnumerator it;

			public virtual cape.File next() {
				string str = null;
				try {
					if(it == null) {
						return(null);
					}
					if(it.MoveNext() == false) {
						return(null);
					}
					str = it.Current as string;
					if(str == null) {
						return(null);
					}
					str = System.IO.Path.Combine(completePath, str);
				}
				catch(System.Exception e) {
					file.onError(e.ToString());
					return(null);
				}
				var v = new cape.FileForDotNet();
				v.setCompletePathRaw(str);
				return((cape.File)v);
			}

			public string getCompletePath() {
				return(completePath);
			}

			public cape.FileForDotNet.MyIterator setCompletePath(string v) {
				completePath = v;
				return(this);
			}

			public cape.FileForDotNet getFile() {
				return(file);
			}

			public cape.FileForDotNet.MyIterator setFile(cape.FileForDotNet v) {
				file = v;
				return(this);
			}
		}

		public override cape.Iterator<cape.File> entries() {
			onError(null);
			if(object.Equals(completePath, null)) {
				return(null);
			}
			var v = new cape.FileForDotNet.MyIterator();
			v.setFile(this);
			v.setCompletePath(completePath);
			try {
				System.Collections.IEnumerable cc = System.IO.Directory.EnumerateFileSystemEntries(completePath);
				v.it = cc.GetEnumerator();
			}
			catch(System.Exception e) {
				onError(e.ToString());
				return(null);
			}
			return((cape.Iterator<cape.File>)v);
		}

		public override bool move(cape.File dest, bool replace) {
			onError(null);
			if(!(dest != null)) {
				onError("null destination");
				return(false);
			}
			if(dest.exists()) {
				if(!replace) {
					onError("target file already exists");
					return(false);
				}
				if(dest.remove() == false) {
					onError("Error when removing `" + dest.getPath() + "': " + dest.getLastErrorDescription());
					return(false);
				}
			}
			var destf = dest as cape.FileForDotNet;
			if(!(destf != null)) {
				return(false);
			}
			var v = true;
			try {
				System.IO.File.Move(completePath, destf.completePath);
			}
			catch(System.Exception e) {
				onError(e.ToString());
				v = false;
			}
			return(v);
		}

		public override bool rename(string newname, bool replacee) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: rename");
			return(false);
		}

		public override bool touch() {
			onError(null);
			if(object.Equals(completePath, null)) {
				return(false);
			}
			var v = true;
			try {
				var fi = new System.IO.FileInfo(completePath);
				if(fi.Exists) {
					System.IO.File.SetLastWriteTime(completePath, System.DateTime.Now);
				}
				else {
					System.IO.File.Create(completePath).Dispose();
				}
			}
			catch(System.Exception e) {
				onError(e.ToString());
				v = false;
			}
			return(v);
		}

		private cape.FileForDotNet.MyFileReader getMyReader() {
			onError(null);
			var v = new cape.FileForDotNet.MyFileReader();
			v.setFile(this);
			if(v.initialize(completePath) == false) {
				return(null);
			}
			return(v);
		}

		public override cape.FileReader read() {
			onError(null);
			return((cape.FileReader)getMyReader());
		}

		public override cape.FileWriter write() {
			onError(null);
			var v = new cape.FileForDotNet.MyFileWriter();
			v.setFile(this);
			if(v.initialize(completePath) == false) {
				v = null;
			}
			return((cape.FileWriter)v);
		}

		public override cape.FileWriter append() {
			onError(null);
			var v = new cape.FileForDotNet.MyFileWriter();
			v.setFile(this);
			v.setAppend(true);
			if(v.initialize(completePath) == false) {
				v = null;
			}
			return((cape.FileWriter)v);
		}

		public override bool makeExecutable() {
			onError(null);
			var v = true;
			if(System.Type.GetType("Mono.Runtime") != null) {
				try {
					System.IO.File.SetAttributes(completePath, (System.IO.FileAttributes)((int)System.IO.File.GetAttributes(completePath) | 0x80000000));
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = false;
				}
			}
			return(v);
		}

		public override cape.FileInfo stat() {
			onError(null);
			var v = new cape.FileInfo();
			v.setFile((cape.File)this);
			v.setOwnerUser(0);
			v.setOwnerGroup(0);
			v.setMode(0);
			v.setExecutable(true);
			v.setLink(false);
			v.setType(cape.FileInfo.FILE_TYPE_UNKNOWN);
			if(completePath != null) {
				try {
					var attrs = System.IO.File.GetAttributes(completePath);
					if(attrs.HasFlag(System.IO.FileAttributes.Directory)) {
						v.setType(FileInfo.FILE_TYPE_DIR);
					}
					else {
						v.setType(FileInfo.FILE_TYPE_FILE);
					}
				}
				catch(System.Exception e) {
					onError(e.ToString());
				}
				if(v.getType() == cape.FileInfo.FILE_TYPE_FILE) {
					try {
						var dnfi = new System.IO.FileInfo(completePath);
						v.setSize((int)dnfi.Length);
						v.setAccessTime((int)dnfi.LastAccessTime.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds);
						v.setModifyTime((int)dnfi.LastWriteTime.Subtract(new System.DateTime(1970, 1, 1)).TotalSeconds);
					}
					catch(System.Exception e) {
						onError(e.ToString());
					}
				}
			}
			return(v);
		}

		public override bool exists() {
			onError(null);
			var fi = stat();
			return(fi.getType() != cape.FileInfo.FILE_TYPE_UNKNOWN);
		}

		public override bool isExecutable() {
			if(cape.OS.isWindows()) {
				return(hasExtension("exe") || hasExtension("bat") || hasExtension("com"));
			}
			return(true);
		}

		public override bool createFifo() {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: createFifo");
			return(false);
		}

		public override bool createDirectory() {
			onError(null);
			var v = false;
			try {
				System.IO.Directory.CreateDirectory(completePath);
				v = true;
			}
			catch(System.Exception e) {
				onError(e.ToString());
				v = false;
			}
			return(v);
		}

		public override bool createDirectoryRecursive() {
			onError(null);
			return(createDirectory());
		}

		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
		static extern bool DeleteFileW([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]string lpFileName);
		[System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
		static extern bool RemoveDirectoryW([System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPWStr)]string lpFileName);
		[System.Runtime.InteropServices.DllImport("kernel32.dll")]
		[return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.I4)]
		static extern int GetLastError();

		public override bool remove() {
			onError(null);
			var v = true;
			if(isWindows) {
				var ec = 0;
				string ff = null;
				ff = @"\\?\" + completePath;
				v = DeleteFileW(ff);
				if(v == false) {
					ec = GetLastError();
				}
				if(v == false) {
					onError("Failed to DeleteFileW: `" + ff + "': " + cape.String.forInteger(ec));
				}
			}
			else {
				try {
					System.IO.File.Delete(completePath);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = false;
				}
			}
			return(v);
		}

		public override bool removeDirectory() {
			onError(null);
			var v = true;
			if(isWindows) {
				string ff = null;
				var ec = 0;
				ff = @"\\?\" + completePath;
				v = RemoveDirectoryW(ff);
				if(v == false) {
					ec = GetLastError();
				}
				if(v == false) {
					onError("Failed to RemoveDirectoryW: `" + ff + "'" + cape.String.forInteger(ec));
				}
			}
			else {
				try {
					System.IO.Directory.Delete(completePath);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = false;
				}
			}
			return(v);
		}

		public override string getPath() {
			return(completePath);
		}

		public override int compareModificationTime(cape.File bf) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: compareModificationTime");
			return(0);
		}

		public override string directoryName() {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: directoryName");
			return(null);
		}

		public override string baseName() {
			onError(null);
			var path = completePath;
			if(object.Equals(path, null)) {
				return(null);
			}
			var rs = cape.String.lastIndexOf(path, System.IO.Path.DirectorySeparatorChar);
			if(rs < 0) {
				return(path);
			}
			return(cape.String.subString(path, rs + 1));
		}

		public override bool isIdentical(cape.File file) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: isIdentical");
			return(false);
		}

		public override byte[] getContentsBuffer() {
			onError(null);
			if(object.Equals(completePath, null)) {
				return(null);
			}
			return(System.IO.File.ReadAllBytes(completePath));
		}

		public override string getContentsString(string encoding) {
			onError(null);
			if(object.Equals(completePath, null)) {
				return(null);
			}
			string v = null;
			if(object.Equals(encoding, null)) {
				try {
					v = System.IO.File.ReadAllText(completePath);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else if(object.Equals(encoding, "UTF-8")) {
				try {
					v = System.IO.File.ReadAllText(completePath, System.Text.Encoding.UTF8);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else if(object.Equals(encoding, "ASCII")) {
				try {
					v = System.IO.File.ReadAllText(completePath, System.Text.Encoding.ASCII);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else if(object.Equals(encoding, "UCS2")) {
				try {
					v = System.IO.File.ReadAllText(completePath, System.Text.Encoding.Unicode);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else if(object.Equals(encoding, "UTF-7")) {
				try {
					v = System.IO.File.ReadAllText(completePath, System.Text.Encoding.UTF7);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else if(object.Equals(encoding, "UTF-32")) {
				try {
					v = System.IO.File.ReadAllText(completePath, System.Text.Encoding.UTF32);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			else {
				try {
					v = System.IO.File.ReadAllText(completePath);
				}
				catch(System.Exception e) {
					onError(e.ToString());
					v = null;
				}
			}
			return(v);
		}

		public override bool setContentsBuffer(byte[] buf) {
			onError(null);
			if(buf == null) {
				return(false);
			}
			try {
				System.IO.File.WriteAllBytes(completePath, buf);
			}
			catch(System.Exception e) {
				onError(e.ToString());
				return(false);
			}
			return(true);
		}

		public override bool isNewerThan(cape.File bf) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: isNewerThan");
			return(false);
		}

		public override bool isOlderThan(cape.File bf) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: isOlderThan");
			return(false);
		}

		public override bool writeFromReader(cape.Reader reader, bool append) {
			System.Diagnostics.Debug.WriteLine("--- stub --- cape.FileForDotNet :: writeFromReader");
			return(false);
		}

		public override bool setContentsString(string str, string encoding) {
			onError(null);
			if(object.Equals(str, null)) {
				return(false);
			}
			System.Text.Encoding ee;
			if(encoding == null || encoding.Equals("UTF-8") || encoding.Equals("UTF8")) {
				ee = new System.Text.UTF8Encoding(false);
			}
			else if(encoding.Equals("ASCII")) {
				ee = System.Text.Encoding.ASCII;
			}
			else if(encoding.Equals("UCS-2") || encoding.Equals("UCS2")) {
				ee = new System.Text.UnicodeEncoding(false, false);
			}
			else {
				return(false);
			}
			try {
				System.IO.File.WriteAllText(completePath, str, ee);
			}
			catch(System.Exception e) {
				onError(e.ToString());
				return(false);
			}
			return(true);
		}
	}
}
