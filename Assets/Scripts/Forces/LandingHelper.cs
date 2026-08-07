using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo.Forces
{
    public class LandingHelper
    {
        private readonly Rigidbody _rigidbody;
        private readonly Transform _transform;
        private readonly LandingHelperConfig _config;

        public LandingHelper(Rigidbody rigidbody, Transform transform, LandingHelperConfig config)
        {
            _rigidbody = rigidbody;
            _transform = transform;
            _config = config;
        }

        public void ApplyForce(ForceModel forceModel)
        {
            Vector3 localWorldUp = _transform.InverseTransformDirection(Vector3.up);

            var pitchError = localWorldUp.z;
            var rollError = -localWorldUp.x;

            Vector3 localAngularVelocity = _transform.InverseTransformDirection(_rigidbody.angularVelocity);

            var pitchTorque = pitchError * _config.UpRightPower - localAngularVelocity.x * _config.DampingPower;
            var rollTorque = rollError * _config.UpRightPower - localAngularVelocity.z * _config.DampingPower;
            
            forceModel.AddTorque(new Vector3(pitchTorque, 0, rollTorque));
        }
    }
}