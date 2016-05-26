#version 400
layout(location = 0) in vec3 position;
layout(location = 1) in vec3 normal;
layout(location = 2) in vec2 textcoords;

uniform mat4 Projection;
uniform mat4 Model;
uniform mat4 View;


out vec4 position0;
out vec2 textcoords0;


void main()
{	
	gl_Position =  Projection * View * Model * vec4(position, 1.0);	
	position0 = Model * vec4(position, 1.0);
	textcoords0 = textcoords;
	
}