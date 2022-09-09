using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts.Player
{
    public class PlayerHealthBarView : MonoBehaviour
    {
        [SerializeField] private Player _player;
        
        [SerializeField] private Image _lineSpentHealth;
        [SerializeField] private Image _healthLine;
        [SerializeField] private Text _health;

        private void Awake()
        {
            _player.HealthChanged += OnHealthUpdated;
        }

        private void OnHealthUpdated(int health)
        {
            if (health <= 0)
                gameObject.SetActive(false);

            var normalizedHealth = health / (float) _player.InitialHealth;
            _healthLine.fillAmount = normalizedHealth;
            _lineSpentHealth.DOFillAmount(normalizedHealth, 0.5f);
            _health.text = health.ToString();
        }
    }
}
