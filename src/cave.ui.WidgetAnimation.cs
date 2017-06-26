
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
	public class WidgetAnimation
	{
		public static cave.ui.WidgetAnimation forDuration(cave.GuiApplicationContext context, long duration) {
			var v = new cave.ui.WidgetAnimation(context);
			v.setDuration(duration);
			return(v);
		}

		private cave.GuiApplicationContext context = null;
		private long duration = (long)0;
		private System.Collections.Generic.List<System.Action<double>> callbacks = null;
		private System.Action endListener = null;
		private bool shouldStop = false;
		private bool shouldLoop = false;

		public WidgetAnimation(cave.GuiApplicationContext context) {
			this.context = context;
			callbacks = new System.Collections.Generic.List<System.Action<double>>();
		}

		public cave.ui.WidgetAnimation addCallback(System.Action<double> callback) {
			if(callback != null) {
				callbacks.Add(callback);
			}
			return(this);
		}

		public cave.ui.WidgetAnimation addCrossFade(Windows.UI.Xaml.UIElement from, Windows.UI.Xaml.UIElement to, bool removeFrom = false) {
			var ff = from;
			var tt = to;
			var rf = removeFrom;
			addCallback((double completion) => {
				cave.ui.Widget.setAlpha(ff, 1.00 - completion);
				cave.ui.Widget.setAlpha(tt, completion);
				if(rf && completion >= 1.00) {
					cave.ui.Widget.removeFromParent(ff);
				}
			});
			return(this);
		}

		public cave.ui.WidgetAnimation addFadeIn(Windows.UI.Xaml.UIElement from) {
			var ff = from;
			addCallback((double completion) => {
				cave.ui.Widget.setAlpha(ff, completion);
			});
			return(this);
		}

		public cave.ui.WidgetAnimation addFadeOut(Windows.UI.Xaml.UIElement from, bool removeAfter = false) {
			var ff = from;
			var ra = removeAfter;
			addCallback((double completion) => {
				cave.ui.Widget.setAlpha(ff, 1.00 - completion);
				if(ra && completion >= 1.00) {
					cave.ui.Widget.removeFromParent(ff);
				}
			});
			return(this);
		}

		public cave.ui.WidgetAnimation addFadeOutIn(Windows.UI.Xaml.UIElement from) {
			var ff = from;
			addCallback((double completion) => {
				var r = cape.Math.remainder(completion, 1.00);
				if(r < 0.50) {
					cave.ui.Widget.setAlpha(ff, 1.00 - r * 2);
				}
				else {
					cave.ui.Widget.setAlpha(ff, (r - 0.50) * 2);
				}
			});
			return(this);
		}

		public virtual void tick(double completion) {
			if(callbacks != null) {
				var n = 0;
				var m = callbacks.Count;
				for(n = 0 ; n < m ; n++) {
					var callback = callbacks[n];
					if(callback != null) {
						callback(completion);
					}
				}
			}
		}

		public bool onProgress(long elapsedTime) {
			var completion = (double)elapsedTime / (double)duration;
			tick(completion);
			if(shouldLoop == false && completion >= 1.00 || shouldStop) {
				onAnimationEnded();
				return(false);
			}
			return(true);
		}

		public virtual void onAnimationEnded() {
			if(endListener != null) {
				endListener();
			}
		}

		public void start() {
			System.Diagnostics.Debug.WriteLine("[cave.ui.WidgetAnimation.start] (WidgetAnimation.sling:217:2): Not implemented");
		}

		public long getDuration() {
			return(duration);
		}

		public cave.ui.WidgetAnimation setDuration(long v) {
			duration = v;
			return(this);
		}

		public System.Action getEndListener() {
			return(endListener);
		}

		public cave.ui.WidgetAnimation setEndListener(System.Action v) {
			endListener = v;
			return(this);
		}

		public bool getShouldStop() {
			return(shouldStop);
		}

		public cave.ui.WidgetAnimation setShouldStop(bool v) {
			shouldStop = v;
			return(this);
		}

		public bool getShouldLoop() {
			return(shouldLoop);
		}

		public cave.ui.WidgetAnimation setShouldLoop(bool v) {
			shouldLoop = v;
			return(this);
		}
	}
}
