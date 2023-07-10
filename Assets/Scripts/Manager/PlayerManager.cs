using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;

public class PlayerManager : SingletonBase<PlayerManager>
{
    [SerializeField] private float timePerCoinsAdded = 1.0f;

    [SerializeField] private List<Yard> yards;
    
    private float _coins;
    public float GetCoins() => _coins;
    public Action<float> OnCoinsChanged = delegate(float f) {  };

    private float _coinsForTickets = 10.0f;
    public float GetCoinsForTickets() => _coinsForTickets;
    public Action<float> OnCoinsForTicketsChanged = delegate(float f) {  };

    [SerializeField] private float _ticketPrice = 0;
    [SerializeField] private float _ticketPriceMultiplier = 0.1f;
    public float GetTicketPrice() => _ticketPrice;

    private void Start()
    {
        _ticketPrice = 0;
        
        UpdateTicketPrice();
    }

    private IEnumerator UpdateCoinsForTickets()
    {
        while (true)
        {
            yield return new WaitForSeconds(timePerCoinsAdded);
            AddCoinsForTickets(_ticketPrice);
        }
    }
    
    public void AddCoins(float value)
    {
        _coins += value;
        OnCoinsChanged?.Invoke(_coins);
    }

    public void AddCoinsForTickets(float value)
    {
        _coinsForTickets += value;
        OnCoinsForTicketsChanged?.Invoke(_coinsForTickets);
    }

    public void UpdateTicketPrice()
    {
        _ticketPrice = 0;
        
        yards.ForEach(yard =>
        {
            _ticketPrice += yard.GetCurrentTicketPrice();
        });
        
        print(_ticketPrice);
    }

    public void CollectCoins()
    {
        AddCoins(_coinsForTickets);

        _coinsForTickets = 0;
        OnCoinsForTicketsChanged?.Invoke(_coinsForTickets);
    }

    public void SubtractCoins(float value)
    {
        _coins -= value;
        OnCoinsChanged?.Invoke(_coins);
    }
}
