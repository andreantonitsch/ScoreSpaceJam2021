﻿Shader "Unlit/PlayerShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    _Color("Color", Color) = (1,1,1,1)
    _FlashColor("FlashColor", Color) = (1,1,1,1)
    _FlashPeriod("FlashPeriod", Float) = 0.1
    _Flashing("Flashing", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color, _FlashColor;
            float _FlashPeriod, _Flashing;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float t = _Time.y % _FlashPeriod;

            float4 col = lerp(_Color, _FlashColor, t * _Flashing);
                return col;
            }
            ENDCG
        }
    }
}
