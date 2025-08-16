using System;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Player
{
    public class PlayerPresenter : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly IInputService _inputProvider;
        private readonly SignalBus _signalBus;

        public PlayerPresenter(
            PlayerModel model,
            PlayerView view,
            IInputService inputProvider,
            SignalBus signalBus)
        {
            _model = model;
            _view = view;
            _inputProvider = inputProvider;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            // Первоначальная синхронизация
            _view.UpdateView(_model.TransformData);
        }

        public void Tick()
        {
            Vector2 movementInput = _inputProvider.GetMoveAxis();
            float rotationInput = _inputProvider.GetRotation();
            
            _model.UpdateTransform(movementInput, rotationInput, Time.deltaTime);
            _model.UpdateInvulnerability(Time.deltaTime);
            
            _view.UpdateView(_model.TransformData);
            _view.SetInvulnerabilityEffect(_model.IsInvulnerable);
        
            // Отправка сигналов для UI
            /*_signalBus.Fire(new PlayerStateChangedSignal {
                Position = _model.Transform.Position,
                Rotation = _model.Transform.Rotation,
                Speed = _model.Transform.Velocity.magnitude
            });*/
        }

        public void Dispose()
        {
            Object.Destroy(_view.gameObject);
        }
    }
}