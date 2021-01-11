Shader "Unlit/DistortionExplosion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("ExplosionRim", Color) = (1,1,1,1)
        [PerRendererData]_DifractionStrength("DifractionStrength", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                 "Queue"  = "Transparent"}
        LOD 100
        GrabPass { "_GameTexture" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 grabPos : TEXCOORD1;
                float3 world_normal : NORMAL;
                float4 view_dir : TEXCOORD2;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DifractionStrength;
            sampler2D _GameTexture;
            float4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                o.world_normal = UnityObjectToWorldNormal(v.normal);
                float3 dir = WorldSpaceViewDir(v.vertex);
                float d = length(dir);
                dir = normalize(dir);
                o.view_dir = float4(dir, d);
                return o;
            }

            float expImpulse(float x, float k)
            {
                float h = k * x;
                return h * exp(1.0 - h);
            }


            fixed4 frag(v2f i) : SV_Target
            {

                float d = dot(i.world_normal, float3(0,0,-1));
                float4 shifted_uv = float4(float2(ddx(i.view_dir.w), ddy(i.view_dir.w)) * _DifractionStrength + i.grabPos.xy, 0,0);

                

                float4 Difracted_image = tex2D(_GameTexture, shifted_uv);

                float4 col = lerp(Difracted_image, (Difracted_image + _Color) / 2, expImpulse(d,8));
                return col;
            }
            ENDCG
        }
    }
}
