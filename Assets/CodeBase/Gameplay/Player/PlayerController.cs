using CodeBase.Gameplay.Movers;
using CodeBase.Gameplay.Physic;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        private IInputService _inputService;
        private IStaticDataService _staticDataService;
        
        private CustomVelocity _velocity;
        private IMover _mover;

        [Inject]
        public void Construct(IInputService inputService, IStaticDataService staticDataService)
        {
            _inputService = inputService;
            _staticDataService = staticDataService;
            
            _velocity = new CustomVelocity(_rigidbody2D);
            _mover = new PhysicMover(_velocity);
        }

        private void Update()
        {
            _mover.Tick(_inputService.GetMoveAxis(), Time.deltaTime);
            _velocity.Tick(Time.deltaTime);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _velocity.HandleCollision(other);
        }
    }
}