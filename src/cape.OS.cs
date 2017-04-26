
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
	public class OS
	{
		public OS() {
		}

		private static cape.BooleanObject isWindowsFlag = null;
		private static cape.BooleanObject isLinuxFlag = null;
		private static cape.BooleanObject isOSXFlag = null;
		private static cape.BooleanObject isAndroidFlag = null;
		private static cape.BooleanObject isIOSFlag = null;

		public static bool isWindows() {
			if(cape.OS.isWindowsFlag != null) {
				return(cape.OS.isWindowsFlag.toBoolean());
			}
			var v = cape.OS.isSystemType("windows");
			cape.OS.isWindowsFlag = cape.Boolean.asObject(v);
			return(v);
		}

		public static bool isLinux() {
			if(cape.OS.isLinuxFlag != null) {
				return(cape.OS.isLinuxFlag.toBoolean());
			}
			var v = cape.OS.isSystemType("linux");
			cape.OS.isLinuxFlag = cape.Boolean.asObject(v);
			return(v);
		}

		public static bool isOSX() {
			if(cape.OS.isOSXFlag != null) {
				return(cape.OS.isOSXFlag.toBoolean());
			}
			var v = cape.OS.isSystemType("osx");
			cape.OS.isOSXFlag = cape.Boolean.asObject(v);
			return(v);
		}

		public static bool isAndroid() {
			if(cape.OS.isAndroidFlag != null) {
				return(cape.OS.isAndroidFlag.toBoolean());
			}
			var v = cape.OS.isSystemType("android");
			cape.OS.isAndroidFlag = cape.Boolean.asObject(v);
			return(v);
		}

		public static bool isIOS() {
			if(cape.OS.isIOSFlag != null) {
				return(cape.OS.isIOSFlag.toBoolean());
			}
			var v = cape.OS.isSystemType("ios");
			cape.OS.isIOSFlag = cape.Boolean.asObject(v);
			return(v);
		}

		public static bool isSystemType(string id) {
			if(object.Equals(id, "windows")) {
				var c = 0;
				c = System.IO.Path.DirectorySeparatorChar;
				if(c == '\\') {
					return(true);
				}
				return(false);
			}
			if(object.Equals(id, "osx")) {
				if(cape.FileInstance.forPath("/Applications").isDirectory()) {
					return(true);
				}
				return(false);
			}
			if(((object.Equals(id, "posix")) || (object.Equals(id, "linux"))) || (object.Equals(id, "unix"))) {
				if(cape.FileInstance.forPath("/bin/sh").isFile()) {
					return(true);
				}
				return(false);
			}
			return(false);
		}

		public static bool openFile(cape.File file) {
			if(cape.OS.isWindows()) {
				var pl = cape.ProcessLauncher.forCommand("explorer", new object[] {
					file.getPath()
				});
				if(pl.executeSilent() == 0) {
					return(true);
				}
				return(false);
			}
			if(cape.OS.isOSX()) {
				var pl1 = cape.ProcessLauncher.forCommand("open", new object[] {
					file.getPath()
				});
				if(pl1.executeSilent() == 0) {
					return(true);
				}
				return(false);
			}
			if(cape.OS.isLinux()) {
				var pl2 = cape.ProcessLauncher.forCommand("xdg-open", new object[] {
					file.getPath()
				});
				if(pl2.executeSilent() == 0) {
					return(true);
				}
				return(false);
			}
			return(false);
		}
	}
}
