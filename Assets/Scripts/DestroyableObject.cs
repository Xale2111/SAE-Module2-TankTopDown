using System;
using UnityEngine;

public class DestroyableBox : MonoBehaviour
{

    [SerializeField] private float _capForce;
    [SerializeField] private float _boxForce;

    [SerializeField] private Rigidbody _capRb;
    [SerializeField] private Rigidbody _boxRb;
    [SerializeField] private Collider _boxCollider;

    public Action<DestroyableBox> onBoxDestroy;
    
    private Vector3 _projectilePosition;
    
  public void SetProjectile(Vector3 projectilePosition)
  {
      _projectilePosition = projectilePosition;
    }
    public void Explode()
    {
        _capRb.AddForce(Vector3.up * _capForce);
        _boxRb.AddForce((_projectilePosition - transform.position) * _boxForce);
        
        Destroy(_boxCollider);
        
        onBoxDestroy.Invoke(this);
    }
}