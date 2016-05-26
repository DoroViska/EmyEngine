#version 400
#define MAP_NONE 0x0001
#define MAP_DEFUSE 0x0010
#define MAP_AMBIENT 0x0100
#define MAP_SPECULAR 0x1000
#define MAP_CMP( var , def_ ) ((var & def_) == def_)


uniform mat4 Projection;
uniform mat4 Model;
uniform mat4 View;
uniform mat4 Bias;

uniform vec4 ColorDefuse;
uniform vec4 ColorAmbient;
uniform vec4 ColorSpecular; 
uniform sampler2D DefuseMap;
uniform sampler2D AmbientMap;
uniform sampler2D SpecularMap;
uniform sampler2D ShadowMap;
uniform int MapActivity;

in vec4 position0;
in vec3 normal0;
in vec2 textcoords0;
in vec4 ShadowCoord;




void main()
{	
	

	vec4 CDefuse = ColorDefuse;
	vec4 CAmbient = ColorAmbient;
	vec4 CSpecular = ColorSpecular;
	if(MAP_CMP(MapActivity,MAP_DEFUSE))		{ CDefuse *= texture2D(DefuseMap,textcoords0); }
	if(MAP_CMP(MapActivity,MAP_AMBIENT))	{ CAmbient *=  texture2D(AmbientMap,textcoords0); }
	if(MAP_CMP(MapActivity,MAP_SPECULAR))	{ CSpecular *=  texture2D(SpecularMap,textcoords0); }

	
	vec3 LPos = vec3(28.4008,10.2799,28.6522);
	vec3 Normal_cameraspace = ( View  * vec4(normal0,0)).xyz; 
	vec3 Position_worldspace = (Model * position0).xyz;
	vec3 LightPosition_cameraspace = ( View * vec4(LPos,1)).xyz; 
	vec3 vertexPosition_cameraspace = ( View * Model * position0).xyz;
	vec3 EyeDirection_cameraspace = vec3(0,0,0) - vertexPosition_cameraspace;
	
	vec3 l = normalize(LightPosition_cameraspace  + EyeDirection_cameraspace);
	vec3 n = normalize(Normal_cameraspace);
	vec3 E = normalize(EyeDirection_cameraspace);
	vec3 R = reflect(-l,n);
	
	float LightPower  = 1.0;
	//vec4 LightColor = vec4(1.0,1.0 / 255.0 * 220.0,1.0 / 255.0 * 115.0,1.0);
	vec4 LightColor = vec4(1.0,1.0,1.0,1.0);
	float cosTheta = clamp( dot( n,l ), 0.0,1.0);
	float cosAlpha = clamp( dot( E,R ), 0.0,1.0);
	
	float bias = 0.001 * tan(acos(cosTheta)); 
    bias = clamp(bias, 0.0,0.01);
	float visibility = 1.0;

	if ( texture( ShadowMap, (ShadowCoord.xy/ShadowCoord.w) ).z  <   (ShadowCoord.z- bias)/ShadowCoord.w )
	{	
		visibility = 0.01;
	}
	
	 
	
	vec4 color = (CAmbient) + 
				 (CDefuse * visibility*  LightColor * LightPower  * cosTheta / 1.0) + 
				 (CSpecular* visibility * LightColor * LightPower * pow(cosAlpha,5));

	gl_FragColor = color;
}