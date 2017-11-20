// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Caustics" {
	Properties{
		_Color("Color", Color) = (1,1,1,0)
		_ShadowTex("Cookie", 2D) = "black" { TexGen ObjectLinear }
		_Size("Grid Size", Float) = 1
	}
		Subshader{
		Tags{ "RenderType" = "Transparent" "Queue" = "Transparent" }
		Pass{
		ZWrite Off
		Blend One One

		CGPROGRAM
#pragma vertex vert
#pragma fragment frag

#include "UnityCG.cginc"
	struct v2f {
		float4 pos : SV_POSITION;
		float2 uv : TEXCOORD0;
	};
	sampler2D _ShadowTex;
	float4 _ShadowTex_ST;
	float4 _Color;
	float4x4 unity_Projector;
	fixed _Size;
	v2f vert(appdata_tan v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(mul(unity_Projector, v.vertex).xy, _ShadowTex) * _Size;
		return o;
	}
	half4 frag(v2f i) : COLOR{
		return tex2D(_ShadowTex, fmod(i.uv, (_Size))) * _Color;
	}
		ENDCG
	}
	}
}
