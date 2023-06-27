using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : SingletonBase<PlayerManager>
{
    [SerializeField] private float timePerCoinsAdded = 1.0f;
    
    private float _coins;
    public float GetCoins() => _coins;
    public Action<float> OnCoinsChanged = delegate(float f) {  };

    private float _coinsForTickets = 10.0f;
    public float GetCoinsForTickets() => _coinsForTickets;
    public Action<float> OnCoinsForTicketsChanged = delegate(float f) {  };

    [SerializeField] private float _ticketPrice = 10.0f;
    [SerializeField] private float _ticketPriceMultiplier = 0.1f;
    public float GetTicketPrice() => _ticketPrice;

    private void Start()
    {
        StartCoroutine(UpdateCoinsForTickets());
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
        _ticketPrice += _ticketPrice * _ticketPriceMultiplier;
    }

    public void CollectCoins()
    {
        AddCoins(_coinsForTickets);

        _coinsForTickets = 0;
        OnCoinsForTicketsChanged?.Invoke(_coinsForTickets);
    }
}
