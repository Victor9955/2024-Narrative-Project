using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPipetteSound : MonoBehaviour
{
    public static PlayPipetteSound instance;

    [SerializeField] AudioSource audioSource;

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void Play()
    {
        audioSource.Play();
    }
}
