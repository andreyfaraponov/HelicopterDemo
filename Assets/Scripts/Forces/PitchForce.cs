using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.Forces
{
    public class PitchForce : IForce
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly PitchConfig _config;

        public PitchForce(Rigidbody mainRigidbody, Transform transform, PitchConfig config)
        {
            _rigidbody = mainRigidbody;
            _transform = transform;
            _config = config;
        }

        public void ApplyForce(ForceModel forceModel, MovementModel movementModel)
        {
            var input = movementModel.Movement.y * _config.Direction;

            Vector3 localAngularVelocity = _transform.InverseTransformDirection(_rigidbody.angularVelocity);
            var speed = localAngularVelocity.x;
            
            bool hasInput = Mathf.Abs(input) > _config.InputDeadZone;

            if (hasInput)
            {
                bool alreadyPitching = Mathf.Abs(speed) > _config.MaxAngularSpeed
                    && Mathf.Approximately(Mathf.Sign(speed), Mathf.Sign(input));
                
                if (alreadyPitching)
                {
                    return;
                }
                
                forceModel.AddTorque(Vector3.right * input * _config.TorquePower);
            }
            else
            {
                var dampingTorque = -speed * _config.DampingPower;
                forceModel.AddTorque(Vector3.right * dampingTorque);
            }
        }
    }
}