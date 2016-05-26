#version 400
#define MAP_NONE 0x0001
#define MAP_DEFUSE 0x0010
#define MAP_AMBIENT 0x0100
#define MAP_SPECULAR 0x1000
#define MAP_CMP( var , def_ ) ((var & def_) == def_)


struct LightSource
{
	int Type;
	vec3 Position;
	vec3 Attenuation;
	vec3 Direction;
	vec3 Colour;
	float OuterCutoff;
	float InnerCutoff;
	float Exponent;
};

uniform int NumLight;
uniform LightSource Light[10];


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


in vec2 textcoords0;
in vec4 ShadowCoord;

in vec4 vWorldVertex;
in vec3 vWorldNormal;
in vec3 vViewVec;


void main()
{	
	

	vec4 CDefuse = ColorDefuse;
	vec4 CAmbient = ColorAmbient;
	vec4 CSpecular = ColorSpecular;
	if(MAP_CMP(MapActivity,MAP_DEFUSE))		{ CDefuse *= texture2D(DefuseMap,textcoords0); }
	if(MAP_CMP(MapActivity,MAP_AMBIENT))	{ CAmbient *=  texture2D(AmbientMap,textcoords0); }
	if(MAP_CMP(MapActivity,MAP_SPECULAR))	{ CSpecular *=  texture2D(SpecularMap,textcoords0); }
	
	float bias = 0.001; 
    bias = clamp(bias, 0.0,0.01);
	float visibility = 1.0;

	if ( texture( ShadowMap, (ShadowCoord.xy/ShadowCoord.w) ).z  <   (ShadowCoord.z- bias)/ShadowCoord.w )
	{	
		visibility = 0.01;
	}
	
    vec3 normal = normalize(vWorldNormal);

	vec3 colour = CAmbient.xyz;
	for (int i = 0; i < NumLight; i++)
	{
		// Calculate diffuse term
		vec3 lightVec = normalize(Light[i].Position - vWorldVertex.xyz);
		float l = dot(normal, lightVec);
		if ( l > 0.0 )
		{
			
			// Calculate spotlight effect
			float spotlight = 1.0;
			if ( Light[i].Type == 1 )
			{
				spotlight = max(-dot(lightVec, Light[i].Direction), 0.0);
				float spotlightFade = clamp((Light[i].OuterCutoff - spotlight) / (Light[i].OuterCutoff - Light[i].InnerCutoff), 0.0, 1.0);
				spotlight = pow(spotlight * spotlightFade, Light[i].Exponent);
			}
			
			// Calculate specular term
			vec3 r = -normalize(reflect(lightVec, normal));
			float s = pow(max(dot(r, vViewVec), 0.0), 10.0);
			
			// Calculate attenuation factor
			float d = distance(vWorldVertex.xyz, Light[i].Position);
			float a = 1.0 / (Light[i].Attenuation.x + (Light[i].Attenuation.y * d) + (Light[i].Attenuation.z * d * d));
			
			// Add to colour
			colour += ((CDefuse.xyz * l) + (CSpecular.xyz * s)) * Light[i].Colour * a * spotlight;
			
		}
	}
	
	gl_FragColor = vec4(colour * visibility, CDefuse.a);
}