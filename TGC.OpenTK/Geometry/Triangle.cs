using System;
using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometry
{
	public class Triangle
	{
		Color ColorVertice1 { get; set;}
		Color ColorVertice2 { get; set;}
		Color ColorVertice3 { get; set;}

		Vector3 Vertice1 { get; set;}
		Vector3 Vertice2 { get; set;}
		Vector3 Vertice3 { get; set;}

		public Triangle(Vector3 vertice1, Color colorVertice1, Vector3 vertice2, Color colorVertice2, Vector3 vertice3, Color colorVertice3){
			ColorVertice1 = colorVertice1;
			ColorVertice2 = colorVertice2;
			ColorVertice3 = colorVertice3;

			Vertice1 = vertice1;
			Vertice2 = vertice2;
			Vertice3 = vertice3;
		}

		public Triangle(Vector3 vertice1, Vector3 vertice2, Vector3 vertice3, Color color){
			ColorVertice1 = color;
			ColorVertice2 = color;
			ColorVertice3 = color;

			Vertice1 = vertice1;
			Vertice2 = vertice2;
			Vertice3 = vertice3;
		}

		public void Render(){
			GL.Begin(BeginMode.Triangles);
			GL.Color3(ColorVertice1);
			GL.Vertex3(Vertice1);
			GL.Color3(ColorVertice2);
			GL.Vertex3(Vertice2);
			GL.Color3(ColorVertice3);
			GL.Vertex3(Vertice3);
			GL.End();
		}
	}
}

