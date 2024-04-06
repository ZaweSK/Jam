using System.Collections.Generic;
using System.Security.Cryptography;
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


    private bool _phase = false;
    

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }
    
        private float _currentDuration = 0f;
        private float _duration = 3f;
    
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

        
        // ====================================================================== 
        Debug.Log($"XXX  ---------------- ");
        Debug.Log($"XXX INPUT HORIZONTAL {Input.GetAxis("Horizontal")}");
        Debug.Log($"XXX INPUT VERTICAL {Input.GetAxis("Vertical")}");
        //
        // if (Game.Instance.RotatePlayerAlways) {
        //     transform.Rotate(0, Game.Instance.PlayerRotationValue, 0);
        // }
        
        
        var inputHorizontal = Input.GetAxis("Horizontal"); // Dolava / doprava
        var inputVertical = Input.GetAxis("Vertical"); // Dopredu/ dozadu
        



        if (inputVertical == 0) {
            _currentDuration = 0f;
        }
        
        if (inputVertical != 0) {

                if (_phase) {
                    _currentDuration += Time.deltaTime;
                 }
                else {
                    _currentDuration -= Time.deltaTime;
                }
               
                var normalizedDuration = _currentDuration / _duration;
                
            
                
                
                float adjustedValue = Mathf.Lerp(-0.5f, 0.5f, normalizedDuration);
                
                
                float pingPongValue = Mathf.PingPong(_currentDuration / _duration, 1f);
                
                Debug.Log($"XXX adjusted {adjustedValue}");
                
                inputHorizontal = Input.GetAxis("Horizontal") + adjustedValue;
                
                Debug.Log($"XXX after {inputHorizontal}");
        
                if  (normalizedDuration >= 1f || normalizedDuration <= 0f) {
                   _phase = !_phase;
                }
            
        }
        
        var verticalVelocity = targetMovingSpeed * inputVertical;
        var horizontalVelocity = targetMovingSpeed * inputHorizontal;
        
        var targetVelocity =new Vector2( horizontalVelocity, verticalVelocity);
        
        if (Game.Instance.InvertKeys) {
            targetVelocity = new Vector2(-targetVelocity.x, -targetVelocity.y);
        }
        
     
        
        transform.Rotate(0, 0.4f, 0);

        
        
        // if (Game.Instance.RotatePlayerWhenMoving) {
        //     rotation = Quaternion.Euler(0, Game.Instance.PlayerRotationValue, 0);
        // }

        // Quaternion constantRotation = Quaternion.Euler(0, 0.4F, 0);
        //
        // Debug.Log($"XXX Before applying {transform.rotation}");
        // Debug.Log($"XXX rotation {constantRotation}");
        // transform.rotation *= constantRotation;
        //
        // Debug.Log($"XXX After applying {transform.rotation}");
        
        
        var rotation = transform.rotation;
        var result = rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        rigidbody.velocity = result;
        
        
        // LOG 
        // Debug.Log($"XXX INPUT HORIZONTAL {Input.GetAxis("Horizontal")}");
        // Debug.Log($"XXX INPUT VERTICAL {Input.GetAxis("Vertical")}");
        // Debug.Log($"XXX TARGET VELOCITY {targetVelocity}");
        // Debug.Log($"XXX APPLYING: {targetVelocity} * {rotation} = {result}");

    }
    
}