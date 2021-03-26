using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Actions : MonoBehaviour
{
    #region Variables
    [Header("Variables")]

    #region Player Detection

    [SerializeField]
    private float _checkRate = 0f;
    [SerializeField]
    private float _nextForPlayerCheck = 0f;
    [SerializeField]
    private float _detectionRadius = 50f;

    [SerializeField]
    private LayerMask _playerLayer;
    [SerializeField]
    private LayerMask _sightLayer;

    private RaycastHit hit;

    #endregion

    #region Wandering

    [SerializeField]
    private float _wanderRange = 10f; 
    //[SerializeField]
    //private float _wanderCheckRate = 5f;
    //[SerializeField]
    //private float _nextWanderCheck = 0f;
    private NavMeshHit _navHit;
    private Vector3 _wanderTarget;

    #endregion

    #region Attack

    [SerializeField]
    private float _attackDamage = 15f;
    [SerializeField]
    private float _attackRate = 1f;
    [SerializeField]
    private float _attackRange = 3.5f; 
    [SerializeField]
    private float _nextAttack = 0f; //licznik za pomoca ktorego sie sprawdza czy juz mozna bic

    #endregion

    #endregion

    #region References
    [Header("References")]

    [SerializeField]
    private Enemy_Master _enemyMaster = null;
    [SerializeField]
    private Transform _myHead = null;
    [SerializeField]
    private NavMeshAgent _agent = null;
    [SerializeField]
    private Transform _attackTarget = null;
    
    private Collider[] _hitColliders = null; //TODO naprawic detekcje ze œcianami i wywaliæ to

    #endregion

    private void OnEnable()
    {
        SetInitialReferences();
        
        _enemyMaster.EventEnemyDie += DestroyThis;
        _enemyMaster.EventEnemySetNavTarget += SetAttackTarget;
    }

    private void Update()
    {
        CarryOutDetection();
        TryPersureTarget();
        CheckIfDestinationReached();
        CheckIfShouldWander();
        TryToAttack();
    }

    private void OnDisable()
    {
        _enemyMaster.EventEnemyDie -= DestroyThis;
        _enemyMaster.EventEnemySetNavTarget -= SetAttackTarget;
    }

    private void SetAttackTarget(Transform target)
    {
        _attackTarget = target.root;
    }
    
    private void TryToAttack()
    {
        if (_attackTarget != null) //czy mamy cel
        {
            if (Time.time >= _nextAttack) //czy juz moze atakowac
            {
                _nextAttack = Time.time + _attackRate; //zawsze zerowaæ TODO sprobowaæ coœ z boolem

                if (Vector3.Distance(transform.position, _attackTarget.position) <= _attackRange) //czy w zasiegu
                {
                    Vector3 tmp = new Vector3(_attackTarget.position.x, transform.position.y, _attackTarget.position.z); //odwrocic zeby byl twarza do gracza
                    transform.LookAt(tmp);
                    _enemyMaster.CallEventEnemyAttack();
                    _enemyMaster.isOnRoute = false;
                }
            }
            
        }
    }

    public void OnAttack() //wywo³ywana przez animacje
    {
        if (_attackTarget != null) //czy mamy cel
        {
            if (Vector3.Distance(transform.position, _attackTarget.position) <= _attackRange && _attackTarget.GetComponent<Player_Master>() != null) //czy w zasiegu
            {
                Vector3 toOther = _attackTarget.position + transform.position;
                //Debug.Log(Vector3.Dot(toOther,transform.forward).ToString());

                //funkcja dot zwraca wartoœci od -1 do 1 (w teorii) i jak jest na minusie to drugi obiekt jest za tym obiektem
                if (Vector3.Dot(transform.forward, toOther) > 0.5f) //czy jest przed nim gracz
                {
                    _attackTarget.GetComponent<Player_Master>().CallEventPlayerTakeDamage(_attackDamage);
                }
            }
        }
    } 

    private void CarryOutDetection()
    {     
        if (Time.time >= _nextForPlayerCheck)
        {
            _nextForPlayerCheck = Time.time + _checkRate;
        
            _hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _playerLayer);

            if (_hitColliders.Length > 0)
            {
                _enemyMaster.CallEventEnemySetNavTarget(_hitColliders[0].transform);

                //_agent.SetDestination(_hitColliders[0].transform.position);
                //_enemyMaster.CallEventEnemyWalking();
            }

            

            /*
            Collider[] colliders = Physics.OverlapSphere(transform.position, _detectRadius, _playerLayer);


            
            if (colliders.Length > 0) 
            {
                foreach(Collider potencialTargetCollider in colliders)
                {
                    if (potencialTargetCollider.CompareTag("Player"))
                    {
                        if (CanPotencialTargetBeSeen(potencialTargetCollider.transform))
                        {
                            break;
                        }
                    }
                }
            }
            else
            {
                _enemyMaster.CallEventEnemyLostTarget();
            }  
            
            */
        }
    }

    private void CheckIfDestinationReached()
    {
        if (_enemyMaster.isOnRoute)
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _enemyMaster.isOnRoute = false;
                _enemyMaster.CallEventEnemyReachedNavTarget();
            }
        }
    }

    //wprawia w ruch
    private void TryPersureTarget()
    {
        if (_enemyMaster.myTarget != null && _agent != null && !_enemyMaster.isNavPaused)
        {
            _agent.SetDestination(_enemyMaster.myTarget.position);

            if (_agent.remainingDistance > _agent.stoppingDistance)
            {
                _enemyMaster.CallEventEnemyWalking();
                _enemyMaster.isOnRoute = true;
            }
        }
    }

    //sprawdzenie czy gracz widoczny -> a nie za œcian¹
    private bool CanPotencialTargetBeSeen(Transform potencialTarget)
    {       
        if (Physics.Raycast(_myHead.position, potencialTarget.position, out hit, _sightLayer))
        {
            if (hit.transform == potencialTarget)
            {
                Debug.Log("calling");
                _enemyMaster.CallEventEnemySetNavTarget(potencialTarget);
                return true;
            }
            else
            {
                Debug.Log("NOTcalling");
                _enemyMaster.CallEventEnemyLostTarget();
                return false;
            }
        }
        else
        {
            Debug.Log("FFFFCK");
            _enemyMaster.CallEventEnemyLostTarget();
            return false;
        }        
    }

    //jak nie ma nic do roboty to niech se pochodzi
    private void CheckIfShouldWander()
    {
        if (_enemyMaster.myTarget == null && !_enemyMaster.isOnRoute && !_enemyMaster.isNavPaused)
        {
            if (RandomWanderTarget())
            {
                _agent.SetDestination(_wanderTarget);
                _enemyMaster.isOnRoute = true;
                _enemyMaster.CallEventEnemyWalking();
            }
        }
    }

    private bool RandomWanderTarget()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * _wanderRange;

        //sprawdz czy moze tam isc
        if (NavMesh.SamplePosition(randomPoint, out _navHit, 1f, NavMesh.AllAreas)) 
        {          
            _wanderTarget = _navHit.position;
            return true;
        }
        else
        {
            _wanderTarget = transform.position;
            return false;
        }              
    }

    private void SetInitialReferences()
    {
        _enemyMaster = GetComponent<Enemy_Master>();
        _checkRate = Random.Range(0.8f, 1.2f);
        _agent = GetComponent<NavMeshAgent>();

        if (_myHead == null) 
        {
            _myHead = transform;
        }
    }

    private void DestroyThis()
    {
        this.enabled = false;
    }
}
