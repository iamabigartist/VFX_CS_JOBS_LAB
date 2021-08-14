using System.Diagnostics;
using System.Threading;
using UnityEngine;
public class TestBall : MonoBehaviour
{
    float x;
    Transform t;
    Vector3 p;
    Thread m_tMover;
    Thread m_tDelayTimer;


    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        p = t.position;
        x = 0;


        m_tDelayTimer = new Thread( () => { DelayController( 1, 100 ); } );
        m_tDelayTimer.Start();

    }

    // Update is called once per frame
    void Update()
    {
        p = t.position;
        p.x = x;
        t.position = p;
    }

    void OnApplicationQuit()
    {
        m_tDelayTimer.Abort();
        m_tMover.Abort();
    }

    void DelayController(int run_ticks, int sleep_ticks)
    {
        var target_thread = new Thread( Mover );
        var timer = Stopwatch.StartNew();
        target_thread.Start();
        while (true)
        {
            if (timer.ElapsedTicks >= run_ticks)
            {
                if (!target_thread.IsAlive) { return; }
                target_thread.Suspend();
                timer.Restart();
                while (timer.ElapsedTicks < sleep_ticks) { }
                if (!target_thread.IsAlive) { return; }
                target_thread.Resume();
                timer.Restart();
            }
        }
    }

    void Mover()
    {
        for (int i = 0; i < 1000000; i++)
        {
            x += 0.0001f;
        }
    }


}
