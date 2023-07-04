using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Gameplay
{
    public class Yard : MonoBehaviour
    {
        [Header("Price")]
        [SerializeField] private float basePrice = 10.0f;
        [SerializeField] private float currentPriceMultiplier = 0.2f;

        [Header("TicketPrice")] 
        [SerializeField] private float baseTicketPrice = 10.0f;
        [SerializeField] private float ticketPriceMultiplier = 0.2f;
        private float _ticketPrice;
        
        [Header("Yard Settings")]
        [SerializeField] private List<Animal> animals = new List<Animal>();
        
        private float _currentPrice;
        [SerializeField] private int _level = 1;

        [Header("UI")] 
        [SerializeField] private Sprite yardIcon;

        private GameObject _currentYard;

        public int GetLevel() => _level;
        
        public float GetCurrentPrice() => _currentPrice;
        public float GetCurrentTicketPrice() => _ticketPrice;
        public Sprite GetYardIcon() => yardIcon;

        public Action<float> OnCurrentPriceChanged = delegate(float f) {  };
        public Action<float> OnCurrentTicketPriceChanged = delegate(float f) {  };

        public List<Animal> GetAnimals() => animals;

        private int _currentAnimalIndex;
        public int GetCurrentAnimalIndex() => _currentAnimalIndex;
        
        private void Start()
        {
            _currentPrice = basePrice;
            _ticketPrice = baseTicketPrice;
            
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

            _ticketPrice += (_ticketPrice * ticketPriceMultiplier);
            OnCurrentTicketPriceChanged?.Invoke(_ticketPrice);
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

            _currentAnimalIndex = animals.IndexOf(newAnimal);
            print(_currentAnimalIndex);
        }

        public Animal GetAnimalByLevel(int level)
        {
            return animals.Find(animal => animal.level == level);
        }
    }
}