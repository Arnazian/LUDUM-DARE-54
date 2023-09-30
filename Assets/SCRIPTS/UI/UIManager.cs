using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject treasureView;
    [SerializeField] private GameObject hud;

    public void OpenTreasureView()
    {
        treasureView?.SetActive(true);
        hud?.SetActive(false);
    }
    public void CloseTreasureView()
    {
        treasureView?.SetActive(false); 
        hud?.SetActive(true);
    }

    #region  Singleton implementation
    public static UIManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null)
            Destroy(this.gameObject);
        else
            instance = this;
    }

    #endregion

}
