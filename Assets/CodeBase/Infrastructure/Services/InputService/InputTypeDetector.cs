using CodeBase.Data.StaticData;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputTypeDetector : ITickable
    {
        private readonly IInputService _inputService;
        private readonly IMobileInputProvider _mobileInputProvider;

        public InputTypeDetector(IInputService inputService, IMobileInputProvider mobileInputProvider)
        {
            _inputService = inputService;
            _mobileInputProvider = mobileInputProvider;
        }
        public void Tick()
        {
            if (Input.anyKey)
            {
                _mobileInputProvider.MobileInput?.Hide();
                _inputService.SetInputType(InputType.Keyboard);
            }else if (Input.touchCount > 0)
            {
                _mobileInputProvider.MobileInput?.Show();
                _inputService.SetInputType(InputType.Touchscreen);
            }
        }
    }
}