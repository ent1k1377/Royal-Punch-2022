using Resources.Scripts.Storage;
using UnityEngine;
using UnityEngine.UI;

namespace Resources.Scripts
{
    public class MainMenuView : MonoBehaviour
    {
        [SerializeField] private Button _health;
        [SerializeField] private Image _healthImage;
        [SerializeField] private Text _levelHealth;
        [SerializeField] private Text _costHealth;
        
        [SerializeField] private Button _strength;
        [SerializeField] private Image _strengthImage;
        [SerializeField] private Text _levelStrength;
        [SerializeField] private Text _costStrength;

        [SerializeField] private Text _playerLevel;
        [SerializeField] private Text _numberCoins;
        
        [SerializeField] private Color _disabledButton;

        private readonly Color _activabledButton = new(255, 255, 255, 0);
        
        private Storage.Storage _storage;
        private BustersData _bustersData;
        private PlayerData _playerData;

        private bool _isCanBuyHealthBuster => _playerData.NumberCoins >= _bustersData.CostHealth;
        private bool _isCanBuyStrengthBuster => _playerData.NumberCoins >= _bustersData.CostStrength;

        private void Start()
        {
            _storage = new Storage.Storage();
            _bustersData = _storage.Load<BustersData>();
            _playerData = _storage.Load<PlayerData>();
            UpdateButtons();
        }

        private void Load()
        {
            _levelHealth.text = $"LV. {_bustersData.LevelHealth}";
            _costHealth.text = _bustersData.CostHealth.ToString();
            _levelStrength.text = $"LV. {_bustersData.LevelStrength}";
            _costStrength.text = _bustersData.CostStrength.ToString();
            
            _playerLevel.text = $"LEVEL {_playerData.Level}";
            _numberCoins.text = _playerData.NumberCoins.ToString();
        }

        public void BuyHealthBuster()
        {
            if (_isCanBuyHealthBuster)
            {
                _playerData.NumberCoins -= _bustersData.CostHealth;
                _bustersData.LevelHealth += 1;
                _bustersData.CostHealth = (int) (_bustersData.CostHealth * _bustersData.CostIncreaseMultiplierHealth);
                
                _storage.Save(_bustersData);
                _storage.Save(_playerData);
            }
                
            UpdateButtons();
        }
        
        public void BuyStrengthBuster()
        {
            if (_isCanBuyStrengthBuster)
            {
                _playerData.NumberCoins -= _bustersData.CostStrength;
                _bustersData.LevelStrength += 1;
                _bustersData.CostStrength = (int) (_bustersData.CostStrength * _bustersData.CostIncreaseMultiplierStrength);
                
                _storage.Save(_bustersData);
                _storage.Save(_playerData);
            }
                
            UpdateButtons();
        }

        private void UpdateButtons()
        {
            if (_isCanBuyHealthBuster)
                UpdateButton(_health, _healthImage, true, _activabledButton);
            else
                UpdateButton(_health, _healthImage, false, _disabledButton);

            if (_isCanBuyStrengthBuster)
                UpdateButton(_strength, _strengthImage, true, _activabledButton);
            else
                UpdateButton(_strength, _strengthImage, false, _disabledButton);
            Load();
        }

        private static void UpdateButton(Button button, Image image, bool isInteractable, Color color)
        {
            button.interactable = isInteractable;
            image.color = color;
        }
    }
}
