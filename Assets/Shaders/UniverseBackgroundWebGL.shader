Shader "Unlit/UniverseBackgroundWebGL"
{
    Properties
    {
        _FineNoiseTex("Fine Noise Texture", 2D) = "white" {}
        _CoarseNoiseTex("Coarse Noise Texture", 2D) = "white" {}
        _NoiseSlice ("NoiseSlice", Vector) = (1,1,1,1)
        _StarHarmonics ("Star Harmonics", Range(1, 10)) = 3
        _StarGridSide ("Bright Star Grid Side", Float) = 5
        _StarBright ("Star Brightness", Float) = 0.3
        _StarSat ("Star Saturation", Float) = 0.6
        _TimeScale ("TimeScale", Float) = 1.0
        _BGSat ("BG Saturation", Float) = 0.2
        _BGBright ("BG Brightnes", Float) = 0.2
        _BGIntensity ("BGIntensity", Float) = 0.3
        _SlideIntensity ("SlideIntensity", Float) = 5
        _StarDensity ("Star Density", Float) = 0.2
        _NebulaIntensity ("Nebula Density", Float) = 0.2
        _NebulaSat ("Nebula Saturation", Float) = 0.2
        _NebulaBright ("Nebula Brightness", Float) = 0.2
        _NebulaHarmonics("Nebula Harmonics", Vector) = (2, 5, 0, 0)
        _NebulaHue ("Nebula Hue", Float) = 0.3
        _NebulaRampTex("Nebula Ramp Texture", 2D) = "white" {}
        _StarDirection("StarDirection", Vector) = (1,0,0,0)
        _RampLine("Ramp Line", Int) = 0
        _NebulaValue("NebulaValue", Float) = 1.0
        _NebulaSpeed("NebulaSpeed", Float) = 1.0
        _NebulaOffset("NebulaOffset", Float) = 1.0
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

            #include "UnityCG.cginc"
            #include "Include/coordinates.hlsl"
            #include "Include/hsv.hlsl"

            struct appdata
            {
                fixed4 vertex : POSITION;
                fixed2 uv : TEXCOORD0;
            };

            struct v2f
            {
                fixed2 uv : TEXCOORD0;
                fixed4 vertex : SV_POSITION;
            };

            fixed4 _CoarseNoiseTex_ST;
            sampler2D _FineNoiseTex, _CoarseNoiseTex, _NebulaRampTex;
            fixed2 _NoiseSlice;
            uint _StarHarmonics;
            fixed _StarGridSide, _NebulaIntensity, _StarDensity, _StarBright, _StarSat, _TimeScale, _BGSat, _BGBright, _BGIntensity, _SlideIntensity;
            fixed _NebulaSat, _NebulaBright, _NebulaHue;
            fixed2 _NebulaHarmonics;
            fixed2 _MousePosition;
            fixed _RampLine;
            fixed2 _StarDirection;
            fixed _NebulaValue, _NebulaSpeed, _NebulaOffset;
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _CoarseNoiseTex);
                return o;
            }


            fixed2 hash22(fixed2 xy) {
                return fixed2(frac(sin(dot(xy, fixed2(64.432432, 27.564749))) * 514.9456)
                    , frac(sin(dot(xy, fixed2(631.6554897, 78.16556))) * 897.5898489));
            }

            fixed hash21(fixed2 x)
            {
                return frac(sin(dot(x, fixed2(29.54983, 3.123))) * 54398.13124);
            }

            
            fixed hash11(fixed x)
            {
                return frac(sin(x + 445.5234) * 117.523354);
            }

            
            fixed4 Nebula(fixed2 uv, fixed harmonic)
            {
                fixed2 mouse_offset = (_MousePosition / _ScreenParams.xy) * _SlideIntensity * 5 * harmonic;
                fixed4 sliding_fine_noise = tex2D(_FineNoiseTex, ((uv + mouse_offset + ((_Time.x + _NebulaOffset + harmonic) / _NebulaSpeed) * _StarDirection) / 2 ) * harmonic);

                fixed r = 1-sliding_fine_noise.r;
                fixed g = sliding_fine_noise.g;
                fixed b = sliding_fine_noise.b;

                fixed4 nebula_color = fixed4(HSV2RGB(fixed3(_NebulaHue, _NebulaSat, _NebulaBright)), 1.0);
                //return sliding_fine_noise;
                //fixed nebula_noise = step(_NoiseSlice.x, sliding_fine_noise.r);
                //nebula_noise = nebula_noise * step(sliding_fine_noise.r, _NoiseSlice.y);

                fixed nebula_noise = smoothstep(_NoiseSlice.x, _NoiseSlice.y , r * r);
                
                nebula_noise = (_NoiseSlice.y - nebula_noise) / (_NoiseSlice.y - _NoiseSlice.x);
                
                fixed dist_noise = (tex2D(_NebulaRampTex, fixed2(g * b + ((_Time.x * 0.2) ), _RampLine / 1024 )) - 0.5f) * 2;
                //fixed dist_noise = tex2D(_NebulaRampTex, fixed2(g * b, 0.5f));
                //fixed dist_noise = g * b;
                
                //nebula_color *= nebula_noise * _NebulaIntensity / dist_noise;
                nebula_color *= nebula_noise * _NebulaIntensity / pow(dist_noise, 2);

                return fixed4(nebula_color.xyz, nebula_noise);
            }

            fixed4 BrightStar(fixed2 uv, fixed4 noise_col, fixed harmonic) 
            {
                harmonic *= 0.3f;
                //fixed2 mouse_offset = (_MousePosition / _ScreenParams.xy) * 5* _SlideIntensity * harmonic;
                fixed2 time_offset = _StarDirection * (_Time.y / harmonic) / _TimeScale;
                fixed2 harmonic_offset = uv * harmonic;

                fixed4 star_grid_coords = cell_coordinates(uv /*+ mouse_offset */+ time_offset + harmonic_offset, _StarGridSide * harmonic);


                fixed4 color = 0.0f;
                fixed intensity = abs(sin((_Time.x / 10 + star_grid_coords.z / 10)) /2 );

                

                for (int k = -1; k <= 1; k++)
                {
                    for (int l = -1; l <= 1; l++)
                    {
                        fixed2 offset = fixed2(k, l);
                        fixed2 offsetcoords = star_grid_coords.xy +  offset;

                        fixed3 col = HSV2RGB(fixed3(hash11(hash21(offsetcoords)), _StarSat, _StarBright));

                        fixed2 rand_point = hash22(offsetcoords * harmonic);

                        fixed density = hash21(offsetcoords + 37.0f);

                        fixed d = distance(star_grid_coords.zw  , rand_point.xy + offset);
                        color += fixed4(col / (d * d), 1.0) * step(density, _StarDensity);
                    }
                }
                return color * intensity;
            }



            fixed4 frag(v2f i) : SV_Target
            {



                fixed4 col = fixed4(0.0f, 0.0f, 0.0f, 0.0f);
                // sample the texture
                fixed4 sliding_coarse_noise = tex2D(_CoarseNoiseTex, (i.uv + _Time.x / 30) /2);
                fixed4 fine_noise = tex2D(_FineNoiseTex, i.uv);
                
                
                
                fixed4 bg_color = fixed4( HSV2RGB(fixed3(sliding_coarse_noise.x, _BGSat, _BGBright)) * sliding_coarse_noise.y * _BGIntensity, 1.0f);
                

                fixed4 nebula_color = fixed4(0.0f, 0.0f, 0.0f, 0.0f);
                fixed nebula_noise = 0.0f;
                for (fixed j = 1; j <= 1; j++)
                {
                    fixed4 nebula = Nebula( i.uv  , j);
                    nebula_color += fixed4(nebula.xyz, 0.0f) / j ;
                    nebula_noise = max(nebula_noise, nebula.w);
                }


                fixed4 front_stars = fixed4(0.0f, 0.0f, 0.0f, 1.0f);
                fixed4 back_stars = fixed4(0.0f, 0.0f, 0.0f, 1.0f);

                for (fixed j = 1; j < 4; j++)
                {
                    front_stars += BrightStar(i.uv , fine_noise, j) / j;
                }
                back_stars = BrightStar(i.uv, fine_noise, _StarHarmonics) / _StarHarmonics;

                fixed4 nebula_backstars = lerp(back_stars, nebula_color, 1) * _NebulaValue;

                col = front_stars + nebula_backstars;



                return saturate(bg_color + col);
            }
            ENDCG
        }
    }
}
