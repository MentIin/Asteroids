namespace CodeBase.Gameplay.Services.Providers
{
    public class PlayerProvider
    {
        private Player.Player _player;

        public Player.Player Player => _player;
        public bool PlayerExists => _player != null;
        
        public void RegisterPlayer(Player.Player player)
        {
            _player = player;
        }

        public void UnregisterPlayer()
        {
            _player = null;
        }
    }
}