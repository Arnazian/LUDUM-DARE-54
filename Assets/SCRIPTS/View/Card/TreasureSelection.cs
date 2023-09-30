using System.Collections;
using System.Collections.Generic;
using Sisus.ComponentNames;
using UnityEngine;

public class TreasureSelection : MonoBehaviour
{
    [SerializeField] private CardComponent Card;
    void Start()
    {
        GameSession.OnStateChanged += OnStateChanged;
    }

    void OnDestroy()
    {
        GameSession.OnStateChanged -= OnStateChanged;
    }

    void OnStateChanged(GameSession.State state)
    {
        if (state != GameSession.State.LOOT) return;
        GameSession.OfferedCard = new Cards.Dagger(); //TODO: replace with random card that gets better with combat difficulty
        Card.Card = GameSession.OfferedCard;
        Card.gameObject.SetActive(true);
    }
}
