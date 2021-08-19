using System;
using System.Threading;
using System.Threading.Tasks;
using Algorithms.Common;
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
    ComputeBuffer buffer;
    Array_CS_VFX_Player<ParticlePlayer, ArrayTo3DCircleCS>
        Player;

    Thread sort_thread;
    float sleep_tick = 1, run_tick = 1;
    string s_sleep_tick, s_run_tick;


    // Start is called before the first frame update
    void Start()
    {
        array = new float[Count];
        buffer = new ComputeBuffer( Count, sizeof(float) );
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
                sorted_array = buffer
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
    }

    void OnDestroy()
    {
        buffer.Release();
    }

    void OnGUI()
    {

        var can_start_new =
            (sort_thread.ThreadState & ThreadState.Aborted) > 0 ||
            (sort_thread.ThreadState & ThreadState.Stopped) > 0 ||
            (sort_thread.ThreadState & ThreadState.Unstarted) > 0;
        GUI.enabled = can_start_new;
        GUILayout.Label( $"run_tick" );
        s_run_tick = GUILayout.TextField( s_run_tick );
        if (s_run_tick.IsNumber())
        {
            run_tick = Convert.ToInt32( s_run_tick );
        }
        else
        {
            GUILayout.Label( "Invalid Number" );
        }

        GUILayout.Label( $"sleep_tick" );
        s_sleep_tick = GUILayout.TextField( s_sleep_tick );
        if (s_sleep_tick.IsNumber())
        {
            sleep_tick = Convert.ToInt32( s_sleep_tick );
        }
        else
        {
            GUILayout.Label( "Invalid Number" );
        }


        if (GUILayout.Button( "RandomSort" ))
        {
            Task.Run( () => { array.RandomSort( 10000 ); } );
        }
        if (GUILayout.Button( "BubbleSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.BubbleSort(); } );
            sort_thread.StartSlow( 10, 0 );
            // sort_thread.Start();
            // Task.Run( () => { array.BubbleSort(); } );
        }
        if (GUILayout.Button( "SelectionSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.SelectionSort(); } );
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "CombSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.CombSort(); } );
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "ShellSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.ShellSort(); } );
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "CycleSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.CycleSort(); } );
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "GnomeSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.GnomeSort(); } );
            sort_thread.StartSlow( 1, 1 );
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
            sort_thread.StartSlow( 1, 1 );
        }
        if (GUILayout.Button( "QuickSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.QuickSort(); } );
            sort_thread.StartSlow( 1, 3 );
        }
        if (GUILayout.Button( "OddEvenSort" ))
        {
            sort_thread.Abort();
            sort_thread = new Thread( () => { array.OddEvenSort(); } );
            sort_thread.StartSlow( 1, 1 );
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

    }




}
