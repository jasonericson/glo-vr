/******************************************************************************/
/*!
All content © 2015 DigiPen (USA) Corporation, all rights reserved.
\file   FogShader.shader
\author Robert Marchesani
\par    email: r.marchesani\@hotmail.com
\par    DigiPen login: r.marchesani
\par    Course: GAM450
\brief  
    Defines the Fog shader.
*/
/******************************************************************************/

Shader "Glo/Fog" {
	Properties {
		_TopLayer ("Top Layer", 2D) = "white" {}
		_MidLayer ("Mid Layer", 2D) = "white" {}
		_LowLayer ("Low Layer", 2D) = "white" {}
		_BaseColor ("Base Color", Color) = (1,1,1,1)
		_TintColor ("Tint Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType" = "Opaque" }
		CGPROGRAM
		#pragma surface surf Lambert
		struct Input {
			float2 uv_TopLayer;
			float2 uv_MidLayer;
			float2 uv_LowLayer;
		};
		float4 _BaseColor;
		float4 _TintColor;
		sampler2D _TopLayer;
		sampler2D _MidLayer;
		sampler2D _LowLayer;
		void surf (Input IN, inout SurfaceOutput o) {
			float3 finalColor = _BaseColor.rgb;
			
			float4 thisLayer = tex2D(_LowLayer, IN.uv_LowLayer);
			thisLayer.rgb *= _TintColor.rgb;
			finalColor = finalColor.rgb * (1 - thisLayer.a) + thisLayer.rgb * thisLayer.a;
			
			thisLayer = tex2D(_MidLayer, IN.uv_MidLayer);
			thisLayer.rgb *= _TintColor.rgb;
			finalColor = finalColor.rgb * (1 - thisLayer.a) + thisLayer.rgb * thisLayer.a;
			
			thisLayer = tex2D(_TopLayer, IN.uv_TopLayer);
			thisLayer.rgb *= _TintColor.rgb;
			finalColor = finalColor.rgb * (1 - thisLayer.a) + thisLayer.rgb * thisLayer.a;
			
			o.Albedo = finalColor;
		}
		ENDCG
	} 
	Fallback "Diffuse"
}