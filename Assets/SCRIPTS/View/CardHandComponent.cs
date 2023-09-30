using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandComponent : MonoBehaviour
{
    [SerializeField] private CardComponent CardTemplate;
    [SerializeField] private Transform layoutRoot;

    private readonly Dictionary<AbstractCard, CardComponent> instances = new();

    [Header("Other Settings")]
    public float PlayYThreshold;

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
        cardComponent.IsOverDropRegion = () => cardComponent.DragVisual.localPosition.y >= PlayYThreshold;
        cardComponent.IsPlayable = () => card.Cooldown.Value <= 0 && GameSession.GameState == GameSession.State.COMBAT;
        cardComponent.OnDrop += OnCardDropped;
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

    void OnCardDropped(CardComponent card, PointerEventData e)
    {
        if (card.DragVisual.localPosition.y < PlayYThreshold) return;
        Combat.Active.PlayCard(card.Card);
    }
}
