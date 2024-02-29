Shader "Game/KeySilhouette" {
	Properties {
		_Color ("Color", Vector) = (1,1,1,1)
		_Poof ("Poof", Float) = 0
		_w ("width", Float) = 0
		_shiftMin ("shift_min", Float) = 0
		_shiftMax ("shift_max", Float) = 0
		_Shift ("shift", Float) = 0
		_Position ("Position", Vector) = (0,0,0,0)
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