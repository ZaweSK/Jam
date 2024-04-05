using UnityEngine;

namespace Scenes.Scripts {
    
    public class PlaneRotator: MonoBehaviour {

        public void Update() {
            
            if (Game.Instance.RotateWorld) {
                // invert the rotation
                transform.Rotate(0, Game.Instance.RotateValue, 0);
            }
            
        }
    }
}