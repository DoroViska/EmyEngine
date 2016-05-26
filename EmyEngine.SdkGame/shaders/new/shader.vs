#version 400

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 textcoords;

uniform mat4 Projection;
uniform mat4 Model;
uniform mat4 View;
uniform mat4 Bias;


out vec2 textcoords0;
out vec4 ShadowCoord;


out vec4 vWorldVertex;
out vec3 vWorldNormal;
out vec3 vViewVec;

void main()
{
	gl_Position =  Projection * View * Model * vec4(position, 1.0);
	
	vWorldVertex = Model * vec4(position, 1.0);
	vec4 viewVertex = View * vWorldVertex;
	vWorldNormal = normalize(mat3(Model) * normal);
	vViewVec = normalize(-viewVertex.xyz);



	textcoords0 = textcoords;
	
	ShadowCoord =   Bias *  Model *vec4(position,1);
}