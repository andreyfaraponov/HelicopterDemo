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
        
        [SerializeField] private TMP_Text inputKeys1;
        [SerializeField] private TMP_Text inputKeys2;
        [SerializeField] private TMP_Text inputKeys3;
        [SerializeField] private Button closeButton;

        private void Awake()
        {
            closeButton.onClick.AddListener(async () =>
            {
                CloseEvent?.Invoke();
                await HideAsync();
            });
        }

        public void UpdateKeys(InputReader inputReader)
        {
            var input = inputReader.InputSchema;
            inputKeys1.text = input.Helicopter.HorizontalMovement.GetBindingDisplayString();
            inputKeys2.text = input.Helicopter.HeightAxis.GetBindingDisplayString();
            inputKeys3.text = input.Helicopter.HorizontalRotation.GetBindingDisplayString();
        }
    }
}