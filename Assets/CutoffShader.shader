Shader "91YGame/TransparentCutoff" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        //申明_Cutoff变量;
        _Cutoff("Cutoff Value",Range(0.01,1))=0.5
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        LOD 200
        
        //告诉着色器 我们需要的是一个裁剪类型的着色器;
        CGPROGRAM
        #pragma surface surf Lambert alphatest:_Cutoff

        sampler2D _MainTex;
        struct Input {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = c.rgb;
            o.Alpha = c.b;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}