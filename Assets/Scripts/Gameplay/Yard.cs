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
        [SerializeField] private List<Animal> animals = new List<Animal>();
    
        private float _currentPrice;
        [SerializeField] private int _level = 1;

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
            
            PlayerManager.Instance.UpdateTicketPrice();
            
            _level++;
            SpawnNewAnimalsInYard(_level);
        }

        private void SpawnNewAnimalsInYard(int level)
        {
           // if (level > animals.Count) return;

            int curAnimalLevel;

            Animal newAnimal = GetAnimalByLevel(level);
            if (_currentYard)
            {
                Animal animal = _currentYard.GetComponent<Animal>();
                curAnimalLevel = animal.level;
                
                if (newAnimal == null) return;
                
                Destroy(_currentYard);
            }
            
            if (!newAnimal) return;
            _currentYard = Instantiate(newAnimal.gameObject, transform.position, transform.rotation, transform);
        }

        public Animal GetAnimalByLevel(int level)
        {
            foreach (var zoo in animals)
            {
                if (zoo.level == level) return zoo;
            }

            return null;
        }
    }
}