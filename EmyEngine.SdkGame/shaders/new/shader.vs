#version 400

layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 textcoords;

uniform mat4 Projection;
uniform mat4 Model;
uniform mat4 View;
uniform mat4 Bias;



out vec4 position0;
out vec3 normal0;
out vec2 textcoords0;
out vec4 ShadowCoord;
void main()
{
	gl_Position =  Projection * View * Model * vec4(position, 1.0);
	
	position0 = vec4(position, 1.0);
	normal0 = (View * Model  * vec4(normal, 0.0)).xyz;	
	textcoords0 = textcoords;
	
	ShadowCoord =   Bias *  Model *vec4(position,1);
}