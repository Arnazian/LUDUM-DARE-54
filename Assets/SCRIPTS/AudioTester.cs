using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTester : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            audioSource.Play(); 
        }
    }
}
