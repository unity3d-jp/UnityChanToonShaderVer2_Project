// Utility scripts for the post processing stack
// https://github.com/keijiro/PostProcessingUtilities

using UnityEngine;

namespace UnityEngine.PostProcessing.Utilities
{
    [RequireComponent(typeof(PostProcessingBehaviour))]
    public class PostProcessingController : MonoBehaviour
    {
        #region Public structs

        public bool controlAntialiasing;
        public bool enableAntialiasing;
        public AntialiasingModel.Settings antialiasing;

        public bool controlAmbientOcclusion;
        public bool enableAmbientOcclusion;
        public AmbientOcclusionModel.Settings ambientOcclusion;

        public bool controlScreenSpaceReflection;
        public bool enableScreenSpaceReflection;
        public ScreenSpaceReflectionModel.Settings screenSpaceReflection;

        public bool controlDepthOfField = true;
        public bool enableDepthOfField;
        public DepthOfFieldModel.Settings depthOfField;

        public bool controlMotionBlur;
        public bool enableMotionBlur;
        public MotionBlurModel.Settings motionBlur;

        public bool controlEyeAdaptation;
        public bool enableEyeAdaptation;
        public EyeAdaptationModel.Settings eyeAdaptation;

        public bool controlBloom;
        public bool enableBloom;
        public BloomModel.Settings bloom;

        public bool controlColorGrading;
        public bool enableColorGrading;
        public ColorGradingModel.Settings colorGrading;

        public bool controlUserLut;
        public bool enableUserLut;
        public UserLutModel.Settings userLut;

        public bool controlChromaticAberration;
        public bool enableChromaticAberration;
        public ChromaticAberrationModel.Settings chromaticAberration;

        public bool controlGrain;
        public bool enableGrain;
        public GrainModel.Settings grain;

        public bool controlVignette;
        public bool enableVignette;
        public VignetteModel.Settings vignette;

        #endregion

        #region Private members

        // Cloned profile
        PostProcessingProfile _profile;

        #endregion

        #region MonoBehaviour functions

        void Start()
        {
            // Replace the profile with its clone.
            var postfx = GetComponent<PostProcessingBehaviour>();
            _profile = Instantiate<PostProcessingProfile>(postfx.profile);
            postfx.profile = _profile;

            // Initialize the public structs with the current profile.
            enableAntialiasing = _profile.antialiasing.enabled;
            antialiasing = _profile.antialiasing.settings;

            enableAmbientOcclusion = _profile.ambientOcclusion.enabled;
            ambientOcclusion = _profile.ambientOcclusion.settings;

            enableScreenSpaceReflection = _profile.screenSpaceReflection.enabled;
            screenSpaceReflection = _profile.screenSpaceReflection.settings;

            enableDepthOfField = _profile.depthOfField.enabled;
            depthOfField = _profile.depthOfField.settings;

            enableMotionBlur = _profile.motionBlur.enabled;
            motionBlur = _profile.motionBlur.settings;

            enableEyeAdaptation = _profile.eyeAdaptation.enabled;
            eyeAdaptation = _profile.eyeAdaptation.settings;

            enableBloom = _profile.bloom.enabled;
            bloom = _profile.bloom.settings;

            enableColorGrading = _profile.colorGrading.enabled;
            colorGrading = _profile.colorGrading.settings;

            enableUserLut = _profile.userLut.enabled;
            userLut = _profile.userLut.settings;

            enableChromaticAberration = _profile.chromaticAberration.enabled;
            chromaticAberration = _profile.chromaticAberration.settings;

            enableGrain = _profile.grain.enabled;
            grain = _profile.grain.settings;

            enableVignette = _profile.vignette.enabled;
            vignette = _profile.vignette.settings;
        }

        void Update()
        {
            if (controlAntialiasing)
            {
                if (enableAntialiasing != _profile.antialiasing.enabled)
                    _profile.antialiasing.enabled = enableAntialiasing;

                if (enableAntialiasing)
                    _profile.antialiasing.settings = antialiasing;
            }

            if (controlAmbientOcclusion)
            {
                if (enableAmbientOcclusion != _profile.ambientOcclusion.enabled)
                    _profile.ambientOcclusion.enabled = enableAmbientOcclusion;

                if (enableAmbientOcclusion)
                    _profile.ambientOcclusion.settings = ambientOcclusion;
            }

            if (controlScreenSpaceReflection)
            {
                if (enableScreenSpaceReflection != _profile.screenSpaceReflection.enabled)
                    _profile.screenSpaceReflection.enabled = enableScreenSpaceReflection;

                if (enableScreenSpaceReflection)
                    _profile.screenSpaceReflection.settings = screenSpaceReflection;
            }

            if (controlDepthOfField)
            {
                if (enableDepthOfField != _profile.depthOfField.enabled)
                    _profile.depthOfField.enabled = enableDepthOfField;

                if (enableDepthOfField)
                    _profile.depthOfField.settings = depthOfField;
            }

            if (controlMotionBlur)
            {
                if (enableMotionBlur != _profile.motionBlur.enabled)
                    _profile.motionBlur.enabled = enableMotionBlur;

                if (enableMotionBlur)
                    _profile.motionBlur.settings = motionBlur;
            }

            if (controlEyeAdaptation)
            {
                if (enableEyeAdaptation != _profile.eyeAdaptation.enabled)
                    _profile.eyeAdaptation.enabled = enableEyeAdaptation;

                if (enableEyeAdaptation)
                    _profile.eyeAdaptation.settings = eyeAdaptation;
            }

            if (controlBloom)
            {
                if (enableBloom != _profile.bloom.enabled)
                    _profile.bloom.enabled = enableBloom;

                if (enableBloom)
                    _profile.bloom.settings = bloom;
            }

            if (controlColorGrading)
            {
                if (enableColorGrading != _profile.colorGrading.enabled)
                    _profile.colorGrading.enabled = enableColorGrading;

                if (enableColorGrading)
                    _profile.colorGrading.settings = colorGrading;
            }

            if (controlUserLut)
            {
                if (enableUserLut != _profile.userLut.enabled)
                    _profile.userLut.enabled = enableUserLut;

                if (enableUserLut)
                    _profile.userLut.settings = userLut;
            }

            if (controlChromaticAberration)
            {
                if (enableChromaticAberration != _profile.chromaticAberration.enabled)
                    _profile.chromaticAberration.enabled = enableChromaticAberration;

                if (enableChromaticAberration)
                    _profile.chromaticAberration.settings = chromaticAberration;
            }

            if (controlGrain)
            {
                if (enableGrain != _profile.grain.enabled)
                    _profile.grain.enabled = enableGrain;

                if (enableGrain)
                    _profile.grain.settings = grain;
            }

            if (controlVignette)
            {
                if (enableVignette != _profile.vignette.enabled)
                    _profile.vignette.enabled = enableVignette;

                if (enableVignette)
                    _profile.vignette.settings = vignette;
            }
        }

        #endregion
    }
}
