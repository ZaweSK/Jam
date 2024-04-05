using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour  {
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();
    
    
    

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        Debug.Log($"XXX  ---------------- ");
        Debug.Log($"XXX INPUT HORIZONTAL {Input.GetAxis("Horizontal")}");
        Debug.Log($"XXX INPUT VERTICAL {Input.GetAxis("Vertical")}");
        
        // Get targetVelocity from input.
        
        
        var inputHorizontal = Input.GetAxis("Horizontal"); // Dolava / doprava
        var inputVertical = Input.GetAxis("Vertical"); // Dopredu/ dozadu
        
        var verticalVelocity = targetMovingSpeed * inputVertical;
        var horizontalVelocity = targetMovingSpeed * inputHorizontal;
        
        var targetVelocity =new Vector2( horizontalVelocity, verticalVelocity);
        
        if (Game.Instance.InvertKeys)
        {
            targetVelocity = new Vector2(-targetVelocity.x, -targetVelocity.y);
        }
        
        Debug.Log($"XXX TARGET VELOCITY {targetVelocity}");
        Debug.Log($"XXX ROTATION: {transform.rotation}");
        
        var result = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        Debug.Log($"XXX APPLYING: {targetVelocity} * {transform.rotation} = {result}");

        // Apply movement.
        rigidbody.velocity = result;
    }
}