Shader "Toony Colors Pro 2/User/StealthMasterSkinsShader" {
	Properties {
		[TCP2HeaderHelp(BASE, Base Properties)] _Color ("Color", Vector) = (1,1,1,1)
		_HColor ("Highlight Color", Vector) = (0.785,0.785,0.785,1)
		_SColor ("Shadow Color", Vector) = (0.195,0.195,0.195,1)
		_MainTex ("Main Texture", 2D) = "white" {}
		[TCP2Separator] [TCP2Header(RAMP SETTINGS)] [TCP2Gradient] _Ramp ("Toon Ramp (RGB)", 2D) = "gray" {}
		[TCP2Separator] [TCP2HeaderHelp(MATCAP, MatCap)] _MatCapR ("MatCapR (RGB)", 2D) = "black" {}
		_MatCapG ("MatCapG (RGB)", 2D) = "black" {}
		_MatCapB ("MatCapB (RGB)", 2D) = "black" {}
		_MatCapMask ("MatCap Mask", 2D) = "black" {}
		[TCP2Separator] [TCP2HeaderHelp(EMISSION, Emission)] _EmissionColor ("Emission Color", Vector) = (1,1,1,1)
		[TCP2Separator] _FlatSilhouetteColor ("Silhouette Color", Vector) = (1,1,1,1)
		_AddColor ("Add Color", Vector) = (0.85023,0.85034,0.85045,0.85056)
		[HideInInspector] __dummy__ ("unused", Float) = 0
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
	Fallback "Diffuse"
	//CustomEditor "TCP2_MaterialInspector_SG"
}