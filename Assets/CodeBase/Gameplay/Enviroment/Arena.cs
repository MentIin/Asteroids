using System.Collections.Generic;
using CodeBase.Data;
using CodeBase.Gameplay.Player;
using CodeBase.Interfaces.Infrastructure.Services;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Enviroment
{
    public class Arena : ITickable
    {
        private readonly IStaticDataService _staticDataService;
        private readonly PlayerProvider _playerProvider;
        
        public Vector2 Size { get; private set; }
        public Vector2 Extends => Size / 2;
        public Vector2 Center => Extends;
        
        private List<IArenaMember> _members = new List<IArenaMember>();

        public Arena(IStaticDataService staticDataService, PlayerProvider playerProvider)
        {
            _staticDataService = staticDataService;
            _playerProvider = playerProvider;
        }

        public void Initialize()
        {
            Size = _staticDataService.ForMap().Size;
        }
        public void Tick()
        {
            HandleMembers();
        }

        private void HandleMembers()
        {
            foreach (var member in _members)
            {
                TeleportIfOutsideArena(member.TransformData);
                member.transform.position = GetViewPosition(member.TransformData);
            }
        }

        private void TeleportIfOutsideArena(TransformData transformData)
        {
            if (transformData.Position.x < 0) transformData.Position.x += Size.x;
            else if (transformData.Position.x > Size.x) transformData.Position.x -= Size.x;
            
            if (transformData.Position.y < 0) transformData.Position.y += Size.y;
            else if (transformData.Position.y > Size.y) transformData.Position.y -= Size.y;
        }

        public void RegisterMember(IArenaMember arenaMember)
        {
            _members.Add(arenaMember);
        }

        private Vector2 GetViewPosition(TransformData data)
        {
            Vector2 res = new Vector2(data.Position.x, data.Position.y);
            
            if (Mathf.Abs(data.Position.x - _playerProvider.Player.TransformData.Position.x) > Extends.x)
            {
                if (res.x - Extends.x < 0)
                {
                    res.x += Size.x;
                }else if (res.x + Extends.x > Size.x)
                {
                    res.x -= Size.x;
                }
            }else if (Mathf.Abs(data.Position.y - _playerProvider.Player.TransformData.Position.y) > Extends.y)
            {
                if (res.y - Extends.y < 0)
                {
                    res.y += Size.y;
                }else if (res.y + Extends.y > Size.y)
                {
                    res.y -= Size.y;
                }
            }
            
            return res;
        }
    }
}