using UnityEngine;

namespace Resources.Scripts.Player
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Boss.Boss _boss;
        
        private int _strength;

        private void Start()
        {
            _strength = _player.GetStrength();
        }
    }
}