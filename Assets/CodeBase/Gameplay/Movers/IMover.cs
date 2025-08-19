using UnityEngine;

namespace CodeBase.Gameplay.Movers
{
    public interface IMover
    {
        public void Tick(Vector2 directionAxis, float deltaTime);
    }
}