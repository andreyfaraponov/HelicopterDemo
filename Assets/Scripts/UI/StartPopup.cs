using System;
using UnityEngine;
using UnityEngine.UI;

namespace HelicopterDemo.UI
{
    public class StartPopup : BasePanel
    {
        public event Action StartEvent;
        public event Action HelpEvent;

        [SerializeField] private Button startButton;
        [SerializeField] private Button helpButton;
        
        private void Awake()
        {
            startButton.onClick.AddListener(OnStartButtonClick);
            helpButton.onClick.AddListener(OnHelpButtonClick);
        }

        private void OnHelpButtonClick()
        {
            HelpEvent?.Invoke();
        }

        private async void OnStartButtonClick()
        {
            StartEvent?.Invoke();
            await HideAsync();
        }
    }
}