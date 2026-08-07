using UnityEngine;

namespace HelicopterDemo.Models
{
    public class ForceModel
    {
        public Vector3 Force { get; set; }
        public Vector3 Torque { get; set; }
        public bool AllZero => Force == Vector3.zero && Torque == Vector3.zero;

        public void AddForce(Vector3 force)
        {
            Force += force;
        }
        
        public void AddTorque(Vector3 torque)
        {
            Torque += torque;
        }
        
        public void Clear()
        {
            Force = Vector3.zero;
            Torque = Vector3.zero;
        }
    }
}