
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
	public class TextButtonEntity : motion.MultiSpriteEntity, cave.PointerListener
	{
		public static motion.TextButtonEntity forText(string text) {
			var tp = motion.TextProperties.forText(text);
			tp.setFontSizeAbsolute(2.00);
			tp.setFontSizeDescription("5mm");
			return(motion.TextButtonEntity.forTextProperties(tp));
		}

		public static motion.TextButtonEntity forTextProperties(motion.TextProperties text) {
			var v = new motion.TextButtonEntity();
			v.setText(text);
			return(v);
		}

		private cave.Color backgroundColor = null;
		private cave.Color pressedColor = null;
		private motion.TextProperties text = null;
		private motion.TextureSprite boxSprite = null;
		private motion.TextSprite textSprite = null;
		private bool pressed = false;
		private System.Action clickHandler = null;

		public TextButtonEntity() {
			backgroundColor = cave.Color.forRGB(128, 128, 128);
			pressedColor = cave.Color.white();
		}

		public override void initialize() {
			base.initialize();
			var tt = text;
			if(tt == null) {
				tt = motion.TextProperties.forText("Button");
				tt.setFontSizeAbsolute(2.00);
			}
			var bgc = backgroundColor;
			if(bgc == null) {
				bgc = cave.Color.forRGBA(0, 0, 0, 0);
			}
			boxSprite = motion.SpriteUtil.addColorSprite((motion.Scene)scene, layer, bgc, 0.10);
			textSprite = layer.addTextSprite(text);
			var th = textSprite.getHeight();
			boxSprite.resize(textSprite.getWidth() + th, th * 2);
			layoutSprites(getX(), getY());
		}

		public void resize(double width, double height) {
			if(boxSprite != null) {
				boxSprite.resize(width, height);
				onLayout();
			}
		}

		public void onPressedChanged() {
			if(pressed) {
				boxSprite.setTexture(((motion.Scene)scene).createTextureForColor(pressedColor));
			}
			else {
				boxSprite.setTexture(((motion.Scene)scene).createTextureForColor(backgroundColor));
			}
		}

		public override void cleanup() {
			base.cleanup();
			if(boxSprite != null) {
				boxSprite.removeFromContainer();
				boxSprite = null;
			}
			if(textSprite != null) {
				textSprite.removeFromContainer();
				textSprite = null;
			}
		}

		public double getWidth() {
			if(boxSprite != null) {
				return(boxSprite.getWidth());
			}
			return(0.00);
		}

		public double getHeight() {
			if(boxSprite != null) {
				return(boxSprite.getHeight());
			}
			return(0.00);
		}

		public override void layoutSprites(double x, double y) {
			boxSprite.move(x, y);
			textSprite.move(x + boxSprite.getWidth() / 2 - textSprite.getWidth() / 2, y + boxSprite.getHeight() / 2 - textSprite.getHeight() / 2);
		}

		public virtual void onClicked() {
			if(clickHandler != null) {
				clickHandler();
			}
		}

		public virtual bool onPointerEvent(cave.PointerEvent @event) {
			if(@event.isInside(getX(), getY(), getWidth(), getHeight()) == false) {
				if(pressed) {
					pressed = false;
					onPressedChanged();
				}
				return(false);
			}
			if(pressed) {
				if(pressed && @event.action == cave.PointerEvent.CANCEL) {
					pressed = false;
					onPressedChanged();
					return(false);
				}
				if(pressed && @event.action == cave.PointerEvent.UP) {
					pressed = false;
					onPressedChanged();
					onClicked();
					return(false);
				}
				return(true);
			}
			if(@event.action == cave.PointerEvent.DOWN) {
				pressed = true;
				onPressedChanged();
				return(true);
			}
			return(false);
		}

		public cave.Color getBackgroundColor() {
			return(backgroundColor);
		}

		public motion.TextButtonEntity setBackgroundColor(cave.Color v) {
			backgroundColor = v;
			return(this);
		}

		public cave.Color getPressedColor() {
			return(pressedColor);
		}

		public motion.TextButtonEntity setPressedColor(cave.Color v) {
			pressedColor = v;
			return(this);
		}

		public motion.TextProperties getText() {
			return(text);
		}

		public motion.TextButtonEntity setText(motion.TextProperties v) {
			text = v;
			return(this);
		}

		public System.Action getClickHandler() {
			return(clickHandler);
		}

		public motion.TextButtonEntity setClickHandler(System.Action v) {
			clickHandler = v;
			return(this);
		}
	}
}
