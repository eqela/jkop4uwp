
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
	public class SpriteUtil
	{
		public SpriteUtil() {
		}

		public static motion.TextureSprite addColorSprite(motion.Scene scene, motion.SpriteLayer layer, cave.Color color, double width) {
			return(motion.SpriteUtil.addColorSprite(scene, layer, color, width, 0.00));
		}

		public static motion.TextureSprite addColorSprite(motion.Scene scene, motion.SpriteLayer layer, cave.Color color, double width, double height) {
			var text = scene.createTextureForColor(color);
			if(text == null) {
				return(null);
			}
			return(layer.addTextureSpriteForSize(text, width, height));
		}

		public static motion.TextureSprite addTextureSpriteForWidth(motion.Scene scene, motion.SpriteLayer layer, motion.Texture texture, double width) {
			return(layer.addTextureSpriteForSize(texture, width, 0.00));
		}

		public static motion.TextureSprite addTextureSpriteForHeight(motion.Scene scene, motion.SpriteLayer layer, motion.Texture texture, double height) {
			return(layer.addTextureSpriteForSize(texture, 0.00, height));
		}

		public static motion.TextureSprite addTextureSpriteForSize(motion.Scene scene, motion.SpriteLayer layer, motion.Texture texture, double width, double height) {
			return(layer.addTextureSpriteForSize(texture, width, height));
		}

		public static motion.TextSprite addTextSprite(motion.Scene scene, motion.SpriteLayer layer, string text) {
			return(layer.addTextSprite(motion.TextProperties.forText(text)));
		}

		public static motion.TextSprite addTextSpriteWithAbsoluteSize(motion.Scene scene, motion.SpriteLayer layer, string text, double size) {
			var tp = motion.TextProperties.forText(text);
			tp.setFontSizeAbsolute(size);
			return(layer.addTextSprite(tp));
		}

		public static motion.TextSprite addTextSpriteWithRelativeSize(motion.Scene scene, motion.SpriteLayer layer, string text, double size) {
			var tp = motion.TextProperties.forText(text);
			tp.setFontSizeRelative(size);
			return(layer.addTextSprite(tp));
		}
	}
}
