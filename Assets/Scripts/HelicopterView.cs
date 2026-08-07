using UnityEngine;

namespace HelicopterDemo
{
    public interface IHelicopterView
    {
        void Initialize(MovementModel movementModel);
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        [SerializeField] private Rigidbody mainRigidbody;

        [Header("Physics Vertical")] [SerializeField]
        private float verticalForce = 6000;

        [SerializeField] private float verticalDamping = 2000;
        [SerializeField] private float minLiftMultiplier = 0.2f;
        [SerializeField] private float maxLiftMultiplier = 1.8f;
        [SerializeField] private float finalLift;

        [Header("Physics Forward/Back")] [SerializeField]
        private float forwardForce = 18000;

        [SerializeField] private float backwardForce = 12000;
        [SerializeField] private float maxForwardSpeed = 35f;
        [SerializeField] private float maxBackwardSpeed = 15f;

        [Header("Pitch Lean")] [SerializeField]
        private float pitchTorque = 12000;

        [SerializeField] private float maxAngularSpeed = 0.8f;
        [SerializeField] private float maxPitchAngularSpeed = 0.8f;
        [SerializeField] private float pitchLeanDirection = -1f;

        [SerializeField] private float inputDeadZone = 0.5f;


        [Header("View")] [SerializeField] private HelicopterVisual visual;

        private MovementModel _inputModel;
        private float _currentRotorRpm;

        private void FixedUpdate()
        {
            ApplyHeight();
            ApplyForwardMovement();
        }

        public void Initialize(MovementModel movementModel)
        {
            _inputModel = movementModel;
            visual.Initialize(movementModel);
        }

        private void ApplyForwardMovement()
        {
            if (Mathf.Abs(_inputModel.Movement.y) <= inputDeadZone)
            {
                return;
            }

            var invertedMovementY = -_inputModel.Movement.y;

            var signedForwardSpeed = Vector3.Dot(mainRigidbody.linearVelocity, transform.forward);
            var maxSpeed = invertedMovementY > 0 ? maxForwardSpeed : maxBackwardSpeed;
            var isMovingSameDirection =
                Mathf.Approximately(Mathf.Sign(signedForwardSpeed), Mathf.Sign(invertedMovementY));
            var canAccelerate = !isMovingSameDirection || Mathf.Abs(signedForwardSpeed) < maxSpeed;

            if (canAccelerate)
            {
                var force = invertedMovementY > 0 ? forwardForce : backwardForce;
                mainRigidbody.AddForce(transform.forward * force, ForceMode.Force);
            }

            ApplyPitchLean(invertedMovementY);
        }

        private void ApplyPitchLean(float movementY)
        {
            var localAngularVelocity = transform.InverseTransformDirection(mainRigidbody.angularVelocity);
            var torqueDirection = movementY * pitchLeanDirection;
            var alreadyPitchingTooFast =
                Mathf.Abs(localAngularVelocity.x) > maxPitchAngularSpeed
                && Mathf.Approximately(Mathf.Sign(localAngularVelocity.x), Mathf.Sign(torqueDirection));

            if (alreadyPitchingTooFast)
            {
                return;
            }

            var xAxis = Vector3.right;
            mainRigidbody.AddRelativeTorque(xAxis * torqueDirection * pitchTorque, ForceMode.Force);
        }

        private void ApplyHeight()
        {
            float hoverForce = mainRigidbody.mass * -Physics.gravity.y;
            float targetLift = hoverForce + _inputModel.HeiphtInput * verticalForce;

            float minLift = hoverForce * minLiftMultiplier;
            float maxLift = hoverForce * maxLiftMultiplier;

            targetLift = Mathf.Clamp(targetLift, minLift, maxLift);

            float dampingForce = 0;

            var dampingMultiplier = Mathf.Approximately(_inputModel.HeiphtInput, 0f) ? 1f : 0.25f;

            float verticalSpeed = Vector3.Dot(mainRigidbody.linearVelocity, Vector3.up);
            dampingForce = -dampingMultiplier * verticalSpeed * verticalDamping;


            finalLift = targetLift + dampingForce;

            var upDot = Vector3.Dot(transform.up, Vector3.up);
            upDot = Mathf.Clamp(upDot, 0.35f, 1f);
            var compensatedLift = finalLift / upDot;
            mainRigidbody.AddForce(transform.up * compensatedLift, ForceMode.Force);
        }
    }
}