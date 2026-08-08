using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace HelicopterDemo.UI
{
    public class HelpOverlay : BasePanel
    {
        public event Action CloseEvent;
        
        [SerializeField] private TMP_Text pitchForwardKey;
        [SerializeField] private TMP_Text pitchBackwardKey;
        [SerializeField] private TMP_Text rollRightKey;
        [SerializeField] private TMP_Text rollLeftKey;
        [SerializeField] private TMP_Text rotateRightKey;
        [SerializeField] private TMP_Text rotateLeftKey;
        [SerializeField] private TMP_Text liftUpKey;
        [SerializeField] private TMP_Text liftDownKey;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(() =>
            {
                CloseEvent?.Invoke();
                HideAsync();
            });
        }

        public void UpdateKeys(InputReader inputReader)
        {
            var input = inputReader.InputSchema;
            var movementString = input.Helicopter.HorizontalMovement.GetBindingDisplayString()?.Split("/");
            Debug.Log(input.Helicopter.HorizontalMovement.GetBindingDisplayString());

            if (movementString != null)
            {
                pitchForwardKey.text = movementString[1];
                pitchBackwardKey.text = movementString[2];
                rollRightKey.text = movementString[3];
                rollLeftKey.text = movementString[2];
            }

            var heightString = input.Helicopter.HeightAxis.GetBindingDisplayString()?.Split("/");
            Debug.Log(input.Helicopter.HeightAxis.GetBindingDisplayString());

            if (heightString != null)
            {
                liftUpKey.text = heightString[0];
                liftDownKey.text = heightString[1];
            }

            var rotationString = input.Helicopter.HorizontalRotation.GetBindingDisplayString()?.Split("/");
            Debug.Log(input.Helicopter.HorizontalRotation.GetBindingDisplayString());

            if (rotationString != null)
            {
                rotateRightKey.text = rotationString[1];
                rotateLeftKey.text = rotationString[0];
            }
        }
    }
}