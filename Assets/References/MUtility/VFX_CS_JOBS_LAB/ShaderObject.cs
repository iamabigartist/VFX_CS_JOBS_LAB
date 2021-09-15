using UnityEngine;
using UnityEngine.VFX;
// ReSharper disable UnusedMemberInSuper.Global
public abstract class ShaderObject
{
    public abstract class ShaderArgs { }
    public abstract void init_id();
    public abstract void bind(ShaderArgs args);
}

public abstract class VFXGraphObject : ShaderObject
{
    public abstract class VFXGraphArgs : ShaderArgs { }
    protected VisualEffect _this;
}

public abstract class ComputeShaderObject : ShaderObject
{
    public abstract class ComputeShaderArgs : ShaderArgs { }
    protected ComputeShader _this;
    protected readonly Vector3[] num_threads_array;
    protected Vector3[] data_count_array;

    protected ComputeShaderObject(Vector3[] num_threads_array)
    {
        this.num_threads_array = num_threads_array;
    }

    Vector3Int thread_group_size(int kernel)
    {
        var num_thread = num_threads_array[kernel];
        var data_count = data_count_array[kernel];
        return new Vector3Int(
            Mathf.CeilToInt( data_count.x / num_thread.x ),
            Mathf.CeilToInt( data_count.y / num_thread.y ),
            Mathf.CeilToInt( data_count.z / num_thread.z ) );
    }
    public void Dispatch(int kernel)
    {
        var cur_thread_group_size = thread_group_size( kernel );
        _this.Dispatch( kernel, cur_thread_group_size.x, cur_thread_group_size.y, cur_thread_group_size.z );
    }

}
