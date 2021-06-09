using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public static Player instance;

    [Header("Variables")]
    [SerializeField]
    private float _maxHealth = 100f;
    [SerializeField]
    private float _currentHealth = 100f;

    [SerializeField]
    private int _healthPackAmount = 0;
    [SerializeField]
    private int _powerUpAmount = 0;

    [Header("References")]
    [SerializeField]
    private Slider _healthBar = null;
    [SerializeField]
    private Player_Master _playerMaster = null;

    [Header("Main UI")]
    [SerializeField]
    private Text _healthPackAmountUIText = null;
    [SerializeField]
    private Text _powerUpAmountUIText = null;
    [SerializeField]
    private Sprite _crosshair = null;
    [SerializeField]
    private Sprite _emptyCrosshair = null;
    [SerializeField]
    private AudioSource _clickSound = null;
    [SerializeField]
    private CanvasGroup _tooltip = null;


    public bool IsPoweredUp = false;

    #region GetComponented References
    private FirstPersonAIO _playerController;

    public Camera cam = null;

    #endregion

    #region Weapons

    #region Weapon UI
    [Header("Weapon UI")]
    //Primary weapon
    [SerializeField]
    private Image _primaryWeaponImage = null;
    [SerializeField]
    private Text _primaryWeaponClipText = null;
    [SerializeField]
    private Text _primaryWeaponReserveText = null;

    //Secondary weapon
    [SerializeField]
    private Image _secondaryWeaponImage = null;
    [SerializeField]
    private Text _secondaryWeaponClipText = null;
    [SerializeField]
    private Text _secondaryWeaponReserveText = null;


    #endregion

    #region Equiped weapons

    [Header("Equiped weapons")]

    [SerializeField]
    private RangeWeapon _currentPrimaryWeapon = null;
    [SerializeField]
    private RangeWeapon _currentSecondaryWeapon = null;
    [SerializeField]
    private RangeWeapon _currentlyHeldWeapon = null;
    

    #endregion

    #region Weapon Prefabs
    [Header("Weapon Prefabs")]
    [SerializeField]
    private RangeWeapon _revolver;
    [SerializeField]
    private RangeWeapon _crossbow;
    [SerializeField]
    private RangeWeapon _ak47;
    [SerializeField]
    private RangeWeapon _sword;
    [SerializeField]
    private RangeWeapon _shotgun;
    [SerializeField]
    private RangeWeapon _orbGun;

    #endregion

    [SerializeField]
    private float _nextFire = 0;

    [SerializeField]
    private float _nextSwap = 0;

    #endregion

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SetInitialReferences();

        _playerMaster.EventPlayerTakeDamage += TakeDamage;
        _playerMaster.EventPlayerHeal += Heal;
        PlayerInputHandler.instance.onLeftMouseButtonPressed += Fire;
        PlayerInputHandler.instance.onReloadButtonPressed += Reload;
        PlayerInputHandler.instance.onOneButtonPressed += SwapWeapons;
        PlayerInputHandler.instance.onTwoButtonPressed += SwapWeapons;
        PlayerInputHandler.instance.onXButtonPressed += PowerUp;

        UpdateAmmoUI();
    }

    private void SetInitialReferences()
    {
        _playerController = GetComponent<FirstPersonAIO>();

        StartCoroutine(CrosshairCheck());

        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _maxHealth;

        _primaryWeaponImage.sprite = _currentPrimaryWeapon.WeaponUIImage;
        _secondaryWeaponImage.sprite = _currentSecondaryWeapon.WeaponUIImage;

        _currentlyHeldWeapon = _currentPrimaryWeapon;

        _currentlyHeldWeapon.gameObject.SetActive(true);
        _currentSecondaryWeapon.gameObject.SetActive(false);    

        cam = GetComponentInChildren<Camera>();
        _playerMaster = GetComponent<Player_Master>();
        if (_tooltip != null)
        {
            ToggleTooltip(false, "");
        }
    }

    private IEnumerator CrosshairCheck()
    {
        yield return new WaitForSeconds(0.1f);
        if (!_currentlyHeldWeapon.HasCrosshair)
        {
            _playerController.crosshairImage.sprite = _emptyCrosshair;
        }
    }

    private IEnumerator HideTooltip()
    {
        yield return new WaitForSeconds(5f);
        ToggleTooltip(false,"");
    }

    public void ToggleTooltip(bool value,string text)
    {
        _tooltip.GetComponentInChildren<Text>().text = text;

        if (value)
        {
            _tooltip.alpha = 1;

        }
        else
        {
            _tooltip.alpha = 0;
        }

        _tooltip.interactable = value;
        _tooltip.blocksRaycasts = value;

        StartCoroutine(HideTooltip());
    }

    private void SwapWeapons(int value)
    {
        if (_currentlyHeldWeapon.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentlyHeldWeapon.IdleSecondAnimationName))
        {
            if (Time.time > _nextSwap)
            {
                _nextSwap = Time.time + 1f;
            }
            else
            {
                return;
            }

            if (value == 1)
            {
                if (_currentlyHeldWeapon == _currentSecondaryWeapon)
                {
                    _currentlyHeldWeapon.MyAnimator.SetTrigger("Stow");
                    StartCoroutine(SwapDelay(_currentPrimaryWeapon));
                }
            }
            else
            {
                if (_currentlyHeldWeapon == _currentPrimaryWeapon)
                {
                    _currentlyHeldWeapon.MyAnimator.SetTrigger("Stow");
                    StartCoroutine(SwapDelay(_currentSecondaryWeapon));
                }
            }
            return;
        }

        if (!_currentlyHeldWeapon.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentlyHeldWeapon.IdleAnimationName))
            return;

        if (Time.time > _nextSwap)
        {
            _nextSwap = Time.time + 1f;
        }
        else
        {
            return;
        }

        if (value == 1)
        {
            if (_currentlyHeldWeapon == _currentSecondaryWeapon)
            {
                _currentlyHeldWeapon.MyAnimator.SetTrigger("Stow");
                StartCoroutine(SwapDelay(_currentPrimaryWeapon));
            }          
        }
        else
        {
            if (_currentlyHeldWeapon == _currentPrimaryWeapon)
            {
                _currentlyHeldWeapon.MyAnimator.SetTrigger("Stow");
                StartCoroutine(SwapDelay(_currentSecondaryWeapon));
            }
        }
    }

    private IEnumerator SwapDelay(RangeWeapon weapon)
    {
        yield return new WaitForSeconds(0.4f);
        _currentlyHeldWeapon.gameObject.SetActive(false);
        _currentlyHeldWeapon = weapon;
        _currentlyHeldWeapon.gameObject.SetActive(true);
        _currentlyHeldWeapon.MyAnimator.SetTrigger("Ready");

        if (_currentlyHeldWeapon.HasCrosshair)
        {
            _playerController.crosshairImage.sprite = _crosshair;
        }
        else
        {
            _playerController.crosshairImage.sprite = _emptyCrosshair;
        }
    }

    public void AssignWeapons(string name1, string name2)
    {
        switch (name1)
        {
            case "Shotgun":
                {
                    _currentPrimaryWeapon = _shotgun; 
                }
                break;
            case "Crossbow":
                {
                    _currentPrimaryWeapon = _crossbow;
                }
                break;
            case "Sword":
                {
                    _currentPrimaryWeapon = _sword;
                }
                break;
            case "AK47":
                {
                    _currentPrimaryWeapon = _ak47;
                }
                break;
            case "OrbGun":
                {
                    _currentPrimaryWeapon = _orbGun;
                }
                break;
            case "Revolver":
                {
                    _currentPrimaryWeapon = _revolver;
                }
                break;
        }

        switch (name2)
        {
            case "Shotgun":
                {
                    _currentSecondaryWeapon = _shotgun;
                }
                break;
            case "Crossbow":
                {
                    _currentSecondaryWeapon = _crossbow;
                }
                break;
            case "Sword":
                {
                    _currentSecondaryWeapon = _sword;
                }
                break;
            case "AK47":
                {
                    _currentSecondaryWeapon = _ak47;
                }
                break;
            case "OrbGun":
                {
                    _currentSecondaryWeapon = _orbGun;
                }
                break;
            case "Revolver":
                {
                    _currentSecondaryWeapon = _revolver;
                }
                break;
        }
    }

    public void UpdateAmmoUI()
    {
        if (!_currentPrimaryWeapon.IsMelee)
        {
            _primaryWeaponClipText.text = _currentPrimaryWeapon.CurrentClip.ToString();
            _primaryWeaponReserveText.text = _currentPrimaryWeapon.CurrentReserve.ToString();
        }
        else
        {
            _primaryWeaponClipText.text = string.Empty;
            _primaryWeaponReserveText.text = string.Empty;
        }

        if (!_currentSecondaryWeapon.IsMelee)
        {
            _secondaryWeaponClipText.text = _currentSecondaryWeapon.CurrentClip.ToString();
            _secondaryWeaponReserveText.text = _currentSecondaryWeapon.CurrentReserve.ToString();
        }
        else
        {
            _secondaryWeaponClipText.text = string.Empty;
            _secondaryWeaponReserveText.text = string.Empty;
        }


       
    }

    public void Fire()
    {
        if (_currentlyHeldWeapon.CurrentClip <= 0)
        {
            _clickSound.PlayDelayed(0.1f);
            return;
        }
            

        if (_currentlyHeldWeapon.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentlyHeldWeapon.IdleAnimationName) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _currentlyHeldWeapon.RateOfFire;
            _currentlyHeldWeapon.MyAnimator.SetTrigger("Fire");
            _currentlyHeldWeapon.Shoot();
            
        }
  
    }

    public void Reload()
    {
        if (_currentlyHeldWeapon.IsMelee)
            return;

        if (_currentlyHeldWeapon.CurrentReserve <= 0)
            return;

        if (_currentlyHeldWeapon.CurrentClip == _currentlyHeldWeapon.ClipSize)
            return;

        if (_currentlyHeldWeapon.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentlyHeldWeapon.IdleSecondAnimationName))
        {
            _currentlyHeldWeapon.MyAnimator.SetTrigger("Reload");
            _currentlyHeldWeapon.Reload();
            return;
        }

        if (!_currentlyHeldWeapon.MyAnimator.GetCurrentAnimatorStateInfo(0).IsName(_currentlyHeldWeapon.IdleAnimationName))
            return;

        _currentlyHeldWeapon.MyAnimator.SetTrigger("Reload");
        _currentlyHeldWeapon.Reload();
    }

    public void PickUpHealthPack()
    {
        _healthPackAmount++;
        _healthPackAmountUIText.text = _healthPackAmount.ToString();
    }

    public void PickUpPowerUp()
    {
        _powerUpAmount++;
        _powerUpAmountUIText.text = _powerUpAmount.ToString();
    }

    public void PickUpAmmo()
    {
        _currentPrimaryWeapon.ReplenishAmmo();
        _currentSecondaryWeapon.ReplenishAmmo();

        UpdateAmmoUI();
    }

    private void PowerUp()
    {
        if (_powerUpAmount <= 0)
            return;

        _powerUpAmount--;
        _powerUpAmountUIText.text = _powerUpAmount.ToString();

        IsPoweredUp = true;
        StartCoroutine(PowerUpDuration());
    }

    private IEnumerator PowerUpDuration()
    {
        yield return new WaitForSeconds(10f);
        IsPoweredUp = false;
    }

    private void TakeDamage(float damage)
    {
        if (IsPoweredUp)
        {
            _currentHealth -= damage/2;
            Mathf.Clamp(_currentHealth, 0, _maxHealth);
            _healthBar.value = _currentHealth;

            if (_currentHealth <= 0)
            {
                //Death
                _playerMaster.CallEventPlayerDie();
                Debug.Log("YOU HAVE DIED!");
            }
            return;
        }

        _currentHealth -= damage;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthBar.value = _currentHealth;

        if (_currentHealth <= 0)
        {
            //Death
            _playerMaster.CallEventPlayerDie();
            Debug.Log("YOU HAVE DIED!"); 
        }
    }

    private void Heal(float healAmount)
    {
        if (_currentHealth == _maxHealth)
            return;

        _currentHealth += healAmount;
        Mathf.Clamp(_currentHealth, 0, _maxHealth);
        _healthBar.value = _currentHealth;

        _healthPackAmount--;
        _healthPackAmountUIText.text = _healthPackAmount.ToString();
    }
 

    private void OnDisable()
    {
        _playerMaster.EventPlayerTakeDamage -= TakeDamage;
        _playerMaster.EventPlayerHeal -= Heal;
        PlayerInputHandler.instance.onLeftMouseButtonPressed -= Fire;
        PlayerInputHandler.instance.onReloadButtonPressed -= Reload;
        PlayerInputHandler.instance.onOneButtonPressed -= SwapWeapons;
        PlayerInputHandler.instance.onTwoButtonPressed -= SwapWeapons;
        PlayerInputHandler.instance.onXButtonPressed -= PowerUp;
    }

}
