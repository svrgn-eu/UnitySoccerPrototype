// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PixelCamera/ReduceColorDepth" {
	Properties {
		_MainTex ("Input Image (RGB)", 2D) = "white" {}
	
		_RedShades ("Red Shades", Range(2,256)) = 8
		_GreenShades ("Green Shades", Range(2,256)) = 8
		_BlueShades ("Blue Shades", Range(2,256)) = 4
		
		_GammaCorrection ("Gamma Adjust", Range(0.1, 2.0)) = 0.8
	}
	SubShader {
		Pass {
		CGPROGRAM

		#pragma vertex vert 
		#pragma fragment frag
		
		#include "UnityCG.cginc"
		
        sampler2D _MainTex;
        int _RedShades;
        int _GreenShades;
        int _BlueShades;
        float _GammaCorrection;
		
		struct v2f {
            float4 pos : SV_POSITION;
            float2 uv : TEXCOORD0;
        };
        
        float4 _MainTex_ST;

		v2f vert(appdata_base v) 
		{
			v2f o;
			o.pos = UnityObjectToClipPos(v.vertex);  
			o.uv = TRANSFORM_TEX (v.texcoord, _MainTex);
			return o;
		}

		fixed4 frag(v2f i) : SV_Target
		{
			float4 pixel = pow(tex2D(_MainTex, i.uv), _GammaCorrection);
			pixel.r = round(pixel.r * (_RedShades - 1)) / _RedShades;
			pixel.g = round(pixel.g * (_GreenShades - 1)) / _GreenShades;
			pixel.b = round(pixel.b * (_BlueShades - 1)) / _BlueShades;
			return pixel;
		}

		ENDCG
		}
	}
	FallBack "Diffuse"
}