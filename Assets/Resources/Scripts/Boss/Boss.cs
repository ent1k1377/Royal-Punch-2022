using Resources.Scripts.Storage;
using UnityEngine;

namespace Resources.Scripts.Boss
{
    public class Boss : MonoBehaviour
    {
        private Storage.Storage _storage;
        private BossData _bossData;

        public int Health => _bossData.Health;
        public int Strength => _bossData.Strength;
        
        private void Awake()
        {
            _storage = new Storage.Storage();
            _bossData = _storage.Load<BossData>();   
        }
    }
}