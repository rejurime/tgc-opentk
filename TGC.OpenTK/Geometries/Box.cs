using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometries
{
	/// <summary>
	///     Caja 3D de tamaño variable, con color y textura.
	/// </summary>
	public class Box : Geometry
	{
		int vertexArrayObjectsHandle;
		int positionVboHandle;
		int elementBufferObjectsHandle;

		public Box(Vector3[] positionVboData, int[] indicesVboData)
		{
			PositionVertexBufferObjectData = positionVboData;
			IndicesVertexBufferObjectData = indicesVboData;

			CreateVertexBufferObjects();
			CreateVertexArrayObjects();
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
			ColorVertexBufferObjectData = new Vector3[] { new Vector3(1f, 0f, 0f),
				new Vector3( 0f, 0f, 1f), new Vector3( 0f,  1f, 0f),new Vector3(1f, 0f, 0f),
				new Vector3( 0f, 0f, 1f), new Vector3( 0f,  1f, 0f),new Vector3(1f, 0f, 0f),
				new Vector3( 0f, 0f, 1f)};

			var x = size.X / 2;
			var y = size.Y / 2;
			var z = size.Z / 2;

			PositionVertexBufferObjectData = new[]{
				new Vector3(-x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, y + center.Y, -z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, -z  + center.Z)};

			IndicesVertexBufferObjectData = new[] {
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

			CreateVertexBufferObjects();
			CreateVertexArrayObjects();
		}

		/// <summary>
		///     Crea una caja con el centro y tamaño especificado, con el color especificado
		/// </summary>
		/// <param name="center">Centro de la caja</param>
		/// <param name="size">Tamaño de la caja</param>
		/// <param name="color">Color de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 center, Vector3 size, Color color) : this(center, size)
		{
			Vector3 vectorColor = new Vector3(color.R, color.G, color.B);
			ColorVertexBufferObjectData = new Vector3[] { vectorColor, vectorColor, vectorColor, vectorColor, vectorColor, vectorColor, vectorColor, vectorColor };
		}

		internal void CreateVertexBufferObjects()
		{
			positionVboHandle = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(PositionVertexBufferObjectData.Length * Vector3.SizeInBytes), PositionVertexBufferObjectData, BufferUsageHint.StaticDraw);

			elementBufferObjectsHandle = GL.GenBuffer();
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObjectsHandle);
			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * IndicesVertexBufferObjectData.Length), IndicesVertexBufferObjectData, BufferUsageHint.StaticDraw);
		}

		internal void CreateVertexArrayObjects()
		{
			vertexArrayObjectsHandle = GL.GenVertexArray();
			GL.BindVertexArray(vertexArrayObjectsHandle);

			GL.EnableVertexAttribArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObjectsHandle);
		}

		public override void Render()
		{
			GL.BindVertexArray(vertexArrayObjectsHandle);
			GL.DrawElements(PrimitiveType.Triangles, IndicesVertexBufferObjectData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}

		public override void Update()
		{

		}
	}
}