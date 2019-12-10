using UnityEngine;

public class UISFX : MonoBehaviour
{
    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource.GetComponent<AudioSource>();    
    }

    public void OnHover()
    {
        _audioSource.PlayOneShot(AudioClipReference.instance.onButtonHover);
    }

    public void OnClick()
    {
        _audioSource.PlayOneShot(AudioClipReference.instance.onButtonClick);
    }
}
