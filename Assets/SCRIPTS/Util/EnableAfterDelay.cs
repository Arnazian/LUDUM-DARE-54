using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableAfterDelay : MonoBehaviour
{
    [SerializeField] private float delayInSeconds;
    [SerializeField] private GameObject objectToEnable;
   
    void OnEnable()
    {
        if (objectToEnable == null) return;

        objectToEnable.SetActive(false);
        StartCoroutine(CoroutineEnableAfterDelay());        
    }

    IEnumerator CoroutineEnableAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        objectToEnable.SetActive(true);
    }
}
