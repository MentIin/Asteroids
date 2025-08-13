using CodeBase.Infrastructure;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Overlays
{
    public class AwaitingOverlayProxy : IAwaitingOverlay
    {
        private AwaitingOverlay.Factory factory;
        private IAwaitingOverlay impl;

        public AwaitingOverlayProxy(AwaitingOverlay.Factory factory) => 
            this.factory = factory;

        public void InitializeAsync()
        {
            impl = factory.Create(InfrastructureAssetPath.AwaitingOverlay);
            
            Debug.Log(impl);
        }

        public void Show(string withMessage) => impl.Show(withMessage);

        public void Hide() => impl.Hide();
    }
}