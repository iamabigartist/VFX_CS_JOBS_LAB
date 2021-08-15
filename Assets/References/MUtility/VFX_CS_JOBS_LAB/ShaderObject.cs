using UnityEngine;
using UnityEngine.VFX;
interface IShaderInitable
{
    public void init_shader();
    public void init_id();
    public void bind_id();
}

public abstract class VFXGraphObject : IShaderInitable
{
    VisualEffect _this;
    public abstract void init_shader();
    public abstract void init_id();
    public abstract void bind_id();
}

public abstract class ComputeShaderObject : IShaderInitable
{
    ComputeShader _this;
    readonly Vector3[] num_threads_array;
    Vector3[] data_count_array;
    Vector3Int thread_group_size(int kernel)
    {
        var num_thread = num_threads_array[kernel];
        var data_count = data_count_array[kernel];
        return new Vector3Int(
            Mathf.CeilToInt( num_thread.x / data_count.x ),
            Mathf.CeilToInt( num_thread.y / data_count.y ),
            Mathf.CeilToInt( num_thread.z / data_count.z ) );
    }
    public void Dispatch(int kernel)
    {
        var cur_thread_group_size = thread_group_size( kernel );
        _this.Dispatch( kernel, cur_thread_group_size.x, cur_thread_group_size.y, cur_thread_group_size.z );
    }
    public abstract void init_shader();
    public abstract void init_id();
    public abstract void bind_id();
}
