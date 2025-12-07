// Light vertex shader
// Standard issue vertex shader, apply matrices, pass info to pixel shader
Texture2D texture0 : register(t0);
SamplerState sampler0 : register(s0);

cbuffer MatrixBuffer : register(b0)
{
    matrix worldMatrix;
    matrix viewMatrix;
    matrix projectionMatrix;
};

cbuffer TimeBuffer : register(b1)
{
    float time;
    float3 padding;
};

struct InputType
{
    float4 position : POSITION;
    float2 tex : TEXCOORD0;
    float3 normal : NORMAL;
};

struct OutputType
{
    float4 position : SV_POSITION;
    float2 tex : TEXCOORD0;
    float3 normal : NORMAL;
};

// Apply a sine-wave deformation to the vertex position and set an appropriate normal.
void ApplySineWave(inout float4 position, inout float3 normal, float time)
{
    float phase = position.x * 0.1 + time;
    position.y += sin(phase) * 2.0;
    normal = float3(-cos(phase), 1.0, 0.0);
}

float GetHeight(float2 uv)
{
    float4 textureColour;
    textureColour = texture0.SampleLevel(sampler0, uv, 0);
    
    float height = textureColour.x * 7;
    return height;
}

OutputType main(InputType input)
{
    OutputType output;
    
    input.position.y = GetHeight(input.tex);
	
	// Calculate the position of the vertex against the world, view, and projection matrices.
    output.position = mul(input.position, worldMatrix);
    output.position = mul(output.position, viewMatrix);
    output.position = mul(output.position, projectionMatrix);

	// Store the texture coordinates for the pixel shader.
    output.tex = input.tex;

	// Calculate the normal vector against the world matrix only and normalise.
    output.normal = mul(input.normal, (float3x3) worldMatrix);
    output.normal = normalize(output.normal);

    return output;
}