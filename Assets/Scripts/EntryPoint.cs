using HelicopterDemo.Configs;
using HelicopterDemo.HelicopterMono;
using HelicopterDemo.UI;
using UnityEngine;

namespace HelicopterDemo
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private HelicopterView helicopterView;
        [SerializeField] private HelicopterConfig helicopterConfig;
        [SerializeField] private RectTransform popupsRoot;
        [SerializeField] private StartPopup startPopupPrefab;
        [SerializeField] private StatsView statsView;
        [SerializeField] private HelpOverlay helpOverlay;

        private InputReader _inputReader;
        private WindowsService _windowsService;
        private DemoController _demoController;

        private void Awake()
        {
            _inputReader = new InputReader();
            _windowsService = new WindowsService(popupsRoot, startPopupPrefab, helpOverlay);
            _demoController =
                new DemoController(helicopterView, helicopterConfig, statsView, _windowsService, _inputReader);
            helpOverlay.UpdateKeys(_inputReader);
        }

        private void Start()
        {
            _demoController.Start();
        }

        private void OnDestroy()
        {
            _inputReader?.Dispose();
        }
    }
}