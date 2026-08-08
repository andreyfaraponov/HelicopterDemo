using HelicopterDemo.HelicopterMono;
using TMPro;
using UnityEngine;

namespace HelicopterDemo.UI
{
    public class StatsView : BasePanel
    {
        [SerializeField] private TMP_Text speedText;
        [SerializeField] private TMP_Text altitudeText;
        [SerializeField] private TMP_Text positionText;
        [SerializeField] private float altitudeCompensation = 0.06f;

        private Rigidbody _rigidbody;

        private void Update()
        {
            if (_rigidbody == null)
            {
                return;
            }
            
            UpdateStats(_rigidbody.linearVelocity.magnitude, _rigidbody.position.y, _rigidbody.position);
        }

        public void SetObserveObject(HelicopterView helicopterView)
        {
            _rigidbody = helicopterView.MainRigidbody;
        }

        private void UpdateStats(float speed, float altitude, Vector3 position)
        {
            var altitudeCompensated = altitude + altitudeCompensation;
            speedText.text = $"{speed:F2}";
            altitudeText.text = $"{altitudeCompensated:F2}";
            positionText.text = $"x: {position.x:F2}, y: {position.y:F2}, z: {position.z:F2}";
        }
    }
}