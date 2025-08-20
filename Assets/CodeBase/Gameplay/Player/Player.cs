using CodeBase.Data;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Player
{
    public class Player : MonoBehaviour
    {
        
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private PlayerModel _playerModel;
        
        public TransformData TransformData => _playerModel.TransformData;
        [Inject]
        public void Construct(PlayerModel model)
        {
            _playerModel = model;
        }

        private void Update()
        {
            _playerModel.Tick();
            _rigidbody2D.MovePosition(_playerModel.TransformData.Position);
        }

        private void OnCollisionEnter2D(Collision2D other) => _playerModel.OnCollisionEnter2D(other);
    }
}