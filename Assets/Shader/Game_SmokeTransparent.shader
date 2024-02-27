Shader "Game/SmokeTransparent" {
	Properties {
		_Color ("Main Color", Vector) = (1,1,1,1)
		_RimColor ("Rim Color", Vector) = (1,1,1,1)
		_RimSize ("Rim Size", Range(0, 1)) = 0.5
		_RimPower ("Rim Power", Range(0, 5)) = 0
		_RimLightPercent ("Rim Light Percent", Range(0, 5)) = 0.25
		_RimLightPower ("Rim Light Power", Range(0, 3)) = 0
	}
	//DummyShaderTextExporter
	SubShader{
		Tags { "RenderType"="Opaque" }
		LOD 200
		CGPROGRAM
#pragma surface surf Standard
#pragma target 3.0

		fixed4 _Color;
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void surf(Input IN, inout SurfaceOutputStandard o)
		{
			o.Albedo = _Color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
}