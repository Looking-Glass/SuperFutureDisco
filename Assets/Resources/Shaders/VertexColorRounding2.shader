// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:False,mssp:True,bkdf:True,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:0,x:34124,y:32822,varname:node_0,prsc:2|emission-5010-OUT;n:type:ShaderForge.SFN_VertexColor,id:4,x:32946,y:32814,varname:node_4,prsc:2;n:type:ShaderForge.SFN_Time,id:3384,x:32802,y:32737,varname:node_3384,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9059,x:32989,y:32695,varname:node_9059,prsc:2|A-6084-OUT,B-3384-TSL;n:type:ShaderForge.SFN_ValueProperty,id:6084,x:32754,y:32665,ptovrint:False,ptlb:timeScale,ptin:_timeScale,varname:node_6084,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Add,id:9259,x:33372,y:32695,varname:node_9259,prsc:2|A-8979-OUT,B-9774-HOUT;n:type:ShaderForge.SFN_Frac,id:8979,x:33156,y:32695,varname:node_8979,prsc:2|IN-9059-OUT;n:type:ShaderForge.SFN_RgbToHsv,id:9774,x:33209,y:32825,varname:node_9774,prsc:2|IN-4-RGB;n:type:ShaderForge.SFN_HsvToRgb,id:5010,x:33584,y:32811,varname:node_5010,prsc:2|H-9259-OUT,S-9774-SOUT,V-9774-VOUT;proporder:6084;pass:END;sub:END;*/

Shader "Shader Forge/Examples/Vertex Color Rounding2" {
    Properties {
        _timeScale ("timeScale", Float ) = 1
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
            #pragma exclude_renderers gles xbox360 ps3 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _timeScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float4 node_3384 = _Time + _TimeEditor;
                float4 node_9774_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_9774_p = lerp(float4(float4(i.vertexColor.rgb,0.0).zy, node_9774_k.wz), float4(float4(i.vertexColor.rgb,0.0).yz, node_9774_k.xy), step(float4(i.vertexColor.rgb,0.0).z, float4(i.vertexColor.rgb,0.0).y));
                float4 node_9774_q = lerp(float4(node_9774_p.xyw, float4(i.vertexColor.rgb,0.0).x), float4(float4(i.vertexColor.rgb,0.0).x, node_9774_p.yzx), step(node_9774_p.x, float4(i.vertexColor.rgb,0.0).x));
                float node_9774_d = node_9774_q.x - min(node_9774_q.w, node_9774_q.y);
                float node_9774_e = 1.0e-10;
                float3 node_9774 = float3(abs(node_9774_q.z + (node_9774_q.w - node_9774_q.y) / (6.0 * node_9774_d + node_9774_e)), node_9774_d / (node_9774_q.x + node_9774_e), node_9774_q.x);;
                float3 emissive = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((frac((_timeScale*node_3384.r))+node_9774.r)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_9774.g)*node_9774.b);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #include "UnityCG.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma exclude_renderers gles xbox360 ps3 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _timeScale;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_3384 = _Time + _TimeEditor;
                float4 node_9774_k = float4(0.0, -1.0 / 3.0, 2.0 / 3.0, -1.0);
                float4 node_9774_p = lerp(float4(float4(i.vertexColor.rgb,0.0).zy, node_9774_k.wz), float4(float4(i.vertexColor.rgb,0.0).yz, node_9774_k.xy), step(float4(i.vertexColor.rgb,0.0).z, float4(i.vertexColor.rgb,0.0).y));
                float4 node_9774_q = lerp(float4(node_9774_p.xyw, float4(i.vertexColor.rgb,0.0).x), float4(float4(i.vertexColor.rgb,0.0).x, node_9774_p.yzx), step(node_9774_p.x, float4(i.vertexColor.rgb,0.0).x));
                float node_9774_d = node_9774_q.x - min(node_9774_q.w, node_9774_q.y);
                float node_9774_e = 1.0e-10;
                float3 node_9774 = float3(abs(node_9774_q.z + (node_9774_q.w - node_9774_q.y) / (6.0 * node_9774_d + node_9774_e)), node_9774_d / (node_9774_q.x + node_9774_e), node_9774_q.x);;
                o.Emission = (lerp(float3(1,1,1),saturate(3.0*abs(1.0-2.0*frac((frac((_timeScale*node_3384.r))+node_9774.r)+float3(0.0,-1.0/3.0,1.0/3.0)))-1),node_9774.g)*node_9774.b);
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
