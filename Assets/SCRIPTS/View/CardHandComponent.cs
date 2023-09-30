using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandComponent : MonoBehaviour
{
    [SerializeField] private CardComponent CardTemplate;

    [field: SerializeField] public RectTransform[] CardSlots { get; private set; }
    private Dictionary<int, CardComponent> InstancesBySlotID = new();

    [Header("Other Settings")]
    public float PlayYThreshold;

    void Start()
    {
        GameSession.Player.OnCardChanged += UpdateCard;
        for (int i = 0; i < GameSession.Player.Cards.Count; i++)
        {
            AbstractCard card = GameSession.Player.Cards[i];
            UpdateCard(i, card);
        }
    }

    void OnDestroy()
    {
        GameSession.Player.OnCardChanged -= UpdateCard;
    }

    void UpdateCard(int slot, AbstractCard card)
    {
        if (card == null)
        {
            if (InstancesBySlotID.TryGetValue(slot, out var instance))
            {
                Destroy(instance.gameObject);
                InstancesBySlotID.Remove(slot);
            }
        }
        else
        {
            if (!InstancesBySlotID.TryGetValue(slot, out var instance))
                InstancesBySlotID[slot] = instance = Instantiate(CardTemplate, CardSlots[slot]);
            instance.Card = card;
            instance.IsOverDropRegion = () => instance.DragVisual.localPosition.y >= PlayYThreshold;
            instance.IsDraggable = () => card.Cooldown.Value <= 0 && GameSession.GameState == GameSession.State.COMBAT;
            instance.OnDrop = OnCardDropped;
        }
    }

    void OnCardDropped(CardComponent card, PointerEventData e)
    {
        if (card.DragVisual.localPosition.y < PlayYThreshold) return;
        Combat.Active.PlayCard(card.Card);
    }
}

