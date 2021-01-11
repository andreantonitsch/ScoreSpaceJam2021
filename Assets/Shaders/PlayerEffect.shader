// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/TeleportEffect"
{
    Properties
    {
         _Color("Color", Color) = (1,0,0,1)
         [HDR]_DissolveColor("Dissolve Color", Color) = (0,0,1,0)
        _MainTex("Main Tex (RGB)", 2D) = "white" {}
        _NoiseTex("Noise Tex (RGB)", 2D) = "white" {}
        _T("T", Range(0,1)) = 1

        _DissolveRange("DissolveRange", Vector) = (0,0.1,0,0)
        _Test("Test", Vector) = (0,1,0,0)
    }
        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100
            //ZWrite Off
            //Cull Off

            //Blend SrcAlpha OneMinusSrcAlpha

            Pass
            {
                Name "Unshifted"
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float4 normal : NORMAL;

            };

            struct v2f
            {
                float2 uv : TEXCOORD0;

                float4 screenPos : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float3 view_dir : TEXCOORD2;
                float3 normal : NORMAL;
            };

            sampler2D _MainTex, _NoiseTex;
            float4 _MainTex_ST;
            fixed4 _Color, _DissolveColor;
            float _T;
            float2 _DissolveRange;
            float _Test;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.view_dir = normalize(WorldSpaceViewDir(v.vertex));
                //object to world matrix without translation
                o.normal = mul((float3x3)unity_ObjectToWorld,v.normal.xyz/v.normal.w);
                return o;
            }

            float remap_range(float x, float2 old_range, float2 new_range) {
                x -= old_range.x;
                x /= (old_range.y - old_range.x);
                x *= (new_range.y - new_range.x);
                return x + new_range.x;
            }

            float gain(float x, float k)
            {
                const float a = 0.5 * pow(2.0 * ((x < 0.5) ? x : 1.0 - x), k);
                return (x < 0.5) ? a : 1.0 - a;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float n = tex2D(_NoiseTex, (i.uv ) / 5.5 ).g;
                clip(n - _T);

                float n2 = remap_range(n, float2(_T, 1), float2(0, 1));

                //n2 = gain(n2, _Test);

                float4 col = lerp(_DissolveColor, _Color, step( _DissolveRange.x, n2));

                return float4(col.xyz, 1);
            }
            ENDCG
        }
       
    }
}
