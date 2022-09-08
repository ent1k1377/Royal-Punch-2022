using Resources.Scripts.Storage;
using UnityEngine;

namespace Resources.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private Storage.Storage _storage;
        private PlayerData _playerData;
        
        public int GetHealth() => _playerData.Health;
        public int GetStrength() => _playerData.Strength;
        
        private void Start()
        {
            _storage = new Storage.Storage();
            _playerData = _storage.Load<PlayerData>();
        }
    }
}
