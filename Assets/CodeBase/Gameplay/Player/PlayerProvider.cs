namespace CodeBase.Gameplay.Player
{
    public class PlayerProvider
    {
        private Player _player;

        public Player Player => _player;
        public bool PlayerExists => _player != null;
        
        public void RegisterPlayer(Player player)
        {
            _player = player;
        }

        public void UnregisterPlayer()
        {
            _player = null;
        }
    }
}