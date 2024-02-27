Shader "Game/UIBGAlpha" {
	Properties {
		_Color ("Tint", Vector) = (1,1,1,1)
		[PerRendererData] _MainTex ("Texture", 2D) = "white" {}
		_TopColor ("TopColor", Vector) = (1,1,1,1)
		_BottomColor ("BottomColor", Vector) = (1,1,1,1)
		[Header (Pattern)] _Size ("Size", Range(0, 10)) = 3
		_Offset ("Offset", Range(0, 1)) = 0.15
		_ScrollSpeed ("Scroll Speed", Range(0, 5)) = 0.5
		[IntRange] _RotateSpeed ("Rotate Speed", Range(0, 50)) = 5
		[Header (Circle 1)] _CircleColor1 ("Circle Color 1", Vector) = (0.5,0.5,0.5,1)
		_CircleRad1 ("Circle Rad 1", Float) = 1
		_CirclePos1 ("Circle Pos 1", Range(0, 1)) = 0.5
		[Header (Circle 2)] _CircleColor2 ("Circle Color 2", Vector) = (0.5,0.5,0.5,1)
		_CircleRad2 ("Circle Rad 2", Float) = 1
		_CirclePos2 ("Circle Pos 2", Range(0, 1)) = 0.5
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
}