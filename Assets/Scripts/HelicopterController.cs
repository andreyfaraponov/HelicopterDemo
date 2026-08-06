using System;
using UnityEngine;

namespace HelicopterDemo
{
    public class HelicopterController : IDisposable
    {
        private readonly IInputReader _inputReader;
        private readonly IHelicopterView _helicopterView;
        private readonly MovementModel _movementModel = new();

        public HelicopterController(IInputReader inputReader, IHelicopterView helicopterView)
        {
            _inputReader = inputReader;
            _helicopterView = helicopterView;
            _helicopterView.SetMovementModel(_movementModel);

            _inputReader.RotationYAxisEvent += OnUpRotation;
            _inputReader.MovementEvent += OnMovement;
            _inputReader.ThrottleEvent += OnThrottle;
        }

        public void Dispose()
        {
            _inputReader.RotationYAxisEvent -= OnUpRotation;
            _inputReader.MovementEvent -= OnMovement;
            _inputReader.ThrottleEvent -= OnThrottle;
        }

        private void OnThrottle(float value)
        {
            _movementModel.Throttle = value;
        }

        private void OnMovement(Vector2 value)
        {
            _movementModel.Movement = value;
        }

        private void OnUpRotation(float value)
        {
            _movementModel.Rotation = value;
        }
    }
}