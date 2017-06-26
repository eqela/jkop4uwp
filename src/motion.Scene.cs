
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
	public class Scene
	{
		public Scene() {
		}

		protected motion.SceneManager manager = null;
		protected motion.Backend backend = null;
		protected cave.GuiApplicationContext context = null;

		public void setContext(cave.GuiApplicationContext value) {
			this.context = value;
		}

		public motion.Scene setManager(motion.SceneManager value) {
			manager = value;
			return(this);
		}

		public motion.SceneManager getManager() {
			return(manager);
		}

		public motion.Scene setBackend(motion.Backend value) {
			backend = value;
			return(this);
		}

		public motion.Backend getBackend() {
			return(backend);
		}

		public virtual void onPreInitialize(cave.GuiApplicationContext context) {
		}

		public virtual void initialize() {
		}

		public virtual void start() {
		}

		public virtual void onKeyEvent(cave.KeyEvent @event) {
		}

		public virtual void onPointerEvent(cave.PointerEvent @event) {
		}

		public virtual void tick(cape.TimeValue gameTime, double delta) {
		}

		public virtual void stop() {
		}

		public virtual void cleanup() {
		}

		public cave.Image createImageFromResource(string name) {
			return(backend.createImageFromResource(name));
		}

		public motion.Texture createTextureForImageResource(string name) {
			var img = createImageFromResource(name);
			if(img == null) {
				return(null);
			}
			return(createTextureForImage(img));
		}

		public motion.Texture createTextureForImage(cave.Image image) {
			return(backend.createTextureForImage(image));
		}

		public motion.Texture createTextureForColor(cave.Color color) {
			return(backend.createTextureForColor(color));
		}

		public void deleteTexture(motion.Texture texture) {
			backend.deleteTexture(texture);
		}

		public void deleteAllTextures() {
			backend.deleteAllTextures();
		}

		public void replaceScene(motion.Scene scene) {
			if(manager != null) {
				manager.replaceScene(scene);
			}
		}

		public void pushScene(motion.Scene scene) {
			if(manager != null) {
				manager.pushScene(scene);
			}
		}

		public void popScene() {
			if(manager != null) {
				manager.popScene();
			}
		}
	}
}
