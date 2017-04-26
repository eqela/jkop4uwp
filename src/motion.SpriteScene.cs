
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

namespace motion
{
	public class SpriteScene : motion.Scene, motion.SpriteLayer
	{
		public SpriteScene() : base() {
		}

		private motion.SpriteLayer layer = null;
		private cave.Color backgroundColor = null;
		private motion.Texture backgroundTexture = null;
		private motion.TextureSprite backgroundSprite = null;

		public void setBackgroundColor(cave.Color color) {
			backgroundColor = color;
			updateBackgroundColor();
		}

		public motion.Texture getBackgroundTexture() {
			if(backgroundTexture != null) {
				return(backgroundTexture);
			}
			if(backgroundColor != null) {
				return(createTextureForColor(backgroundColor));
			}
			return(null);
		}

		private void updateBackgroundColor() {
			if(backgroundSprite == null) {
				if(layer != null) {
					var txt = getBackgroundTexture();
					if(txt != null) {
						backgroundSprite = layer.addTextureSpriteForSize(txt, layer.getReferenceWidth(), layer.getReferenceHeight());
						backgroundSprite.move((double)0, (double)0);
					}
				}
			}
			else {
				var txt1 = getBackgroundTexture();
				if(txt1 != null) {
					backgroundSprite.setTexture(txt1);
				}
			}
		}

		public override void initialize() {
			base.initialize();
			layer = backend.createSpriteLayer();
			updateBackgroundColor();
		}

		public override void cleanup() {
			base.cleanup();
			layer.removeAllSprites();
			backend.deleteSpriteLayer(layer);
			layer = null;
			backgroundSprite = null;
		}

		public override void onPointerEvent(cave.PointerEvent @event) {
			if(layer != null) {
				@event.x = @event.x * layer.getReferenceWidth();
				@event.y = @event.y * layer.getReferenceHeight();
			}
		}

		public virtual motion.TextureSprite addTextureSpriteForSize(motion.Texture texture, double width, double height) {
			return(layer.addTextureSpriteForSize(texture, width, height));
		}

		public virtual motion.TextSprite addTextSprite(motion.TextProperties text) {
			return(layer.addTextSprite(text));
		}

		public virtual motion.ContainerSprite addContainerSprite(double width, double height) {
			return(layer.addContainerSprite(width, height));
		}

		public virtual void removeSprite(motion.Sprite sprite) {
			layer.removeSprite(sprite);
		}

		public virtual void removeAllSprites() {
			layer.removeAllSprites();
		}

		public virtual void setReferenceWidth(double referenceWidth) {
			layer.setReferenceWidth(referenceWidth);
		}

		public virtual void setReferenceHeight(double referenceHeight) {
			layer.setReferenceHeight(referenceHeight);
		}

		public virtual double getReferenceWidth() {
			return(layer.getReferenceWidth());
		}

		public virtual double getReferenceHeight() {
			return(layer.getReferenceHeight());
		}

		public virtual double getHeightValue(string spec) {
			return(layer.getHeightValue(spec));
		}

		public virtual double getWidthValue(string spec) {
			return(layer.getWidthValue(spec));
		}

		public motion.SpriteLayer getLayer() {
			return(layer);
		}

		public motion.SpriteScene setLayer(motion.SpriteLayer v) {
			layer = v;
			return(this);
		}
	}
}
