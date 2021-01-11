Shader "Unlit/Projectile"
{
    Properties
    {
        _MainTex ("Sprite", 2D) = "white" {}
        _RampTex ("RampTexture", 2D) = "White" {}
        _ToneTex("ToneTexture", 2D) = "White" {}
        _RampValue("RampValue", Float) = 1.0
        _Color("HSV Color", Color) = (1,1,1,1)
        _RampBounds ("Ramp Bounds", Vector) = (0,1,0,0)
        _OutlineValue ("OutlineValue", Vector) = (0,1,0,0)
        _OutlineColor("Outline Color", Color) = (1,1,1,1)
        
        [KeywordEnum(Ramp, Value)] _RampMode("RampMode", Float) = 1.0
        [KeywordEnum(Ramp, Value)] _ToneMode("ToneMode", Float) = 1.0
    }
    SubShader
    {
        Tags {"Queue" = "Transparent" "RenderType" = "Transparent"}
        ZWrite Off
        BlendOP Add
        Blend SrcAlpha OneMinusSrcAlpha
        //Cull front
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile _RAMPMODE_RAMP _RAMPMODE_VALUE
            #pragma multi_compile _TONEMODE_RAMP _TONEMODE_VALUE

            #include "UnityCG.cginc"
            #include "Include/coordinates.hlsl"
            #include "Include/hsv.hlsl"

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

            sampler2D _MainTex, _RampTex;
            float4 _MainTex_ST;
            float2 _RampBounds, _OutlineValue;
            float _RampValue;
            float4 _Color, _OutlineColor;

            float remap_range(float x, float2 old_range, float2 new_range) {
                float y = x;
                y -= old_range.x;
                y /= (old_range.y - old_range.x);
                y *= (new_range.y - new_range.x);
                return y + new_range.x;
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                float4 sprite_tex = tex2D(_MainTex, i.uv).r;

                float ramp_value = 0.0f;
#ifdef _RAMPMODE_RAMP
                float r = tex2D(_RampTex, float2(sprite_tex.r, 0.5));

                r = remap_range(r, _RampBounds, float2(0, 1)) + sin(_Time.x * 3) * 0.1;
                if (_OutlineValue.x < r & _OutlineValue.y > r)
                    return _OutlineColor * 7;

                float4 col  = sprite_tex * _Color;
                //return (ramp_tex > _RampValue)  / (ramp_tex * ramp_tex) * _Color;
                float a = (sprite_tex > _RampValue);
                //return a;
                //return (r * r);
                float4 v = sprite_tex *  ((r * r* r)) * _Color * 2  ;
                return v + sin(_Time.x * 3) * 0.1;
                return -float4(col.xyz  * v, 1);
#endif




                return sprite_tex;
            }
            ENDCG
        }
    }
}
