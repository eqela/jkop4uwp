
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

namespace capex.map
{
	public class PhysicalAddress : cape.StringObject
	{
		public PhysicalAddress() {
		}

		public static capex.map.PhysicalAddress forString(string str) {
			var v = new capex.map.PhysicalAddress();
			v.fromString(str);
			return(v);
		}

		private double latitude = 0.00;
		private double longitude = 0.00;
		private string completeAddress = null;
		private string country = null;
		private string countryCode = null;
		private string administrativeArea = null;
		private string subAdministrativeArea = null;
		private string locality = null;
		private string subLocality = null;
		private string streetAddress = null;
		private string streetAddressDetail = null;
		private string postalCode = null;

		public void fromString(string str) {
			cape.DynamicMap data = null;
			if(!(object.Equals(str, null))) {
				data = cape.JSONParser.parse(str) as cape.DynamicMap;
			}
			if(data == null) {
				data = new cape.DynamicMap();
			}
			latitude = data.getDouble("latitude");
			longitude = data.getDouble("longitude");
			completeAddress = data.getString("completeAddress");
			country = data.getString("country");
			countryCode = data.getString("countryCode");
			administrativeArea = data.getString("administrativeArea");
			subAdministrativeArea = data.getString("subAdministrativeArea");
			locality = data.getString("locality");
			subLocality = data.getString("subLocality");
			streetAddress = data.getString("streetAddress");
			streetAddressDetail = data.getString("streetAddressDetail");
			postalCode = data.getString("postalCode");
		}

		public virtual string toString() {
			var v = new cape.DynamicMap();
			v.set("latitude", latitude);
			v.set("longitude", longitude);
			v.set("completeAddress", (object)completeAddress);
			v.set("country", (object)country);
			v.set("countryCode", (object)countryCode);
			v.set("administrativeArea", (object)administrativeArea);
			v.set("subAdministrativeArea", (object)subAdministrativeArea);
			v.set("locality", (object)locality);
			v.set("subLocality", (object)subLocality);
			v.set("streetAddress", (object)streetAddress);
			v.set("streetAddressDetail", (object)streetAddressDetail);
			v.set("postalCode", (object)postalCode);
			return(cape.JSONEncoder.encode((object)v, false));
		}

		public double getLatitude() {
			return(latitude);
		}

		public capex.map.PhysicalAddress setLatitude(double v) {
			latitude = v;
			return(this);
		}

		public double getLongitude() {
			return(longitude);
		}

		public capex.map.PhysicalAddress setLongitude(double v) {
			longitude = v;
			return(this);
		}

		public string getCompleteAddress() {
			return(completeAddress);
		}

		public capex.map.PhysicalAddress setCompleteAddress(string v) {
			completeAddress = v;
			return(this);
		}

		public string getCountry() {
			return(country);
		}

		public capex.map.PhysicalAddress setCountry(string v) {
			country = v;
			return(this);
		}

		public string getCountryCode() {
			return(countryCode);
		}

		public capex.map.PhysicalAddress setCountryCode(string v) {
			countryCode = v;
			return(this);
		}

		public string getAdministrativeArea() {
			return(administrativeArea);
		}

		public capex.map.PhysicalAddress setAdministrativeArea(string v) {
			administrativeArea = v;
			return(this);
		}

		public string getSubAdministrativeArea() {
			return(subAdministrativeArea);
		}

		public capex.map.PhysicalAddress setSubAdministrativeArea(string v) {
			subAdministrativeArea = v;
			return(this);
		}

		public string getLocality() {
			return(locality);
		}

		public capex.map.PhysicalAddress setLocality(string v) {
			locality = v;
			return(this);
		}

		public string getSubLocality() {
			return(subLocality);
		}

		public capex.map.PhysicalAddress setSubLocality(string v) {
			subLocality = v;
			return(this);
		}

		public string getStreetAddress() {
			return(streetAddress);
		}

		public capex.map.PhysicalAddress setStreetAddress(string v) {
			streetAddress = v;
			return(this);
		}

		public string getStreetAddressDetail() {
			return(streetAddressDetail);
		}

		public capex.map.PhysicalAddress setStreetAddressDetail(string v) {
			streetAddressDetail = v;
			return(this);
		}

		public string getPostalCode() {
			return(postalCode);
		}

		public capex.map.PhysicalAddress setPostalCode(string v) {
			postalCode = v;
			return(this);
		}
	}
}
