using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChasePlayer : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private LayerMask _playerLayer; 
    [SerializeField]
    private float _checkRate = 0f;
    [SerializeField]
    private float _nextCheck = 0f;
    [SerializeField]
    private float _detectionRadius = 15f;

    private Collider[] _hitColliders = null;

    [SerializeField]
    private NavMeshAgent _agent = null;

    public float DetectionRadius
    {
        get => _detectionRadius;
        set => value = _detectionRadius;
    }


    // Start is called before the first frame update
    void Start()
    {
        SetInitialReferences();
        StartCoroutine(ChaseDelay());
    }

    IEnumerator ChaseDelay()
    {
        float tmp = _detectionRadius;
        _detectionRadius = 1f;
        yield return new WaitForSeconds(0.5f);
        _detectionRadius = tmp;
    }
    private void SetInitialReferences()
    {
        _agent = GetComponent<NavMeshAgent>();
        _checkRate = Random.Range(0.8f, 1.2f); //zeby wszyscy przeciwnicy nie sprawdzali w tym samym czasie -> rip fps
    }

    void CheckForPlayer()
    {
        
        if (Time.time > _nextCheck && _agent.enabled)
        {
            _nextCheck = Time.time + _checkRate;
            _hitColliders = Physics.OverlapSphere(transform.position, _detectionRadius, _playerLayer);

            if (_hitColliders.Length > 0)
            {
                _agent.SetDestination(_hitColliders[0].transform.position);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckForPlayer();
    }
}
