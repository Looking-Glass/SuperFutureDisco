// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33032,y:32379,varname:node_3138,prsc:2|emission-8921-OUT,voffset-8458-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32237,y:32250,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_FragmentPosition,id:7347,x:31655,y:32850,varname:node_7347,prsc:2;n:type:ShaderForge.SFN_Add,id:3011,x:31875,y:32626,varname:node_3011,prsc:2|A-7347-Z,B-9243-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9243,x:31703,y:32623,ptovrint:False,ptlb:add,ptin:_add,varname:node_9243,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Divide,id:5504,x:32129,y:32648,varname:node_5504,prsc:2|A-3011-OUT,B-6290-OUT;n:type:ShaderForge.SFN_Frac,id:150,x:32313,y:32595,varname:node_150,prsc:2|IN-5504-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6290,x:31983,y:32763,ptovrint:False,ptlb:div,ptin:_div,varname:node_6290,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Lerp,id:5423,x:32450,y:32497,varname:node_5423,prsc:2|A-7241-RGB,B-9601-OUT,T-150-OUT;n:type:ShaderForge.SFN_Color,id:5715,x:32034,y:32080,ptovrint:False,ptlb:Color_copy,ptin:_Color_copy,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.07843138,c2:0.3921569,c3:0.7843137,c4:1;n:type:ShaderForge.SFN_Posterize,id:8921,x:32575,y:32379,varname:node_8921,prsc:2|IN-9601-OUT,STPS-8513-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8513,x:32423,y:32334,ptovrint:False,ptlb:postSteps,ptin:_postSteps,varname:node_8513,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_NormalVector,id:9663,x:32522,y:32761,prsc:2,pt:False;n:type:ShaderForge.SFN_Multiply,id:8458,x:32708,y:32701,varname:node_8458,prsc:2|A-9106-OUT,B-9663-OUT,C-3222-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3222,x:32655,y:32913,ptovrint:False,ptlb:vertOffsetAmt,ptin:_vertOffsetAmt,varname:node_3222,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Sin,id:9106,x:32273,y:32761,varname:node_9106,prsc:2|IN-5504-OUT;n:type:ShaderForge.SFN_HsvToRgb,id:9601,x:32086,y:32332,varname:node_9601,prsc:2|H-5362-OUT,S-2367-OUT,V-8819-OUT;n:type:ShaderForge.SFN_Frac,id:5362,x:31879,y:32190,varname:node_5362,prsc:2|IN-5504-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2367,x:31902,y:32395,ptovrint:False,ptlb:value,ptin:_value,varname:node_2367,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:8819,x:31889,y:32332,ptovrint:False,ptlb:saturation,ptin:_saturation,varname:_node_2367_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:7241-9243-6290-5715-8513-3222-2367-8819;pass:END;sub:END;*/

Shader "Shader Forge/PartyTime" {
    Properties {
        _Color ("Color", Color) = (0.07843138,0.3921569,0.7843137,1)
        _add ("add", Float ) = 0
        _div ("div", Float ) = 1
        _Color_copy ("Color_copy", Color) = (0.07843138,0.3921569,0.7843137,1)
        _postSteps ("postSteps", Float ) = 3
        _vertOffsetAmt ("vertOffsetAmt", Float ) = 1
        _value ("value", Float ) = 1
        _saturation ("saturation", Float ) = 1
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
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
            uniform float _add;
            uniform float _div;
            uniform float _postSteps;
            uniform float _vertOffsetAmt;
            uniform float _value;
            uniform float _saturation;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_5504 = ((mul(unity_ObjectToWorld, v.vertex).b+_add)/_div);
                v.vertex.xyz += (sin(node_5504)*v.normal*_vertOffsetAmt);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float node_5504 = ((i.posWorld.b+_add)/_div);
                float3 node_9601 = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac(frac(node_5504)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),_value)*_saturation);
                float3 emissive = floor(node_9601 * _postSteps) / (_postSteps - 1);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _add;
            uniform float _div;
            uniform float _vertOffsetAmt;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                float node_5504 = ((mul(unity_ObjectToWorld, v.vertex).b+_add)/_div);
                v.vertex.xyz += (sin(node_5504)*v.normal*_vertOffsetAmt);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
