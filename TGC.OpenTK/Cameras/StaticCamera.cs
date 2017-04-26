using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK
{
	/// <summary>
	///     Camara estatica.
	/// </summary>
	public class StaticCamera
	{
		/// <summary>
		///     Posicion de la camara.
		/// </summary>
		Vector3 Position { get; set; }

		/// <summary>
		///     Posicion del punto al que mira la camara.
		/// </summary>
		Vector3 LookAt { get; set; }

		/// <summary>
		///     Vector direccion hacia arriba (puede diferir si la camara se invierte).
		/// </summary>
		Vector3 UpVector { get; set; }

		Matrix4 projectionMatrix;

		/// <summary>
		///     Configura la posicion de la camara, hacia donde apunta y con el vector arriba (0,1,0).
		/// </summary>
		/// <param name="pos">Posicion de la camara</param>
		/// <param name="lookAt">Punto hacia el cual se quiere ver</param>
		public StaticCamera(Vector3 pos, Vector3 lookAt, float aspectRatio) : this(pos, lookAt, aspectRatio, Vector3.UnitY)
		{
		}

		/// <summary>
		///     Configura la posicion de la camara, hacia donde apunta y cual es el vector arriba.
		/// </summary>
		/// <param name="pos">Posicion de la camara</param>
		/// <param name="lookAt">Punto hacia el cual se quiere ver</param>
		/// <param name="upVector">Vector direccion hacia arriba</param>
		public StaticCamera(Vector3 pos, Vector3 lookAt, float aspectRatio, Vector3 upVector)
		{
			Position = pos;
			LookAt = lookAt;
			UpVector = upVector;
			projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, aspectRatio, 1, 100);
		}

        /// <summary>
        ///     Devuelve la matriz View en base a los valores de la camara. Es invocado en cada update de render.
        /// </summary>
        public Matrix4 GetViewMatrix()
		{
			return Matrix4.LookAt(Position, LookAt, UpVector);
		}

		internal void UpdateFieldOfView(float aspectRatio)
		{
			projectionMatrix = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, aspectRatio, 1.0f, 64.0f);
			//TODO MatrixMode deprecated in 3.2
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projectionMatrix);
		}

		internal void UpdateModelView()
		{
			Matrix4 modelview = GetViewMatrix();
			//TODO deprecated in v3.2
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);
		}
}
}