Shader "Game/UIGray" {
	Properties {
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Saturate ("Saturate", Range(0, 1)) = 1
		_Brightness ("Brightness", Range(0, 1)) = 1
		_ColorAdditive ("Color Additive", Vector) = (1,1,1,1)
		_ColorOverlay ("Color Overlay", Vector) = (1,1,1,0)
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
}