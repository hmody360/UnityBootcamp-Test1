using UnityEngine;

public class SoundMaker : MonoBehaviour
{
    public AudioClip[] SFXClipList;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) //Playing Entry Sound if enemy or player enter.
    {
        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            _audioSource.PlayOneShot(SFXClipList[0]);
        }
    }

    private void OnTriggerExit(Collider other) //Playing Exit Sound if enemy or player enter.
    {
        _audioSource.PlayOneShot(SFXClipList[1]);
    }

}
