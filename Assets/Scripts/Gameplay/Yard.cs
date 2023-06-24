using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Gameplay
{
    public class Yard : MonoBehaviour
    {
        [SerializeField] private float basePrice = 10.0f;
        [SerializeField] private float currentPriceMultiplier = 0.2f;
        [SerializeField] private List<GameObject> animals = new List<GameObject>();
    
        private float _currentPrice;
        private int _level = 1;

        private GameObject _currentYard;

        public float GetCurrentPrice() => _currentPrice;

        public Action<float> OnCurrentPriceChanged = delegate(float f) {  };

        private void Start()
        {
            _currentPrice = basePrice;
            
            SpawnNewAnimalsInYard(_level);
        }

        private void OnMouseDown()
        {
            UIManager.Instance.OpenYardPanel(this);
        }

        public void UpdateYard()
        {
            _currentPrice += _currentPrice * currentPriceMultiplier;
            OnCurrentPriceChanged?.Invoke(_currentPrice);
            
            _level++;
            SpawnNewAnimalsInYard(_level);
        }

        private void SpawnNewAnimalsInYard(int level)
        {
            if (level > animals.Count) return;

            if (_currentYard)
            {
                Destroy(_currentYard);
            }

            _currentYard = Instantiate(animals[level - 1], transform.position, transform.rotation, transform);
        }
    }
}