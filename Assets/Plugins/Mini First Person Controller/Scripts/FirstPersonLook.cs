using UnityEngine;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    void Start()
    {
        // Lock the mouse cursor to the game screen.
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        
        // Get smooth velocity.
        Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        
        Debug.Log($"XXX mouse delta {mouseDelta}");
        Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        Debug.Log($"XXX  rawframeelocity {rawFrameVelocity}");
        frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
        // frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing) + new Vector2(0.01f, 0.001f);

        
        Debug.Log($"XXX frame velocity {frameVelocity}");
        
        velocity += frameVelocity;
        
        Debug.Log($"XXX velocity {velocity}");
        velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        
        // Debug.Log($"XXX camera velocity {velocity}");
        
        // Rotate camera up-down and controller left-right from velocity.
        transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        
    }
}
