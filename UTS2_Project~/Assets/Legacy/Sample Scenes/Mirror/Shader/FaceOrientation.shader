Shader "Unlit/FaceOrientation"
{
    Properties
    {
        _ColorFront ("Front Color", Color) = (1,0.7,0.7,1)
        _ColorBack ("Back Color", Color) = (0.7,1,0.7,1)
    }
    SubShader
    {
        Pass
        {
            Cull Off // Turn off the culling backwards

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0

            float4 vert (float4 vertex : POSITION) : SV_POSITION
            {
                return UnityObjectToClipPos(vertex);
            }

            fixed4 _ColorFront;
            fixed4 _ColorBack;

            fixed4 frag (fixed facing : VFACE) : SV_Target
            {
                // VFACE 
                // The input is a negative value in the frontal direction and a negative value in the reverse direction.
                // Output one of the two colors depending on its value.
                return facing > 0 ? _ColorFront : _ColorBack;
            }
            ENDCG
        }
    }
}