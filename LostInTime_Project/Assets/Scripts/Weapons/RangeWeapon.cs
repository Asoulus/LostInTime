using UnityEngine;
using UnityEngine.UI;

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
    private GameObject _projectileObject = null;

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

    public string FireAnimationName
    {
        get => _idleAnimationName;
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
        _projectilePrefab.GetComponent<Projectile>().damage = _damage;
        _playerCamera = Player.instance.cam;

        //_idleAnimationName = _myAnimator.GetCurrentAnimatorClipInfo(0)[0].clip.name; -> daje animacje wyjmowania a nie idle
    }

    public void Shoot()
    {
        if (_isMelee)
        {
            //TODO turn on collider
            return;
        }

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
                    tmpBodyPart.TakeDamage(_damage);
                    ParticleSystem tmpEffect = Instantiate(_hitEffect, hit.point, transform.rotation);
                    Destroy(tmpEffect.gameObject, 2f);
                }
            }
                    
        }
        else
        {
            SpawnProjectile();

            //TODO fix constant arrow
            if (_projectileObject != null)
            {
                _projectileObject.SetActive(false);
            }
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

    private void SpawnProjectile()
    {
        Quaternion tmpQuat = Quaternion.Euler(0, -90, 90);
        GameObject tmpGO = Instantiate(_projectilePrefab, _firePoint.transform.position, transform.rotation * tmpQuat);
        tmpGO.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _projectileSpeed, ForceMode.Impulse);
        Destroy(tmpGO, _destroyDelay);
    }

}
