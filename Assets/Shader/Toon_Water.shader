Shader "Toon/Water" {
	Properties {
		_Color ("Main Color", Vector) = (0.5,0.5,0.5,1)
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_ToonShade ("ToonShader Cubemap(RGB)", Cube) = "" {}
		_ToonColor ("Toon Color", Vector) = (0.5,0.5,0.5,1)
		_Fade ("Fade", Range(0, 1)) = 0
		_Scale ("Wave Scale", Float) = 0.6
		_Frequency ("Frequency", Float) = 1
		_Speed ("Speed", Float) = 0.5
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		sampler2D _MainTex;
		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
	Fallback "VertexLit"
}