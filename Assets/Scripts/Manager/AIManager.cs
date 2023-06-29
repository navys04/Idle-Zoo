using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : SingletonBase<AIManager>
{
    [SerializeField] private Transform startPosition;
    [SerializeField] private Transform shopPosition;
    [SerializeField] private Transform endPosition;
    [SerializeField] private float timeBetweenSpawn;
    [SerializeField] private Customer customerPrefab;

    private List<Customer> _spawnedCustomers = new List<Customer>();
    private bool _isGameStarted = true;

    public Transform GetShopPosition() => shopPosition;
    public Transform GetEndPosition() => endPosition;

    private void Start()
    {
        StartCoroutine(CustomerSpawn());
    }

    private IEnumerator CustomerSpawn()
    {
        while (_isGameStarted)
        {
            yield return new WaitForSeconds(timeBetweenSpawn);
            SpawnNewCustomer();
        }
    }

    private void CheckCustomers()
    {
        for (int i = 0; i < _spawnedCustomers.Count; i++)
        {
            if (_spawnedCustomers[i] == null) _spawnedCustomers.RemoveAt(i);
        }
    }
    
    private void SpawnNewCustomer()
    {
        Customer newCustomer = Instantiate(customerPrefab, startPosition.position, startPosition.rotation, transform);
    //    CheckCustomers();
        _spawnedCustomers.Add(newCustomer);
    }
}
