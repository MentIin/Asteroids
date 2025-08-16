using CodeBase.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _invulnerabilityEffect;
    
        public void UpdateView(TransformData transformData)
        {
            transform.position = new Vector3(transformData.Position.x, transformData.Position.x, 0f);
            transform.rotation = Quaternion.Euler(0, 0, transformData.Rotation);
        }

        public void SetInvulnerabilityEffect(bool isActive)
        {
            if (isActive && !_invulnerabilityEffect.isPlaying)
            {
                _invulnerabilityEffect.Play();
            }
            else if (!isActive && _invulnerabilityEffect.isPlaying)
            {
                _invulnerabilityEffect.Stop();
            }
        }
    }
}