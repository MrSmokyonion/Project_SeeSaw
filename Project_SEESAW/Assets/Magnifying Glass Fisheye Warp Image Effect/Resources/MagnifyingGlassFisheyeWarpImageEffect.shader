Shader "Hidden/MagnifyingGlassFisheyeWarpImageEffect"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile WARP_TYPE_POW WARP_TYPE_EXP2
			#pragma multi_compile _ IS_MAGNIFYING_GLASS
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float4 scrPos : TEXCOORD0;
				float2 uv : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_TexelSize;

			float2 _Mouse;
			float _Radius;
			float _Distortion;
			float _ScaleArea;

			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.scrPos = ComputeScreenPos(o.pos);
				o.uv = v.uv;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target
			{
				float4 c = 0;
				float2 uv = i.scrPos.xy/i.scrPos.w;
				float2 mousePos = _Mouse.xy / _ScreenParams.xy;
				float wh = _ScreenParams.y / _ScreenParams.x;
				uv.y *= wh;
				mousePos.y *= wh;
				if(length(uv - mousePos) <= _Radius)
				{
					uv -= mousePos;
#if defined(IS_MAGNIFYING_GLASS)
					float newLength = _ScaleArea * _Distortion;//_ScaleArea * pow(length(uv) / _Radius,  _Distortion);
#else
#if defined(WARP_TYPE_POW)
					float newLength = _ScaleArea * pow(length(uv) / _Radius,  _Distortion);
#endif
#if defined(WARP_TYPE_EXP2)
					float newLength = _ScaleArea * exp2(length(uv) / _Radius * _Distortion);
#endif
#endif
					uv *= newLength;
					uv += mousePos;
				}
				uv.y /= wh;
				
				c = tex2D(_MainTex, uv);
				return c;
			}

			ENDCG
		}
	}
}
