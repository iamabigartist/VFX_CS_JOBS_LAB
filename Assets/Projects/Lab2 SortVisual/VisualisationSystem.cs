using System;
using System.Threading;
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
    public bool ShowType1;
    float[] array;
    Vector4[] test_array;
    ComputeBuffer buffer;
    ComputeBuffer test_buffer;
    Array_CS_VFX_Player<ParticlePlayer, ArrayTo3DCircleCS>
        Player;

    Thread sort_thread;


    // Start is called before the first frame update
    void Start()
    {
        array = new float[Count];
        test_array = new Vector4[Count];
        buffer = new ComputeBuffer( Count, sizeof(float) );
        test_buffer = new ComputeBuffer( Count, 4 * sizeof(float) );
        for (int i = 0; i < Count; i++)
        {
            array[i] = i;
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
                type1 = ShowType1,
                sorted_array = buffer,
                test_buffer = test_buffer
            } );
        sort_thread = new Thread( () => { } );
        sort_thread.Start();
    }

    // Update is called once per frame
    void Update()
    {
        buffer.SetData( array );
        Player.ComputeShader.bind_refresh( transform.position, RotateScale, DistantScale, RotateRatio, ShowType1 );
        Player.Refresh();

        test_buffer.GetData( test_array );
    }

    void OnDestroy()
    {
        buffer.Release();
        test_buffer.Release();
    }

    void OnGUI()
    {

        var can_start_new =
            (sort_thread.ThreadState & ThreadState.Aborted) > 0 ||
            (sort_thread.ThreadState & ThreadState.Stopped) > 0 ||
            (sort_thread.ThreadState & ThreadState.Unstarted) > 0;

        ShowType1 = GUILayout.Toggle( ShowType1, "ShowType2" );

        GUI.enabled = can_start_new;


        if (GUILayout.Button( "RandomSort" ))
        {
            Task.Run( () => { array.RandomSort( 10000 ); } );
        }
        if (GUILayout.Button( "BubbleSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.BubbleSort(); } );
            sort_thread.StartSlow( 40, 0 );
            // sort_thread.Start();
            // Task.Run( () => { array.BubbleSort(); } );
        }
        if (GUILayout.Button( "SelectionSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.SelectionSort(); } );
            sort_thread.StartSlow( 1, 2 );
        }
        if (GUILayout.Button( "CombSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.CombSort(); } );
            sort_thread.StartSlow( 1, 10000000 );
        }
        if (GUILayout.Button( "ShellSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.ShellSort(); } );
            sort_thread.StartSlow( 5, 1 );
        }
        if (GUILayout.Button( "CycleSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.CycleSort(); } );
            sort_thread.StartSlow( 100, 1 );
        }
        if (GUILayout.Button( "GnomeSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.GnomeSort(); } );
            sort_thread.StartSlow( 10, 1 );
        }
        if (GUILayout.Button( "HeapSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.HeapSort(); } );
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "InsertionSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.InsertionSort(); } );
            sort_thread.StartSlow( 10, 1 );
        }
        if (GUILayout.Button( "QuickSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.QuickSort(); } );
            sort_thread.StartSlow( 1, 100 );
        }
        if (GUILayout.Button( "OddEvenSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.OddEvenSort(); } );
            sort_thread.StartSlow( 100, 1 );
        }
        if (!can_start_new)
        {
            GUI.enabled = true;
            if (GUILayout.Button( "Cancel" ))
            {
                sort_thread.Abort();
            }

            // GUILayout.Box( $"ThreadState:{sort_thread.ThreadState}" );
        }

        a = GUILayout.TextField( a );

        try
        {
            GUILayout.Box( $"{test_array[Convert.ToInt32( a )]}" );
        }
        catch (Exception e)
        {
            GUILayout.Box( e.ToString() );
        }

    }

    string a = "1";


}
