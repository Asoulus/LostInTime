using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _damage = 50f;
    [SerializeField]
    private float _range = 25f;
    [SerializeField]
    private float _projectileSpeed = 100f;
    [SerializeField]
    private float _destroyDelay = 5f;
    [SerializeField]
    private float _fireRate = 0.5f;
    [SerializeField]
    private bool _isHitscan = false;

    private bool _canFire = true;


    
    [Header("References")]
    [SerializeField]
    private GameObject _projectilePrefab = null;
    [SerializeField]
    private GameObject _firePoint = null;
    [SerializeField]
    private Camera _playerCamera = null; 
    [SerializeField]
    private ParticleSystem _hitEffect = null;    
    [SerializeField]
    private ParticleSystem _muzzleFlash = null; 

    void Start()
    {
        SetInitialReferences();       
    }

    private void SetInitialReferences()
    {
        _projectilePrefab.GetComponent<Projectile>().damage = _damage;
        //TODO zrobic referencje do kamery w innym skrypcie i przypisac tutaj

    }

    private void OnEnable()
    {
        //TODO subskrypcja do input_managera
    }

    private IEnumerator Shoot()
    {
        _muzzleFlash.Play();
        _canFire = false;

        if (_isHitscan)
        {
            RaycastHit hit;

            Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _range);

            if (hit.collider != null)
            {
                BodyPart tmpBodyPart = hit.collider.GetComponent<BodyPart>();

                if (tmpBodyPart != null)
                {
                    tmpBodyPart.TakeDamage(_damage);
                    ParticleSystem tmpEffect = Instantiate(_hitEffect, hit.point, transform.rotation);
                    Destroy(tmpEffect.gameObject, 2f);
                }
            }
                    
        }
        else
        {
            SpawnProjectile();
        }

        yield return new WaitForSeconds(_fireRate);
        _canFire = true;
    }

    private void SpawnProjectile()
    {
        GameObject tmpGO = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation);
        tmpGO.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);
        Destroy(tmpGO, _destroyDelay);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && _canFire)
        {
            StartCoroutine(Shoot());
        }
    }

    private void OnDisable()
    {
        //TODO Unsubskrypcja z input_managera
    }
}
