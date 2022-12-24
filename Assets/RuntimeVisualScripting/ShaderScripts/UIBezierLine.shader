Shader "Unlit/UIBezierLine"
{
    Properties
    {
        MainTex ("Texture", 2D) = "white" {}
        _Color("Color", Color) = ( 0,0,0,0 )
        Thickness("Thickness", float) = 1.0
        ScreenSize("ScreenSize", Vector) = (0,0,0,0 )
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100
        
        blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            int pointsCount;
		    float4 points[ 1000 ];

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D MainTex;
            float4 MainTex_ST;
            float4 ScreenSize;
            float Thickness;
            fixed4 _Color;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(MainTex, i.uv) * _Color;
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);

                float2 xy = i.vertex.xy;
                xy.y = ScreenSize.y - xy.y;
				for (int i = 0; i < pointsCount; i++)
				{
                    //float2 ph = points[ i + 1 ] - points[ i ];
                    //float phLength = length(ph);
                    //ph = ph.xy / phLength;

                    //float2 hp = points[ i ] - points[ i + 1 ];
                    //float hpLength = length(hp);
                    //hp = hp.xy / hpLength;

                    float2 pa1 = xy - points[ i ];
					float pa1Length = length(pa1);

                    if (pa1Length <= Thickness)
                    {
                        col.a = 1.0;
                        return col;
                    }
				}

				col.a = 0.0;
				return col;
            }
            ENDCG
        }
    }
}
