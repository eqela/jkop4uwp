
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

namespace capex.data {
	public interface SQLStatement
	{
		capex.data.SQLStatement setIsStoredProcedure(bool v);
		bool getIsStoredProcedure();
		capex.data.SQLStatement addParamString(string val);
		capex.data.SQLStatement addParamInteger(int val);
		capex.data.SQLStatement addParamLongInteger(long val);
		capex.data.SQLStatement addParamDouble(double val);
		capex.data.SQLStatement addParamBlob(byte[] val);
		capex.data.SQLStatement setParamString(string name, string val);
		capex.data.SQLStatement setParamInteger(string name, int val);
		capex.data.SQLStatement setParamLongInteger(string name, long val);
		capex.data.SQLStatement setParamDouble(string name, double val);
		capex.data.SQLStatement setParamBlob(string name, byte[] val);
		void resetStatement();
		string getError();
	}
}
