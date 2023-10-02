using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverScreen;

    private void Start()
    {
        gameOverScreen.SetActive(false);
        GameSession.OnStateChanged += OnGameOver;
    }
    public void OnGameOver(GameSession.State state)
    {
        if (state != GameSession.State.GAME_OVER) return;
        gameOverScreen.SetActive(true);
        FindObjectOfType<AudioManager>().PlayDeathMusic();
    }
}
