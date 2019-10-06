// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Amplify/BasicWater"
{
	Properties
	{
		_WaveSpeed("Wave Speed", Float) = 1
		_WaveDirection("Wave Direction", Vector) = (1,0,0,0)
		_WaveTile("Wave Tile", Float) = 1
		_WaveStretch("Wave Stretch", Vector) = (0.15,0.02,0,0)
		_WaveScale("WaveScale", Float) = 0.5
		_Smoothness("Smoothness", Float) = 0.9
		_BottomColor("BottomColor", Color) = (1,0.3725491,0.6573205,1)
		_TopColor("TopColor", Color) = (0.4481132,0.7891664,1,1)
		_EdgeDistance("EdgeDistance", Float) = 0.5
		_EdgePower("EdgePower", Float) = 0.5
		_NormalMap("Normal Map", 2D) = "white" {}
		_NormalTile("Normal Tile", Float) = 0
		_PanDir1("PanDir1", Vector) = (0,0,0,0)
		_PanDir2("PanDir2", Vector) = (-1,0,0,0)
		_NormalSpeed("NormalSpeed", Float) = 1
		_SecondMapMultiplier("SecondMapMultiplier", Float) = 3
		_SeaFoam("SeaFoam", 2D) = "white" {}
		_EdgeFoamTile("EdgeFoamTile", Float) = 1
		_SeaFoamTile("SeaFoamTile", Float) = 1
		_SeaNoiseTiling("SeaNoiseTiling", Vector) = (3,1,0,0)
		_SeaFoamSpeed("SeaFoamSpeed", Vector) = (1,0,0,0)
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "UnityStandardUtils.cginc"
		#include "UnityCG.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha noshadow vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float3 worldPos;
			float4 screenPos;
		};

		uniform float _WaveScale;
		uniform float _WaveSpeed;
		uniform float2 _WaveDirection;
		uniform float2 _WaveStretch;
		uniform float _WaveTile;
		uniform sampler2D _NormalMap;
		uniform float2 _PanDir1;
		uniform float _NormalSpeed;
		uniform float _NormalTile;
		uniform float2 _PanDir2;
		uniform float _SecondMapMultiplier;
		uniform float4 _BottomColor;
		uniform float4 _TopColor;
		uniform float2 _SeaFoamSpeed;
		uniform float2 _SeaNoiseTiling;
		uniform sampler2D _SeaFoam;
		uniform float _SeaFoamTile;
		uniform float _EdgeFoamTile;
		UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
		uniform float4 _CameraDepthTexture_TexelSize;
		uniform float _EdgeDistance;
		uniform float _EdgePower;
		uniform float _Smoothness;


		float3 mod2D289( float3 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float2 mod2D289( float2 x ) { return x - floor( x * ( 1.0 / 289.0 ) ) * 289.0; }

		float3 permute( float3 x ) { return mod2D289( ( ( x * 34.0 ) + 1.0 ) * x ); }

		float snoise( float2 v )
		{
			const float4 C = float4( 0.211324865405187, 0.366025403784439, -0.577350269189626, 0.024390243902439 );
			float2 i = floor( v + dot( v, C.yy ) );
			float2 x0 = v - i + dot( i, C.xx );
			float2 i1;
			i1 = ( x0.x > x0.y ) ? float2( 1.0, 0.0 ) : float2( 0.0, 1.0 );
			float4 x12 = x0.xyxy + C.xxzz;
			x12.xy -= i1;
			i = mod2D289( i );
			float3 p = permute( permute( i.y + float3( 0.0, i1.y, 1.0 ) ) + i.x + float3( 0.0, i1.x, 1.0 ) );
			float3 m = max( 0.5 - float3( dot( x0, x0 ), dot( x12.xy, x12.xy ), dot( x12.zw, x12.zw ) ), 0.0 );
			m = m * m;
			m = m * m;
			float3 x = 2.0 * frac( p * C.www ) - 1.0;
			float3 h = abs( x ) - 0.5;
			float3 ox = floor( x + 0.5 );
			float3 a0 = x - ox;
			m *= 1.79284291400159 - 0.85373472095314 * ( a0 * a0 + h * h );
			float3 g;
			g.x = a0.x * x0.x + h.x * x0.y;
			g.yz = a0.yz * x12.xz + h.yz * x12.yw;
			return 130.0 * dot( m, g );
		}


		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			float4 temp_cast_0 = (3.0).xxxx;
			return temp_cast_0;
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float temp_output_6_0 = ( _Time.y * _WaveSpeed );
			float3 ase_worldPos = mul( unity_ObjectToWorld, v.vertex );
			float2 appendResult10 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 WorldSpaceTile11 = appendResult10;
			float2 WaveTileUV21 = ( ( WorldSpaceTile11 * _WaveStretch ) * _WaveTile );
			float2 panner3 = ( temp_output_6_0 * _WaveDirection + WaveTileUV21);
			float simplePerlin2D1 = snoise( panner3 );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float2 panner24 = ( temp_output_6_0 * _WaveDirection + ( WaveTileUV21 * float2( 0.1,0.1 ) ));
			float simplePerlin2D25 = snoise( panner24 );
			simplePerlin2D25 = simplePerlin2D25*0.5 + 0.5;
			float WavePattern31 = ( simplePerlin2D1 + simplePerlin2D25 );
			float3 WaveHeight34 = ( ( float3(0,1,0) * _WaveScale ) * WavePattern31 );
			v.vertex.xyz += WaveHeight34;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float3 ase_worldPos = i.worldPos;
			float2 appendResult10 = (float2(ase_worldPos.x , ase_worldPos.z));
			float2 WorldSpaceTile11 = appendResult10;
			float2 temp_output_66_0 = ( WorldSpaceTile11 * _NormalTile );
			float2 panner72 = ( 1.0 * _Time.y * ( ( _PanDir1 * float2( 0.001,0.001 ) ) * _NormalSpeed ) + temp_output_66_0);
			float2 panner73 = ( 1.0 * _Time.y * ( _PanDir2 * float2( 0.001,0.001 ) ) + ( ( temp_output_66_0 * ( _NormalTile * _SecondMapMultiplier ) ) * ( _NormalSpeed * -1.0 ) ));
			float3 Normal81 = BlendNormals( UnpackNormal( tex2D( _NormalMap, panner72 ) ) , UnpackNormal( tex2D( _NormalMap, panner73 ) ) );
			o.Normal = Normal81;
			float2 panner104 = ( 1.0 * _Time.y * _SeaFoamSpeed + ( _SeaNoiseTiling * WorldSpaceTile11 ));
			float simplePerlin2D105 = snoise( panner104 );
			float4 temp_output_111_0 = ( saturate( simplePerlin2D105 ) * tex2D( _SeaFoam, ( WorldSpaceTile11 * _SeaFoamTile ) ) );
			float4 SeaFoam100 = temp_output_111_0;
			float temp_output_6_0 = ( _Time.y * _WaveSpeed );
			float2 WaveTileUV21 = ( ( WorldSpaceTile11 * _WaveStretch ) * _WaveTile );
			float2 panner3 = ( temp_output_6_0 * _WaveDirection + WaveTileUV21);
			float simplePerlin2D1 = snoise( panner3 );
			simplePerlin2D1 = simplePerlin2D1*0.5 + 0.5;
			float2 panner24 = ( temp_output_6_0 * _WaveDirection + ( WaveTileUV21 * float2( 0.1,0.1 ) ));
			float simplePerlin2D25 = snoise( panner24 );
			simplePerlin2D25 = simplePerlin2D25*0.5 + 0.5;
			float WavePattern31 = ( simplePerlin2D1 + simplePerlin2D25 );
			float clampResult43 = clamp( WavePattern31 , 0.0 , 1.0 );
			float4 lerpResult41 = lerp( _BottomColor , ( _TopColor + SeaFoam100 ) , clampResult43);
			float4 Albedo49 = lerpResult41;
			o.Albedo = Albedo49.rgb;
			float4 ase_screenPos = float4( i.screenPos.xyz , i.screenPos.w + 0.00000000001 );
			float4 ase_screenPosNorm = ase_screenPos / ase_screenPos.w;
			ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
			float screenDepth52 = LinearEyeDepth(SAMPLE_DEPTH_TEXTURE( _CameraDepthTexture, ase_screenPosNorm.xy ));
			float distanceDepth52 = abs( ( screenDepth52 - LinearEyeDepth( ase_screenPosNorm.z ) ) / ( _EdgeDistance ) );
			float4 clampResult61 = clamp( ( ( tex2D( _SeaFoam, ( WorldSpaceTile11 * _EdgeFoamTile ) ) + ( 1.0 - distanceDepth52 ) ) * _EdgePower ) , float4( 0,0,0,0 ) , float4( 1,1,1,0 ) );
			float4 Edge57 = clampResult61;
			o.Emission = Edge57.rgb;
			o.Smoothness = _Smoothness;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}