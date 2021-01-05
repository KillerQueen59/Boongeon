// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hovl/Particles/SwordSlash1"
{
	Properties
	{
		_TintColor ("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		_MainTex ("Particle Texture", 2D) = "white" {}
		_InvFade ("Soft Particles Factor", Range(0.01,3.0)) = 1.0
		_Main("Main", 2D) = "white" {}
		_Emissiontex("Emission tex", 2D) = "white" {}
		_Opacity("Opacity", Float) = 20
		_Dissolve("Dissolve", 2D) = "white" {}
		_Emission("Emission", Float) = 2
		_Color0("Color 0", Color) = (0,0,0,0)
		_Desaturation("Desaturation", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}


	Category 
	{
		SubShader
		{
			Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" }
			Blend SrcAlpha OneMinusSrcAlpha
			ColorMask RGB
			Cull Off
			Lighting Off 
			ZWrite Off
			ZTest LEqual
			
			Pass {
			
				CGPROGRAM
				#pragma vertex vert
				#pragma fragment frag
				#pragma target 2.0
				#pragma multi_compile_particles
				#pragma multi_compile_fog
				#include "UnityShaderVariables.cginc"


				#include "UnityCG.cginc"

				struct appdata_t 
				{
					float4 vertex : POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_VERTEX_INPUT_INSTANCE_ID
					float4 ase_texcoord1 : TEXCOORD1;
				};

				struct v2f 
				{
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
					float4 texcoord : TEXCOORD0;
					UNITY_FOG_COORDS(1)
					#ifdef SOFTPARTICLES_ON
					float4 projPos : TEXCOORD2;
					#endif
					UNITY_VERTEX_INPUT_INSTANCE_ID
					UNITY_VERTEX_OUTPUT_STEREO
					float4 ase_texcoord3 : TEXCOORD3;
				};
				
				
				#if UNITY_VERSION >= 560
				UNITY_DECLARE_DEPTH_TEXTURE( _CameraDepthTexture );
				#else
				uniform sampler2D_float _CameraDepthTexture;
				#endif

				//Don't delete this comment
				// uniform sampler2D_float _CameraDepthTexture;

				uniform sampler2D _MainTex;
				uniform fixed4 _TintColor;
				uniform float4 _MainTex_ST;
				uniform float _InvFade;
				uniform float4 _Color0;
				uniform sampler2D _Emissiontex;
				uniform float4 _Emissiontex_ST;
				uniform float _Desaturation;
				uniform float _Emission;
				uniform sampler2D _Main;
				uniform float4 _Main_ST;
				uniform float _Opacity;
				uniform sampler2D _Dissolve;
				uniform float4 _Dissolve_ST;
				float3 HSVToRGB( float3 c )
				{
					float4 K = float4( 1.0, 2.0 / 3.0, 1.0 / 3.0, 3.0 );
					float3 p = abs( frac( c.xxx + K.xyz ) * 6.0 - K.www );
					return c.z * lerp( K.xxx, saturate( p - K.xxx ), c.y );
				}
				
				float3 RGBToHSV(float3 c)
				{
					float4 K = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
					float4 p = lerp( float4( c.bg, K.wz ), float4( c.gb, K.xy ), step( c.b, c.g ) );
					float4 q = lerp( float4( p.xyw, c.r ), float4( c.r, p.yzx ), step( p.x, c.r ) );
					float d = q.x - min( q.w, q.y );
					float e = 1.0e-10;
					return float3( abs(q.z + (q.w - q.y) / (6.0 * d + e)), d / (q.x + e), q.x);
				}

				v2f vert ( appdata_t v  )
				{
					v2f o;
					UNITY_SETUP_INSTANCE_ID(v);
					UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
					UNITY_TRANSFER_INSTANCE_ID(v, o);
					o.ase_texcoord3 = v.ase_texcoord1;

					v.vertex.xyz +=  float3( 0, 0, 0 ) ;
					o.vertex = UnityObjectToClipPos(v.vertex);
					#ifdef SOFTPARTICLES_ON
						o.projPos = ComputeScreenPos (o.vertex);
						COMPUTE_EYEDEPTH(o.projPos.z);
					#endif
					o.color = v.color;
					o.texcoord = v.texcoord;
					UNITY_TRANSFER_FOG(o,o.vertex);
					return o;
				}

				fixed4 frag ( v2f i  ) : SV_Target
				{
					#ifdef SOFTPARTICLES_ON
						float sceneZ = LinearEyeDepth (SAMPLE_DEPTH_TEXTURE_PROJ(_CameraDepthTexture, UNITY_PROJ_COORD(i.projPos)));
						float partZ = i.projPos.z;
						float fade = saturate (_InvFade * (sceneZ-partZ));
						i.color.a *= fade;
					#endif

					float2 uv_Emissiontex = i.texcoord.xy * _Emissiontex_ST.xy + _Emissiontex_ST.zw;
					float3 hsvTorgb43 = RGBToHSV( tex2D( _Emissiontex, uv_Emissiontex ).rgb );
					float4 uv132 = i.ase_texcoord3;
					uv132.xy = i.ase_texcoord3.xy * float2( 1,1 ) + float2( 0,0 );
					float3 hsvTorgb44 = HSVToRGB( float3(( hsvTorgb43.x + uv132.z ),hsvTorgb43.y,hsvTorgb43.z) );
					float3 desaturateInitialColor46 = hsvTorgb44;
					float desaturateDot46 = dot( desaturateInitialColor46, float3( 0.299, 0.587, 0.114 ));
					float3 desaturateVar46 = lerp( desaturateInitialColor46, desaturateDot46.xxx, _Desaturation );
					float4 _Vector1 = float4(-0.3,1,-2,1);
					float3 temp_cast_1 = (_Vector1.x).xxx;
					float3 temp_cast_2 = (_Vector1.y).xxx;
					float3 temp_cast_3 = (_Vector1.z).xxx;
					float3 temp_cast_4 = (_Vector1.w).xxx;
					float3 clampResult37 = clamp( (temp_cast_3 + (desaturateVar46 - temp_cast_1) * (temp_cast_4 - temp_cast_3) / (temp_cast_2 - temp_cast_1)) , float3( 0,0,0 ) , float3( 1,1,1 ) );
					float2 uv_Main = i.texcoord.xy * _Main_ST.xy + _Main_ST.zw;
					float clampResult5 = clamp( ( tex2D( _Main, uv_Main ).a * _Opacity ) , 0.0 , 1.0 );
					float4 _SpeedMainTexUVNoise = float4(0,0,0,0);
					float2 appendResult26 = (float2(_SpeedMainTexUVNoise.z , _SpeedMainTexUVNoise.w));
					float4 uv0_Dissolve = i.texcoord;
					uv0_Dissolve.xy = i.texcoord.xy * _Dissolve_ST.xy + _Dissolve_ST.zw;
					float2 panner28 = ( 1.0 * _Time.y * appendResult26 + uv0_Dissolve.xy);
					float2 break29 = panner28;
					float2 appendResult30 = (float2(break29.x , ( uv132.w + break29.y )));
					float t21 = uv0_Dissolve.w;
					float w19 = uv0_Dissolve.z;
					float3 _Vector0 = float3(0.45,0,1);
					float ifLocalVar15 = 0;
					if( ( tex2D( _Dissolve, appendResult30 ).r * t21 ) >= w19 )
					ifLocalVar15 = _Vector0.y;
					else
					ifLocalVar15 = _Vector0.z;
					float4 appendResult6 = (float4(( ( _Color0 * i.color ) + ( float4( clampResult37 , 0.0 ) * _Emission * i.color ) ).rgb , ( i.color.a * clampResult5 * ifLocalVar15 )));
					

					fixed4 col = appendResult6;
					UNITY_APPLY_FOG(i.fogCoord, col);
					return col;
				}
				ENDCG 
			}
		}	
	}
	
	
	
}
/*ASEBEGIN
Version=16800
418;636;850;1052;2147.683;512.1678;1.819735;True;False
Node;AmplifyShaderEditor.Vector4Node;24;-1841.387,322.843;Float;False;Constant;_SpeedMainTexUVNoise;Speed MainTex U/V + Noise;3;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;33;-1082.243,-771.2961;Float;True;Property;_Emissiontex;Emission tex;1;0;Create;True;0;0;False;0;None;6cc3d96675ff6e84aba34f7c4bc65a91;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;18;-1375.148,384.8417;Float;False;0;12;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;26;-1530.972,523.2922;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;28;-1109.666,606.493;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;32;-1008.45,111.2962;Float;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RGBToHSVNode;43;-790.9394,-725.2483;Float;False;1;0;FLOAT3;0,0,0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.BreakToComponentsNode;29;-892.1788,617.0017;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;45;-574.5346,-568.1847;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-627.3167,677.1434;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HSVToRGBNode;44;-470.9129,-729.5052;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;47;-270.5224,-508.5591;Float;False;Property;_Desaturation;Desaturation;6;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DesaturateOpNode;46;-158.5033,-774.4081;Float;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;30;-585.9617,530.4772;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-1056.738,480.4555;Float;False;t;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector4Node;36;78.40589,-466.0594;Float;False;Constant;_Vector1;Vector 1;4;0;Create;True;0;0;False;0;-0.3,1,-2,1;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;12;-287.1259,350.5467;Float;True;Property;_Dissolve;Dissolve;3;0;Create;True;0;0;False;0;a82b3ac4c2c008046966593492fc37c2;a82b3ac4c2c008046966593492fc37c2;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;23;-233.5985,577.4151;Float;False;21;t;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;34;285.9299,-586.5805;Float;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;1,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;19;-1063.472,345.7932;Float;False;w;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;2;-296.7824,-42.32771;Float;True;Property;_Main;Main;0;0;Create;True;0;0;False;0;None;39264afbdcef0ad4b94caeff0ee4489f;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;4;-83.4223,170.6978;Float;False;Property;_Opacity;Opacity;2;0;Create;True;0;0;False;0;20;20;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;17;-0.613778,622.4282;Float;False;Constant;_Vector0;Vector 0;3;0;Create;True;0;0;False;0;0.45,0,1;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.ColorNode;40;413.031,-783.4872;Float;False;Property;_Color0;Color 0;5;0;Create;True;0;0;False;0;0,0,0,0;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.VertexColorNode;8;117.0936,-215.9465;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;14;72.53825,400.5338;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;22;35.59181,512.3385;Float;False;19;w;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;3;24.0196,17.01687;Float;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;39;435.4381,-346.5544;Float;False;Property;_Emission;Emission;4;0;Create;True;0;0;False;0;2;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;37;542.9785,-506.3854;Float;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;686.6464,-663.8107;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;678.4387,-472.6388;Float;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ClampOpNode;5;178.0719,21.2527;Float;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ConditionalIfNode;15;278.5831,385.9034;Float;True;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;7;346.1999,-76.61214;Float;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;41;876.6212,-627.5263;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;25;-1477.972,240.2926;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;6;523.1243,-87.29204;Float;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;712.3647,-62.70739;Float;False;True;2;Float;;0;7;Hovl/Particles/SwordSlash1;0b6a9f8b4f707c74ca64c0be8e590de0;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;2;5;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;True;True;True;True;False;0;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;4;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;False;0;False;False;False;False;False;False;False;False;False;False;True;0;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;26;0;24;3
WireConnection;26;1;24;4
WireConnection;28;0;18;0
WireConnection;28;2;26;0
WireConnection;43;0;33;0
WireConnection;29;0;28;0
WireConnection;45;0;43;1
WireConnection;45;1;32;3
WireConnection;31;0;32;4
WireConnection;31;1;29;1
WireConnection;44;0;45;0
WireConnection;44;1;43;2
WireConnection;44;2;43;3
WireConnection;46;0;44;0
WireConnection;46;1;47;0
WireConnection;30;0;29;0
WireConnection;30;1;31;0
WireConnection;21;0;18;4
WireConnection;12;1;30;0
WireConnection;34;0;46;0
WireConnection;34;1;36;1
WireConnection;34;2;36;2
WireConnection;34;3;36;3
WireConnection;34;4;36;4
WireConnection;19;0;18;3
WireConnection;14;0;12;1
WireConnection;14;1;23;0
WireConnection;3;0;2;4
WireConnection;3;1;4;0
WireConnection;37;0;34;0
WireConnection;42;0;40;0
WireConnection;42;1;8;0
WireConnection;38;0;37;0
WireConnection;38;1;39;0
WireConnection;38;2;8;0
WireConnection;5;0;3;0
WireConnection;15;0;14;0
WireConnection;15;1;22;0
WireConnection;15;2;17;2
WireConnection;15;3;17;2
WireConnection;15;4;17;3
WireConnection;7;0;8;4
WireConnection;7;1;5;0
WireConnection;7;2;15;0
WireConnection;41;0;42;0
WireConnection;41;1;38;0
WireConnection;25;0;24;1
WireConnection;25;1;24;2
WireConnection;6;0;41;0
WireConnection;6;3;7;0
WireConnection;1;0;6;0
ASEEND*/
//CHKSM=0E8544B58D6128DB45D077D27B59E757EA79DAB9