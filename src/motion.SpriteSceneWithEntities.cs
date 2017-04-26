
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
	public class SpriteSceneWithEntities : motion.SpriteScene, motion.EntityScene
	{
		public SpriteSceneWithEntities() : base() {
		}

		private motion.Entity[] entities = null;
		private System.Collections.Generic.List<cave.PointerListener> pointerListeners = new System.Collections.Generic.List<cave.PointerListener>();
		private System.Collections.Generic.List<cave.KeyListener> keyListeners = new System.Collections.Generic.List<cave.KeyListener>();
		private int highestIndex = -1;
		private cave.PointerListener capturedPointerListener = null;

		public override void cleanup() {
			base.cleanup();
			removeAllEntities();
		}

		public override void tick(cape.TimeValue gameTime, double delta) {
			base.tick(gameTime, delta);
			if(entities != null) {
				var n = 0;
				var m = entities.Length;
				for(n = 0 ; n < m ; n++) {
					var entity = entities[n];
					if(entity != null) {
						if(entity == null) {
							continue;
						}
						entity.tick(gameTime, delta);
					}
				}
			}
		}

		public override void onKeyEvent(cave.KeyEvent @event) {
			if(keyListeners != null) {
				var n = 0;
				var m = keyListeners.Count;
				for(n = 0 ; n < m ; n++) {
					var keyListener = keyListeners[n];
					if(keyListener != null) {
						keyListener.onKeyEvent(@event);
						if(@event.isConsumed) {
							break;
						}
					}
				}
			}
		}

		public override void onPointerEvent(cave.PointerEvent @event) {
			base.onPointerEvent(@event);
			if(capturedPointerListener != null) {
				if(capturedPointerListener.onPointerEvent(@event) == false) {
					capturedPointerListener = null;
				}
				@event.consume();
				return;
			}
			if(pointerListeners != null) {
				var n = 0;
				var m = pointerListeners.Count;
				for(n = 0 ; n < m ; n++) {
					var pointerListener = pointerListeners[n];
					if(pointerListener != null) {
						if(@event.isConsumed) {
							break;
						}
						if(pointerListener.onPointerEvent(@event)) {
							@event.consume();
							capturedPointerListener = pointerListener;
							break;
						}
					}
				}
			}
		}

		public void grow() {
			var nsz = 0;
			if(entities == null) {
				nsz = 1024;
			}
			else {
				nsz = entities.Length * 2;
			}
			var v = new motion.Entity[nsz];
			if(entities != null) {
				var osz = entities.Length;
				for(var n = 0 ; n < osz ; n++) {
					v[n] = entities[n];
				}
			}
			entities = v;
		}

		public virtual void addEntity(motion.Entity entity) {
			if(entity == null) {
				return;
			}
			var thisIndex = highestIndex + 1;
			var count = 0;
			if(entities != null) {
				count = entities.Length;
			}
			if(thisIndex >= count) {
				grow();
			}
			entity.setScene((motion.EntityScene)this);
			entities[thisIndex] = entity;
			entity.index = thisIndex;
			highestIndex = thisIndex;
			if(entity is cave.PointerListener) {
				pointerListeners.Add((cave.PointerListener)entity);
			}
			if(entity is cave.KeyListener) {
				keyListeners.Add((cave.KeyListener)entity);
			}
			entity.initialize();
		}

		public virtual void removeEntity(motion.Entity entity) {
			if(entity == null) {
				return;
			}
			var eidx = entity.index;
			if(eidx < 0) {
				return;
			}
			if(entity is cave.PointerListener) {
				cape.Vector.removeValue(pointerListeners, (cave.PointerListener)entity);
			}
			if(entity is cave.KeyListener) {
				cape.Vector.removeValue(keyListeners, (cave.KeyListener)entity);
			}
			entity.cleanup();
			entity.setScene(null);
			entity.index = -1;
			if(highestIndex == eidx) {
				entities[eidx] = null;
			}
			else {
				var n = entities[highestIndex];
				entities[eidx] = n;
				if(n != null) {
					n.index = eidx;
				}
				entities[highestIndex] = null;
			}
			highestIndex--;
		}

		public virtual void removeAllEntities() {
			if(entities == null) {
				return;
			}
			var esz = entities.Length;
			for(var n = 0 ; n < esz ; n++) {
				var e = entities[n];
				if(e == null) {
					continue;
				}
				e.cleanup();
				e.setScene(null);
				e.index = -1;
			}
			entities = null;
		}
	}
}
