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

        private Yard _currentYard;

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
    }
}
