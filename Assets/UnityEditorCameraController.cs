using UnityEngine;
public class UnityEditorCameraController : MonoBehaviour
{

#region Config

    [Tooltip("WASD Move")][Range(1, 100)]
    public int MoveSpeed;
    float move_speed => MoveSpeed;

    [Tooltip("MMB Drag")][Range(1, 100)]
    public int DragScale;
    float drag_scale => DragScale;

    [Tooltip("RMB Rotate")][Range(1, 100)]
    public int RotateSensitive;
    float rotate_sensitive => RotateSensitive;

    [Tooltip("Flip the y axis rotation of input")]
    public bool y_flip;
    [Tooltip("Flip the x axis rotation of input")]
    public bool x_flip;

#endregion

#region Component

    Transform target_t;
    Transform t;

#endregion

#region Operation

    static Vector2 mouse_move => new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    static Vector2 wasd_move => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

    enum InputMode
    {
        Drag,
        Rotate,
        None
    }
    (
        InputMode input_mode,
        Vector2 move,
        Vector2 drag,
        Vector2 rotate
        )
        user_input = (InputMode.None, Vector2.zero, Vector2.zero, Vector2.zero);

    void check_input_mode()
    {
        if (user_input.input_mode == InputMode.None)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                user_input.input_mode = InputMode.Rotate;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse2))
            {
                user_input.input_mode = InputMode.Drag;
            }
        }
        else
        {
            if (
                Input.GetKeyUp(KeyCode.Mouse1) && user_input.input_mode == InputMode.Rotate ||
                Input.GetKeyUp(KeyCode.Mouse2) && user_input.input_mode == InputMode.Drag)
            {
                user_input.input_mode = InputMode.None;
            }
        }
    }

    void refresh_input()
    {
        user_input.move = move_speed * wasd_move;

        user_input.drag =
            user_input.input_mode == InputMode.Drag ?
                mouse_move * drag_scale
                : Vector2.zero;

        user_input.rotate =
            user_input.input_mode == InputMode.Rotate ?
                mouse_move * rotate_sensitive
                : Vector2.zero;
    }

    void apply_input()
    {
        target_t.Translate(Time.deltaTime * new Vector3(user_input.move.x, 0, user_input.move.y), target_t);

        var e = target_t.rotation.eulerAngles;
        target_t.rotation = Quaternion.Euler(e + new Vector3(-user_input.rotate.y, user_input.rotate.x, 0));

        target_t.Translate(Time.deltaTime * new Vector3(-user_input.drag.x, -user_input.drag.y, 0), target_t);

        t.position = Vector3.Lerp(t.position, target_t.position, 0.3f);
        t.rotation = Quaternion.Lerp(t.rotation, target_t.rotation, 0.3f);
    }

#endregion

#region Event

    void Start()
    {
        target_t = new GameObject().transform;
        target_t.parent = transform.parent;
        t = transform;
        target_t.position = t.position;
        target_t.rotation = t.rotation;
    }

    void Update()
    {
        // Debug.Log($"x: {Input.GetAxis("Mouse X")}, y: {Input.GetAxis("Mouse Y")}");
        // Debug.Log($"h: {Input.GetAxis("Horizontal")}, v: {Input.GetAxis("Vertical")}");
        check_input_mode();
        refresh_input();
        apply_input();
    }

#endregion

}