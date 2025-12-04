using System.Collections;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float _range = 10f;
    [SerializeField] private float _rotateSpeed = 2.5f;
    [SerializeField] private float _idleRotateSpeed = 20f;
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Transform _cannonBase;
    [SerializeField] private Transform _cannonBarrel;
    [SerializeField] private LayerMask _layer;

    [SerializeField] private float _dps = 1f;

    [SerializeField] private Transform _laserStartPosition;
    [SerializeField] private LineRenderer _laser;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!_playerPosition)
        {
            _playerPosition = GameObject.FindWithTag("Player").transform;
        }

    }

    // Update is called once per frame
    void Update()
    {
        RotateCannon();
    }

    private void RotateCannon()
    {
        Quaternion barrelRotation;
        Quaternion baseRotation;
        if (_playerPosition != null)
        {
            Vector3 distance = _playerPosition.position - _cannonBarrel.position;


            if (_playerPosition != null && distance.magnitude < _range)
            {
                StartCoroutine(ShootSequence_co());

                barrelRotation = Quaternion.Lerp(_cannonBarrel.rotation, Quaternion.LookRotation(distance),
                    Time.deltaTime * _rotateSpeed);

                Ray ray = new Ray(_cannonBarrel.position, _cannonBarrel.forward);

            }
            else
            {
                StopCoroutine(ShootSequence_co());
                _cannonBarrel.Rotate(0f,_idleRotateSpeed*Time.deltaTime,0f);
                Quaternion targetBarrelRotation = Quaternion.Euler(0f, _cannonBarrel.eulerAngles.y, 0f);

                barrelRotation = Quaternion.Lerp(_cannonBarrel.rotation, targetBarrelRotation, Time.deltaTime * (_rotateSpeed / 10f));
            }
            
            baseRotation = barrelRotation;
            baseRotation.x = 0;
            baseRotation.z = 0;
            _cannonBase.rotation = baseRotation;
            _cannonBarrel.rotation = barrelRotation;
        }
        else
        {
            StopCoroutine(ShootSequence_co());
        }


    }

    private void DoLaserShoot()
    {
        if (Physics.Raycast(_cannonBarrel.position, _cannonBarrel.forward, out RaycastHit hit, _range, _layer))
        {
            if (hit.collider.gameObject.TryGetComponent(out DamageTaker damageTaker))
            {
                damageTaker.TakeDamages(_dps * Time.deltaTime);
                _laser.enabled = true;
                _laser.SetPosition(0, _laserStartPosition.position);
                _laser.SetPosition(_laser.positionCount - 1, hit.point);
                Debug.DrawLine(_cannonBarrel.position, _cannonBarrel.forward * _range, Color.green, 0.25f);
            }
        }
        else
        {
            Debug.DrawLine(_cannonBarrel.position, _cannonBarrel.forward * _range, Color.red);
            _laser.enabled = false;
        }
    }

    private IEnumerator ShootSequence_co()
    {
        do
        {
            DoLaserShoot();
            yield return new WaitForSeconds(_fireRate);
        } while (true);

    }

    private void OnDrawGizmos()
    {
        // Set the color with custom alpha.
        Gizmos.color = new Color(1f, 0f, 0f); // Red with custom alpha

        Gizmos.DrawWireSphere(transform.position, _range);
    }
}

