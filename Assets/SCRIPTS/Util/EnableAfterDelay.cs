using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delayInSeconds;
    [SerializeField] private GameObject objectToEnable;
   
    void Start()
    {
        StartCoroutine(CoroutineEnableAfterDelay());        
    }

    IEnumerator CoroutineEnableAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        objectToEnable.SetActive(true);
    }
}
