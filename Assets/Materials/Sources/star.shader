// Shader created with Shader Forge v1.38 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.38;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,atwp:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:33968,y:33036,varname:node_3138,prsc:2|emission-4790-OUT;n:type:ShaderForge.SFN_Tex2d,id:1399,x:32441,y:32756,ptovrint:False,ptlb:Surface Texture 1,ptin:_SurfaceTexture1,varname:node_1399,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3cded139e5bb52444ae4e9e8668148c2,ntxv:0,isnm:False|UVIN-6312-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:2517,x:32497,y:33330,ptovrint:False,ptlb:Surface Texture 2,ptin:_SurfaceTexture2,varname:_node_1399_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3cded139e5bb52444ae4e9e8668148c2,ntxv:0,isnm:False|UVIN-9994-UVOUT;n:type:ShaderForge.SFN_Slider,id:5917,x:32046,y:32397,ptovrint:False,ptlb:Glow,ptin:_Glow,varname:node_5917,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Add,id:297,x:32840,y:32949,varname:node_297,prsc:2|A-3549-OUT,B-1399-RGB;n:type:ShaderForge.SFN_Color,id:8042,x:32441,y:33124,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_8042,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.3103448,c3:0,c4:1;n:type:ShaderForge.SFN_Add,id:5122,x:33069,y:33039,varname:node_5122,prsc:2|A-297-OUT,B-8042-RGB;n:type:ShaderForge.SFN_Add,id:8500,x:32827,y:33272,varname:node_8500,prsc:2|A-3549-OUT,B-2517-RGB;n:type:ShaderForge.SFN_Add,id:2824,x:33069,y:33201,varname:node_2824,prsc:2|A-8500-OUT,B-8042-RGB;n:type:ShaderForge.SFN_Time,id:2976,x:31574,y:33609,varname:node_2976,prsc:2;n:type:ShaderForge.SFN_Panner,id:9994,x:32193,y:33329,varname:node_9994,prsc:2,spu:1,spv:1|UVIN-2837-UVOUT,DIST-2360-OUT;n:type:ShaderForge.SFN_TexCoord,id:2837,x:31755,y:33321,varname:node_2837,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2360,x:31941,y:33690,varname:node_2360,prsc:2|A-2976-T,B-8635-OUT;n:type:ShaderForge.SFN_Vector2,id:780,x:31283,y:33763,varname:node_780,prsc:2,v1:1,v2:0;n:type:ShaderForge.SFN_Panner,id:6312,x:32183,y:32631,varname:node_6312,prsc:2,spu:1,spv:1|UVIN-4935-UVOUT,DIST-2863-OUT;n:type:ShaderForge.SFN_TexCoord,id:4935,x:31745,y:32623,varname:node_4935,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:2863,x:31931,y:32992,varname:node_2863,prsc:2|A-1295-T,B-6040-OUT;n:type:ShaderForge.SFN_Time,id:1295,x:31564,y:32911,varname:node_1295,prsc:2;n:type:ShaderForge.SFN_Vector2,id:695,x:31250,y:33065,varname:node_695,prsc:2,v1:0,v2:1;n:type:ShaderForge.SFN_Slider,id:793,x:31188,y:33197,ptovrint:False,ptlb:Tex1 Speed,ptin:_Tex1Speed,varname:node_793,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:6040,x:31614,y:33054,varname:node_6040,prsc:2|A-695-OUT,B-793-OUT;n:type:ShaderForge.SFN_Slider,id:2113,x:31185,y:33928,ptovrint:False,ptlb:Text2 Speed,ptin:_Text2Speed,varname:_node_793_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:-1,cur:0,max:1;n:type:ShaderForge.SFN_Multiply,id:8635,x:31659,y:33844,varname:node_8635,prsc:2|A-780-OUT,B-2113-OUT;n:type:ShaderForge.SFN_Multiply,id:3315,x:33322,y:33101,varname:node_3315,prsc:2|A-5122-OUT,B-2824-OUT;n:type:ShaderForge.SFN_Add,id:4790,x:33312,y:33268,varname:node_4790,prsc:2|A-5122-OUT,B-2824-OUT;n:type:ShaderForge.SFN_Subtract,id:3549,x:32563,y:32516,varname:node_3549,prsc:2|A-5917-OUT,B-1522-OUT;n:type:ShaderForge.SFN_Slider,id:1522,x:32026,y:32512,ptovrint:False,ptlb:Darken,ptin:_Darken,varname:node_1522,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.367521,max:5;proporder:1399-2517-5917-8042-793-2113-1522;pass:END;sub:END;*/

Shader "Shader Forge/star" {
    Properties {
        _SurfaceTexture1 ("Surface Texture 1", 2D) = "white" {}
        _SurfaceTexture2 ("Surface Texture 2", 2D) = "white" {}
        _Glow ("Glow", Range(0, 1)) = 1
        _Color ("Color", Color) = (1,0.3103448,0,1)
        _Tex1Speed ("Tex1 Speed", Range(-1, 1)) = 0
        _Text2Speed ("Text2 Speed", Range(-1, 1)) = 0
        _Darken ("Darken", Range(0, 5)) = 1.367521
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
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _SurfaceTexture1; uniform float4 _SurfaceTexture1_ST;
            uniform sampler2D _SurfaceTexture2; uniform float4 _SurfaceTexture2_ST;
            uniform float _Glow;
            uniform float4 _Color;
            uniform float _Tex1Speed;
            uniform float _Text2Speed;
            uniform float _Darken;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_3549 = (_Glow-_Darken);
                float4 node_1295 = _Time;
                float2 node_6312 = (i.uv0+(node_1295.g*(float2(0,1)*_Tex1Speed))*float2(1,1));
                float4 _SurfaceTexture1_var = tex2D(_SurfaceTexture1,TRANSFORM_TEX(node_6312, _SurfaceTexture1));
                float3 node_5122 = ((node_3549+_SurfaceTexture1_var.rgb)+_Color.rgb);
                float4 node_2976 = _Time;
                float2 node_9994 = (i.uv0+(node_2976.g*(float2(1,0)*_Text2Speed))*float2(1,1));
                float4 _SurfaceTexture2_var = tex2D(_SurfaceTexture2,TRANSFORM_TEX(node_9994, _SurfaceTexture2));
                float3 node_2824 = ((node_3549+_SurfaceTexture2_var.rgb)+_Color.rgb);
                float3 emissive = (node_5122+node_2824);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
