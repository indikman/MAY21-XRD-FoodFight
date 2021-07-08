using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonAnimated : MonoBehaviour
{
    public AudioClip clickPressed;
    public AudioClip clickReleased;
    public Animator anim;

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            anim.SetTrigger("Pressed");
            audioSource.PlayOneShot(clickPressed);
            OnPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            anim.SetTrigger("Released");
            audioSource.PlayOneShot(clickReleased);
            OnReleased?.Invoke();
        }
    }
}
