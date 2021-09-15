using UnityEngine;
using UnityEngine.VFX;
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace VFX_CS_JOBS_LAB
{
    public class ArrayTo3DCircleCS : ComputeShaderObject
    {

        public class ArgsAll : ComputeShaderArgs
        {
            public Vector3 origin;
            public float rotate_scale;
            public float distant_scale;
            public float rotate_ratio;
            public bool type1;
            public int array_len;
            public ComputeBuffer sorted_array;
            public ComputeBuffer test_buffer;
            public RenderTexture render_texture;
        }

        (
            int origin,
            int rotate_scale,
            int distant_scale,
            int rotate_ratio,
            int type1,
            int array_len,
            int sorted_array,
            int test_buffer,
            int render_texture
            )
            ids;

        public ArrayTo3DCircleCS(
            int array_len) :
            base( new[] { new Vector3( 1024, 1, 1 ) } )
        {
            _this = Resources.Load<ComputeShader>( "ArrayTo3DCircleCS" );

            data_count_array = new[] { new Vector3( array_len, 1, 1 ) };
        }

        public override void init_id()
        {
            ids = (Shader.PropertyToID( "origin" ),
                   Shader.PropertyToID( "rotate_scale" ),
                   Shader.PropertyToID( "distant_scale" ),
                   Shader.PropertyToID( "rotate_ratio" ),
                   Shader.PropertyToID( "type1" ),
                   Shader.PropertyToID( "array_len" ),
                   Shader.PropertyToID( "sorted_array" ),
                   Shader.PropertyToID( "test_buffer" ),
                   Shader.PropertyToID( "render_texture" ));
        }

        public override void bind(ShaderArgs _args)
        {
            var args = _args as ArgsAll;
            _this.SetVector( ids.origin, args.origin );
            _this.SetFloat( ids.rotate_scale, args.rotate_scale );
            _this.SetFloat( ids.distant_scale, args.distant_scale );
            _this.SetFloat( ids.rotate_ratio, args.rotate_ratio );
            _this.SetBool( ids.type1, args.type1 );
            _this.SetInt( ids.array_len, args.array_len );
            _this.SetBuffer( 0, ids.sorted_array, args.sorted_array );
            _this.SetBuffer( 0, ids.test_buffer, args.test_buffer );
            _this.SetTexture( 0, ids.render_texture, args.render_texture );
        }

        public void bind_refresh(
            Vector3 origin,
            float rotate_scale,
            float distant_scale,
            float rotate_ratio,
            bool type1)
        {
            _this.SetVector( ids.origin, origin );
            _this.SetFloat( ids.rotate_scale, rotate_scale );
            _this.SetFloat( ids.distant_scale, distant_scale );
            _this.SetFloat( ids.rotate_ratio, rotate_ratio );
            _this.SetBool( ids.type1, type1 );
        }
    }

    public class ParticlePlayer : VFXGraphObject
    {

        public class ArgsAll : VFXGraphArgs
        {
            public int Count;
            public RenderTexture RT;
        }

        (
            int Count,
            int RT
            )
            ids;


        public ParticlePlayer(Component behaviour)
        {
            _this = behaviour.GetComponent<VisualEffect>();
        }

        public override void init_id()
        {
            ids = (Shader.PropertyToID( "Count" ),
                   Shader.PropertyToID( "RT" ));
        }

        public override void bind(ShaderArgs _args)
        {
            var args = _args as ArgsAll;
            _this.SetInt( ids.Count, args.Count );
            _this.SetTexture( ids.RT, args.RT );
        }
    }

}
