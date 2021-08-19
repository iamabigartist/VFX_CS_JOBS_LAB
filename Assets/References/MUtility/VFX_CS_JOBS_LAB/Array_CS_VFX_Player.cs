using UnityEngine;
namespace VFX_CS_JOBS_LAB
{
    /// <summary>
    ///     Use an array as a input to the compute_shader and output a render_texture to be rendered by the visual_effect.
    /// </summary>
    public class Array_CS_VFX_Player
    {
        public RenderTexture RenderTexture { get; }

        public VFXGraphObject VFXGraph { get; }

        public ComputeShaderObject ComputeShader { get; }

        public Array_CS_VFX_Player(
            int array_len,
            VFXGraphObject vfxg,
            ComputeShaderObject cs)
        {
            RenderTexture = new RenderTexture(
                array_len, 1, 0,
                RenderTextureFormat.ARGBFloat,
                RenderTextureReadWrite.Linear )
            {
                enableRandomWrite = true
            };
            VFXGraph = vfxg;
            VFXGraph.init_id();
            ComputeShader = cs;
            ComputeShader.init_id();
        }

        public void Config(
            ShaderObject.ShaderArgs VFXArgs,
            ShaderObject.ShaderArgs ComputeArgs)
        {
            VFXGraph.bind( VFXArgs );
            ComputeShader.bind( ComputeArgs );
        }

        public void Refresh()
        {
            ComputeShader.Dispatch( 0 );
        }
    }
}
