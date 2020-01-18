// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "g51/Sprite-Cutoff"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
		[PerRendererData] _AlphaTex ("External Alpha", 2D) = "white" {}
		[Header(____________ Shader Propertys ____________)][Space(16)]_Cutoff("Cutoff", Range( 0 , 1)) = 0
		_Tint("Tint", Color) = (1,1,1,1)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
	}

	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" "PreviewType"="Plane" "CanUseSpriteAtlas"="True" }

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		
		
		Pass
		{
		CGPROGRAM
			
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma multi_compile _ ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			

			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				
			};
			
			uniform fixed4 _Color;
			uniform float _EnableExternalAlpha;
			uniform sampler2D _MainTex;
			uniform sampler2D _AlphaTex;
			uniform float4 _Tint;
			uniform float4 _MainTex_ST;
			uniform float _Cutoff;
			
			v2f vert( appdata_t IN  )
			{
				v2f OUT;
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
				UNITY_TRANSFER_INSTANCE_ID(IN, OUT);
				
				
				IN.vertex.xyz +=  float3(0,0,0) ; 
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);

#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				fixed4 alpha = tex2D (_AlphaTex, uv);
				color.a = lerp (color.a, alpha.r, _EnableExternalAlpha);
#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}
			
			fixed4 frag(v2f IN  ) : SV_Target
			{
				float4 appendResult42 = (float4(_Tint.r , _Tint.g , _Tint.b , 1.0));
				float2 uv_MainTex = IN.texcoord.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float4 tex2DNode6 = tex2D( _MainTex, uv_MainTex );
				float4 appendResult36 = (float4(tex2DNode6.rgb , 1.0));
				float4 TextureColor35 = appendResult36;
				float4 lerpResult46 = lerp( IN.color , ( IN.color * TextureColor35 ) , _Tint.a);
				float Alpha31 = tex2DNode6.a;
				float4 appendResult38 = (float4(1.0 , 1.0 , 1.0 , ceil( ( Alpha31 - _Cutoff ) )));
				
				fixed4 c = ( appendResult42 * lerpResult46 * appendResult38 );
				c.rgb *= c.a;
				return c;
			}
		ENDCG
		}
	}
	CustomEditor "ASEMaterialInspector"
	
	
}
/*ASEBEGIN
Version=17000
0;0;1920;1019;3157.177;504.1772;1;True;False
Node;AmplifyShaderEditor.TemplateShaderPropertyNode;5;-3090.414,-16.50349;Float;False;0;0;_MainTex;Shader;0;5;SAMPLER2D;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;6;-2855.471,-18.42798;Float;True;Property;_TextureSample0;Texture Sample 0;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;36;-2555.665,-14.13708;Float;False;COLOR;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;35;-2411.665,-19.13707;Float;False;TextureColor;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;-2409.426,78.26735;Float;False;Alpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;74;-2138.76,-81.23169;Float;False;Property;_Tint;Tint;1;0;Create;True;0;0;False;0;1,1,1,1;1,1,1,0.2470588;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;37;-1793,-23;Float;False;35;TextureColor;1;0;OBJECT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;80;-1870.76,-50.23169;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;17;-1777,-183;Float;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;39;-1840,189;Float;False;Property;_Cutoff;Cutoff;0;0;Create;True;0;0;False;2;Header(____________ Shader Propertys ____________);Space(16);0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;79;-1905.76,-77.23169;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-1740,118;Float;False;31;Alpha;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;78;-1887.76,-65.23169;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;75;-1890.76,-250.2317;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;76;-1872.76,-236.2317;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;47;-1587,-108;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;77;-1856.76,-220.2317;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;23;-1564,149;Float;False;2;0;FLOAT;0;False;1;FLOAT;0.75;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;82;-1423.76,48.76831;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;81;-1403.76,32.76831;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.WireNode;67;-1418.864,-106.7019;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CeilOpNode;9;-1434,149;Float;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;42;-1775,-329;Float;False;COLOR;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;46;-1328,-179;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.DynamicAppendNode;38;-1305,77;Float;False;FLOAT4;4;0;FLOAT;1;False;1;FLOAT;1;False;2;FLOAT;1;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.WireNode;85;-1195.649,-292.4271;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;84;-1157.649,-107.4271;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;86;-1175.649,-279.4271;Float;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.WireNode;83;-1140.649,60.57288;Float;False;1;0;FLOAT4;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-1024,-128;Float;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT4;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;-836.8118,-129.5598;Float;False;True;2;Float;ASEMaterialInspector;0;6;g51/Sprite-Cutoff;0f8ba0101102bb14ebf021ddadce9b49;True;SubShader 0 Pass 0;0;0;SubShader 0 Pass 0;2;True;3;1;False;-1;10;False;-1;0;1;False;-1;0;False;-1;False;False;True;2;False;-1;False;False;True;2;False;-1;False;False;True;5;Queue=Transparent=Queue=0;IgnoreProjector=True;RenderType=Transparent=RenderType;PreviewType=Plane;CanUseSpriteAtlas=True;False;0;False;False;False;False;False;False;False;False;False;False;True;2;0;;0;0;Standard;0;0;1;True;False;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;0
WireConnection;6;0;5;0
WireConnection;36;0;6;0
WireConnection;35;0;36;0
WireConnection;31;0;6;4
WireConnection;80;0;74;3
WireConnection;79;0;74;1
WireConnection;78;0;74;2
WireConnection;75;0;79;0
WireConnection;76;0;78;0
WireConnection;47;0;17;0
WireConnection;47;1;37;0
WireConnection;77;0;80;0
WireConnection;23;0;33;0
WireConnection;23;1;39;0
WireConnection;82;0;74;4
WireConnection;81;0;82;0
WireConnection;67;0;47;0
WireConnection;9;0;23;0
WireConnection;42;0;75;0
WireConnection;42;1;76;0
WireConnection;42;2;77;0
WireConnection;46;0;17;0
WireConnection;46;1;67;0
WireConnection;46;2;81;0
WireConnection;38;3;9;0
WireConnection;85;0;42;0
WireConnection;84;0;46;0
WireConnection;86;0;85;0
WireConnection;83;0;38;0
WireConnection;48;0;86;0
WireConnection;48;1;84;0
WireConnection;48;2;83;0
WireConnection;0;0;48;0
ASEEND*/
//CHKSM=154889D645B9FC85E913060CC4469C96FDB0B833