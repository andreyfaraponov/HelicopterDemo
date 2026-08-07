using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.Forces
{
    public class RollForce : IForce
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly RollConfig _config;

        public RollForce(Rigidbody rigidbody, Transform transform, RollConfig config)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _config = config;
        }

        public void ApplyForce(ForceModel forceModel, MovementModel movementModel)
        {
            var rollInput = movementModel.Movement.x * _config.Direction;

            var localAngularVelocity = _transform.InverseTransformDirection(_rigidbody.angularVelocity);
            var rollSpeed = localAngularVelocity.z;

            bool hasInput = Mathf.Abs(rollInput) > _config.InputDeadZone;

            if (hasInput)
            {
                bool alreadyRolling = Mathf.Abs(rollSpeed) > _config.MaxAngularSpeed
                                      && Mathf.Approximately(Mathf.Sign(rollSpeed), Mathf.Sign(rollInput));

                if (alreadyRolling)
                {
                    return;
                }

                forceModel.AddTorque(Vector3.forward * rollInput * _config.TorquePower);
            }
            else
            {
                var dampingTorque = -rollSpeed * _config.Damping;
                forceModel.AddTorque(Vector3.forward * dampingTorque);
            }
        }
    }
}