using System.Threading.Tasks;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HelicopterDemo.UI
{
    public interface IWindowsService
    {
        StartPopup GetStartPopup();
        Task<HelpOverlay> ShowHelpOverlayAsync();
    }

    public class WindowsService : IWindowsService
    {
        private readonly RectTransform _popupsRoot;
        private readonly StartPopup _startPopupPrefab;
        private readonly HelpOverlay _helpOverlay;

        public WindowsService(RectTransform popupsRoot, StartPopup startPopupPrefab, HelpOverlay helpOverlay)
        {
            _popupsRoot = popupsRoot;
            _startPopupPrefab = startPopupPrefab;
            _helpOverlay = helpOverlay;
        }

        public StartPopup GetStartPopup()
        {
            var popup = Object.Instantiate(_startPopupPrefab, _popupsRoot);
            popup.gameObject.SetActive(false);
            return popup;
        }
        
        public async Task<HelpOverlay> ShowHelpOverlayAsync()
        {
            _helpOverlay.gameObject.SetActive(true);
            await _helpOverlay.ShowAsync();
            return _helpOverlay;
        }
    }
}