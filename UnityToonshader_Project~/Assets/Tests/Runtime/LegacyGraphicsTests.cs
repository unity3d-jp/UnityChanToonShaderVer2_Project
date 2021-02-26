using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
public class LegacyGraphicsTests 
{
    [UnityTest, Category("UnityToonLegacy")]
    public IEnumerator Run(GraphicsTestCase testCase)
    {
        // Always wait one frame for scene load
        yield return null;
    }
}
