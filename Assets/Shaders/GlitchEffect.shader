// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/GlitchEffect"
{
    Properties
    {
         _Color("Color", Color) = (1,0,0,1)
         _RimColor("RimColor", Color) = (0,0,1,0)
        _MainTex("Main Tex (RGB)", 2D) = "white" {}
        _NoiseTex("Noise Tex (RGB)", 2D) = "white" {}
        _Slide("Slide", Float) = 1.0
        _Slices("Slices", Int) = 1
        _TimeScale("TimeScale", Float) = 1
        _SlideRatio("SlideRatio", Float) = 0.1
        _BlinkRatio("BlinkRatio", Float) = 0.01
        _Transparency("TransparencyRange", Vector) = (0, 1, 0, 0)
        _RimLightValue("RimParamenter", Float) = 0.0
    }
        SubShader
        {
            Tags { "RenderType" = "Transparent" 
                   "Queue"="Transparent" }
            LOD 100
            ZWrite Off
            //Cull Off

            //Blend SrcAlpha OneMinusSrcAlpha
            Blend SrcAlpha OneMinusSrcAlpha

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
            fixed4 _Color, _RimColor;
            float _Slide;
            float _Slices, _TimeScale, _SlideRatio, _BlinkRatio;
            float2 _Transparency;
            float _RimLightValue;

            float hash11(float x)
            {
                return frac(sin(x + 445.5234) * 117.523354);
            }

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                o.screenPos = ComputeScreenPos(o.vertex);
                o.view_dir = normalize(WorldSpaceViewDir(v.vertex));
                //object to world matrix without translation
                o.normal = mul((float3x3)unity_ObjectToWorld,v.normal.xyz/v.normal.w);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {

                float2 screen_coords = i.screenPos.xy / i.screenPos.w;

                //float slice = trunc(i.uv.y * _Slices);
                float slice = trunc(screen_coords.y * _Slices);
                float clip_value = hash11(slice * (trunc((_Time.w + _TimeScale) / _TimeScale))) > _SlideRatio;
                clip(clip_value - 0.01);

                float3 noise = tex2D(_NoiseTex, float2((slice + (_Time.w + _TimeScale)) / _Slices, (frac((_Time.w + _TimeScale) / (_TimeScale * 2)))));


                float VN_dot = abs(dot(i.normal, i.view_dir));

                float alpha_blink = noise < _BlinkRatio;

                
                fixed4 col = lerp(_RimColor, _Color, VN_dot * VN_dot +  _RimLightValue);
                col.a = lerp( lerp(_Transparency.x, _Transparency.y, noise.y), _Transparency.y, 1 - (VN_dot + sin(_Time.w *1) / 30)) * alpha_blink * noise.z;

                
                //return float4(i.normal.xyz, 1);
                return col * 2;
            }
            ENDCG
        }
        Pass
        {
            Name "Shifted"
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

                sampler2D _MainTex;
                float4 _MainTex_ST;
                fixed4 _Color, _RimColor;
                float _Slide;
                float _Slices, _TimeScale, _SlideRatio;
                float2 _Transparency;
                float _RimLightValue;

                float hash11(float x)
                {
                    return frac(sin(x + 445.5234) * 117.523354);
                }

                v2f vert(appdata v)
                {
                    //float3 worldPos = mul(unity_ObjectToWorld, float4(v.vertex.xyz, 1.0)).xyz;
                    //Per vertex view dir
                    //float3 view_dir = _WorldSpaceCameraPos.xyz - mul(unity_ObjectToWorld, v.vertex).xyz;
                    //Per camera forward view dir
                    float3 cam_forward = UNITY_MATRIX_IT_MV[2].xyz;
                    float3 up = UNITY_MATRIX_IT_MV[1].xyz;
                    float3 side = normalize(cross(cam_forward, up));

                    float3 pos = v.vertex.xyz + side * _Slide * sin((pow(((hash11(_Time.w) - .5) * 2),3.0) ));

                    v.vertex.xyz = pos;


                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    o.screenPos = ComputeScreenPos(o.vertex);
                    o.view_dir = normalize(WorldSpaceViewDir(v.vertex));

                    o.normal = mul((float3x3)unity_ObjectToWorld, v.normal.xyz / v.normal.w);
                    return o;
                }

                fixed4 frag(v2f i) : SV_Target
                {
                float2 screen_coords = i.screenPos.xy / i.screenPos.w;

                float slice = trunc(screen_coords.y * _Slices) ;
                //float slice = trunc(i.uv.y * _Slices);
                float clip_value = hash11(slice * (trunc((_Time.w + _TimeScale)))) <= _SlideRatio;
                clip(clip_value - 0.1);

                float VN_dot = abs(dot(i.normal, i.view_dir));


                fixed4 col = lerp(_RimColor, _Color, VN_dot * VN_dot + _RimLightValue);
                col.a = lerp(_Transparency.x, _Transparency.y, 1 - (VN_dot + sin(_Time.w * 1) / 30));
                return col * 3;
                }
                ENDCG
        }
    }
}
