using UnityEngine;

namespace HelicopterDemo
{
    public class HelicopterVisual : MonoBehaviour
    {
        [SerializeField] private Transform mainRotor;
        [SerializeField] private float idleRpm = 280;
        [SerializeField] private float maxRpm = 850;
        [SerializeField] private float rotorRpmResponse = 6;

        private float _currentRotorRpm;
        private MovementModel _movementModel;

        public void Initialize(MovementModel movementModel)
        {
            _movementModel = movementModel;
        }

        private void Update()
        {
            UpdateCurrentRpm(_movementModel.HeiphtInput);
            var degPerSec = _currentRotorRpm * 360f / 60f;
            mainRotor.Rotate(Vector3.forward, degPerSec * Time.deltaTime, Space.Self);
        }

        public void UpdateCurrentRpm(float input)
        {
            float input01 = Mathf.InverseLerp(-1f, 1f, input);
            float targetRpm = Mathf.Lerp(idleRpm, maxRpm, input01);

            _currentRotorRpm = Mathf.Lerp(_currentRotorRpm, targetRpm,
                1f - Mathf.Exp(-rotorRpmResponse * Time.deltaTime));
        }
    }
}