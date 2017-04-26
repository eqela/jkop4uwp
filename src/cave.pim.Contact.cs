
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

namespace cave.pim
{
	public class Contact
	{
		public Contact() {
		}

		private string name = null;
		private System.Collections.Generic.List<string> phoneNumbers = null;

		public cave.pim.Contact addToPhoneNumbers(string value) {
			if(object.Equals(value, null)) {
				return(this);
			}
			if(phoneNumbers == null) {
				phoneNumbers = new System.Collections.Generic.List<string>();
			}
			phoneNumbers.Add(value);
			return(this);
		}

		public string getPhoneNumberString() {
			if((phoneNumbers == null) || (cape.Vector.getSize(phoneNumbers) < 1)) {
				return(null);
			}
			var sb = new cape.StringBuilder();
			if(phoneNumbers != null) {
				var n = 0;
				var m = phoneNumbers.Count;
				for(n = 0 ; n < m ; n++) {
					var phoneNumber = phoneNumbers[n];
					if(phoneNumber != null) {
						if(sb.count() > 0) {
							sb.append(", ");
						}
						sb.append(phoneNumber);
					}
				}
			}
			return(sb.toString());
		}

		public string getName() {
			return(name);
		}

		public cave.pim.Contact setName(string v) {
			name = v;
			return(this);
		}

		public System.Collections.Generic.List<string> getPhoneNumbers() {
			return(phoneNumbers);
		}

		public cave.pim.Contact setPhoneNumbers(System.Collections.Generic.List<string> v) {
			phoneNumbers = v;
			return(this);
		}
	}
}
