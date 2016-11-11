using OpenTK;

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

		/// <summary>
		///     Configura la posicion de la camara, hacia donde apunta y con el vector arriba (0,1,0).
		/// </summary>
		/// <param name="pos">Posicion de la camara</param>
		/// <param name="lookAt">Punto hacia el cual se quiere ver</param>
		public StaticCamera(Vector3 pos, Vector3 lookAt) : this(pos, lookAt, Vector3.UnitY)
		{
			
		}

		/// <summary>
		///     Configura la posicion de la camara, hacia donde apunta y cual es el vector arriba.
		/// </summary>
		/// <param name="pos">Posicion de la camara</param>
		/// <param name="lookAt">Punto hacia el cual se quiere ver</param>
		/// <param name="upVector">Vector direccion hacia arriba</param>
		public StaticCamera(Vector3 pos, Vector3 lookAt, Vector3 upVector)
		{
			Position = pos;
			LookAt = lookAt;
			UpVector = upVector;
		}

        /// <summary>
        ///     Devuelve la matriz View en base a los valores de la camara. Es invocado en cada update de render.
        /// </summary>
        public Matrix4 GetViewMatrix()
		{
			return Matrix4.LookAt(Position, LookAt, UpVector);
		}
	}
}