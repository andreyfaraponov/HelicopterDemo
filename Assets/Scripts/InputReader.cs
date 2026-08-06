using System;
using Demo;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IInputReader
    {
        event Action<float> RotationYAxisEvent;
        event Action<Vector2> MovementEvent;
        event Action<float> ThrottleEvent;
    }

    public class InputReader : IDisposable, IInputReader
    {
        public event Action<float> RotationYAxisEvent;
        public event Action<Vector2> MovementEvent;
        public event Action<float> ThrottleEvent;

        private readonly HelicopterInput _input = new();

        public InputReader()
        {
            _input.Helicopter.HorizontalRotation.performed += ctx => RotationYAxisEvent?.Invoke(ctx.ReadValue<float>());
            _input.Helicopter.HorizontalRotation.canceled += _ => RotationYAxisEvent?.Invoke(0);
            _input.Helicopter.HorizontalMovement.performed += ctx => MovementEvent?.Invoke(ctx.ReadValue<Vector2>());
            _input.Helicopter.HorizontalMovement.canceled += _ => MovementEvent?.Invoke(Vector2.zero);
            _input.Helicopter.HeightAxis.performed += ctx => ThrottleEvent?.Invoke(ctx.ReadValue<float>());
            _input.Helicopter.HeightAxis.canceled += _ => ThrottleEvent?.Invoke(0);
        }

        public void Dispose()
        {
            _input.Dispose();
        }
    }
}