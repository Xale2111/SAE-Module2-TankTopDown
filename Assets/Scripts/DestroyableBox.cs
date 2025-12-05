using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class DestroyableBox : MonoBehaviour
{

    [SerializeField] private float _capForce;
    [SerializeField] private float _boxForce;

    [SerializeField] private Rigidbody _capRb;
    [SerializeField] private Rigidbody _boxRb;

    [SerializeField] private GameObject _explosion;
    
    [SerializeField] private UnityEvent _onDestroy;
    
    public Action<DestroyableBox> OnBoxDestroy;

    private Vector3 _projectilePosition;

    public void SetProjectilePosition(Vector3 projectilePosition)
    {
        _projectilePosition = projectilePosition;
    }
    
    public void Explode()
    {
        Debug.Log("Pan t'es mort !");
        _capRb.AddForce(Vector3.up * _capForce);
        _boxRb.AddForce((_projectilePosition - transform.position) * _boxForce);
        
        Instantiate(_explosion,transform.position, Quaternion.identity);
        
        Collider myCollider = GetComponent<Collider>();
        Destroy(myCollider);
            
        _onDestroy.Invoke();
        OnBoxDestroy.Invoke(this);
    }
    
}