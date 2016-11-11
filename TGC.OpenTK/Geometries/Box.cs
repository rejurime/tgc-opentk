using System.Drawing;
using OpenTK;

namespace TGC.OpenTK.Geometries
{
	/// <summary>
	///     Caja 3D de tamaño variable, con color y textura.
	/// </summary>
	public class Box : Geometry
	{
		/// <summary>
		///     Color de los vertices de la caja
		/// </summary>
		public Color Color { get; set;}

		public Box(Vector3[] positionVboData, int[] indicesVboData) : base(positionVboData, indicesVboData)
		{
			
		}

		/// <summary>
		///     Crea una caja con centro (0,0,0) y de tamaño 1.
		/// </summary>
		public Box() : this(Vector3.Zero, Vector3.One)
		{

		}

		/// <summary>
		///     Crea una caja con centro (0,0,0) y el tamaño especificado.
		/// </summary>
		/// <param name="size">Tamaño de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 size) : this(Vector3.Zero, size)
		{

		}


        /// <summary>
        ///     Crea una caja con el centro y tamaño especificado.
        /// </summary>
        /// <param name="center">Centro de la caja</param>
        /// <param name="size">Tamaño de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 center, Vector3 size)
		{
			Color = Color.White;

			var x = size.X / 2;
			var y = size.Y / 2;
			var z = size.Z / 2;

			PositionVboData = new[]{
				new Vector3(-x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, y + center.Y, -z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, -z  + center.Z)};

			IndicesVboData = new[] {
                // Top face
                3, 2, 6, 6, 7, 3,
                // Back face
                7, 6, 5, 5, 4, 7,
                // Left face
                4, 0, 3, 3, 7, 4,
                // Bottom face
                0, 1, 5, 5, 4, 0,
                // Right face
				1, 5, 6, 6, 2, 1 };

			CreateVBOs();
			CreateVAOs();
		}

		/// <summary>
		///     Crea una caja con el centro y tamaño especificado, con el color especificado
		/// </summary>
		/// <param name="center">Centro de la caja</param>
		/// <param name="size">Tamaño de la caja</param>
		/// <param name="color">Color de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 center, Vector3 size, Color color): this(center, size)
		{
			Color = color;
		}
	}
}