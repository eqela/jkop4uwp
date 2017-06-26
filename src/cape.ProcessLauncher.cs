
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

namespace cape {
	/// <summary>
	/// The ProcessLauncher class provides a mechanism for starting and controlling
	/// additional processes.
	/// </summary>

	public class ProcessLauncher
	{
		private class MyStringPipeHandler : cape.BufferDataReceiver
		{
			private cape.StringBuilder builder = null;
			private string encoding = null;

			public MyStringPipeHandler() {
				encoding = "UTF-8";
			}

			public virtual void onBufferData(byte[] data, long size) {
				if(builder == null || data == null || size < 1) {
					return;
				}
				var str = cape.String.forBuffer(cape.Buffer.getSubBuffer(data, (long)0, size), encoding);
				if(!(str != null)) {
					return;
				}
				builder.append(str);
			}

			public cape.StringBuilder getBuilder() {
				return(builder);
			}

			public cape.ProcessLauncher.MyStringPipeHandler setBuilder(cape.StringBuilder v) {
				builder = v;
				return(this);
			}

			public string getEncoding() {
				return(encoding);
			}

			public cape.ProcessLauncher.MyStringPipeHandler setEncoding(string v) {
				encoding = v;
				return(this);
			}
		}

		private class MyBufferPipeHandler : cape.BufferDataReceiver
		{
			public MyBufferPipeHandler() {
			}

			private byte[] data = null;

			public virtual void onBufferData(byte[] newData, long size) {
				data = cape.Buffer.append(data, newData, size);
			}

			public byte[] getData() {
				return(data);
			}

			public cape.ProcessLauncher.MyBufferPipeHandler setData(byte[] v) {
				data = v;
				return(this);
			}
		}

		private class QuietPipeHandler : cape.BufferDataReceiver
		{
			public QuietPipeHandler() {
			}

			public virtual void onBufferData(byte[] data, long size) {
			}
		}

		public static cape.ProcessLauncher forSelf() {
			var exe = cape.CurrentProcess.getExecutableFile();
			if(!(exe != null)) {
				return(null);
			}
			var v = new cape.ProcessLauncher();
			v.setFile(exe);
			return(v);
		}

		/// <summary>
		/// Creates a launcher for the given executable file. If the file does not exist,
		/// this method returns a null object instead.
		/// </summary>

		public static cape.ProcessLauncher forFile(cape.File file, object[] @params = null) {
			if(file == null || file.isFile() == false) {
				return(null);
			}
			var v = new cape.ProcessLauncher();
			v.setFile(file);
			if(@params != null) {
				var n = 0;
				var m = @params.Length;
				for(n = 0 ; n < m ; n++) {
					var param = @params[n] as string;
					if(param != null) {
						v.addToParams(param);
					}
				}
			}
			return(v);
		}

		/// <summary>
		/// Creates a process launcher for the given command. The command can either be a
		/// full or relative path to an executable file or, if not, a matching executable
		/// file will be searched for in the PATH environment variable (or through other
		/// applicable standard means on the given platform), and an appropriately
		/// configured ProcessLauncher object will be returned. However, if the given
		/// command is not found, this method returns null.
		/// </summary>

		public static cape.ProcessLauncher forCommand(string command, object[] @params = null) {
			if(cape.String.isEmpty(command)) {
				return(null);
			}
			cape.File file = null;
			if(cape.String.indexOf(command, cape.Environment.getPathSeparator()) >= 0) {
				file = cape.FileInstance.forPath(command);
			}
			else {
				file = cape.Environment.findCommand(command);
			}
			return(cape.ProcessLauncher.forFile(file, @params));
		}

		/// <summary>
		/// Creates a new process launcher object for the given string, which includes a
		/// complete command line for executing the process, including the command itself
		/// and all the parameters, delimited with spaces. If parameters will need to
		/// contain space as part of their value, those parameters can be enclosed in double
		/// quotes. This method will return null if the command does not exist and/or is not
		/// found.
		/// </summary>

