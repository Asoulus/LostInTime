using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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
    private float _rateOfFire = 0.5f;
    [SerializeField]
    private bool _isHitscan = false;
    [SerializeField]
    private int _clipSize = 0;
    [SerializeField]
    private int _currentClip = 0;
    [SerializeField]
    private int _reserveSize = 0;
    [SerializeField]
    private int _currentReserve = 0;
    [SerializeField]
    private bool _hasCrosshair = true;
    [SerializeField]
    private bool _isMelee = false;
    [SerializeField]
    private bool _isShotgun = false;
    [SerializeField]
    private bool _isCrossbow = false;

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
    private Sprite _weaponUIImage = null;
    [SerializeField]
    private Animator _myAnimator;
    [SerializeField]
    private string _idleAnimationName = string.Empty;
    [SerializeField]
    private string _idleSecondAnimationName = string.Empty;

    public bool IsMelee
    {
        get => _isMelee;
    }

    public bool HasCrosshair
    {
        get => _hasCrosshair;
    }

    public int CurrentClip
    {
        get => _currentClip;
    }

    public int ClipSize
    {
        get => _clipSize;
    }

    public int CurrentReserve
    {
        get => _currentReserve;
    }

    public float RateOfFire
    {
        get => _rateOfFire;
    }

    public Animator MyAnimator
    {
        get => _myAnimator;
    }

    public string IdleAnimationName
    {
        get => _idleAnimationName;
    }

    public string IdleSecondAnimationName
    {
        get => _idleSecondAnimationName;
    }

    public Sprite WeaponUIImage
    {
        get => _weaponUIImage;
    }

    void Start()
    {
        SetInitialReferences();       
    }

    private void SetInitialReferences()
    {
        _myAnimator = GetComponent<Animator>();

        Projectile tmp = _projectilePrefab.GetComponent<Projectile>();
        if (tmp!=null)
        {
            tmp.damage = _damage;
        }
        
        _playerCamera = Player.instance.cam;

        //_idleAnimationName = _myAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name; -> daje animacje wyjmowania a nie idle
    }

    public void Shoot()
    {
        _currentClip -= 1;

        if (_isHitscan)
        {
            RaycastHit hit;

            Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.forward, out hit, _range);           

            if (hit.collider != null)
            {
                BodyPart tmpBodyPart = hit.collider.GetComponent<BodyPart>();

                if (tmpBodyPart != null)
                {
                    if (Player.instance.IsPoweredUp)
                    {
                        tmpBodyPart.TakeDamage(_damage);
                        ParticleSystem tmpEffect2 = Instantiate(_hitEffect, hit.point, transform.rotation);
                        Destroy(tmpEffect2.gameObject, 2f);
                        return;
                    }

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

        Player.instance.UpdateAmmoUI();
    }   

    public void Reload()
    {
        if (_currentReserve >= _clipSize)
        {
            _currentClip = _clipSize;
            _currentReserve -= _clipSize;
        }
        else
        {
            _currentClip = _currentReserve;
            _currentReserve = 0;
        }

        Player.instance.UpdateAmmoUI();
    }

    public void ReplenishAmmo()
    {
        if (CurrentReserve >= _reserveSize)
            return;

        _currentReserve += 2 * ClipSize;
        _currentReserve = Mathf.Clamp(_currentReserve, 0, _reserveSize);
    }

    private void SpawnProjectile()
    {
        if (_isShotgun)
        {
            
            if (Player.instance.IsPoweredUp)
            {
                Projectile[] tmp;

                tmp = _projectilePrefab.GetComponentsInChildren<Projectile>();

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i].damage *= 2;
                }

                Quaternion tmpClusterQuat2 = Quaternion.Euler(-90, -90, 0);
                GameObject tmpClusterGO2 = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpClusterQuat2);
                tmpClusterGO2.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);

                for (int i = 0; i < tmp.Length; i++)
                {
                    tmp[i].damage = _damage;
                }
                _projectilePrefab.GetComponent<Projectile>().damage = _damage;
                Destroy(tmpClusterGO2, _destroyDelay);

                return;
            }

            Quaternion tmpClusterQuat = Quaternion.Euler(-90, -90, 0);
            GameObject tmpClusterGO = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpClusterQuat);
            tmpClusterGO.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);

            _projectilePrefab.GetComponent<Projectile>().damage = _damage;
            Destroy(tmpClusterGO, _destroyDelay);

            return;
        }

        if (_isMelee)
        {
            if (Player.instance.IsPoweredUp)
            {
                _projectilePrefab.GetComponent<Projectile>().damage *= 2;
            }

            Quaternion tmpQuats = Quaternion.Euler(0, 0, 0);
            GameObject tmpGOs = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpQuats);
            tmpGOs.transform.parent = this.gameObject.transform;
            tmpGOs.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);
            _projectilePrefab.GetComponent<Projectile>().damage = _damage;
            Destroy(tmpGOs, _destroyDelay);
            return;
        }

        if (_isCrossbow)
        {
            if (Player.instance.IsPoweredUp)
            {
                _projectilePrefab.GetComponent<Projectile>().damage *= 2;
            }

            Quaternion tmpQuatC = Quaternion.Euler(0, -90, 90);
            GameObject tmpGOC = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpQuatC);
            tmpGOC.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);
            tmpGOC.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.right * -5, ForceMode.Impulse);
            _projectilePrefab.GetComponent<Projectile>().damage = _damage;
            Destroy(tmpGOC, _destroyDelay);
            return;
        }

        if (Player.instance.IsPoweredUp)
        {
            _projectilePrefab.GetComponent<Projectile>().damage *= 2;
        }

        Quaternion tmpQuat = Quaternion.Euler(0, -90, 90);
        GameObject tmpGO = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpQuat);
        tmpGO.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);
        _projectilePrefab.GetComponent<Projectile>().damage = _damage;
        Destroy(tmpGO, _destroyDelay);
    }

}
