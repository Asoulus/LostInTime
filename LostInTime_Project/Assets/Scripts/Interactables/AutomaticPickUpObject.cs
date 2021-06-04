using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class AutomaticPickUpObject : MonoBehaviour
{
    [Header("Values")]
    [SerializeField]
    private float _animationDuration = 2f;

    [SerializeField]
    private float _movementHeight = 0.5f;

    [Header("References")]
    [SerializeField]
    private Collider _collider = null;
    [SerializeField]
    protected AudioSource _pickUpSound= null;

    private void Start()
    {
        SetInitialReferences();

        _collider.enabled = false;

        LeanTween.moveY(this.gameObject, transform.position.y + 5, 0.5f).setEaseOutCirc().setOnComplete(BounceDown);
        LeanTween.moveX(this.gameObject, transform.position.x + Random.Range(-2,2), _animationDuration);
        LeanTween.moveZ(this.gameObject, transform.position.z + Random.Range(-2, 2), _animationDuration);
        
    }

    private void BounceDown()
    {
        LeanTween.moveY(this.gameObject, transform.position.y - 5, 1.5f).setEaseOutBounce().setOnComplete(Idle);
    }

    private void Idle()
    {
        _collider.enabled = true;

        LeanTween.moveY(this.gameObject, transform.position.y + _movementHeight, _animationDuration).setEaseLinear().setLoopPingPong();
        LeanTween.rotateAround(gameObject, Vector3.up, 360, _animationDuration * 2).setLoopClamp();
    }

    private void SetInitialReferences()
    {
        _collider = GetComponent<Collider>();
        _pickUpSound = GetComponent<AudioSource>();
    }
}
