
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

namespace motion {
	public class SpriteSheetEntity : motion.SpriteEntity
	{
		public SpriteSheetEntity() : base() {
		}

		protected motion.Sprite sprite = null;
		private motion.Texture[] imageSheet = null;
		private int arraySize = 0;
		private int currentFrame = -1;
		private motion.TextureSprite textureSprite = null;
		private long delay = (long)0;
		private cape.TimeValue timeVal = null;

		public override void initialize() {
			base.initialize();
			sprite = base.sprite;
			textureSprite = sprite as motion.TextureSprite;
			timeVal = cape.TimeValue.forSeconds((long)1);
		}

		public bool setImageSheet(System.Collections.Generic.List<cave.Image> imgs) {
			if(imgs == null) {
				return(false);
			}
			currentFrame = -1;
			imageSheet = new motion.Texture[cape.Vector.getSize(imgs)];
			var i = 0;
			if(imgs != null) {
				var n = 0;
				var m = imgs.Count;
				for(n = 0 ; n < m ; n++) {
					var txt = imgs[n];
					if(txt != null) {
						imageSheet[i] = ((motion.Scene)scene).createTextureForImage(txt);
						i++;
					}
				}
			}
			arraySize = imageSheet.Length;
			return(true);
		}

		public void setFramesPerSecond(int fps) {
			var v = 1000 / fps;
			if(timeVal != null) {
				timeVal.setMicroSeconds((long)(v * 1000));
			}
			delay = (long)(v * 1000);
		}

		public bool nextFrame() {
			if(imageSheet == null || arraySize < 1) {
				sprite = null;
				return(false);
			}
			currentFrame++;
			if(currentFrame >= arraySize) {
				currentFrame = 0;
			}
			if(textureSprite != null) {
				textureSprite.setTexture(imageSheet[currentFrame]);
			}
			return(true);
		}

		public override void tick(cape.TimeValue gameTime, double delta) {
			base.tick(gameTime, delta);
			if(timeVal != null) {
				var timeDiff = cape.TimeValue.diff(gameTime, timeVal) % 1000000;
				if(timeDiff >= delay) {
					timeVal.setMicroSeconds(gameTime.getMicroSeconds());
					timeVal.setSeconds(gameTime.getSeconds());
					nextFrame();
				}
			}
		}
	}
}
