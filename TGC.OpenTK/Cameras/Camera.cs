using OpenTK;

namespace TGC.OpenTK
{
	/// <summary>
	///     Camara estatica.
	/// </summary>
	public class Camera
	{
		protected readonly Vector3 DEFAULT_UP_VECTOR = new Vector3(0.0f, 1.0f, 0.0f);

		/// <summary>
		///     Posicion de la camara.
		/// </summary>
		public Vector3 Position { get; set; }

		/// <summary>
		///     Posicion del punto al que mira la camara.
		/// </summary>
		public Vector3 LookAt { get; set; }

		/// <summary>
		///     Vector direccion hacia arriba (puede diferir si la camara se invierte).
		/// </summary>
		Vector3 UpVector { get; set; }
	}
}