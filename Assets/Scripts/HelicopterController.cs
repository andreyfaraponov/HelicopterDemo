using System;
using HelicopterDemo.Configs;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IVehicleController : IDisposable
    {
    }

    public class HelicopterController : IVehicleController
    {
        private readonly IInputReader _inputReader;
        private readonly MovementModel _movementModel = new();

        public HelicopterController(IInputReader inputReader, IHelicopterView helicopterView,
            HelicopterConfig helicopterConfig)
        {
            _inputReader = inputReader;
            helicopterView.Initialize(_movementModel, helicopterConfig);

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
            _movementModel.Movement = value;
        }

        private void OnUpRotation(float value)
        {
            _movementModel.Yaw = value;
        }
    }
}