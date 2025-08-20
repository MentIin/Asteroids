using CodeBase.Data;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;

namespace CodeBase.Gameplay.Enviroment
{
    public class Arena
    {
        private readonly IStaticDataService _staticDataService;
        private TransformData _playerTransformData;
        
        public Vector2 Size { get; private set; }
        public Vector2 Extends => Size / 2;
        public Vector2 Center => Extends;

        public Arena(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void Initialize(TransformData playerTransformData)
        {
            _playerTransformData = playerTransformData;
            Size = new Vector2(10f, 10f);
        }

        public void TeleportIfOutsideArena(TransformData transformData)
        {
            if (transformData.Position.x < 0) transformData.Position.x += Size.x;
            else if (transformData.Position.x > Size.x) transformData.Position.x -= Size.x;
            
            if (transformData.Position.y < 0) transformData.Position.y += Size.y;
            else if (transformData.Position.y > Size.y) transformData.Position.y -= Size.y;
        }

        public Vector2 GetViewPosition(TransformData data)
        {
            Vector2 res = new Vector2(data.Position.x, data.Position.y);
            
            if (Mathf.Abs(data.Position.x - _playerTransformData.Position.x) > Extends.x)
            {
                res.x -= Extends.x;
                if (res.x < 0)
                {
                    res.x += Size.x;
                }
            }
            
            return res;
        }
    }
}