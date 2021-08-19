using System.Threading.Tasks;
using Algorithms.Sorting;
using UnityEngine;
using VFX_CS_JOBS_LAB;
public class VisualisationSystem : MonoBehaviour
{
    public RenderTexture[] RenderTextures;
    public int Count;
    public float DistantScale;
    public float RotateScale;
    public float RotateRatio;
    float[] array;
    ComputeBuffer buffer;
    Array_CS_VFX_Player<ParticlePlayer, ArrayTo3DCircleCS>
        Player;

    // Start is called before the first frame update
    void Start()
    {
        array = new float[Count];
        buffer = new ComputeBuffer( Count, sizeof(float) );
        for (int i = 0; i < Count; i++)
        {
            array[i] = Random.Range( 0, Count );
        }

        buffer.SetData( array );

        Player = new
            Array_CS_VFX_Player
            <ParticlePlayer,
                ArrayTo3DCircleCS>( Count,
                new ParticlePlayer( transform.GetChild( 0 ) ),
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
        Player.ComputeShader.bind_refresh( transform.position, RotateScale, DistantScale, RotateRatio );
        Player.Refresh();
    }

    void OnDestroy()
    {
        buffer.Release();
    }

    void OnGUI()
    {
        if (GUILayout.Button( "BubbleSort" ))
        {
            Task.Run( () => { array.BubbleSort(); } );
        }
        if (GUILayout.Button( "RandomSort" ))
        {
            Task.Run( () => { array.RandomSort( 1000 ); } );
        }
    }




}
