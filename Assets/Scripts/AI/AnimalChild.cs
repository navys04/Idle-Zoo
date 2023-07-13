using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(NavMeshAgent))]
public class AnimalChild : MonoBehaviour
{
    /** List of state names for animals
     * Animations will be played when animal stops in any point
     */
    [SerializeField] private List<string> stateNames = new List<string>();

    [SerializeField] private float timeBetweenMoving = 5.0f;

    private Collider _collider;
    private Animator _animator;
    private NavMeshAgent _agent;

    private Vector3 _currentDestinationPoint;
    private float _currentDistanceToDestPoint;
    private static readonly int IsMovingState = Animator.StringToHash("isMoving");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _collider = GetComponentInParent<Animal>().parentYard.GetCollider();
        
        MoveToNextState();
    }

    private void Update()
    {
        if (_currentDestinationPoint == Vector3.zero) return;
        _currentDistanceToDestPoint = Vector3.Distance(transform.position, _currentDestinationPoint);
    }

    /** Gets random point and moves AI to this point */ 
    private void MoveToNextState()
    {
        Vector3 randomPoint = GetRandomPointInBounds(_collider);
        randomPoint.y = transform.position.y;
        _agent.SetDestination(randomPoint);

        StartCoroutine(WaitForStateEnds());
    }

    private IEnumerator WaitForStateEnds()
    {
        _animator.SetBool(IsMovingState, true);
        yield return new WaitWhile(() => _currentDistanceToDestPoint > 1);
        _animator.SetBool(IsMovingState, false);
        _currentDestinationPoint = Vector3.zero;
        PlayRandomState();
        yield return new WaitForSeconds(timeBetweenMoving);
        MoveToNextState();
    }

    private void PlayRandomState()
    {
        if (stateNames.Count == 0) return; 
        int randStateInd = Random.Range(0, stateNames.Count);

        string state = "Base Layer." + stateNames[randStateInd];
        _animator.Play(state);
    }
    
    /** Returns random point in bounds */
    private Vector3 GetRandomPointInBounds(Collider collider)
    {
        float x = Random.Range(collider.bounds.min.x, collider.bounds.max.x);
        float y = Random.Range(collider.bounds.min.y, collider.bounds.max.y);
        float z = Random.Range(collider.bounds.min.z, collider.bounds.max.z);

        return new Vector3(x, y, z);
    }
}
