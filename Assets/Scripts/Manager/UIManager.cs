using System;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace Manager
{
    public class UIManager : SingletonBase<UIManager>
    {
        [Header("Yard")]
        [SerializeField] private YardPanel yardShopPanel;

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

            yardShopPanel.yardCostText.text = yard.GetCurrentPrice().ToString("0");
            yardShopPanel.yardIcon.sprite = yard.GetYardIcon();
            _currentYard.OnCurrentPriceChanged += OnYardCurrentPriceChanged;
            _currentYard.OnCurrentTicketPriceChanged += OnCurrentYardTicketPriceChanged;
            yardShopPanel.ticketCostText.text = yard.GetCurrentTicketPrice().ToString("0");

            yardShopPanel.gameObject.SetActive(true);
            yardShopPanel.UpdateLevelOnYard(yard);
        }

        public void CloseYardPanel()
        {
            _currentYard.OnCurrentPriceChanged -= OnYardCurrentPriceChanged;
            _currentYard.OnCurrentTicketPriceChanged -= OnCurrentYardTicketPriceChanged;
            yardShopPanel.gameObject.SetActive(false);
        }

        public void TryUpdateYard()
        {
            if (!_currentYard) return;
        
            _currentYard.UpdateYard();
            yardShopPanel.UpdateLevelOnYard(_currentYard);
        }

        private void OnYardCurrentPriceChanged(float value)
        {
            yardShopPanel.yardCostText.text = value.ToString("0");
        }

        private void OnCurrentYardTicketPriceChanged(float value)
        {
            yardShopPanel.ticketCostText.text = value.ToString("0");
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
