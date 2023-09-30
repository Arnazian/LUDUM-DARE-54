using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandComponent : MonoBehaviour
{
    [SerializeField] private CardComponent CardTemplate;
    [SerializeField] private Transform layoutRoot;


    void Start()
    {
        var player = new Player();
        player.cards.Add(new Cards.Dagger());
        player.cards.Add(new Cards.Dagger());
        player.cards.Add(new Cards.Dagger());
        Init(player);
    }

    void Init(Player player)
    {
        foreach (var card in player.cards)
        {
            var cardComponent = Instantiate(CardTemplate, layoutRoot);
            cardComponent.Card = card;
        }
    }
}
