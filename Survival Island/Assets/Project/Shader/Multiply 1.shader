// Upgrade NOTE: replaced '_Projector' with 'unity_Projector'
// Upgrade NOTE: replaced '_ProjectorClip' with 'unity_ProjectorClip'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

 Shader "Projector/Additive"
 {
     Properties
     {
         _Decal ("Decal", 2D) = "white" { TexGen ObjectLinear   }
         _Color ("Color", Color) = (0.5, 0.5, 0.5, 1)
     }
     
     Subshader
     {
         Pass
         {
             ZWrite On
             Fog { Color (1, 1, 1) }
             Offset -1, -1
             Blend One One
             Cull Back
              
             Tags { "RenderType"="Geometry-10" }
             
             CGPROGRAM
             #include "UnityCG.cginc"
             #pragma vertex vert
             #pragma fragment frag
     
             sampler2D _Decal;
             float4x4 unity_Projector;
             float4x4 unity_ProjectorClip;
             float4 _Color;
     
             struct vin
             {
                 float4  pos     :     POSITION;
                 float4  uv        :     TEXCOORD0;
             };
             
             struct v2f
             {
                 float4 vertex    : SV_POSITION;
                 float4 uv        : TEXCOORD0;
                 float4 pclip    : TEXCOORD1;
             };
     
             
             v2f vert (vin i)
             {
                 v2f o;
                 o.vertex     = UnityObjectToClipPos(i.pos);
                 o.uv         = mul(unity_Projector, i.pos);
                 o.pclip     = mul(unity_ProjectorClip, i.pos);
                 return o;
             }
     
             float4 frag(v2f i) : COLOR
             {
                 clip(1-i.pclip.z);
                 clip(i.pclip.z);
                 clip(i.uv.x);
                 clip(1-i.uv.x);
                 clip(i.uv.y);
                 clip(1-i.uv.y);
                 float4 col = tex2Dproj(_Decal, i.uv) * _Color;
                 return col;
             }
 
             ENDCG
         }
 
     }
 }	