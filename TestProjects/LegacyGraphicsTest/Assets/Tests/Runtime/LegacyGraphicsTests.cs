using System.Collections;
using System.Linq;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.XR;
using UnityEngine.TestTools.Graphics;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.IO;

public class LegacyGraphicsTests 
{
    [UnityTest, Category("UnityToonLegacy")]
    public IEnumerator Run(GraphicsTestCase testCase)
    {
        // Always wait one frame for scene load
        yield return null;
    }
}
