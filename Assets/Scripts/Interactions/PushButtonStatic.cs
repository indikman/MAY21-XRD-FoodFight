using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonStatic : MonoBehaviour
{
    public Transform downTransform;
    public Transform buttonTransform;
    public AudioClip clickPressed;
    public AudioClip clickReleased;

    public UnityEvent OnPressed;
    public UnityEvent OnReleased;

    private Vector3 initialPosition;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        initialPosition = buttonTransform.position;
        audioSource = GetComponent<AudioSource>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("hand"))
        {
            buttonTransform.position = downTransform.position;
            audioSource.PlayOneShot(clickPressed);
            OnPressed?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("hand"))
        {
            buttonTransform.position = initialPosition;
            audioSource.PlayOneShot(clickReleased);
            OnReleased?.Invoke();
        }
    }

}
