// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32515,y:32567,varname:node_3138,prsc:2|emission-7241-RGB,clip-4569-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32180,y:32546,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:7976,x:31513,y:32865,varname:node_7976,prsc:2;n:type:ShaderForge.SFN_Sin,id:4090,x:32006,y:32864,varname:node_4090,prsc:2|IN-8937-OUT;n:type:ShaderForge.SFN_Multiply,id:8937,x:31841,y:32699,varname:node_8937,prsc:2|A-6539-OUT,B-7441-OUT,C-5468-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6539,x:31612,y:32608,ptovrint:False,ptlb:tiling,ptin:_tiling,varname:node_6539,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:5;n:type:ShaderForge.SFN_Tau,id:7441,x:31645,y:32720,varname:node_7441,prsc:2;n:type:ShaderForge.SFN_Subtract,id:4569,x:32219,y:32761,varname:node_4569,prsc:2|A-4090-OUT,B-1861-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1861,x:32061,y:32743,ptovrint:False,ptlb:width,ptin:_width,varname:_clamp2,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:5468,x:31779,y:32895,varname:node_5468,prsc:2|A-7976-Z,B-5512-OUT;n:type:ShaderForge.SFN_Time,id:1701,x:31594,y:33053,varname:node_1701,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5512,x:31779,y:33053,varname:node_5512,prsc:2|A-1701-TSL,B-9307-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9307,x:31610,y:33211,ptovrint:False,ptlb:Speed,ptin:_Speed,varname:node_9307,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-6539-1861-9307;pass:END;sub:END;*/

Shader "Shader Forge/GridShader" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _tiling ("tiling", Float ) = 5
        _width ("width", Float ) = 0.5
        _Speed ("Speed", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform float _tiling;
            uniform float _width;
            uniform float _Speed;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 node_1701 = _Time + _TimeEditor;
                clip((sin((_tiling*6.28318530718*(i.posWorld.b+(node_1701.r*_Speed))))-_width) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _tiling;
            uniform float _width;
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                clip((sin((_tiling*6.28318530718*i.posWorld.r))-_width) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
