#version 130

varying vec3 vertex_color;

void main()
{
  //gl_Position = gl_ModelViewProjectionMatrix * gl_Vertex;
  gl_Position = ftransform();
  vertex_color = gl_Vertex.xyz;
}