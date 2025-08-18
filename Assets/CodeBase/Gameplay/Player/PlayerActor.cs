using System;
using CodeBase.Interfaces.Infrastructure.Services;
using CodeBase.Models;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Player
{
    public class PlayerActor : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private readonly IInputService _inputProvider;

        public PlayerActor(
            PlayerModel model,
            PlayerView view,
            IInputService inputProvider)
        {
            Debug.Log("PlayerActor Constructor");
            _model = model;
            _view = view;
            _inputProvider = inputProvider;
            
            Debug.Log(_view.gameObject.name);
        }

        public void Initialize()
        {
            Debug.Log("PlayerActor Initialize");
            // Первоначальная синхронизация
            _view.UpdateView(_model.TransformData);
        }

        public void Tick()
        {
            Debug.Log("PlayerActor Tick");
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