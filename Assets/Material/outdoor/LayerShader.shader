Shader "Unlit/LayerShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OffsetX ("OffsetX", Float) = 0
		_OffsetY ("OffsetY", Float) = 0
	}
	SubShader
	{
		// inside SubShader
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }

		// inside Pass
		Blend SrcAlpha OneMinusSrcAlpha

		Pass {
			CGPROGRAM

			#pragma vertex vert             
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			Float _OffsetX;
			Float _OffsetY;

			struct vertInput {
        	    float4 pos : POSITION;
			};  

			struct vertOutput {
				float4 pos : SV_POSITION;
	            float4 screenpos : TEXCOORD0;
			};

			vertOutput vert(vertInput input) {
				vertOutput o;
				o.pos = UnityObjectToClipPos(input.pos);
				o.screenpos = ComputeScreenPos(o.pos);
				return o;
			}

			fixed4 frag(vertOutput output) : COLOR {
				float2 screenUV = output.screenpos.xy / output.screenpos.w;
				screenUV.x -= _OffsetX;
				screenUV.y -= _OffsetY;
				return tex2D(_MainTex, screenUV);
			}
			ENDCG
		}
	}
}
