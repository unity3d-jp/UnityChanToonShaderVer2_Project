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
            Cull Off // 裏向きのカリングをオフにします

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
                // VFACE 入力は正面向きでは負の値、
                // 裏向きでは負の値です。その値によって 
                // 2 色のうちの 1 つを出力します。
                return facing > 0 ? _ColorFront : _ColorBack;
            }
            ENDCG
        }
    }
}