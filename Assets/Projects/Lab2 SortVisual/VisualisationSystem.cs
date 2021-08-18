using UnityEngine;
using VFX_CS_JOBS_LAB;
[ExecuteAlways]
public class VisualisationSystem : MonoBehaviour
{
    public int Count;
    public float DistantScale;
    public float RotateScale;
    int[] array;
    Array_CS_VFX_Player
        <ParticlePlayer.ArgsAll,
            ArrayTo3DCircleCS.ArgsAll>
        Player;

    // Start is called before the first frame update
    void Start()
    {
        array = new int[Count];
        for (int i = 0; i < Count; i++)
        {
            array[i] = i;
        }

        Player = new
            Array_CS_VFX_Player
            <ParticlePlayer.ArgsAll,
                ArrayTo3DCircleCS.ArgsAll>(
                Count,
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
                sorted_array = array
            } );
    }

    // Update is called once per frame
    void Update()
    {
        Player.Refresh();
    }
}
