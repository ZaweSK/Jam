using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
         
        [SerializeField]
        public bool InvertKeys = false;
        
        [SerializeField]
        public bool RotateWorld = false;
        
        [SerializeField]
        public float RotateValue = 0.04f;

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

