using System;
using Unity.Cinemachine;
using UnityEngine;

public class SwitchCameraView : MonoBehaviour
{
    [SerializeField] private Vector3 _newValue;
    
    [SerializeField] CinemachineFollow _camera;

    private Vector3 _defaultValue;

    private void Start()
    {
        _defaultValue = _camera.FollowOffset;
        Debug.Log(_defaultValue);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camera.FollowOffset = _newValue;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _camera.FollowOffset = _defaultValue;
        }
    }
}
