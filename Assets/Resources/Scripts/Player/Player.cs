using Resources.Scripts.Storage;
using UnityEngine;
using UnityEngine.Events;

namespace Resources.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private Storage.Storage _storage;
        private PlayerData _playerData;

        private int _initialHealth;
        private int _health;
        private int _strength;

        public int InitialHealth => _initialHealth;
        public int Health => _health;
        public int Strength => _strength;
        
        public event UnityAction<int> HealthChanged;
        
        private void Awake()
        {
            _storage = new Storage.Storage();
            _playerData = _storage.Load<PlayerData>();
            _health = _playerData.Health;
            _strength = _playerData.Strength;
            _initialHealth = _playerData.Health;
        }

        public void TakeDamage(int damage)
        {
            if (_health - damage <= 0)
                Die();
            _health -= damage;
            HealthChanged?.Invoke(_health);
        }

        private void Die()
        {
            
        }
    }
}
