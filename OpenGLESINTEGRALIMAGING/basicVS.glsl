//varying vec4 vertexWorldPos;

uniform int flag;

uniform int flag_two_dimension;

void main()
{
	if (flag== 0)
	{
		 gl_FrontColor = gl_Color;
	}
	else
	{
		gl_TexCoord[0] = gl_MultiTexCoord0;
	}
   
	if(flag_two_dimension == 1)
	{
		vec4 temp_vec = vec4(gl_Vertex.x, gl_Vertex.y, 0.0,1.0);
		gl_Position = (gl_ModelViewProjectionMatrix * temp_vec);
	}
	else
	{
		gl_Position = ftransform();
	}

	//vertexWorldPos = gl_ModelViewMatrixInverse * gl_Position;
}