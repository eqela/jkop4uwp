
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

namespace capex.util {
	public class Matrix33
	{
		public Matrix33() {
		}

		public static capex.util.Matrix33 forZero() {
			return(capex.util.Matrix33.forValues(new double[] {
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00
			}));
		}

		public static capex.util.Matrix33 forIdentity() {
			return(capex.util.Matrix33.forValues(new double[] {
				1.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix33 invertMatrix(capex.util.Matrix33 m) {
			var d = m.v[0] * m.v[4] * m.v[8] + m.v[3] * m.v[7] * m.v[2] + m.v[6] * m.v[1] * m.v[5] - m.v[0] * m.v[7] * m.v[5] - m.v[3] * m.v[1] * m.v[8] - m.v[6] * m.v[4] * m.v[2];
			var v = new capex.util.Matrix33();
			v.v[0] = (m.v[4] * m.v[8] - m.v[7] * m.v[5]) / d;
			v.v[3] = (m.v[6] * m.v[5] - m.v[3] * m.v[8]) / d;
			v.v[6] = (m.v[3] * m.v[7] - m.v[6] * m.v[4]) / d;
			v.v[1] = (m.v[7] * m.v[2] - m.v[1] * m.v[8]) / d;
			v.v[4] = (m.v[0] * m.v[8] - m.v[6] * m.v[2]) / d;
			v.v[7] = (m.v[6] * m.v[1] - m.v[0] * m.v[7]) / d;
			v.v[2] = (m.v[1] * m.v[5] - m.v[4] * m.v[2]) / d;
			v.v[5] = (m.v[3] * m.v[2] - m.v[0] * m.v[5]) / d;
			v.v[8] = (m.v[0] * m.v[4] - m.v[3] * m.v[1]) / d;
			return(v);
		}

		public static capex.util.Matrix33 forTranslate(double translateX, double translateY) {
			return(capex.util.Matrix33.forValues(new double[] {
				1.00,
				0.00,
				translateX,
				0.00,
				1.00,
				translateY,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix33 forRotation(double angle) {
			var c = cape.Math.cos(angle);
			var s = cape.Math.sin(angle);
			return(capex.util.Matrix33.forValues(new double[] {
				c,
				s,
				0.00,
				-s,
				c,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix33 forRotationWithCenter(double angle, double centerX, double centerY) {
			var translate = capex.util.Matrix33.forTranslate(centerX, centerY);
			var rotate = capex.util.Matrix33.forRotation(angle);
			var translateBack = capex.util.Matrix33.forTranslate(-centerX, -centerY);
			var translatedRotated = capex.util.Matrix33.multiplyMatrix(translate, rotate);
			return(capex.util.Matrix33.multiplyMatrix(translatedRotated, translateBack));
		}

		public static capex.util.Matrix33 forSkew(double skewX, double skewY) {
			return(capex.util.Matrix33.forValues(new double[] {
				1.00,
				skewX,
				0.00,
				skewY,
				1.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix33 forScale(double scaleX, double scaleY) {
			return(capex.util.Matrix33.forValues(new double[] {
				scaleX,
				0.00,
				0.00,
				0.00,
				scaleY,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix33 forFlip(bool flipX, bool flipY) {
			var xmat33 = capex.util.Matrix33.forValues(new double[] {
				1.00,
				0.00,
				0.00,
				0.00,
				-1.00,
				0.00,
				0.00,
				0.00,
				1.00
			});
			var ymat33 = capex.util.Matrix33.forValues(new double[] {
				-1.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				1.00
			});
			if(flipX && flipY) {
				return(capex.util.Matrix33.multiplyMatrix(xmat33, ymat33));
			}
			else if(flipX) {
				return(xmat33);
			}
			else if(flipY) {
				return(ymat33);
			}
			return(capex.util.Matrix33.forIdentity());
		}

		public static capex.util.Matrix33 forValues(double[] mv) {
			var v = new capex.util.Matrix33();
			var i = 0;
			for(i = 0 ; i < 9 ; i++) {
				if(i >= mv.Length) {
					v.v[i] = 0.00;
				}
				else {
					v.v[i] = mv[i];
				}
			}
			return(v);
		}

		public static capex.util.Matrix33 multiplyScalar(double v, capex.util.Matrix33 mm) {
			var mat33 = capex.util.Matrix33.forZero();
			mat33.v[0] = mm.v[0] * v;
			mat33.v[1] = mm.v[1] * v;
			mat33.v[2] = mm.v[2] * v;
			mat33.v[3] = mm.v[3] * v;
			mat33.v[4] = mm.v[4] * v;
			mat33.v[5] = mm.v[5] * v;
			mat33.v[6] = mm.v[6] * v;
			mat33.v[7] = mm.v[7] * v;
			mat33.v[8] = mm.v[8] * v;
			return(mat33);
		}

		public static capex.util.Matrix33 multiplyMatrix(capex.util.Matrix33 a, capex.util.Matrix33 b) {
			var matrix33 = new capex.util.Matrix33();
			matrix33.v[0] = a.v[0] * b.v[0] + a.v[1] * b.v[3] + a.v[2] * b.v[6];
			matrix33.v[1] = a.v[0] * b.v[1] + a.v[1] * b.v[4] + a.v[2] * b.v[7];
			matrix33.v[2] = a.v[0] * b.v[2] + a.v[1] * b.v[5] + a.v[2] * b.v[8];
			matrix33.v[3] = a.v[3] * b.v[0] + a.v[4] * b.v[3] + a.v[5] * b.v[6];
			matrix33.v[4] = a.v[3] * b.v[1] + a.v[4] * b.v[4] + a.v[5] * b.v[7];
			matrix33.v[5] = a.v[3] * b.v[2] + a.v[4] * b.v[5] + a.v[5] * b.v[8];
			matrix33.v[6] = a.v[6] * b.v[0] + a.v[7] * b.v[3] + a.v[8] * b.v[6];
			matrix33.v[7] = a.v[6] * b.v[1] + a.v[7] * b.v[4] + a.v[8] * b.v[7];
			matrix33.v[8] = a.v[6] * b.v[2] + a.v[7] * b.v[5] + a.v[8] * b.v[8];
			return(matrix33);
		}

		public static capex.util.Vector2 multiplyVector(capex.util.Matrix33 a, capex.util.Vector2 b) {
			var x = a.v[0] * b.x + a.v[1] * b.y + a.v[2] * 1.00;
			var y = a.v[3] * b.x + a.v[4] * b.y + a.v[5] * 1.00;
			return(capex.util.Vector2.create(x, y));
		}

		public double[] v = new double[9];
	}
}
