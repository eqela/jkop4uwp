
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

namespace cave.ui {
	public class WebImageWidget : cave.ui.AsynchronousImageWidget
	{
		public WebImageWidget() : this(cave.GuiApplicationContextForUWP.getInstance()) {
		}

		public static cave.ui.WebImageWidget forPlaceholderImage(cave.GuiApplicationContext context, cave.Image image) {
			var v = new cave.ui.WebImageWidget(context);
			v.setWidgetPlaceholderImage(image);
			return(v);
		}

		public WebImageWidget(cave.GuiApplicationContext context) : base(context) {
		}

		public void setWidgetImageResource(string resName) {
			var img = onStartLoading(false);
			if(img != null) {
				img.setWidgetImageResource(resName);
			}
			onEndLoading();
		}

		public void setWidgetImage(cave.Image image) {
			var img = onStartLoading(false);
			if(img != null) {
				img.setWidgetImage(image);
			}
			onEndLoading();
		}

		public void setWidgetImageUrl(string url, System.Action<cape.Error> callback = null) {
			setWidgetImageUrl(url, null, null, callback);
		}

		public void setWidgetImageUrl(string url, cape.KeyValueList<string, string> headers, byte[] body, System.Action<cape.Error> callback) {
			var client = capex.web.NativeWebClient.instance();
			if(client == null) {
				cape.Log.error((cape.LoggingContext)context, "Failed to create web client.");
				if(callback != null) {
					callback(cape.Error.forCode("noWebClient"));
				}
				return;
			}
			cape.Log.debug((cape.LoggingContext)context, "WebImageWidget" + ": Start loading image: `" + url + "'");
			var img = onStartLoading();
			var uu = url;
			var cb = callback;
			client.query("GET", url, headers, body, (string rcode, cape.KeyValueList<string, string> rheaders, byte[] rbody) => {
				onEndLoading();
				if(rbody == null || cape.Buffer.getSize(rbody) < 1) {
					cape.Log.error((cape.LoggingContext)context, "WebImageWidget" + ": FAILED loading image: `" + uu + "'");
					if(cb != null) {
						cb(cape.Error.forCode("failedToDownload"));
					}
					return;
				}
				string mimeType = null;
				System.Collections.Generic.List<cape.KeyValuePair<string, string>> hdrv = rheaders.asVector();
				if(hdrv != null) {
					var n = 0;
					var m = hdrv.Count;
					for(n = 0 ; n < m ; n++) {
						var hdr = hdrv[n];
						if(hdr != null) {
							if(cape.String.equalsIgnoreCase(hdr.key, "content-type")) {
								var vv = hdr.value;
								if(!(object.Equals(vv, null))) {
									var sc = cape.String.indexOf(vv, ';');
									if(sc < 0) {
										mimeType = vv;
									}
									else {
										mimeType = cape.String.getSubString(vv, sc);
									}
								}
							}
						}
					}
				}
				var imgo = context.getImageForBuffer(rbody, mimeType);
				if(imgo == null) {
					cape.Log.error((cape.LoggingContext)context, "WebImageWidget" + ": Failed to create image from the returned data");
					if(cb != null) {
						cb(cape.Error.forCode("failedToCreateImage"));
					}
					return;
				}
				cape.Log.debug((cape.LoggingContext)context, "WebImageWidget" + ": DONE loading image: `" + uu + "'");
				img.setWidgetImage(imgo);
			});
		}
	}
}
