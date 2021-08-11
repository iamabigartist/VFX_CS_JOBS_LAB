using UnityEngine;
using UnityEngine.VFX;
public class WhoKnowsTexture : MonoBehaviour
{

#region Config

    [SerializeField] int Count;

#endregion

#region Datas

    RenderTexture rt;
    void init_data()
    {
        rt = new RenderTexture(Count, 1, 0, RenderTextureFormat.ARGBFloat, RenderTextureReadWrite.Linear)
        {
            enableRandomWrite = true
        };
    }

#endregion

#region VFXG

    /// <summary>
    ///     Simply show the particle using a render texture.
    /// </summary>
    VisualEffect vfxg;
    (
        int ViewBox,
        int Count,
        int RT
        )
        vfxg_ids;
    void init_vfxg()
    {
        vfxg = GetComponent<VisualEffect>();
        init_vfxg_ids();
        bind_vfxg_ids();
    }

    void init_vfxg_ids()
    {
        vfxg_ids.Count = Shader.PropertyToID("Count");
        vfxg_ids.ViewBox = Shader.PropertyToID("ViewBox");
        vfxg_ids.RT = Shader.PropertyToID("RT");
    }

    void bind_vfxg_ids()
    {
        vfxg.SetTexture(vfxg_ids.RT, rt);
        vfxg.SetInt(vfxg_ids.Count, Count);
    }

#endregion

#region CS

    /// <summary>
    ///     draw into a render texture.
    /// </summary>
    ComputeShader cs;
    (
        int array_len,
        int render_texture
        )
        cs_ids;

    const int num_threads_x = 1024;
    int thread_group_size_x => Mathf.CeilToInt(Count / (float)num_threads_x);
    void init_cs()
    {
        cs = Resources.Load<ComputeShader>("DrawTextureCS");
        init_cs_ids();
        bind_cs_ids();
    }

    void init_cs_ids()
    {
        cs_ids.array_len = Shader.PropertyToID("array_len");
        cs_ids.render_texture = Shader.PropertyToID("render_texture");
    }

    void bind_cs_ids()
    {
        cs.SetTexture(0, cs_ids.render_texture, rt);
        cs.SetInt(cs_ids.array_len, Count);
    }

#endregion



#region Events

    void Start()
    {
        init_data();
        init_cs();
        init_vfxg();

    }

    void Update()
    {
        cs.Dispatch(0, thread_group_size_x, 1, 1);
    }

#endregion
}