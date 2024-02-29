Shader "Hologram" {
	Properties {
		[TCP2HeaderHelp(Outline)] _OutlineWidth ("Width", Range(0.1, 4)) = 1
		[TCP2OutlineNormalsGUI] __outline_gui_dummy__ ("_unused_", Float) = 0
		[TCP2Separator] _NDVMinFrag ("NDV Min", Range(0, 2)) = 0.5
		_NDVMaxFrag ("NDV Max", Range(0, 2)) = 1
		[TCP2Separator] [TCP2ColorNoAlpha] [HDR] _HologramColor ("Hologram Color", Vector) = (0,0.502,1,1)
		_ScanlinesTex ("Scanlines Texture", 2D) = "white" {}
		[TCP2UVScrolling] _ScanlinesTex_SC ("Scanlines Texture UV Scrolling", Vector) = (1,1,0,0)
		[HideInInspector] __dummy__ ("unused", Float) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType" = "Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = 1;
		}
		ENDCG
	}
	Fallback "Diffuse"
	//CustomEditor "ToonyColorsPro.ShaderGenerator.MaterialInspector_SG2"
}