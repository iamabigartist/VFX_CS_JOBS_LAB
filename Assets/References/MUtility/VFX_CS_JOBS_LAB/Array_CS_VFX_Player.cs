using UnityEngine;
namespace VFX_CS_JOBS_LAB
{
    /// <summary>
    ///     Use an array as a input to the compute_shader and output a render_texture to be rendered by the visual_effect.
    /// </summary>
    public class Array_CS_VFX_Player<TVFXGraphObject, TComputeShaderObject>
        where TVFXGraphObject : VFXGraphObject
        where TComputeShaderObject : ComputeShaderObject
    {

        public RenderTexture RenderTexture { get; }
        public int render_texture_col_len;

        public TVFXGraphObject VFXGraph { get; }

        public TComputeShaderObject ComputeShader { get; }

        public Array_CS_VFX_Player(
            int render_texture_row_len,
            int render_texture_col_len,
            TVFXGraphObject vfxg,
            TComputeShaderObject cs)
        {
            RenderTexture = new RenderTexture(
                render_texture_row_len,
                render_texture_col_len, 0,
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
