using JetBrains.Annotations;
using UnityEngine;

public class TreeSize : MonoBehaviour
{
    [SerializeField] private int minSize = 4; 
    [SerializeField] private int maxSize = 7; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int size = Random.Range(minSize, maxSize);
        gameObject.transform.localScale = new Vector3(size,size,size);
    }
}
