using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource[] _objectExplosionSources;
    [SerializeField] private AudioSource[] _playerShootSources;


    public void PlayObjectExplosion()
    {
        _objectExplosionSources[Random.Range(0,_objectExplosionSources.Length-1)].Play();
    }

    public void PlayPlayerShoot()
    {
        _playerShootSources[Random.Range(0,_playerShootSources.Length-1)].Play();
    }
}
