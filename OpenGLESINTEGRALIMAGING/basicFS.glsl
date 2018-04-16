
uniform sampler2D colorMap;

uniform vec4 color = vec4(1.0, 0.0, 0.0,1.0);

//varying vec4 vertexWorldPos;

uniform int flag;


void main()
{
	//vec3 pos = gl_Position;
	if (flag== 0)
	{
		gl_FragColor = gl_Color;
	}
	else
	{
		gl_FragColor = texture2D(colorMap, gl_TexCoord[0].st);
	}
	//gl_FragColor = gl_Color;

	//gl_FragColor = vec4(vec3(gl_TexCoord[0]), 1.0);
}