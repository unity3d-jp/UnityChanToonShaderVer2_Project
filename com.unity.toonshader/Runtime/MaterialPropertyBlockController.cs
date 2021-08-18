using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPropertyBlockController : MonoBehaviour
{
    public  GameObject[] objs;
    [SerializeField]
    Renderer[] renderers;
    [SerializeField]
    MaterialPropertyBlock[] materialPropertyBlocks;
    private void Awake()
    {
        if ( objs == null || objs.Length == 0 )
        {
            return;
        }
        int objCount = objs.Length;
        int rendererCount = 0;
        for ( int ii = 0; ii < objCount;ii++)
        {
            var renderer = objs[ii].GetComponent<Renderer>();
            if ( renderer == null )
            {
                continue;
            }
            rendererCount++;

        }
        if (rendererCount != 0)
        {
            renderers = new Renderer[rendererCount];
            materialPropertyBlocks = new MaterialPropertyBlock[rendererCount];
            int rendererCount2 = 0;
            for (int ii = 0; ii < objCount; ii++)
            {
                var renderer = objs[ii].GetComponent<Renderer>();
                if (renderer == null)
                {
                    continue;
                }
                renderers[rendererCount2] = renderer;
                materialPropertyBlocks[rendererCount2] = new MaterialPropertyBlock();
                rendererCount2++;

            }
            Debug.Assert(rendererCount2 == rendererCount);
        }

    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (renderers == null || renderers.Length == 0)
        {
            return;
        }

        Color col = Color.green;
        int length = renderers.Length;
        for ( int ii = 0; ii < length; ii ++)
        {
            renderers[ii].GetPropertyBlock(materialPropertyBlocks[ii]);

            materialPropertyBlocks[ii].SetColor("_UnlitColor", col);

            renderers[ii].SetPropertyBlock(materialPropertyBlocks[ii]);
        }

    }
}
