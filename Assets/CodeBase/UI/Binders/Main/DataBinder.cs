using UnityEngine;
using Zenject;

namespace CodeBase.UI.Binders.Main
{
    public class DataBinder : MonoBehaviour
    {
        private DiContainer _container;

        [Inject]
        public void Construct(DiContainer container)
        {
            _container = container;
        }
    }
}