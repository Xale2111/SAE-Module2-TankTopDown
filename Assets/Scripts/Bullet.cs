using UnityEngine;

public class Bullet : MonoBehaviour
{
     [SerializeField] private float _forceIntensity = 50f;
    
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
            Destroy(gameObject);
        }
    
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Bullet hit something, a trigger : " + other.gameObject.name);
            Destroy(gameObject);
        }
}
