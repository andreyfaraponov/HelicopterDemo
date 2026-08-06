using System;
using UnityEngine;

namespace HelicopterDemo
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private HelicopterView helicopterView;
        
        private HelicopterController _controller;
        private InputReader _inputReader;

        private void Awake()
        {
            _inputReader = new InputReader();
        }

        private void Start()
        {
            _controller = new HelicopterController(_inputReader, helicopterView);
        }

        private void OnDestroy()
        {
            _controller?.Dispose();
            _inputReader?.Dispose();
        }
    }
}