using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField]
    private float _destroyDelay;

    [Header("References")]
    [SerializeField]
    private GameObject _FireSound;

    private void OnEnable()
    {
        GameObject tmp = Instantiate(_FireSound, this.transform.root);

        Destroy(tmp, 2.5f);
    }


}
