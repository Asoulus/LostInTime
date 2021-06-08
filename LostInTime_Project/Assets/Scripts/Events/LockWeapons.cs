using UnityEngine;

public class LockWeapons : MonoBehaviour
{
    public static LockWeapons instance;

    private static string _shotgunUnlock = "ShotgunUnlock";
    private static string _revolverUnlock = "RevolverUnlock";
    private static string _swordUnlock = "SwordUnlock";
    private static string _crossbowUnlock = "CrossbowUnlock";
    private static string _ak47Unlock = "Ak47Unlock";
    private static string _orbGunUnlock = "OrbGunUnlock";


    public bool _isShotgunUnlocked = false;
    public bool _isRevolverUnlocked = false;
    public bool _isAk47Unlocked = false;
    public bool _isSwordUnlocked = false;
    public bool _isOrbGunUnlocked = false;
    public bool _isCrossbowUnlocked = false;

    #region objects

    [Header("References")]
    [SerializeField]
    private GameObject _Shotgun = null;
    [SerializeField]
    private GameObject _Ak47 = null;
    [SerializeField]
    private GameObject _Crossbow = null;
    [SerializeField]
    private GameObject _Sword = null;
    [SerializeField]
    private GameObject _OrbGun = null;
    [SerializeField]
    private GameObject _Revolver = null;
    [SerializeField]
    private GameObject _Arrow = null;

    [SerializeField]
    private Material[] _blackMats;


    #endregion

    private void Awake()
    {
        instance = this;
    }


    private void Start()
    {
        PlayerPrefs.SetInt(_revolverUnlock, 1);
        PlayerPrefs.SetInt(_crossbowUnlock, 1);

        _isShotgunUnlocked = (PlayerPrefs.GetInt(_shotgunUnlock) == 1);
        _isRevolverUnlocked = (PlayerPrefs.GetInt(_revolverUnlock) == 1);
        _isSwordUnlocked = (PlayerPrefs.GetInt(_swordUnlock) == 1);
        _isCrossbowUnlocked = (PlayerPrefs.GetInt(_crossbowUnlock) == 1);
        _isAk47Unlocked = (PlayerPrefs.GetInt(_ak47Unlock) == 1);
        _isOrbGunUnlocked = (PlayerPrefs.GetInt(_orbGunUnlock) == 1);

        BlackOutMesh();
    }

    public bool ReturnLocked(string name)
    {
        bool tmp = false;
        switch (name)
        {
            case "Shotgun":
                {
                    tmp = _isShotgunUnlocked;
                }break;
            case "Crossbow":
                {
                    tmp = _isCrossbowUnlocked;
                }
                break;
            case "Sword":
                {
                    tmp = _isSwordUnlocked;
                }
                break;
            case "AK47":
                {
                    tmp = _isAk47Unlocked;
                }
                break;
            case "OrbGun":
                {
                    tmp = _isOrbGunUnlocked;
                }
                break;
            case "Revolver":
                {
                    tmp = _isRevolverUnlocked;
                }
                break;

        }
        return tmp;
    }

    private void BlackOutMesh()
    {
        //shotgun
        if (!_isShotgunUnlocked)
        {
            Renderer tmp = _Shotgun.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }
        }

        //revolver
        if (!_isRevolverUnlocked)
        {
            Renderer tmp = _Revolver.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }
        }

        //sword
        if (!_isSwordUnlocked)
        {
            Renderer tmp = _Sword.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }
        }

        //crossbow
        if (!_isCrossbowUnlocked)
        {
            Renderer tmp = _Crossbow.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }

            Renderer tmp2 = _Arrow.GetComponent<Renderer>();
            for (int i = 0; i < tmp2.materials.Length; i++)
            {
                tmp2.materials = _blackMats;
            }
        }

        //orbgun
        if (!_isOrbGunUnlocked)
        {
            Renderer tmp = _OrbGun.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }
        }

        //ak47
        if (!_isAk47Unlocked)
        {
            Renderer tmp = _Ak47.GetComponent<Renderer>();
            for (int i = 0; i < tmp.materials.Length; i++)
            {
                tmp.materials = _blackMats;
            }
        }
    }
}
