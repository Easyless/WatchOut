using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class windsound : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    void Start()
    {
        
    }
    void Awake()
    {

        this.audioSource = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void windsoundstart()
    {
        audioSource.Play();
    }

    public void windsoundstop()
    {
        audioSource.Stop();
    }
}
