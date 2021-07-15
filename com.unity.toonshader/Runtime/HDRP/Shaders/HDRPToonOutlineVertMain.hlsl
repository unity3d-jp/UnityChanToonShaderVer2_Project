//Unity Toon Shader/HDRP
//nobuyuki@unity3d.com
//toshiyuki@unity3d.com (Universal RP/HDRP) 


#if 1
        float4 objPos = mul(unity_ObjectToWorld, float4(0, 0, 0, 1));
        float2 Set_UV0 = inputMesh.uv0;
        float4 _Outline_Sampler_var = tex2Dlod(_Outline_Sampler, float4(TRANSFORM_TEX(Set_UV0, _Outline_Sampler), 0.0, 0));
        //v.2.0.4.3 baked Normal Texture for Outline
        float3 normalDir = UnityObjectToWorldNormal(inputMesh.normalOS);
        float3 tangentDir = normalize(mul(unity_ObjectToWorld, float4(inputMesh.tangentOS.xyz, 0.0)).xyz);
        float3 bitangentDir = normalize(cross(normalDir, tangentDir) * inputMesh.tangentOS.w);
        float3x3 tangentTransform = float3x3(tangentDir, bitangentDir, normalDir);
        //UnpackNormal() can't be used, and so as follows. Do not specify a bump for the texture to be used.
        float4 _BakedNormal_var = (tex2Dlod(_BakedNormal, float4(TRANSFORM_TEX(Set_UV0, _BakedNormal), 0.0, 0)) * 2 - 1);
        float3 _BakedNormalDir = normalize(mul(_BakedNormal_var.rgb, tangentTransform));
        //end
        float Set_Outline_Width = (_Outline_Width*0.001*smoothstep(_Farthest_Distance, _Nearest_Distance, distance(objPos.rgb, _WorldSpaceCameraPos))*_Outline_Sampler_var.rgb).r;
        Set_Outline_Width *= (1.0f - _ZOverDrawMode);
        //v.2.0.7.5
        float4 _ClipCameraPos = mul(UNITY_MATRIX_VP, float4(_WorldSpaceCameraPos.xyz, 1));
        //v.2.0.7
#if defined(UNITY_REVERSED_Z)
    //v.2.0.4.2 (DX)
        _Offset_Z = _Offset_Z * -0.01;
#else
    //OpenGL
        _Offset_Z = _Offset_Z * 0.01;
#endif
        //v2.0.4
#ifdef _OUTLINE_NML
        //v.2.0.4.3 baked Normal Texture for Outline
        float4 clipPos= UnityObjectToClipPos(lerp(float4(inputMesh.positionOS + inputMesh.normalOS*Set_Outline_Width, 1), float4(inputMesh.positionOS.xyz + _BakedNormalDir * Set_Outline_Width, 1), _Is_BakedNormal));
#elif _OUTLINE_POS
        Set_Outline_Width = Set_Outline_Width * 2;
        float signVar = dot(normalize(inputMesh.positionOS), normalize(inputMesh.normalOS)) < 0 ? -1 : 1;
        float4 clipPos = UnityObjectToClipPos(float4(inputMesh.positionOS + signVar * normalize(inputMesh.positionOS)*Set_Outline_Width, 1));
#endif
        //v.2.0.7.5
        clipPos.z = clipPos.z + _Offset_Z * _ClipCameraPos.z;

        float4 rws = mul(UNITY_MATRIX_I_P, clipPos); // use UNITY_MATRIX_I_P instead of unity_CameraInvProjection.
        rws = mul(UNITY_MATRIX_I_V, rws);            // use UNITY_MATRIX_I_V instead of unity_cameraToWorld.
#ifndef TESSELLATION_ON
        varyingsType.vmesh.positionCS = clipPos;
#endif // TESSELLATION_ON
        varyingsType.vmesh.positionRWS = rws.xyz;

#endif // #if 1
