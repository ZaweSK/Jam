using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Game : MonoBehaviour {
    [Space(10)] [Header("VELOCITY")] [SerializeField]
    public bool InvertKeys = false;


    [Space(10)] [Header("WORLD")] [SerializeField]
    public bool RotateWorld = false;

    [SerializeField] public float RotateValue = 0.04f;

    [Space(10)]
    [Header("PLAYER ROTATION")]
    
    [SerializeField] 
    public bool RotatePlayerAlways;
    
    [SerializeField] 
    public bool RotatePlayerWhenMoving;
    
    [SerializeField]
    public float PlayerRotationValue = 0.04f;
       

    [Space(10)]
    [Header("PLAYER MOVEMENT")]
        
      

        private static Game _instance;
      
        private void Awake() {
            if (_instance == null) {
                _instance = this;
            }
            else {
                Destroy(gameObject);
            }
        }

        public static Game Instance => _instance;
        
    
}

