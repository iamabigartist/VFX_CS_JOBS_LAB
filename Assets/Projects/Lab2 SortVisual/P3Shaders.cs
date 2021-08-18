using UnityEngine;
using UnityEngine.VFX;
// ReSharper disable UnassignedField.Global
// ReSharper disable ClassNeverInstantiated.Global
// ReSharper disable VirtualMemberCallInConstructor

namespace VFX_CS_JOBS_LAB
{
    public class ArrayTo3DCircleCS : ComputeShaderObject<ArrayTo3DCircleCS.ArgsAll>
    {
        ComputeBuffer buffer;

        public class ArgsAll
        {
            public Vector3 origin;
            public float rotate_scale;
            public float distant_scale;
            public int array_len;
            public ComputeBuffer sorted_array;
            public RenderTexture render_texture;
        }

        (
            int origin,
            int rotate_scale,
            int distant_scale,
            int array_len,
            int sorted_array,
            int render_texture
            )
            ids;

        public ArrayTo3DCircleCS(
            int array_len) :
            base( new[] { new Vector3( 1024, 1, 1 ) } )
        {
            _this = Resources.Load<ComputeShader>( "ArrayTo3DCircleCS" );
            buffer = new ComputeBuffer( array_len, sizeof(int) );
            data_count_array = new[] { new Vector3( array_len, 1, 1 ) };
        }

        public override void init_id()
        {
            ids.origin = Shader.PropertyToID( "origin" );
            ids.rotate_scale = Shader.PropertyToID( "rotate_scale" );
            ids.distant_scale = Shader.PropertyToID( "distant_scale" );
            ids.array_len = Shader.PropertyToID( "array_len" );
            ids.sorted_array = Shader.PropertyToID( "sorted_array" );
            ids.render_texture = Shader.PropertyToID( "render_texture" );
        }

        public override void bind(ArgsAll args)
        {
            _this.SetVector( ids.origin, args.origin );
            _this.SetFloat( ids.rotate_scale, args.rotate_scale );
            _this.SetFloat( ids.distant_scale, args.distant_scale );
            _this.SetInt( ids.array_len, args.array_len );
            _this.SetBuffer( 0, ids.sorted_array, args.sorted_array );
            _this.SetTexture( 0, ids.render_texture, args.render_texture );
        }
    }

    public class ParticlePlayer : VFXGraphObject<ParticlePlayer.ArgsAll>
    {

        public class ArgsAll
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
            ids.Count = Shader.PropertyToID( "Count" );
            ids.RT = Shader.PropertyToID( "RT" );
        }

        public override void bind(ArgsAll args)
        {
            _this.SetInt( ids.Count, args.Count );
            _this.SetTexture( ids.RT, args.RT );
        }
    }

}
