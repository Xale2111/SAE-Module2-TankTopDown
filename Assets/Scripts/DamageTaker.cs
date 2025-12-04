using UnityEngine;
using UnityEngine.Events;

public class DamageTaker : MonoBehaviour
{
    [SerializeField] private float _hpMax;
    [SerializeField] private UnityEvent _onDeath;
    [SerializeField] private bool _destroyable = true;
    
    private float _hp;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _hp = _hpMax;
    }

    public void TakeDamages(float damages)
    {
        _hp -= damages;
        if (_hp <= 0)
        {
            _onDeath.Invoke();
            if(_destroyable) Destroy(gameObject);
            else
            {
                enabled = false;
            }
        }
        
    }
    
}
