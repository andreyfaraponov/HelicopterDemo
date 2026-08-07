using System.Collections;
using UnityEngine;

namespace HelicopterDemo.HelicopterMono
{
    public class RearMotorAnimator : MonoBehaviour
    {
        [SerializeField] private Transform rearMotor;
        [SerializeField] private float rotationSpeed;

        private bool _isRotating;
        private float _degPerSecond;
        private Coroutine _rotateCoroutine;

        private void Start()
        {
            StartRotate();
        }

        public void StartRotate()
        {
            _isRotating = true;
            _rotateCoroutine = StartCoroutine(RotatePropellerCoroutine());
        }

        public void StopRotate()
        {
            StopCoroutine(_rotateCoroutine);
            _isRotating = false;
        }

        private IEnumerator RotatePropellerCoroutine()
        {
            _degPerSecond = rotationSpeed * 360 / 60;
            while (_isRotating)
            {
                rearMotor.Rotate(Vector3.right, _degPerSecond * Time.deltaTime, Space.Self);
                yield return null;
            }
        }
    }
}