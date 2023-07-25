using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace WasdswagPostProcess
{

    [System.Serializable] [PostProcess(typeof(PixelationRenderer), PostProcessEvent.AfterStack, "Wasdswag/Pixelation")]
    public class Pixelation : PostProcessEffectSettings
    {
        [Range(0f, 1f)] public FloatParameter Intensity = new FloatParameter { value = 0f };
    }

    public sealed class PixelationRenderer : PostProcessEffectRenderer<Pixelation>
    {

        private const string SHADERPATH = "Hidden/PostProcessing/Pixelation";

        private Shader shader;
        private PropertySheet propertySheet;
        private int maxPixelation = 1024;

        public override void Init() => shader = WasdswagFilters.GetShader(SHADERPATH);

        public override void Render(PostProcessRenderContext context)
        {
            if (shader.IsValid(context, ref propertySheet))
            {

                float power = (settings.Intensity * maxPixelation) + 0.0001f;
                float gain = context.screenWidth / (maxPixelation - power);

                propertySheet.properties.SetFloat("_Width", context.screenWidth);
                propertySheet.properties.SetFloat("_Height", context.screenHeight);
                propertySheet.properties.SetFloat("_CellSize", gain);

                context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 0);
            }

        }
    }
}
