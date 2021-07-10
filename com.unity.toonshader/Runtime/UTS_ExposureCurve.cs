using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.Rendering.Toon
{
    [ExecuteAlways]
    [DisallowMultipleComponent]
    public class UTS_ExposureCurve : MonoBehaviour
    {
        const string EXPOSURE_CURVE_PROP_NMAE = "_ExposureCurveType";
        [SerializeField]
        public int m_CurveType;
        [SerializeField]
        public bool m_LinearFrom0to10 = true;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            const int logMax = 4;
            if ( m_CurveType >= logMax)
            {
                m_CurveType = logMax - 1;
            }
            if ( m_CurveType < 0 )
            {
                m_CurveType = 0;
            }
            Shader.SetGlobalInt(EXPOSURE_CURVE_PROP_NMAE, m_CurveType);
        }
    }

    public enum UTS_ExposureCurveType
    {
        Linear,
        Log,
        Log2,
        Log10
    };

}