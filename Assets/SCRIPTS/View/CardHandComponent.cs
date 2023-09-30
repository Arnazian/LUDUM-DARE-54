using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHandComponent : MonoBehaviour
{
    [SerializeField] private CardComponent CardTemplate;
    [SerializeField] private Transform layoutRoot;

    private readonly Dictionary<AbstractCard, CardComponent> instances = new();

    void Start()
    {
        GameSession.Player.OnCardAdded += AddCard;
        GameSession.Player.OnReplaceCard += ReplaceCard;
        GameSession.Player.OnCardRemoved += RemoveCard;
        foreach (var card in GameSession.Player.Cards)
            AddCard(card);
    }

    void OnDestroy()
    {
        GameSession.Player.OnCardAdded -= AddCard;
        GameSession.Player.OnReplaceCard -= ReplaceCard;
        GameSession.Player.OnCardRemoved -= RemoveCard;
    }

    void AddCard(AbstractCard card)
    {
        var cardComponent = Instantiate(CardTemplate, layoutRoot);
        instances[card] = cardComponent;
        cardComponent.Card = card;
    }

    void ReplaceCard(AbstractCard old, AbstractCard replacement)
    {
        var siblingIndex = instances[old].transform.GetSiblingIndex();
        RemoveCard(old);
        AddCard(replacement);
        instances[replacement].transform.SetSiblingIndex(siblingIndex);
    }

    void RemoveCard(AbstractCard card)
    {
        Destroy(instances[card].gameObject);
        instances.Remove(card);
    }
}
