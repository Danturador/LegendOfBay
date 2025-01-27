Shader "Custom/SpriteGradientTilemap" {
	Properties{
		_MainTex("Sprite Texture", 2D) = "white" {}
		_Color("Down Color", Color) = (1, 1, 1, 1)
		_Color2("Up Color", Color) = (1, 1, 1, 1)
		_GradientScale("Gradient Scale", Range(0.1, 10)) = 1.0
		_Color0Boundary("Color0 Boundary", Range(0, 1)) = 0.5
		_Color1Boundary("Color1 Boundary", Range(0, 1)) = 1.0
		_TransitionSize0("Transition Size 0", Range(0.01, 1)) = 0.1
		_TransitionSize1("Transition Size 1", Range(0.01, 1)) = 0.1
	}
	SubShader{
		Tags { "Queue" = "Transparent" "IgnoreProjector" = "True" }
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			fixed4 _Color;
			fixed4 _Color2;
			sampler2D _MainTex;
			float _GradientScale;
			float _Color0Boundary;
			float _Color1Boundary;
			float _TransitionSize0;
			float _TransitionSize1;

			struct appdata_custom {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 screenPos : TEXCOORD1;
			};

			v2f vert(appdata_custom v) {
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = v.texcoord;
				o.screenPos = o.pos;
				return o;
			}

			float4 frag(v2f i) : COLOR {
				float4 texColor = tex2D(_MainTex, i.uv);
				float t = ((i.screenPos.y + 1.0) * 0.5) * _GradientScale;
				t = clamp(t, 0.0, 1.0);

				float gradientT = 0.0;

				if (t < _Color0Boundary - _TransitionSize0) {
					gradientT = 0.0;
				}
				else if (t < _Color0Boundary) {
						gradientT = (t - (_Color0Boundary - _TransitionSize0)) / _TransitionSize0;
				}
				else if (t < _Color1Boundary - _TransitionSize1) {
					gradientT = 1.0;
				}
				else if (t < _Color1Boundary) {
					gradientT = 1.0 - (t - (_Color1Boundary - _TransitionSize1)) / _TransitionSize1;
				}
				else {
					gradientT = 1.0;
				}

				float4 gradientColor = lerp(_Color, _Color2, gradientT);
				
				return float4(gradientColor.rgb * texColor.rgb, texColor.a);
			}
			
			ENDCG
		}
	}
}