using System;
using Demo;
using UnityEngine;

namespace HelicopterDemo
{
    public interface IInputReader : IDisposable
    {
        event Action<float> RotationYAxisEvent;
        event Action<Vector2> MovementEvent;
        event Action<float> ThrottleEvent;
        void Enable(bool enable);
    }

    public class InputReader : IInputReader
    {
        public event Action<float> RotationYAxisEvent;
        public event Action<Vector2> MovementEvent;
        public event Action<float> ThrottleEvent;

        public readonly HelicopterInput InputSchema;


        public InputReader()
        {
            InputSchema = new HelicopterInput();
            InputSchema.Helicopter.HorizontalRotation.performed += ctx => RotationYAxisEvent?.Invoke(ctx.ReadValue<float>());
            InputSchema.Helicopter.HorizontalRotation.canceled += _ => RotationYAxisEvent?.Invoke(0);
            InputSchema.Helicopter.HorizontalMovement.performed += ctx => MovementEvent?.Invoke(ctx.ReadValue<Vector2>());
            InputSchema.Helicopter.HorizontalMovement.canceled += _ => MovementEvent?.Invoke(Vector2.zero);
            InputSchema.Helicopter.HeightAxis.performed += ctx => ThrottleEvent?.Invoke(ctx.ReadValue<float>());
            InputSchema.Helicopter.HeightAxis.canceled += _ => ThrottleEvent?.Invoke(0);
        }

        public void Dispose()
        {
            InputSchema.Dispose();
        }

        public void Enable(bool enable)
        {
            if (enable)
            {
                InputSchema.Enable();
            }
            else
            {
                InputSchema.Disable();
            }
        }
    }
}