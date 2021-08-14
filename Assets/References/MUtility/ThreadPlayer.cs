using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
public static class ThreadPlayer
{
    public static async void StartSlow(this Thread t, int run_ticks, int sleep_ticks)
    {
        await Task.Run( () =>
        {
            ThreadDelayControl( t, run_ticks, sleep_ticks );
        } );
    }

    static void ThreadDelayControl(Thread target_thread, int run_ticks, int sleep_ticks)
    {
        var timer = Stopwatch.StartNew();
        target_thread.Start();
        while (true)
        {
            if (timer.ElapsedTicks >= run_ticks)
            {
                if (!target_thread.IsAlive) { return; }
                target_thread.Suspend();
                //Reuse for the sleep time
                timer.Restart();
                while (timer.ElapsedTicks < sleep_ticks) { }
                if (!target_thread.IsAlive) { return; }
                target_thread.Resume();
                //Restart run timer
                timer.Restart();
            }
        }
    }
}
