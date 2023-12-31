using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Customer : MonoBehaviour
{
    [SerializeField] private float minimalDistToShop = 50.0f;
    [SerializeField] private float minimalDistToEndPoint = 10.0f;
    [SerializeField] private float timeWaitingInShop = 5.0f;
    
    private NavMeshAgent _agent;
    private AIManager _aiManager;
    private Animator _animator;

    private float _distanceToShop;
    private float _distanceToEndPoint;
    
    private static readonly int IsMoving = Animator.StringToHash("IsMoving");

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _aiManager = AIManager.Instance;
        _animator = GetComponentInChildren<Animator>();

        StartCoroutine(GoToShop());
    }

    private void Update()
    {
        _distanceToShop = Vector3.Distance(transform.position, _aiManager.GetShopPosition().position);
        _distanceToEndPoint = Vector3.Distance(transform.position, _aiManager.GetEndPosition().position);
    }

    private IEnumerator GoToShop()
    {
        yield return new WaitForSeconds(0.2f);
        
        _agent.SetDestination(_aiManager.GetShopPosition().position);
        _animator.SetBool(IsMoving, true);
        yield return new WaitWhile(() => _distanceToShop >= minimalDistToShop);
        _animator.SetBool(IsMoving, false);
        yield return new WaitForSeconds(timeWaitingInShop);
        StartCoroutine(GoToEndPoint());
    }

    private IEnumerator GoToEndPoint()
    {
        PlayerManager playerManager = PlayerManager.Instance;
        playerManager.AddCoinsForTickets(playerManager.GetTicketPrice());
        
        _agent.SetDestination(_aiManager.GetEndPosition().position);
        _animator.SetBool(IsMoving, true);
        print("Going to endPoint");
        print(_distanceToShop);

        yield return new WaitWhile(() => _distanceToEndPoint > minimalDistToEndPoint);
        Destroy(gameObject);
    }
}