		public static cape.ProcessLauncher forString(string str) {
			if(cape.String.isEmpty(str)) {
				return(null);
			}
			var arr = cape.String.quotedStringToVector(str, ' ');
			if(arr == null || cape.Vector.getSize(arr) < 1) {
				return(null);
			}
			var vsz = cape.Vector.getSize(arr);
			var cmd = arr[0];
			object[] @params = null;
			var paramCount = vsz - 1;
			if(paramCount > 0) {
				@params = new object[paramCount];
				for(var n = 1 ; n < vsz ; n++) {
					@params[n - 1] = (object)arr[n];
				}
			}
			return(cape.ProcessLauncher.forCommand(cmd, @params));
		}

		private cape.File file = null;
		private System.Collections.Generic.List<string> @params = null;
		private System.Collections.Generic.Dictionary<string,string> env = null;
		private cape.File cwd = null;
		private int uid = -1;
		private int gid = -1;
		private bool trapSigint = true;
		private bool replaceSelf = false;
		private bool pipePty = false;
		private bool startGroup = false;
		private bool noCmdWindow = false;
		private cape.StringBuilder errorBuffer = null;

		public ProcessLauncher() {
			@params = new System.Collections.Generic.List<string>();
			env = new System.Collections.Generic.Dictionary<string,string>();
		}

		private void appendProperParam(cape.StringBuilder sb, string p) {
			var noQuotes = false;
			if(cape.OS.isWindows()) {
				var rc = cape.String.lastIndexOf(p, ' ');
				if(rc < 0) {
					noQuotes = true;
				}
			}
			sb.append(' ');
			if(noQuotes) {
				sb.append(p);
			}
			else {
				sb.append('\"');
				sb.append(p);
				sb.append('\"');
			}
		}

		/// <summary>
		/// Produces a string representation of this command with the command itself,
		/// parameters and environment variables included.
		/// </summary>

		public string toString(bool includeEnv = true) {
			var sb = new cape.StringBuilder();
			if(includeEnv) {
				System.Collections.Generic.List<string> keys = cape.Map.getKeys(env);
				if(keys != null) {
					var n = 0;
					var m = keys.Count;
					for(n = 0 ; n < m ; n++) {
						var key = keys[n];
						if(key != null) {
							sb.append(key);
							sb.append("=");
							sb.append(env[key]);
							sb.append(" ");
						}
					}
				}
			}
			sb.append("\"");
			if(file != null) {
				sb.append(file.getPath());
			}
			sb.append("\"");
			if(@params != null) {
				var n2 = 0;
				var m2 = @params.Count;
				for(n2 = 0 ; n2 < m2 ; n2++) {
					var p = @params[n2];
					if(p != null) {
						appendProperParam(sb, p);
					}
				}
			}
			return(sb.toString());
		}

		public cape.ProcessLauncher addToParams(string arg) {
			if(!(object.Equals(arg, null))) {
				if(@params == null) {
					@params = new System.Collections.Generic.List<string>();
				}
				@params.Add(arg);
			}
			return(this);
		}

		public cape.ProcessLauncher addToParams(cape.File file) {
			if(file != null) {
				addToParams(file.getPath());
			}
			return(this);
		}

		public cape.ProcessLauncher addToParams(System.Collections.Generic.List<string> @params) {
			if(@params != null) {
				var n = 0;
				var m = @params.Count;
				for(n = 0 ; n < m ; n++) {
					var s = @params[n];
					if(s != null) {
						addToParams(s);
					}
				}
			}
			return(this);
		}

		public void setEnvVariable(string key, string val) {
			if(!(object.Equals(key, null))) {
				if(env == null) {
					env = new System.Collections.Generic.Dictionary<string,string>();
				}
				env[key] = val;
			}
		}

		private cape.Process startProcess(bool wait, cape.BufferDataReceiver pipeHandler, bool withIO = false) {
			System.Diagnostics.Debug.WriteLine("[cape.ProcessLauncher.startProcess] (ProcessLauncher.sling:266:2): Not implemented");
			return(null);
		}

