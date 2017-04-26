
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
	public class SpriteEntity : motion.Entity, motion.Sprite
	{
		public SpriteEntity() : base() {
		}

		protected motion.Sprite sprite = null;
		protected motion.SpriteLayer layer = null;

		public virtual motion.Sprite createSprite() {
			return(null);
		}

		public override void initialize() {
			base.initialize();
			if(layer == null) {
				layer = scene as motion.SpriteLayer;
			}
			if(layer != null) {
				sprite = createSprite();
			}
		}

		public override void cleanup() {
			base.cleanup();
			if(sprite != null) {
				sprite.removeFromContainer();
				sprite = null;
			}
			layer = null;
		}

		public virtual void move(double x, double y) {
			if(sprite != null) {
				sprite.move(x, y);
			}
		}

		public virtual void resize(double w, double h) {
			if(sprite != null) {
				sprite.resize(w, h);
			}
		}

		public virtual void scale(double scalex, double scaley) {
			if(sprite != null) {
				sprite.scale(scalex, scaley);
			}
		}

		public virtual void setRotation(double angle) {
			if(sprite != null) {
				sprite.setRotation(angle);
			}
		}

		public virtual void setAlpha(double alpha) {
			if(sprite != null) {
				sprite.setAlpha(alpha);
			}
		}

		public virtual double getX() {
			if(sprite != null) {
				return(sprite.getX());
			}
			return(0.00);
		}

		public virtual double getY() {
			if(sprite != null) {
				return(sprite.getY());
			}
			return(0.00);
		}

		public virtual double getWidth() {
			if(sprite != null) {
				return(sprite.getWidth());
			}
			return(0.00);
		}

		public virtual double getHeight() {
			if(sprite != null) {
				return(sprite.getHeight());
			}
			return(0.00);
		}

		public virtual double getRotation() {
			if(sprite != null) {
				return(sprite.getRotation());
			}
			return(0.00);
		}

		public virtual double getAlpha() {
			if(sprite != null) {
				return(sprite.getAlpha());
			}
			return(0.00);
		}

		public virtual double getScaleX() {
			if(sprite != null) {
				return(sprite.getScaleX());
			}
			return(0.00);
		}

		public virtual double getScaleY() {
			if(sprite != null) {
				return(sprite.getScaleY());
			}
			return(0.00);
		}

		public virtual void removeFromContainer() {
			remove();
		}
	}
}
