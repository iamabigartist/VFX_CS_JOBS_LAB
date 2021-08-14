using System.Threading;
using UnityEngine;
public class TestBall : MonoBehaviour
{
    float x;
    Transform t;
    Vector3 p;
    Thread m_tMover;


    // Start is called before the first frame update
    void Start()
    {
        t = transform;
        p = t.position;
        x = 0;

        m_tMover = new Thread( Mover );
        m_tMover.StartSlow( 1, 2000 );

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
        m_tMover.Abort();
    }



    void Mover()
    {
        for (int i = 0; i < 1000000; i++)
        {
            x += 0.0001f;
        }
    }


}
