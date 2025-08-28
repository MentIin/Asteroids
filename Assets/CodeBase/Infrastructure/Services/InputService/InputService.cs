using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Interfaces.Infrastructure.Services.UI;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.Services.InputService
{
    public class InputService : IInputService, ITickable, IInitializable
    {
        private readonly IMobileInputProvider _mobileInputProvider;
        private IInputStrategy _currentInputStrategy;

        public InputService(IMobileInputProvider mobileInputProvider)
        {
            _mobileInputProvider = mobileInputProvider;
        }
        
        public float GetMovement() => _currentInputStrategy.GetMovement();

        public float GetRotation() => _currentInputStrategy.GetRotation();

        public bool GetBaseAttack() => _currentInputStrategy.GetBaseAttack();

        public bool GetSpecialAttack() => _currentInputStrategy.GetSkill();

        public void Initialize()
        {
            _currentInputStrategy = new MockInputStrategy();
        }

        public void Tick()
        {
            if (Input.anyKey)
            {
                _mobileInputProvider.MobileInput.Hide();
                _currentInputStrategy = new PCInputStrategy();
            }else if (Input.touchCount > 0)
            {
                _mobileInputProvider.MobileInput.Show();
                _currentInputStrategy = new MobileInputStrategy(_mobileInputProvider);
            }
        }
    }
}