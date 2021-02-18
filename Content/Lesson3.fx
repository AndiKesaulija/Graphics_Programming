#if OPENGL
#define VS_SHADERMODEL vs_3_0
#define PS_SHADERMODEL ps_3_0
#else
#define VS_SHADERMODEL vs_4_0_level_9_1
#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

// External Properties
float4x4 World, View, Projection;


float3 LightPosition;
float3 AmbientPosition;
float3 CameraPosition;

Texture2D DayTex;
sampler2D DayTextureSampler = sampler_state
{
    Texture = <DayTex>;
    MipFilter = POINT;
    MinFilter = ANISOTROPIC;
    MagFilter = ANISOTROPIC;
};
Texture2D NormalTex;
sampler2D NormalTextureSampler = sampler_state
{
    Texture = <NormalTex>;
    MipFilter = POINT;
    MinFilter = ANISOTROPIC;
    MagFilter = ANISOTROPIC;
};

Texture2D SpecularTex;
sampler2D SpecularTextureSampler = sampler_state
{
    Texture = <SpecularTex>;
    MipFilter = POINT;
    MinFilter = ANISOTROPIC;
    MagFilter = ANISOTROPIC;
};



// Getting out vertex data from vertex shader to pixel shader
struct VertexShaderOutput {
    float4 position     : SV_POSITION;
    float4 color        : COLOR0;
    float2 uv           : TEXCOORD0;
    float3 worldPos     : TEXCOORD1;
    float3 worldNormal  : TEXCOORD2;
};

// Vertex shader, receives values directly from semantic channels
VertexShaderOutput MainVS(float4 position : POSITION, float4 color : COLOR0, float2 uv : TEXCOORD, float3 normal : NORMAL)
{
    VertexShaderOutput output = (VertexShaderOutput)0;

    output.position = mul(mul(mul(position, World), View), Projection);
    output.color = color;
    output.uv = uv;

    output.worldPos = mul(position, World);
    output.worldNormal = mul(normal, World);

    return output;
}

// Pixel Shader, receives input from vertex shader, and outputs to COLOR semantic
float4 MainPS(VertexShaderOutput input) : COLOR
{
    float4 texColor = tex2D(DayTextureSampler, input.uv);

    //Normals
    //float3 perturbedNormal = input.worldNormal;
    //perturbedNormal.rg += (normalColor.rg * 2 - 1);//Red,Green
    //perturbedNormal = normalize(perturbedNormal);
    //Lights
    float3 lightDirection = normalize(input.worldPos - LightPosition);
    float3 AmbientDirection = normalize(input.worldPos - AmbientPosition);

    float light = max(dot(input.worldPos, lightDirection), 0.0);//normal
    float ambient = max(dot(input.worldPos, -AmbientDirection), 0.0);

    ////SpecularSettings
    //float powStrength = 15;
    //float ambientStrengh = 3;
    ////SpecularMap
    //float3 perturbedSpecular = input.worldNormal;
    //perturbedSpecular.rgb += specularColor.rgb;
    //perturbedSpecular = normalize(perturbedSpecular);

    ////Specular
    //float3 viewDirection = normalize(input.worldPos - CameraPosition);
    //float3 lightReflection = normalize(-reflect(lightDirection, (perturbedNormal + perturbedSpecular)));
    //float3 ambientReflection = normalize(-reflect(AmbientDirection, (perturbedNormal + (perturbedSpecular / ambientStrengh))));

    ////Reflection
    //float lightSpecular = pow(max(dot(lightReflection, viewDirection), 0.0), powStrength);
    //float ambientSpecular = pow(max(dot(ambientReflection, viewDirection), 0.0), powStrength);
    //float reflection = lightSpecular + ambientSpecular;

    //return float4(((min(light + (ambient / ambientStrengh) + reflection * 4, 1.0)) * texColor.rgb),1);
    return float4(light * texColor);

}

technique
{
    pass
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};