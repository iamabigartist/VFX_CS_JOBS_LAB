using UnityEngine;
using UnityEngine.VFX;
// ReSharper disable UnusedMemberInSuper.Global
interface IShaderBehave<in TSource>
{
    public void init_shader(TSource s);
    public void init_id();
    public void bind(ShaderArgs args);
}

public abstract class ShaderArgs
{
}

public abstract class VFXGraphObject<TSource> : IShaderBehave<TSource>
{
    protected VisualEffect _this;
    public abstract void init_shader(TSource s);
    public abstract void init_id();
    public abstract void bind(ShaderArgs args);
}

public abstract class ComputeShaderObject<TSource> : IShaderBehave<TSource>
{
    protected ComputeShader _this;
    protected readonly Vector3[] num_threads_array;
    protected Vector3[] data_count_array;

    public abstract void init_shader(TSource s);
    public abstract void init_id();
    public abstract void bind(ShaderArgs args);

    protected ComputeShaderObject(Vector3[] num_threads_array)
    {
        this.num_threads_array = num_threads_array;
    }

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

}
