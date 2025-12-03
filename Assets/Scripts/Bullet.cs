using UnityEngine;

public class Bullet : MonoBehaviour
{
     [SerializeField] private float _forceIntensity = 50f;
     [SerializeField] private float _damage = 50f;
    
        private Rigidbody _rb;
    
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            if (_rb)
            {
                _rb.AddRelativeForce(Vector3.forward * _forceIntensity, ForceMode.VelocityChange);
            }
        }
    
        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log("Bullet hit something, a real and solid collider : " + collision.gameObject.name);
            OnTouch(collision.gameObject);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet hit something, a trigger : " + other.gameObject.name);
            OnTouch(other.gameObject);
        }

        private void OnTouch(GameObject touchObject)
        {
            if (touchObject.TryGetComponent(out DestroyableBox destroyableBox))
            {
                destroyableBox.SetProjectile(transform.position);
            }
            
            if (touchObject.TryGetComponent(out DamageTaker damageTaker))
            {
                damageTaker.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
}
