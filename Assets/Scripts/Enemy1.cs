using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Repaso_examen : MonoBehaviour
{
    private NavMeshAgent _agent;

    public enum EnemyState
    {
        Patrolling,
        Searching,
        Chasing,
        Waiting,
        Attacking
    }

    public EnemyState currentState;

    [SerializeField] private Transform[]_patrolPoints;
    private int _patrolIndex;
    private Transform _playerTransform;
    [SerializeField] private float _visionRange = 15;
    [SerializeField] private float _attackRange = 3;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.FindWithTag("Player").transform;
    }

    // Hay que añadir el adon de probuilder, y tambien añadir a la IA el navMesch Agent y crear un empty para colocar el navMesch surface, para que sale encima de las plataformas hay que tocarlo en el apartado del navmesh y poner un navMesch link para que pueda saltar a la zona que queremos o bajar.
    void Start()
    {
        currentState = EnemyState.Patrolling;
        SetPatrolPoint();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            
            case EnemyState.Patrolling:
                Patrol();
            break;
            case EnemyState.Searching:
                Search();
            break;
            case EnemyState.Chasing:
                Chase();
            break;
            case EnemyState.Waiting:
                Wait();
            break;
            case EnemyState.Attacking:
                Attack();
            break;
        }

        /*if(currentState == EnemyState.Patrolling)
        {
            Patrol();
        }
        else if(currentState == EnemyState.Searching)    (esto se puede utilizar remplazando a lo de arriab)
        {
            Search();
        }*/
    }

    void Patrol()
    {
        /*if(Vector3.Distance(transform.position, _playerTransform.position) < _visionRange)
        {
            currentState = EnemyState.Chasing;
        }*/
        if(InRange(_visionRange))
        {
            currentState = EnemyState.Chasing;
        }
        
        if(_agent.remainingDistance < 0.5f)
        {
            SetPatrolPoint();
        }
    }

    void SetPatrolPoint()
    {
        _agent.destination = _patrolPoints[Random.Range(0, _patrolPoints.Length)].position; //busca un punto aleatorio
    }

    void Search()
    {

    }

    void Chase()
    {
        /*if(Vector3.Distance(transform.position, _playerTransform.position) > _visionRange)
        {
            currentState = EnemyState.Patrolling;
        }*/

        if(!InRange(_visionRange))
        {
            currentState = EnemyState.Chasing;
        }

        if(InRange(_attackRange))
        {
            currentState = EnemyState.Attacking;
        }

        _agent.destination = _playerTransform.position;
    }

    bool InRange(float range)
    {
        return Vector3.Distance(transform.position, _playerTransform.position) < range;
    }

    void Wait()
    {

    }

    void Attack()
    {
        Debug.Log("atacando");

        currentState = EnemyState.Searching;
    }
}
