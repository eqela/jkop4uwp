
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

namespace capex.util
{
	public class Matrix44
	{
		public Matrix44() {
		}

		public static capex.util.Matrix44 forZero() {
			return(capex.util.Matrix44.forValues(new double[] {
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
				0.00,
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

		public static capex.util.Matrix44 forIdentity() {
			return(capex.util.Matrix44.forValues(new double[] {
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forTranslate(double translateX, double translateY, double translateZ) {
			return(capex.util.Matrix44.forValues(new double[] {
				1.00,
				0.00,
				0.00,
				translateX,
				0.00,
				1.00,
				0.00,
				translateY,
				0.00,
				0.00,
				1.00,
				translateZ,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forXRotation(double angle) {
			var c = cape.Math.cos(angle);
			var s = cape.Math.sin(angle);
			return(capex.util.Matrix44.forValues(new double[] {
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				c,
				-s,
				0.00,
				0.00,
				s,
				c,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forYRotation(double angle) {
			var c = cape.Math.cos(angle);
			var s = cape.Math.sin(angle);
			return(capex.util.Matrix44.forValues(new double[] {
				c,
				0.00,
				s,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				-s,
				0.00,
				c,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forZRotation(double angle) {
			var c = cape.Math.cos(angle);
			var s = cape.Math.sin(angle);
			return(capex.util.Matrix44.forValues(new double[] {
				c,
				-s,
				0.00,
				0.00,
				s,
				c,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forSkew(double vx, double vy, double vz) {
			return(capex.util.Matrix44.forValues(new double[] {
				1.00,
				vx,
				vx,
				0.00,
				vy,
				1.00,
				vy,
				0.00,
				vz,
				vz,
				1.00,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forXRotationWithCenter(double angle, double centerX, double centerY, double centerZ) {
			var translate = capex.util.Matrix44.forTranslate(centerX, centerY, centerZ);
			var rotate = capex.util.Matrix44.forXRotation(angle);
			var translateBack = capex.util.Matrix44.forTranslate(-centerX, -centerY, -centerZ);
			var translatedRotated = capex.util.Matrix44.multiplyMatrix(translate, rotate);
			return(capex.util.Matrix44.multiplyMatrix(translatedRotated, translateBack));
		}

		public static capex.util.Matrix44 forYRotationWithCenter(double angle, double centerX, double centerY, double centerZ) {
			var translate = capex.util.Matrix44.forTranslate(centerX, centerY, centerZ);
			var rotate = capex.util.Matrix44.forYRotation(angle);
			var translateBack = capex.util.Matrix44.forTranslate(-centerX, -centerY, -centerZ);
			var translatedRotated = capex.util.Matrix44.multiplyMatrix(translate, rotate);
			return(capex.util.Matrix44.multiplyMatrix(translatedRotated, translateBack));
		}

		public static capex.util.Matrix44 forZRotationWithCenter(double angle, double centerX, double centerY, double centerZ) {
			var translate = capex.util.Matrix44.forTranslate(centerX, centerY, centerZ);
			var rotate = capex.util.Matrix44.forZRotation(angle);
			var translateBack = capex.util.Matrix44.forTranslate(-centerX, -centerY, -centerZ);
			var translatedRotated = capex.util.Matrix44.multiplyMatrix(translate, rotate);
			return(capex.util.Matrix44.multiplyMatrix(translatedRotated, translateBack));
		}

		public static capex.util.Matrix44 forScale(double scaleX, double scaleY, double scaleZ) {
			return(capex.util.Matrix44.forValues(new double[] {
				scaleX,
				0.00,
				0.00,
				0.00,
				0.00,
				scaleY,
				0.00,
				0.00,
				0.00,
				0.00,
				scaleZ,
				0.00,
				0.00,
				0.00,
				0.00,
				1.00
			}));
		}

		public static capex.util.Matrix44 forFlipXY(bool flipXY) {
			if(flipXY) {
				return(capex.util.Matrix44.forValues(new double[] {
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					-1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00
				}));
			}
			return(capex.util.Matrix44.forIdentity());
		}

		public static capex.util.Matrix44 forFlipXZ(bool flipXZ) {
			if(flipXZ) {
				return(capex.util.Matrix44.forValues(new double[] {
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					-1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00
				}));
			}
			return(capex.util.Matrix44.forIdentity());
		}

		public static capex.util.Matrix44 forFlipYZ(bool flipYZ) {
			if(flipYZ) {
				return(capex.util.Matrix44.forValues(new double[] {
					-1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00,
					0.00,
					0.00,
					0.00,
					0.00,
					1.00
				}));
			}
			return(capex.util.Matrix44.forIdentity());
		}

		public static capex.util.Matrix44 forValues(double[] mv) {
			var v = new capex.util.Matrix44();
			var i = 0;
			for(i = 0 ; i < 16 ; i++) {
				if(i >= mv.Length) {
					v.v[i] = 0.00;
				}
				else {
					v.v[i] = mv[i];
				}
			}
			return(v);
		}

		public static capex.util.Matrix44 multiplyScalar(double v, capex.util.Matrix44 mm) {
			return(capex.util.Matrix44.forValues(new double[] {
				mm.v[0] * v,
				mm.v[1] * v,
				mm.v[2] * v,
				mm.v[3] * v,
				mm.v[4] * v,
				mm.v[5] * v,
				mm.v[6] * v,
				mm.v[7] * v,
				mm.v[8] * v,
				mm.v[9] * v,
				mm.v[10] * v,
				mm.v[11] * v,
				mm.v[12] * v,
				mm.v[3] * v,
				mm.v[14] * v,
				mm.v[15] * v
			}));
		}

		public static capex.util.Matrix44 multiplyMatrix(capex.util.Matrix44 a, capex.util.Matrix44 b) {
			var matrix44 = new capex.util.Matrix44();
			matrix44.v[0] = (((a.v[0] * b.v[0]) + (a.v[1] * b.v[4])) + (a.v[2] * b.v[8])) + (a.v[3] * b.v[12]);
			matrix44.v[1] = (((a.v[0] * b.v[1]) + (a.v[1] * b.v[5])) + (a.v[2] * b.v[9])) + (a.v[3] * b.v[13]);
			matrix44.v[2] = (((a.v[0] * b.v[2]) + (a.v[1] * b.v[6])) + (a.v[2] * b.v[10])) + (a.v[3] * b.v[14]);
			matrix44.v[3] = (((a.v[0] * b.v[3]) + (a.v[1] * b.v[7])) + (a.v[2] * b.v[11])) + (a.v[3] * b.v[15]);
			matrix44.v[4] = (((a.v[4] * b.v[0]) + (a.v[5] * b.v[4])) + (a.v[6] * b.v[8])) + (a.v[7] * b.v[12]);
			matrix44.v[5] = (((a.v[4] * b.v[1]) + (a.v[5] * b.v[5])) + (a.v[6] * b.v[9])) + (a.v[7] * b.v[13]);
			matrix44.v[6] = (((a.v[4] * b.v[2]) + (a.v[5] * b.v[6])) + (a.v[6] * b.v[10])) + (a.v[7] * b.v[14]);
			matrix44.v[7] = (((a.v[4] * b.v[3]) + (a.v[5] * b.v[7])) + (a.v[6] * b.v[11])) + (a.v[7] * b.v[15]);
			matrix44.v[8] = (((a.v[8] * b.v[0]) + (a.v[9] * b.v[4])) + (a.v[10] * b.v[8])) + (a.v[11] * b.v[12]);
			matrix44.v[9] = (((a.v[8] * b.v[1]) + (a.v[9] * b.v[5])) + (a.v[10] * b.v[9])) + (a.v[11] * b.v[13]);
			matrix44.v[10] = (((a.v[8] * b.v[2]) + (a.v[9] * b.v[6])) + (a.v[10] * b.v[10])) + (a.v[11] * b.v[14]);
			matrix44.v[11] = (((a.v[8] * b.v[3]) + (a.v[9] * b.v[7])) + (a.v[10] * b.v[11])) + (a.v[11] * b.v[15]);
			matrix44.v[12] = (((a.v[12] * b.v[0]) + (a.v[13] * b.v[4])) + (a.v[14] * b.v[8])) + (a.v[15] * b.v[12]);
			matrix44.v[13] = (((a.v[12] * b.v[1]) + (a.v[13] * b.v[5])) + (a.v[14] * b.v[9])) + (a.v[15] * b.v[13]);
			matrix44.v[14] = (((a.v[12] * b.v[2]) + (a.v[13] * b.v[6])) + (a.v[14] * b.v[10])) + (a.v[15] * b.v[14]);
			matrix44.v[15] = (((a.v[12] * b.v[3]) + (a.v[13] * b.v[7])) + (a.v[14] * b.v[11])) + (a.v[15] * b.v[15]);
			return(matrix44);
		}

		public static capex.util.Vector3 multiplyVector(capex.util.Matrix44 a, capex.util.Vector3 b) {
			var x = (((a.v[0] * b.x) + (a.v[1] * b.y)) + (a.v[2] * b.z)) + (a.v[3] * 1.00);
			var y = (((a.v[4] * b.x) + (a.v[5] * b.y)) + (a.v[6] * b.z)) + (a.v[7] * 1.00);
			var z = (((a.v[8] * b.x) + (a.v[9] * b.y)) + (a.v[10] * b.z)) + (a.v[11] * 1.00);
			return(capex.util.Vector3.create(x, y, z));
		}

		public double[] v = new double[16];
	}
}
