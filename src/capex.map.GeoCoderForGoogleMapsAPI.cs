
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
	public class GeoCoderForGoogleMapsAPI : capex.map.GeoCoder
	{
		public static capex.map.GeoCoderForGoogleMapsAPI forAPIKey(string apiKey) {
			if(cape.String.isEmpty(apiKey)) {
				return(null);
			}
			return(new capex.map.GeoCoderForGoogleMapsAPI().setApiKey(apiKey));
		}

		public GeoCoderForGoogleMapsAPI() {
			client = capex.web.NativeWebClient.instance();
		}

		private string apiKey = null;
		private string host = "https://maps.googleapis.com/maps/api/geocode/json?key=";
		private capex.web.WebClient client = null;

		public virtual bool queryAddress(double latitude, double longitude, capex.map.GeoCoderAddressListener listener) {
			if(listener == null) {
				return(false);
			}
			if(client == null) {
				listener.onQueryAddressErrorReceived(cape.Error.forCode("noWebClientInstance"));
				return(false);
			}
			var list = listener;
			client.query("GET", host + apiKey + "&latlng=" + cape.String.forDouble(latitude) + "," + cape.String.forDouble(longitude), null, null, (string statusCode, cape.KeyValueList<string, string> headers, byte[] body) => {
				var data = cape.JSONParser.parse(body) as cape.DynamicMap;
				if(data == null) {
					list.onQueryAddressErrorReceived(cape.Error.forCode("invalidResponseFromGoogleMapsAPI"));
					return;
				}
				var results = data.getDynamicVector("results");
				if(results == null || results.getSize() < 1) {
					list.onQueryAddressErrorReceived(cape.Error.forCode("noResultFound"));
					return;
				}
				var array = results.toVector();
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var result = array[n] as cape.DynamicMap;
						if(result != null) {
							if(result == null) {
								continue;
							}
							var addressComponents = result.getDynamicVector("address_components");
							if(addressComponents == null || addressComponents.getSize() < 1) {
								list.onQueryAddressErrorReceived(cape.Error.forCode("noaddressComponentsFound"));
								return;
							}
							var v = new capex.map.PhysicalAddress();
							var array2 = addressComponents.toVector();
							if(array2 != null) {
								var n2 = 0;
								var m2 = array2.Count;
								for(n2 = 0 ; n2 < m2 ; n2++) {
									var addressComponent = array2[n2] as cape.DynamicMap;
									if(addressComponent != null) {
										if(addressComponent == null) {
											continue;
										}
										var types = addressComponent.getDynamicVector("types");
										if(types == null || types.getSize() < 1) {
											continue;
										}
										var array3 = types.toVector();
										if(array3 != null) {
											var n3 = 0;
											var m3 = array3.Count;
											for(n3 = 0 ; n3 < m3 ; n3++) {
												var type = array3[n3] as string;
												if(type != null) {
													if(object.Equals(type, null)) {
														continue;
													}
													if(cape.String.equals("street_number", type)) {
														v.setStreetAddressDetail(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("route", type)) {
														v.setStreetAddress(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("locality", type)) {
														v.setLocality(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("sublocality", type)) {
														v.setSubLocality(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("administrative_area_level_2", type)) {
														v.setSubAdministrativeArea(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("administrative_area_level_1", type)) {
														v.setAdministrativeArea(addressComponent.getString("long_name"));
														break;
													}
													if(cape.String.equals("country", type)) {
														v.setCountry(addressComponent.getString("long_name"));
														v.setCountryCode(addressComponent.getString("short_name"));
														break;
													}
													if(cape.String.equals("postal_code", type)) {
														v.setPostalCode(addressComponent.getString("long_name"));
														break;
													}
												}
											}
										}
									}
								}
							}
							v.setCompleteAddress(result.getString("formatted_address"));
							var geometry = result.getDynamicMap("geometry");
							if(geometry == null) {
								continue;
							}
							var location = geometry.getDynamicMap("location");
							if(location == null) {
								continue;
							}
							v.setLatitude(location.getDouble("lat"));
							v.setLongitude(location.getDouble("lng"));
							list.onQueryAddressCompleted(v);
							return;
						}
					}
				}
				list.onQueryAddressErrorReceived(cape.Error.forCode("invalidResult"));
			});
			return(true);
		}

		public virtual bool queryLocation(string address, capex.map.GeoCoderLocationListener listener) {
			if(listener == null) {
				return(false);
			}
			if(client == null) {
				listener.onQueryLocationErrorReceived(cape.Error.forCode("noWebClientInstance"));
				return(false);
			}
			var list = listener;
			client.query("GET", host + apiKey + "&address=" + cape.URLEncoder.encode(address), null, null, (string statusCode, cape.KeyValueList<string, string> headers, byte[] body) => {
				var data = cape.JSONParser.parse(body) as cape.DynamicMap;
				if(data == null) {
					list.onQueryLocationErrorReceived(cape.Error.forCode("invalidResponseFromGoogleMapsAPI"));
					return;
				}
				var results = data.getDynamicVector("results");
				if(results == null || results.getSize() < 1) {
					list.onQueryLocationErrorReceived(cape.Error.forCode("noResultFound"));
					return;
				}
				var array = results.toVector();
				if(array != null) {
					var n = 0;
					var m = array.Count;
					for(n = 0 ; n < m ; n++) {
						var result = array[n] as cape.DynamicMap;
						if(result != null) {
							if(result == null) {
								continue;
							}
							var geometry = result.getDynamicMap("geometry");
							if(geometry == null) {
								continue;
							}
							var location = geometry.getDynamicMap("location");
							if(location == null) {
								continue;
							}
							list.onQueryLocationCompleted(new capex.map.GeoLocation().setLatitude(location.getDouble("lat")).setLongitude(location.getDouble("lng")));
							return;
						}
					}
				}
				list.onQueryLocationErrorReceived(cape.Error.forCode("invalidResult"));
			});
			return(true);
		}

		public string getApiKey() {
			return(apiKey);
		}

		public capex.map.GeoCoderForGoogleMapsAPI setApiKey(string v) {
			apiKey = v;
			return(this);
		}
	}
}
