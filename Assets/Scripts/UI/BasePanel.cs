using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace HelicopterDemo.UI
{
    public class BasePanel : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private float showTimeSeconds = .3f;
        
        private TaskCompletionSource<bool> _tcs;
        
        public async Task ShowAsync()
        {
            _tcs?.TrySetCanceled();
            canvasGroup.interactable = false;
            _tcs = new();
            canvasGroup.alpha = 0;
            gameObject.SetActive(true);
            StartCoroutine(ShowCoroutine());
            await _tcs.Task;
            canvasGroup.interactable = true;
        }

        public async Task HideAsync()
        {
            _tcs?.TrySetCanceled();
            _tcs = new();
            StartCoroutine(HideCoroutine());
            await _tcs.Task;
            gameObject.SetActive(false);
        }
        
        private IEnumerator ShowCoroutine()
        {
            while (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha += Time.unscaledDeltaTime / showTimeSeconds;

                if (canvasGroup.alpha >= 1)
                {
                    break;
                }

                yield return null;
            }

            canvasGroup.alpha = 1;
            _tcs.TrySetResult(true);
        }
        
        private IEnumerator HideCoroutine()
        {
            while (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha -= Time.unscaledDeltaTime / showTimeSeconds;

                if (canvasGroup.alpha <= 0)
                {
                    break;
                }

                yield return null;
            }
            
            canvasGroup.alpha = 0;
            
            _tcs.TrySetResult(true);
        }
    }
}