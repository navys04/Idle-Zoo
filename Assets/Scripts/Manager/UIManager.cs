using System;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : SingletonBase<UIManager>
    {
        [Header("Yard")]
        [SerializeField] private GameObject yardShopPanel;
        [SerializeField] private Button yardButton;
        [SerializeField] private Text yardCostText;

        [SerializeField] private Text coinsText;
        [SerializeField] private Text coinsForTicketsText;

        private Yard _currentYard;

        private void OnEnable()
        {
            PlayerManager playerManager = PlayerManager.Instance;
            playerManager.OnCoinsChanged += OnCoinsChanged;
            playerManager.OnCoinsForTicketsChanged += OnCoinsForTicketsChanged;
        }

        private void OnDisable()
        {
            PlayerManager playerManager = PlayerManager.Instance;
            playerManager.OnCoinsChanged -= OnCoinsChanged;
            playerManager.OnCoinsForTicketsChanged -= OnCoinsForTicketsChanged;
        }

        public void OpenYardPanel(Yard yard)
        {
            _currentYard = yard;

            yardCostText.text = yard.GetCurrentPrice().ToString("0");
            _currentYard.OnCurrentPriceChanged += OnYardCurrentPriceChanged;
            
            yardShopPanel.SetActive(true);
        }

        public void CloseYardPanel()
        {
            _currentYard.OnCurrentPriceChanged -= OnYardCurrentPriceChanged;
            yardShopPanel.SetActive(false);
        }

        public void TryUpdateYard()
        {
            if (!_currentYard) return;
        
            _currentYard.UpdateYard();
        }

        private void OnYardCurrentPriceChanged(float value)
        {
            yardCostText.text = value.ToString("0");
        }

        private void OnCoinsChanged(float value)
        {
            coinsText.text = value.ToString("0");
        }
        
        private void OnCoinsForTicketsChanged(float value)
        {
            coinsForTicketsText.text = value.ToString("0");
        }
    }
}
