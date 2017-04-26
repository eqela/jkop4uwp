
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
	public abstract class GeoLocationManager
	{
		public static capex.map.GeoLocationManager forApplicationContext(cape.ApplicationContext context) {
			return(null);
		}

		private System.Collections.Generic.List<System.Action<capex.map.GeoLocation>> listeners = null;

		public GeoLocationManager() {
			listeners = new System.Collections.Generic.List<System.Action<capex.map.GeoLocation>>();
		}

		public void addListener(System.Action<capex.map.GeoLocation> l) {
			listeners.Add(l);
		}

		public void removeListener(System.Action<capex.map.GeoLocation> l) {
			cape.Vector.removeValue(listeners, l);
		}

		public void removeAllListeners() {
			cape.Vector.clear(listeners);
		}

		public void notifyListeners(capex.map.GeoLocation location) {
			if(listeners != null) {
				var n = 0;
				var m = listeners.Count;
				for(n = 0 ; n < m ; n++) {
					var listener = listeners[n];
					if(listener != null) {
						if(listener != null) {
							listener(location);
						}
					}
				}
			}
		}

		public abstract void startLocationUpdates(System.Action<bool> callback);
		public abstract void stopLocationUpdates();
		public abstract void getLastLocation(System.Action<capex.map.GeoLocation, cape.Error> callback);
	}
}
