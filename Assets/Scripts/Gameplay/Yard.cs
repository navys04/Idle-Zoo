using System;
using System.Collections;
using System.Collections.Generic;
using Manager;
using UnityEngine;
using UnityEngine.EventSystems;

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
        [SerializeField] private int _level = 0;

        [Header("UI")] 
        [SerializeField] private Sprite yardIcon;
        [SerializeField] private string yardTitle;

        [Header("Other")] 
        [SerializeField] private Collider collider;

        private GameObject _currentYard;

        public Collider GetCollider() => collider;
        
        public int GetLevel() => _level;
        
        public float GetCurrentPrice() => _currentPrice;
        public float GetCurrentTicketPrice() => _ticketPrice;
        public Sprite GetYardIcon() => yardIcon;
        public string GetYardTitle() => yardTitle;

        public Action<float> OnCurrentPriceChanged = delegate(float f) {  };
        public Action<float> OnCurrentTicketPriceChanged = delegate(float f) {  };

        public List<Animal> GetAnimals() => animals;

        private int _currentAnimalIndex;
        public int GetCurrentAnimalIndex() => _currentAnimalIndex;
        
        private void Start()
        {
            if (_level > 0) SpawnNewAnimalsInYard(_level);
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            UIManager.Instance.OpenYardPanel(this);
        }

        public void UpdateYard()
        {
            if (_level == 0)
            {
                _currentPrice = basePrice;
                _ticketPrice = baseTicketPrice;
                
                OnCurrentPriceChanged?.Invoke(_currentPrice);
                OnCurrentTicketPriceChanged?.Invoke(_ticketPrice);
                PlayerManager.Instance.UpdateTicketPrice();
                
                _level++;
                SpawnNewAnimalsInYard(_level);
                return;
            }
            
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

            int curAnimalLevel = 0;

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
            _currentYard.GetComponent<Animal>().parentYard = this;
            
            _currentAnimalIndex = animals.IndexOf(newAnimal);
            print(_currentAnimalIndex);
        }

        public Animal GetAnimalByLevel(int level)
        {
            return animals.Find(animal => animal.level == level);
        }
        
        public int GetNextLevel()
        {
            int currentAnimalIndex = GetCurrentAnimalIndex();
            int nextAnimalIndex = currentAnimalIndex + 1;
            if (GetAnimals().Count > nextAnimalIndex)
            {
                return GetAnimals()[nextAnimalIndex].level;
            }
        
            print("Animals.Count = " + GetAnimals().Count);
        
            return -1;
        }
    }
}