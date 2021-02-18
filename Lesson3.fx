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
    float4 dayColor = tex2D(DayTextureSampler, input.uv);
   
    ////Lights
    //float3 lightDirection = normalize(input.worldPos - LightPosition);
    //float3 AmbientDirection = normalize(input.worldPos - AmbientPosition);

    //float light = max(dot(input.worldNormal, -lightDirection), 0.0);//normal
    //float ambient = max(dot(input.worldNormal, -AmbientDirection), 0.0);

    ////SpecularSettings
    //float powStrength = 15;
    //float ambientStrengh = 3;

    ////Specular
    //float3 viewDirection = normalize(input.worldPos - CameraPosition);
    //float3 lightReflection = normalize(-reflect(lightDirection, (input.worldNormal)));
    //float3 ambientReflection = normalize(-reflect(AmbientDirection, (input.worldNormal))));

    ////Reflection
    //float lightSpecular = pow(max(dot(lightReflection, viewDirection), 0.0), powStrength);
    //float ambientSpecular = pow(max(dot(ambientReflection, viewDirection), 0.0), powStrength);
    //float reflection = lightSpecular + ambientSpecular;

    //return float4(((min(light + (ambient / ambientStrengh) + reflection * 4, 1.0)) * texColor.rgb), 1);
    return float4(dayColor);
}

technique
{
    pass
    {
        VertexShader = compile VS_SHADERMODEL MainVS();
        PixelShader = compile PS_SHADERMODEL MainPS();
    }
};