using CodeBase.Data.StaticData;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputTypeDetector : ITickable
    {
        private readonly IInputService _inputService;

        public InputTypeDetector(IInputService inputService)
        {
            _inputService = inputService;
        }
        public void Tick()
        {
            if (Input.anyKey)
            {
                _inputService.SetInputType(InputType.Keyboard);
            }else if (Input.touchCount > 0)
            {
                _inputService.SetInputType(InputType.Touchscreen);
            }
        }
    }
}