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
        public int m_logic;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if ( m_logic > 2 )
            {
                m_logic = 2;
            }
            if ( m_logic < 0 )
            {
                m_logic = 0;
            }
            Shader.SetGlobalInt(EXPOSURE_CURVE_PROP_NMAE, m_logic);
        }
    }

    public enum UTS_ExposureCurveType
    {
        Linier,
        Log,
        Log2,

    };

}