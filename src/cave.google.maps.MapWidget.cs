
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

namespace cave.google.maps
{
	public class MapWidget : cave.ui.ScreenAwareWidget
	{
		private class MyMapMarker : cave.google.maps.MapMarker
		{
			public MyMapMarker() {
			}

			private double lat = 0.00;
			private double lon = 0.00;
			private string label = null;
			private string title = null;
			private cave.Image icon = null;
			public object markerObject = null;
			private bool removed = false;

			public virtual void move(double lat, double lon) {
				this.lat = lat;
				this.lon = lon;
				if(markerObject != null) {
				}
			}

			public virtual void remove() {
				if(markerObject != null) {
				}
				removed = true;
			}

			public virtual void setInfoDetail(string dtl, System.Action callback) {
			}

			public virtual double getLat() {
				return(lat);
			}

			public cave.google.maps.MapWidget.MyMapMarker setLat(double v) {
				lat = v;
				return(this);
			}

			public virtual double getLon() {
				return(lon);
			}

			public cave.google.maps.MapWidget.MyMapMarker setLon(double v) {
				lon = v;
				return(this);
			}

			public virtual string getLabel() {
				return(label);
			}

			public cave.google.maps.MapWidget.MyMapMarker setLabel(string v) {
				label = v;
				return(this);
			}

			public virtual string getTitle() {
				return(title);
			}

			public cave.google.maps.MapWidget.MyMapMarker setTitle(string v) {
				title = v;
				return(this);
			}

			public cave.Image getIcon() {
				return(icon);
			}

			public cave.google.maps.MapWidget.MyMapMarker setIcon(cave.Image v) {
				icon = v;
				return(this);
			}

			public bool getRemoved() {
				return(removed);
			}

			public cave.google.maps.MapWidget.MyMapMarker setRemoved(bool v) {
				removed = v;
				return(this);
			}
		}

		public static cave.google.maps.MapWidget forApiKeys(cave.GuiApplicationContext context, string webApiKey, string iosApiKey) {
			var v = new cave.google.maps.MapWidget(context);
			v.setWebApiKey(webApiKey);
			return(v);
		}

		private string webApiKey = null;
		private double centerLat = 0.00;
		private double centerLon = 0.00;
		private int defaultZoomLevel = 14;
		private bool zoomInCenter = true;
		private bool streetViewEnabled = false;
		private cave.GuiApplicationContext context = null;
		private System.Action<double, double> mapClickHandler = null;
		private System.Collections.Generic.List<cave.google.maps.MapWidget.MyMapMarker> markerQueue = null;
		private System.Collections.Generic.List<cape.DynamicMap> coordinateQueue = null;
		private bool mapInitialized = false;

		private MapWidget(cave.GuiApplicationContext context) {
			this.context = context;
		}

		public virtual void onWidgetAddedToScreen(cave.ui.ScreenForWidget screen) {
		}

		public virtual void onWidgetRemovedFromScreen(cave.ui.ScreenForWidget screen) {
		}

		private void onMapClicked(double lat, double lon) {
			if(mapClickHandler != null) {
				mapClickHandler(lat, lon);
			}
		}

		public void addMapClickHandler(System.Action<double, double> handler) {
			mapClickHandler = handler;
		}

		public cave.google.maps.MapMarker addMapMarker(double lat, double lon, string label, string title, cave.Image icon = null) {
			var v = new cave.google.maps.MapWidget.MyMapMarker();
			v.setLat(lat);
			v.setLon(lon);
			v.setLabel(label);
			v.setTitle(title);
			v.setIcon(icon);
			if(mapInitialized) {
				doAddMapMarker(v);
			}
			else {
				if(markerQueue == null) {
					markerQueue = new System.Collections.Generic.List<cave.google.maps.MapWidget.MyMapMarker>();
				}
				markerQueue.Add(v);
			}
			return((cave.google.maps.MapMarker)v);
		}

		public void placeQueuedMarkers() {
			if(markerQueue != null) {
				var n = 0;
				var m = markerQueue.Count;
				for(n = 0 ; n < m ; n++) {
					var marker = markerQueue[n];
					if(marker != null) {
						if(marker.getRemoved() == false) {
							doAddMapMarker(marker);
						}
					}
				}
			}
			markerQueue = null;
		}

		private void doAddMapMarker(cave.google.maps.MapWidget.MyMapMarker marker) {
			var markerSize = (int)context.getHeightValue("10mm");
			System.Diagnostics.Debug.WriteLine("[cave.google.maps.MapWidget.doAddMapMarker] (MapWidget.sling:662:2): Not implemented");
		}

		public void moveToCenter(double lat, double lon) {
			System.Diagnostics.Debug.WriteLine("[cave.google.maps.MapWidget.moveToCenter] (MapWidget.sling:692:2): Not implemented");
		}

		public void zoomToCoordinates(System.Collections.Generic.List<cape.DynamicMap> coors) {
			if(mapInitialized) {
				coordinateQueue = coors;
				doZoomToCoordinates();
			}
			else {
				coordinateQueue = coors;
			}
		}

		public void doZoomToCoordinates() {
			System.Diagnostics.Debug.WriteLine("[cave.google.maps.MapWidget.doZoomToCoordinates] (MapWidget.sling:733:2): Not implemented");
		}

		public string getWebApiKey() {
			return(webApiKey);
		}

		public cave.google.maps.MapWidget setWebApiKey(string v) {
			webApiKey = v;
			return(this);
		}

		public double getCenterLat() {
			return(centerLat);
		}

		public cave.google.maps.MapWidget setCenterLat(double v) {
			centerLat = v;
			return(this);
		}

		public double getCenterLon() {
			return(centerLon);
		}

		public cave.google.maps.MapWidget setCenterLon(double v) {
			centerLon = v;
			return(this);
		}

		public int getDefaultZoomLevel() {
			return(defaultZoomLevel);
		}

		public cave.google.maps.MapWidget setDefaultZoomLevel(int v) {
			defaultZoomLevel = v;
			return(this);
		}

		public bool getZoomInCenter() {
			return(zoomInCenter);
		}

		public cave.google.maps.MapWidget setZoomInCenter(bool v) {
			zoomInCenter = v;
			return(this);
		}

		public bool getStreetViewEnabled() {
			return(streetViewEnabled);
		}

		public cave.google.maps.MapWidget setStreetViewEnabled(bool v) {
			streetViewEnabled = v;
			return(this);
		}
	}
}
