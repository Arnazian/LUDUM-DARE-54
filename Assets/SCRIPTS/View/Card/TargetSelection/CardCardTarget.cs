using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardComponent))]
public class CardCardTarget : MonoBehaviour, ICardTarget
{
    private CardComponent cached_card;
    private CardComponent Card => cached_card ??= GetComponent<CardComponent>();

    public void OnDrop(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractCard)) return;
        ICardTarget.Select(Card.Card);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractCard)) return;
        ICardTarget.Select(Card.Card);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractCard)) return;
        ICardTarget.InvokeEnter(transform.position);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractCard)) return;
        ICardTarget.InvokeExit();
    }
}