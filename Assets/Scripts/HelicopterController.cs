using System;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IVehicleController : IDisposable
    {
    }

    public class HelicopterController : IVehicleController
    {
        private readonly IInputReader _inputReader;
        private readonly IHelicopterView _helicopterView;
        private readonly MovementModel _movementModel = new();

        public HelicopterController(IInputReader inputReader, IHelicopterView helicopterView)
        {
            _inputReader = inputReader;
            _helicopterView = helicopterView;
            _helicopterView.Initialize(_movementModel);

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
            _movementModel.HeightInput = value;
        }

        private void OnMovement(Vector2 value)
        {
            Debug.Log($"Movement: {value}");
            _movementModel.Movement = value;
        }

        private void OnUpRotation(float value)
        {
            _movementModel.Rotation = value;
        }
    }
}