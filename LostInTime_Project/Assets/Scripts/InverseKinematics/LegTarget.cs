using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LegTarget : MonoBehaviour
{
    [SerializeField]
    private GameObject _myPoint = null;
    [SerializeField]
    private float _distance = 2f;
    [SerializeField] 
    private int _id = 0;

    [SerializeField]
    private float _moveSpeed = 0.5f;
    [SerializeField]
    private float _moveAmplitude = 1f;

    private float cTime = 0f; //czas skoku

    private Vector3 _savedPos;

    public bool _isMoving = false;
    private bool _hasMoved = false;

    [SerializeField]
    private LegTarget _myTwinLeg = null;

    private void Start()
    {
        if (_id % 2 == 0)
        {
            transform.position = new Vector3(_myPoint.transform.position.x, _myPoint.transform.position.y, _myPoint.transform.position.z - (_distance * 0.98f));
        }
        else
        {
            transform.position = _myPoint.transform.position;
        }

    }
    
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, _myPoint.transform.position);
    }
    */

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, _myPoint.transform.position);

        if (distance >= _distance)
        {
            _savedPos = _myPoint.transform.position;

            StartCoroutine(MoveLeg());
        }
    }

    private IEnumerator MoveLeg()
    {
        if (_hasMoved)
        {
            yield break;
        }

        if (_isMoving) //zabezpieczenie przed podwojnym ruchem
        {
            yield break; //jak sie juz ruszamy to wrocic
        }

        _isMoving = true;  //nie ruszalismy sie to teraz sie ruszamy                 

        Vector3 startPos = transform.position;
        Vector3 destinationPos = new Vector3(_myPoint.transform.position.x, _myPoint.transform.position.y - 0.75f, _myPoint.transform.position.z) ;       

        //do skoku
        while (MoveLegInArc(startPos, destinationPos, _moveSpeed, 1f))
        {
            yield return null;
        }
        yield return null; 
        cTime = 0; //resetowanie czasu
        _myTwinLeg._hasMoved = false;
        _hasMoved = true;
        _isMoving = false;             
    }

    private bool MoveLegInArc(Vector3 _startPos, Vector3 _goToPosition, float _speed, float amplitudeMultiplier)
    {
        cTime += _speed * Time.deltaTime;
        Vector3 myPos = Vector3.Lerp(_startPos, _goToPosition, cTime);

        myPos.y += (_moveAmplitude * amplitudeMultiplier) * Mathf.Sin(Mathf.Clamp01(cTime) * Mathf.PI);

        return _goToPosition != (transform.position = Vector3.Lerp(transform.position, myPos, cTime));
    }
}