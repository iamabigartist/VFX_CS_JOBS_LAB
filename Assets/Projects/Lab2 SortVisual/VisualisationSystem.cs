using UnityEngine;
using VFX_CS_JOBS_LAB;
[ExecuteAlways]
public class VisualisationSystem : MonoBehaviour
{
    public int Count;
    public float DistantScale;
    public float RotateScale;
    int[] array;
    ComputeBuffer buffer;
    Array_CS_VFX_Player
        Player;

    // Start is called before the first frame update
    void Start()
    {
        array = new int[Count];
        buffer = new ComputeBuffer( Count, sizeof(int) );
        for (int i = 0; i < Count; i++)
        {
            array[i] = i;
        }
        buffer.SetData( array );
        Player = new
            Array_CS_VFX_Player( Count,
                new ParticlePlayer( this ),
                new ArrayTo3DCircleCS( Count ) );

        Player.Config(
            new ParticlePlayer.ArgsAll
            {
                Count = Count,
                RT = Player.RenderTexture
            },
            new ArrayTo3DCircleCS.ArgsAll
            {
                array_len = Count,
                distant_scale = DistantScale,
                origin = transform.position,
                render_texture = Player.RenderTexture,
                rotate_scale = RotateScale,
                sorted_array = buffer
            } );
    }

    // Update is called once per frame
    void Update()
    {
        buffer.SetData( array );
        Player.Refresh();
    }
}
