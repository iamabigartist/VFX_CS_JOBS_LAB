using UnityEngine;
namespace VFX_CS_JOBS_LAB
{
    /// <summary>
    ///     Use an array as a input to the compute_shader and output a render_texture to be rendered by the visual_effect.
    /// </summary>
    public class Array_CS_VFX_Player<TVFXGraph, TVFXGraphSource, TComputeShader, TComputeShaderSource>
        where TVFXGraph : VFXGraphObject<TVFXGraphSource>
        where TComputeShader : ComputeShaderObject<TComputeShaderSource>
    {
        public RenderTexture RenderTexture { get; }
        public TVFXGraph VFXGraph { get; }
        public TComputeShader ComputeShader { get; }
        public Array_CS_VFX_Player(
            int array_len,
            TVFXGraph VFXGraph,
            ShaderArgs VFXArgs,
            TComputeShader ComputeShader,
            ShaderArgs ComputeArgs)
        {
            RenderTexture = new RenderTexture(
                array_len, 1, 0,
                RenderTextureFormat.ARGBFloat,
                RenderTextureReadWrite.Linear )
            {
                enableRandomWrite = true
            };
            this.VFXGraph = VFXGraph;
            this.VFXGraph.init_id();
            this.VFXGraph.bind( VFXArgs );
            this.ComputeShader = ComputeShader;
            this.ComputeShader.init_id();
            this.ComputeShader.bind( ComputeArgs );
        }

        public void Refresh()
        {
            ComputeShader.Dispatch( 0 );
        }

        public void Config(ShaderArgs VFXArgs, ShaderArgs ComputeArgs)
        {
            VFXGraph.bind( VFXArgs );
            ComputeShader.bind( ComputeArgs );
        }
    }
}
