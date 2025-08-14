using CodeBase.Interfaces.Infrastructure.UI.HUD;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.HUD
{
    public class HUDRoot : MonoBehaviour, IHUDRoot
    {



        public class Factory : PlaceholderFactory<HUDRoot>
        {
            
        }
    }
}