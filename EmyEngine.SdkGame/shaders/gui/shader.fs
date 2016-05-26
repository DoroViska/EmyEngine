#version 400
#define MAP_NONE 0x0001
#define MAP_DEFUSE 0x0010
#define MAP_AMBIENT 0x0100
#define MAP_SPECULAR 0x1000
#define MAP_CMP( var , def_ ) ((var & def_) == def_)
uniform vec4 ColorDefuse;
uniform vec4 ColorAmbient;
uniform vec4 ColorSpecular; 
uniform sampler2D DefuseMap;
uniform sampler2D AmbientMap;
uniform sampler2D SpecularMap;
uniform int MapActivity;
in vec4 position0;
in vec2 textcoords0;

uniform vec2 ClipMin;
uniform vec2 ClipMax;

bool CursorCollusion(in vec2 c1Min,in vec2 c1Max,in vec2 c2Min,in vec2 c2Max)
{
	if ((c1Max.x >= c2Min.x && c1Min.x <= c2Max.x) == false) return false;
	if ((c1Max.y >= c2Min.y && c1Min.y <= c2Max.y) == false) return false;
	return true;
}


void main(){
    vec4 CDefuse = ColorDefuse;
	vec4 CAmbient = ColorAmbient;
	//vec4 CSpecular = ColorSpecular;
	if(MAP_CMP(MapActivity,MAP_DEFUSE))		{ CDefuse *= texture2D(DefuseMap,textcoords0); }
	if(MAP_CMP(MapActivity,MAP_AMBIENT))	{ CAmbient *=  texture2D(AmbientMap,textcoords0); }
	//if(MAP_CMP(MapActivity,MAP_SPECULAR))	{ CSpecular *=  texture2D(SpecularMap,textcoords0); }
	//CDefuse = vec4(1.0,1.0,1.0,1.0);
	vec2 v = position0.xy;
	if(CursorCollusion(v,v,ClipMin,ClipMax))	
		gl_FragColor = CDefuse * CAmbient;		
	else
		gl_FragColor = vec4(0.0,0.0,0.0,0.0);
}


