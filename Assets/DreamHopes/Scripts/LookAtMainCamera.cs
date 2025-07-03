using UnityEngine;


    // This script is attached to portal labels to keep them facing the camera
    public class LookAtMainCamera : MonoBehaviour
    {
        
        
        void LateUpdate()
        {
            transform.forward = Camera.main.transform.forward;
        }
    }

