using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.Forces
{
    public class LiftForce : IForce
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly LiftConfig _liftConfig;

        public LiftForce(Rigidbody rigidbody, Transform transform, LiftConfig liftConfig)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _liftConfig = liftConfig;
        }

        public void ApplyForce(ForceModel forceModel, MovementModel movementModel)
        {
            var hoverForce = _rigidbody.mass * -Physics.gravity.y;
            var targetLift = hoverForce + movementModel.HeightInput * _liftConfig.ForcePower;
            
            var minLift = hoverForce * _liftConfig.MinMultiplier;
            var maxLift = hoverForce * _liftConfig.MaxMultiplier;
            
            targetLift = Mathf.Clamp(targetLift, minLift, maxLift);

            var dampingForce = 0f;

            if (Mathf.Abs(movementModel.HeightInput) <= _liftConfig.InputDeadZone)
            {
                float verticalSpeed = Vector3.Dot(_rigidbody.linearVelocity, Vector3.up);
                dampingForce = -verticalSpeed * _liftConfig.DampingPower;
            }
            
            var finalLift = targetLift + dampingForce;
            
            forceModel.AddForce(_transform.up * finalLift);
        }
    }
}