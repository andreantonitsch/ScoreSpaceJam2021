Shader "Unlit/DistortionExplosion"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _DifractionStrength("DifractionStrength", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent"
                 "Queue"  = "Transparent "}
        LOD 100
        GrabPass { "_GameTexture" }
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
                float3 normal : NORMAL;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 grabPos : TEXCOORD1;
                float3 world_normal : NORMAL;
                float3 view_dir : TEXCOORD2;

            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float _DifractionStrength;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.grabPos = ComputeGrabScreenPos(o.vertex);
                o.world_normal = UnityObjectToWorldNormal(v.normal);
                o.view_dir = normalize(WorldSpaceViewDir(v.vertex));
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float d = dot(i.world_normal, i.view_dir);
               float4 shifted_uv = float4(float2(ddx(d), ddy(d)) * _DifractionStrength + i.uv.xy, 0,0);


                float4 Difracted_image = tex2D(_GameTexture, shifted_uv);


                return Difracted_image;
            }
            ENDCG
        }
    }
}