		public cape.Process start() {
			return(startProcess(false, null));
		}

		public cape.ProcessWithIO startWithIO() {
			return(startProcess(false, null, true) as cape.ProcessWithIO);
		}

		public int execute() {
			var cp = startProcess(true, null);
			if(cp == null) {
				return(-1);
			}
			return(cp.getExitStatus());
		}

		public int executeSilent() {
			var cp = startProcess(true, (cape.BufferDataReceiver)new cape.ProcessLauncher.QuietPipeHandler());
			if(cp == null) {
				return(-1);
			}
			return(cp.getExitStatus());
		}

		public int executeToStringBuilder(cape.StringBuilder output) {
			var msp = new cape.ProcessLauncher.MyStringPipeHandler();
			msp.setBuilder(output);
			var cp = startProcess(true, (cape.BufferDataReceiver)msp);
			if(cp == null) {
				return(-1);
			}
			return(cp.getExitStatus());
		}

		public string executeToString() {
			var sb = new cape.StringBuilder();
			if(executeToStringBuilder(sb) < 0) {
				return(null);
			}
			return(sb.toString());
		}

		public byte[] executeToBuffer() {
			var ph = new cape.ProcessLauncher.MyBufferPipeHandler();
			var cp = startProcess(true, (cape.BufferDataReceiver)ph);
			if(cp == null) {
				return(null);
			}
			return(ph.getData());
		}

		public int executeToPipe(cape.BufferDataReceiver pipeHandler) {
			var cp = startProcess(true, pipeHandler);
			if(cp == null) {
				return(-1);
			}
			return(cp.getExitStatus());
		}

		public cape.File getFile() {
			return(file);
		}

		public cape.ProcessLauncher setFile(cape.File v) {
			file = v;
			return(this);
		}

		public System.Collections.Generic.List<string> getParams() {
			return(@params);
		}

		public cape.ProcessLauncher setParams(System.Collections.Generic.List<string> v) {
			@params = v;
			return(this);
		}

		public System.Collections.Generic.Dictionary<string,string> getEnv() {
			return(env);
		}

		public cape.ProcessLauncher setEnv(System.Collections.Generic.Dictionary<string,string> v) {
			env = v;
			return(this);
		}

		public cape.File getCwd() {
			return(cwd);
		}

		public cape.ProcessLauncher setCwd(cape.File v) {
			cwd = v;
			return(this);
		}

		public int getUid() {
			return(uid);
		}

		public cape.ProcessLauncher setUid(int v) {
			uid = v;
			return(this);
		}

		public int getGid() {
			return(gid);
		}

		public cape.ProcessLauncher setGid(int v) {
			gid = v;
			return(this);
		}

		public bool getTrapSigint() {
			return(trapSigint);
		}

		public cape.ProcessLauncher setTrapSigint(bool v) {
			trapSigint = v;
			return(this);
		}

		public bool getReplaceSelf() {
			return(replaceSelf);
		}

		public cape.ProcessLauncher setReplaceSelf(bool v) {
			replaceSelf = v;
			return(this);
		}

		public bool getPipePty() {
			return(pipePty);
		}

		public cape.ProcessLauncher setPipePty(bool v) {
			pipePty = v;
			return(this);
		}

		public bool getStartGroup() {
			return(startGroup);
		}

		public cape.ProcessLauncher setStartGroup(bool v) {
			startGroup = v;
			return(this);
		}

		public bool getNoCmdWindow() {
			return(noCmdWindow);
		}

		public cape.ProcessLauncher setNoCmdWindow(bool v) {
			noCmdWindow = v;
			return(this);
		}

		public cape.StringBuilder getErrorBuffer() {
			return(errorBuffer);
		}

		public cape.ProcessLauncher setErrorBuffer(cape.StringBuilder v) {
			errorBuffer = v;
			return(this);
		}
	}
}
