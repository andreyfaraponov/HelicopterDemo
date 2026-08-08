using System;
using HelicopterDemo.Configs;
using HelicopterDemo.HelicopterMono;
using HelicopterDemo.Models;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IVehicleController : IDisposable
    {
        event Action CrashedEvent;
    }

    public class HelicopterController : IVehicleController
    {
        public event Action CrashedEvent;
        
        private readonly IInputReader _inputReader;
        private readonly MovementModel _movementModel = new();
        private readonly IHelicopterView _helicopterView;

        public HelicopterController(IInputReader inputReader, IHelicopterView helicopterView,
            HelicopterConfig helicopterConfig)
        {
            _inputReader = inputReader;
            _helicopterView = helicopterView;
            _helicopterView.Initialize(_movementModel, helicopterConfig);

            _helicopterView.CrashedEvent += OnCrashed;
            _inputReader.RotationYAxisEvent += OnUpRotation;
            _inputReader.MovementEvent += OnMovement;
            _inputReader.ThrottleEvent += OnThrottle;
        }

        public void Dispose()
        {
            _helicopterView.CrashedEvent -= OnCrashed;
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

        private void OnCrashed()
        {
            CrashedEvent?.Invoke();
        }
    }
}