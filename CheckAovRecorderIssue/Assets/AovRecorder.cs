using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.Rendering.HighDefinition.Attributes;

public class AovRecorder : MonoBehaviour
{
    RTHandle m_TmpRT;       // The RTHandle used to render the AOV
    Texture2D m_ReadBackTexture;

    int m_Frames = 0;

    // Start is called before the first frame update
    void Start()
    {
        var camera = gameObject.GetComponent<Camera>();
        if (camera != null)
        {
            var hdAdditionalCameraData = gameObject.GetComponent<HDAdditionalCameraData>();
            if (hdAdditionalCameraData != null)
            {
                // initialize a new AOV request
                var aovRequest = AOVRequest.NewDefault();

                AOVBuffers[] aovBuffers = null;
                CustomPassAOVBuffers[] customPassAovBuffers = null;

                // Request an AOV with the surface albedo
                aovRequest.SetFullscreenOutput(MaterialSharedProperty.Normal);
                aovBuffers = new[] { AOVBuffers.Color };

                // Allocate the RTHandle that will store the intermediate results
                m_TmpRT = RTHandles.Alloc(camera.pixelWidth, camera.pixelHeight);

                // Add the request to a new AOVRequestBuilder
                var aovRequestBuilder = new AOVRequestBuilder();
                aovRequestBuilder.Add(aovRequest,
                    bufferId => m_TmpRT,
                    null,
                    aovBuffers,
                    customPassAovBuffers,
                    bufferId => m_TmpRT,
                    (cmd, textures, customPassTextures, properties) =>
                    {
                        // callback to read back the AOV data and write them to disk
                        if (textures.Count > 0)
                        {
                            m_ReadBackTexture = m_ReadBackTexture ?? new Texture2D(camera.pixelWidth, camera.pixelHeight, TextureFormat.RGBAFloat, false);
                            RenderTexture.active = textures[0].rt;
                            m_ReadBackTexture.ReadPixels(new Rect(0, 0, camera.pixelWidth, camera.pixelHeight), 0, 0, false);
                            m_ReadBackTexture.Apply();
                            RenderTexture.active = null;
                            byte[] bytes = m_ReadBackTexture.EncodeToPNG();
                            System.IO.File.WriteAllBytes($"output_{m_Frames++}.png", bytes);
                        }

                    });

                // Now build the AOV request
                var aovRequestDataCollection = aovRequestBuilder.Build();

                // And finally set the request to the camera
                hdAdditionalCameraData.SetAOVRequests(aovRequestDataCollection);
            }
        }
    }
}