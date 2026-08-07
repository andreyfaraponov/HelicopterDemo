using System;
using UnityEngine;

namespace HelicopterDemo.HelicopterMono
{
    public class GroundDetector : MonoBehaviour
    {
        public event Action<bool> GroundDetectedEvent;
        
        private void OnTriggerEnter(Collider other)
        {
            GroundDetectedEvent?.Invoke(true);
        }

        private void OnTriggerExit(Collider other)
        {
            GroundDetectedEvent?.Invoke(false);
        }
    }
}