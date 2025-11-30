using UnityEngine;
using UnityEngine.AI;

public class EnemyPatrolFollow : MonoBehaviour
{
    public Transform patrolPointA; //StartPatrolPoint
    public Transform patrolPointB; //EndPatrolPoint
    public float followPlayerRange = 6f; //Distance required for enemy to start following player, and stop following when left.

    private GameObject _player;
    private NavMeshAgent _agent; 
    private Transform _currentTarget; //Current Patrol Point to go to.
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>(); //Getting navmeshagent from the enemy (done in awake to avoid sync issues)
    }

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player"); //Get player using Tag
        _currentTarget = patrolPointA; //Set Start Patrol Point
        _agent.SetDestination(_currentTarget.position); // start moving towards the current destination.
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position); //caluclating distance between enemy and player.

        if (distanceToPlayer <= followPlayerRange) //if Player is within follow range start following player
        {
            _agent.SetDestination(_player.transform.position);
        }
        else //otherwise go back to patrolling
        {
            patrolTwoPoints();
        }
    }

    private void patrolTwoPoints() //
    {
        if(!_agent.pathPending && _agent.remainingDistance < 0.5f) //Making sure the path has been computed and the agent is close to the patrol point(this is because agents may not land on the exact points sometimes)
        {
            _currentTarget = (_currentTarget == patrolPointA ? patrolPointB : patrolPointA);
            _agent.SetDestination(_currentTarget.position);
        }
    }
}
