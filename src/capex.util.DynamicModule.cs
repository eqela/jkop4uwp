
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

namespace capex.util {
	public class DynamicModule
	{
		public DynamicModule() {
		}

		public static capex.util.DynamicModule forFile(cape.LoggingContext ctx, cape.File file) {
			var v = new capex.util.DynamicModule();
			if(v.loadFile(ctx, file) == false) {
				v = null;
			}
			return(v);
		}

		public static capex.util.DynamicModule forObject(cape.LoggingContext ctx, object oo) {
			if(!(oo != null)) {
				return(null);
			}
			System.Reflection.Assembly asm = null;
			var type = oo.GetType();
			if(type != null) {
				asm = System.Reflection.IntrospectionExtensions.GetTypeInfo(type).Assembly;
			}
			if(!(asm != null)) {
				return(null);
			}
			var v = new capex.util.DynamicModule();
			v.setAssembly(asm);
			return(v);
		}

		public static bool isModuleFile(cape.LoggingContext ctx, cape.File file) {
			if(!(file != null)) {
				return(false);
			}
			if(file.hasExtension("dll")) {
				return(true);
			}
			return(false);
		}

		private System.Reflection.Assembly assembly = null;

		public string getModuleDescription() {
			if(assembly != null) {
				return(assembly.FullName);
			}
			return("builtin");
		}

		public bool loadFile(cape.LoggingContext ctx, cape.File file) {
			if(!(file != null)) {
				ctx.logError("DynamicModule" + ": Null file");
				return(false);
			}
			if(!file.isFile()) {
				ctx.logError("DynamicModule" + ": Not a file: `" + file.getPath() + "'");
				return(false);
			}
			ctx.logDebug("Loading dynamic module: `" + file.getPath() + "'");
			string error = null;
			ctx.logError("Loading assemblies on UWP: Not supported");
			if(assembly == null) {
				if(error != null) {
					ctx.logDebug(error);
				}
				ctx.logError("Failed to load assembly: `" + file.getPath() + "'");
				return(false);
			}
			ctx.logDebug("Assembly successfully loaded: `" + file.getPath() + "'");
			return(true);
		}

		public bool executeStaticMethod(cape.LoggingContext ctx, string entityName, string methodName, object[] @params) {
			if(!(assembly != null)) {
				ctx.logError("executeStaticMethod: No assembly has been loaded.");
				return(false);
			}
			System.Type mainType = null;
			mainType = assembly.GetType(entityName);
			if(!(mainType != null)) {
				ctx.logError("Failed to find class `" + entityName + "' in dynamic module: `" + getModuleDescription() + "'");
				return(false);
			}
			string error = null;
			System.Reflection.MethodInfo methodRef = null;
			ctx.logError("GetMethod on UWP: Not supported");
			if(methodRef == null) {
				if(error != null) {
					ctx.logDebug(error);
				}
				ctx.logError("Failed to find method `" + methodName + "' in entity `" + entityName + "' in module: `" + getModuleDescription() + "'");
				return(false);
			}
			try {
				methodRef.Invoke(null, @params);
			}
			catch(System.Exception e) {
				error = e.ToString();
			}
			if(error != null) {
				ctx.logDebug(error);
				ctx.logError("Failed to call method `" + methodName + "' in entity `" + entityName + "' in module: `" + getModuleDescription() + "'");
				return(false);
			}
			return(true);
		}

		public object createObject(cape.LoggingContext ctx, string className) {
			if(!(assembly != null)) {
				ctx.logError("createObject: No assembly has been loaded.");
				return(null);
			}
			System.Type type = null;
			type = assembly.GetType(className);
			if(!(type != null)) {
				ctx.logError("Failed to find class `" + className + "' in dynamic module: `" + getModuleDescription() + "'");
				return(null);
			}
			string error = null;
			System.Reflection.ConstructorInfo constructor = null;
			ctx.logError("GetConstructor on UWP: Not supported");
			if(constructor == null) {
				if(error != null) {
					ctx.logDebug(error);
				}
				ctx.logError("Failed to find a default constructor in class `" + className + "' in module: `" + getModuleDescription() + "'");
				return(null);
			}
			object v = null;
			try {
				v = constructor.Invoke(null);
			}
			catch(System.Exception e) {
				error = e.ToString();
			}
			if(error != null) {
				ctx.logDebug(error);
				ctx.logError("Failed to call default constructor of class `" + className + "' in module: `" + getModuleDescription() + "'");
				return(null);
			}
			if(v == null) {
				ctx.logError("Constructor called without error for class `" + className + "' in module `" + getModuleDescription() + "', but no object was created!");
				return(null);
			}
			return(v);
		}

		public System.Reflection.Assembly getAssembly() {
			return(assembly);
		}

		public capex.util.DynamicModule setAssembly(System.Reflection.Assembly v) {
			assembly = v;
			return(this);
		}
	}
}
