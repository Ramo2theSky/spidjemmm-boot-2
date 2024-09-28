Shader "Trash/Blur" {

    Properties {
        _MainTex ("Main Texture", 2D) = "white" { }
        _Blur ("Blur Offset", Range(0, 2)) = 1
        _Tint ("Tint", Color) = (1, 1, 1, 1)
    }

    SubShader {
        Tags { "RenderType" = "Transparent" }

        Pass {

            CGPROGRAM
            #pragma vertex VertexStage
            #pragma fragment FragmentStage
            
            #include "UnityCG.cginc"

            struct VertexInput {
                float4 vertex : POSITION;
                float4 color : COLOR;
            };

            struct FragmentInput {
                float2 uv : TEXCOORD0;
                float2 offset : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float4 color : COLOR;
            };

            sampler2D _CameraSortingLayerTexture;   // Texture captured by camera.
            float4 _CameraSortingLayerTexture_TexelSize;

            sampler2D _MainTex;

            float _Blur;
            float4 _Tint;

            // To do: do someting with the alpha.
            FragmentInput VertexStage (VertexInput v) {
                FragmentInput f;
                f.vertex = UnityObjectToClipPos(v.vertex);
                f.uv = 0.5 * (f.vertex.xy + 1);
                f.color = v.color * _Tint;
                f.offset = _Blur * _CameraSortingLayerTexture_TexelSize.xy;   // Offset in pixels.
                return f;
            }

            float4 FragmentStage (FragmentInput f) : SV_Target {
                float4 col = 0;
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(f.offset.x, f.offset.y));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(-f.offset.x, f.offset.y));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(-f.offset.x, -f.offset.y));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(f.offset.x, -f.offset.y));

                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(f.offset.x, 0));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(0, f.offset.y));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(-f.offset.x, 0));
                col += tex2D(_CameraSortingLayerTexture, f.uv + float2(0, -f.offset.y));
                col /= 8;
                return float4(col.rgb * f.color.rgb, 1);
            }
            ENDCG
        }
    }
}
