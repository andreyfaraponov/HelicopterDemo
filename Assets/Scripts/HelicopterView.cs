using UnityEngine;

namespace HelicopterDemo
{
    public interface IHelicopterView
    {
        void Initialize(MovementModel movementModel);
    }

    public class HelicopterView : MonoBehaviour, IHelicopterView
    {
        [Header("Physics")] [SerializeField] private Rigidbody mainRigidbody;
        [SerializeField] private float verticalForce = 6000;
        [SerializeField] private float verticalDamping = 2000;
        [SerializeField] private float minLiftMultiplier = 0.2f;
        [SerializeField] private float maxLiftMultiplier = 1.8f;
        [SerializeField] private float finalLift;

        [Header("View")]
        [SerializeField] private HelicopterVisual visual;

        private MovementModel _model;
        private float _currentRotorRpm;

        private void FixedUpdate()
        {
            ApplyThrottle();
        }

        public void Initialize(MovementModel movementModel)
        {
            _model = movementModel;
            visual.Initialize(movementModel);
        }

        private void ApplyThrottle()
        {
            float hoverForce = mainRigidbody.mass * -Physics.gravity.y;
            float targetLift = hoverForce + _model.HeiphtInput * verticalForce;

            float minLift = hoverForce * minLiftMultiplier;
            float maxLift = hoverForce * maxLiftMultiplier;

            targetLift = Mathf.Clamp(targetLift, minLift, maxLift);

            float dampingForce = 0;

            if (Mathf.Approximately(_model.HeiphtInput, 0f))
            {
                float verticalSpeed = Vector3.Dot(mainRigidbody.linearVelocity, transform.up);
                dampingForce = -verticalSpeed * verticalDamping;
            }

            finalLift = targetLift + dampingForce;
            mainRigidbody.AddForce(transform.up * finalLift, ForceMode.Force);
        }
    }
}