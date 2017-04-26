using OpenTK;
using System.Drawing;

namespace TGC.OpenTK.Geometries
{
	public class Triangle : Geometry
	{
		public Triangle(Vector3 vertice1, Color colorVertice1, Vector3 vertice2, Color colorVertice2, Vector3 vertice3, Color colorVertice3)
		{
			PositionVertexBufferObjectData = new Vector3[] { vertice1, vertice2, vertice3 };
			IndicesVertexBufferObjectData = new[] { 0, 1, 2 };
			ColorVertexBufferObjectData = new Vector3[] { new Vector3(colorVertice1.R, colorVertice1.G, colorVertice1.B), new Vector3(colorVertice2.R, colorVertice2.G, colorVertice2.B), new Vector3(colorVertice3.R, colorVertice3.G, colorVertice3.B) };
		}

		public Triangle(Vector3 vertice1, Vector3 vertice2, Vector3 vertice3, Color colorVertice): this(vertice1, colorVertice, vertice2, colorVertice, vertice3, colorVertice)
		{
		}

		public override void Render()
		{

		}

		public override void Update()
		{

		}
	}
}