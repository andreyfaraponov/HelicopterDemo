using HelicopterDemo.Configs;
using HelicopterDemo.HelicopterMono;
using HelicopterDemo.UI;
using UnityEngine;

namespace HelicopterDemo
{
    public class DemoController
    {
        private readonly HelicopterView _helicopterView;
        private readonly HelicopterConfig _helicopterConfig;
        private readonly StatsView _statsView;
        private readonly WindowsService _windowsService;
        private readonly IInputReader _inputReader;

        private IVehicleController _controller;
        private bool _isHelpOpened;

        public DemoController(HelicopterView helicopterView, HelicopterConfig helicopterConfig, StatsView statsView,
            WindowsService windowsService, IInputReader inputReader)
        {
            _helicopterView = helicopterView;
            _helicopterConfig = helicopterConfig;
            _statsView = statsView;
            _windowsService = windowsService;
            _inputReader = inputReader;
        }

        public async void Start()
        {
            _statsView.SetObserveObject(_helicopterView);
            var popup = _windowsService.GetStartPopup();
            popup.UpdateTitle("Helicopter demo!");
            await popup.ShowAsync();
            popup.StartEvent += OnStart;
            popup.HelpEvent += OnHelp;
        }

        private async void OnHelp()
        {
            if (_isHelpOpened)
            {
                return;
            }

            Time.timeScale = 0;
            _isHelpOpened = true;
            var helpOverlay = await _windowsService.ShowHelpOverlayAsync();
            helpOverlay.CloseEvent += OnClose;
            
            void OnClose()
            {
                helpOverlay.CloseEvent -= OnClose;
                _isHelpOpened = false;
                Time.timeScale = 1;
            }
        }

        private void OnStart()
        {
            _helicopterView.ResetAll();
            _controller = new HelicopterController(_inputReader, _helicopterView, _helicopterConfig);
            _controller.CrashedEvent += OnCrashed;
            _inputReader.Enable(enable: true);
        }

        private async void OnCrashed()
        {
            _inputReader.Enable(false);
            _controller.CrashedEvent -= OnCrashed;
            _controller?.Dispose();
            var popup = _windowsService.GetStartPopup();
            popup.UpdateTitle("Helicopter was crashed!");
            await popup.ShowAsync();
            popup.StartEvent += OnStart;
            popup.HelpEvent += OnHelp;
        }
    }
}