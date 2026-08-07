using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.Forces
{
    public class YawForce : IForce
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly YawConfig _config;

        public YawForce(Rigidbody rigidbody, Transform transform, YawConfig config)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _config = config;
        }

        public void ApplyForce(ForceModel forceModel, MovementModel movementModel)
        {
            var input = movementModel.Yaw;
            Vector3 localEulerAngles = _transform.InverseTransformDirection(_rigidbody.angularVelocity);
            var speed = localEulerAngles.y;

            bool hasInput = Mathf.Abs(input) > _config.InputDeadZone;

            if (hasInput)
            {
                bool alreadyYawing = Mathf.Abs(speed) > _config.MaxAngularSpeed
                    && Mathf.Approximately(Mathf.Sign(speed), Mathf.Sign(input));
                
                if (alreadyYawing)
                {
                    return;
                }
                
                forceModel.AddTorque(Vector3.up * input * _config.TorquePower);
            }
            else
            {
                var damping = -speed * _config.DampingPower;
                
                forceModel.AddTorque(Vector3.up * damping);
            }
        }
    }
}