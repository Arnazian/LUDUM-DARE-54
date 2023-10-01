using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHandComponent : MonoBehaviour
{
    [Header("Component References")]
    [SerializeField] private CardComponent CardTemplate;
    [SerializeField] private CardSelectionVisualizer CardSelection;

    [field: SerializeField] public RectTransform[] CardSlots { get; private set; }
    private Dictionary<int, CardComponent> InstancesBySlotID = new();

    [Header("Other Settings")]
    public float PlayYThreshold;

    bool IsPlayersTurn = false;

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
            instance.IsDraggable = () => IsPlayersTurn && selectionRoutine == null && card.Cooldown.Value <= 0 && GameSession.GameState == GameSession.State.COMBAT;
            instance.OnDrop = OnCardDropped;
        }
    }

    void OnCardDropped(CardComponent card, PointerEventData e)
    {
        if (selectionRoutine != null || card.DragVisual.localPosition.y < PlayYThreshold) return;
        selectionRoutine = StartCoroutine(PlayCardRoutine(card));
    }

    Coroutine selectionRoutine;
    IEnumerator PlayCardRoutine(CardComponent card)
    {
        Queue<Type> selectionTypes = new(card.Card.Selections);
        List<object> selections = new();
        bool selecting = false;
        Action<object> callback = (obj) =>
        {
            selecting = false;
            selections.Add(obj);
        };
        ICardTarget.OnFinishSelection += callback;
        bool cancel = false;
        while (selectionTypes.Count > 0 && !cancel)
        {
            selecting = true;
            ICardTarget.DoSelection(card.transform.position + Vector3.up * 1f, selectionTypes.Dequeue());
            while (selecting)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    cancel = true;
                    ICardTarget.Cancel();
                    break;
                }
                yield return null;
            }
        }
        ICardTarget.OnFinishSelection -= callback;
        if (!cancel) Combat.Active.PlayCard(card.Card, selections.ToArray());
        selectionRoutine = null;
    }
}

