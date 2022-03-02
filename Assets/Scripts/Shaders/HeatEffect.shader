Shader "Custom/HeatEffect"
{

	// variables
	Properties{
		_Color("Color", Color) = (1, 1, 1, 1)
		_MainTex("Texture", 2D) = "white" {}
		_NoiseTex("Noise Texture", 2D) = "gray" {}
		_TimeMultiplayer("Time Multiplayer", Range(0, 10)) = 1
		_VertexMultiplayer("Vertex Multiplayer", Range(0, 10)) = 1
		_VerticeMultiplayer("Vertice Multiplayer", Range(0, 2)) = 0
	}


	// actual shader code
	SubShader{
		Pass{
			CGPROGRAM
			#pragma vertex MyVertexProgram
			#pragma fragment MyFragmentProgram

			#include "UnityCG.cginc"

			struct VertexData {
				float4 position : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct VertexToFragment {
				float4 position : SV_POSITION;
				float2 uv : TEXCOORD0;
				float2 uvNoise : TEXCOORD1;
			};

			float4 _Color;
			sampler2D _MainTex; float4 _MainTex_ST;
			sampler2D _NoiseTex; float4 _NoiseTex_ST;
			float _TimeMultiplayer;
			float _VertexMultiplayer;
			float _VerticeMultiplayer;

			// calculate the data for the VertexToFragment struct
			// that is then passed on to the Fragment function
			VertexToFragment MyVertexProgram(VertexData vertex)
			{
				VertexToFragment v2f;
				vertex.position.x += sin((_Time.y * _TimeMultiplayer) + (vertex.position.y) * _VertexMultiplayer) * _VerticeMultiplayer;
				v2f.position = UnityObjectToClipPos(vertex.position);
				v2f.uv = vertex.uv * _MainTex_ST.xy + _MainTex_ST.zw;
				v2f.uvNoise = vertex.uv * _NoiseTex_ST.xy + _NoiseTex_ST.zw;

				return v2f;
			}

			float4 MyFragmentProgram(VertexToFragment v2f) : SV_TARGET
			{
				float4 colorValue = tex2D(_MainTex, v2f.uv);
				float4 noiseColorValue = tex2D(_NoiseTex, v2f.uv);
				clip(noiseColorValue.rgb);

				return ((colorValue * _Color));
			}
			ENDCG
		}
	}
}
