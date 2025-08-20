using CodeBase.Data;
using CodeBase.Gameplay.Enviroment;
using CodeBase.Gameplay.Movers;
using CodeBase.Gameplay.Physic;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerModel : ITickable
    {
        private IInputService _inputService;
        private IStaticDataService _staticDataService;
        private readonly Arena _arena;

        private TransformData _transformData;
        private CustomVelocity _velocity;
        private IMover _mover;

        public TransformData TransformData => _transformData;

        public PlayerModel(IInputService inputService, IStaticDataService staticDataService, Arena arena)
        {
            _inputService = inputService;
            _staticDataService = staticDataService;
            _arena = arena;

            _transformData = new TransformData(Vector2.zero);
            _velocity = new CustomVelocity(_transformData);
            _mover = new PhysicMover(_velocity);
        }

        public void Tick()
        {
            _mover.Tick(_inputService.GetMoveAxis(), Time.deltaTime);
            _velocity.Tick(Time.deltaTime);
            _arena.TeleportIfOutsideArena(_transformData);
        }

        public void OnCollisionEnter2D(Collision2D other)
        {
            _velocity.HandleCollision(other);
        }

        
    }
}