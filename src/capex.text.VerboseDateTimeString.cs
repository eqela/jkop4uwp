
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

namespace capex.text {
	public class VerboseDateTimeString
	{
		public VerboseDateTimeString() {
		}

		public static string forNow() {
			return(capex.text.VerboseDateTimeString.forDateTime(cape.DateTime.forNow()));
		}

		public static string forDateTime(cape.DateTime dd) {
			if(!(dd != null)) {
				return("NODATE");
			}
			var sb = new cape.StringBuilder();
			sb.append(capex.text.VerboseDateTimeString.getShortDayName(dd.getWeekDay()));
			sb.append(", ");
			sb.append(cape.String.forIntegerWithPadding(dd.getDayOfMonth(), 2, "0"));
			sb.append(' ');
			sb.append(capex.text.VerboseDateTimeString.getShortMonthName(dd.getMonth()));
			sb.append(' ');
			sb.append(cape.String.forInteger(dd.getYear()));
			sb.append(' ');
			sb.append(cape.String.forIntegerWithPadding(dd.getHours(), 2, "0"));
			sb.append(':');
			sb.append(cape.String.forIntegerWithPadding(dd.getMinutes(), 2, "0"));
			sb.append(':');
			sb.append(cape.String.forIntegerWithPadding(dd.getSeconds(), 2, "0"));
			sb.append(" GMT");
			return(sb.toString());
		}

		public static string getTimeStringForDateTime(cape.DateTime dd, bool includeTimeZone = false) {
			if(!(dd != null)) {
				return("NOTIME");
			}
			var sb = new cape.StringBuilder();
			sb.append(cape.String.forIntegerWithPadding(dd.getHours(), 2, "0"));
			sb.append(':');
			sb.append(cape.String.forIntegerWithPadding(dd.getMinutes(), 2, "0"));
			sb.append(':');
			sb.append(cape.String.forIntegerWithPadding(dd.getSeconds(), 2, "0"));
			if(includeTimeZone) {
				sb.append(" GMT");
			}
			return(sb.toString());
		}

		public static string getDateStringForDateTime(cape.DateTime dd) {
			if(!(dd != null)) {
				return("NODATE");
			}
			var sb = new cape.StringBuilder();
			sb.append(capex.text.VerboseDateTimeString.getLongMonthName(dd.getMonth()));
			sb.append(' ');
			sb.append(cape.String.forInteger(dd.getDayOfMonth()));
			sb.append(", ");
			sb.append(cape.String.forInteger(dd.getYear()));
			return(sb.toString());
		}

		public static string getShortDayName(int n) {
			switch(n) {
				case 1: {
					return("Sun");
				}
				case 2: {
					return("Mon");
				}
				case 3: {
					return("Tue");
				}
				case 4: {
					return("Wed");
				}
				case 5: {
					return("Thu");
				}
				case 6: {
					return("Fri");
				}
				case 7: {
					return("Sat");
				}
			}
			return(null);
		}

		public static string getShortMonthName(int n) {
			switch(n) {
				case 1: {
					return("Jan");
				}
				case 2: {
					return("Feb");
				}
				case 3: {
					return("Mar");
				}
				case 4: {
					return("Apr");
				}
				case 5: {
					return("May");
				}
				case 6: {
					return("Jun");
				}
				case 7: {
					return("Jul");
				}
				case 8: {
					return("Aug");
				}
				case 9: {
					return("Sep");
				}
				case 10: {
					return("Oct");
				}
				case 11: {
					return("Nov");
				}
				case 12: {
					return("Dec");
				}
			}
			return(null);
		}

		public static string getLongMonthName(int n) {
			switch(n) {
				case 1: {
					return("January");
				}
				case 2: {
					return("February");
				}
				case 3: {
					return("March");
				}
				case 4: {
					return("April");
				}
				case 5: {
					return("May");
				}
				case 6: {
					return("June");
				}
				case 7: {
					return("July");
				}
				case 8: {
					return("August");
				}
				case 9: {
					return("September");
				}
				case 10: {
					return("October");
				}
				case 11: {
					return("November");
				}
				case 12: {
					return("December");
				}
			}
			return(null);
		}
	}
}
