using System.Collections;
using System.Collections.Generic;
using Sisus.ComponentNames;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class TreasureSelection : MonoBehaviour
{
    [SerializeField] private CardComponent card;
    [SerializeField] private float discardYThreshold = 4;
    [SerializeField] private CardHandComponent cardHand;

    [SerializeField] private GameObject treasureSelectionScreen;
    [SerializeField] private Image treasureSelectionBackground;
    void Start()
    {
        treasureSelectionScreen.SetActive(false);
        GameSession.OnStateChanged += OnStateChanged;
        card.OnDrop = OnDrop;
        card.IsOverDropRegion = () => card.DragVisual.localPosition.y > discardYThreshold;
        card.IsDraggable = () => GameSession.GameState == GameSession.State.LOOT;
        card.OnDragging = OnDrag;
    }

    void OnDestroy()
    {
        GameSession.OnStateChanged -= OnStateChanged;
        card.OnDrop -= OnDrop;
    }    

    void OnStateChanged(GameSession.State state)
    {
        if (state != GameSession.State.LOOT)
        {
            StartCoroutine(CoroutineTurnOffTreasureSelectionScreen());
            return;
        }
        treasureSelectionScreen.SetActive(true);
            

        GameSession.OfferedCard = Cards.CardGroups.GetRandom(EncounterGroups.Difficulty.Hard);
                
        card.Card = GameSession.OfferedCard;
        // card.gameObject.SetActive(true);
    }

    void OnDrag(CardComponent c, PointerEventData e)
    {
        foreach (var slot in cardHand.CardSlots)
        {
            var rectTransform = slot.transform as RectTransform;
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            var rect = new Rect(corners[0], corners[2] - corners[0]);
            if (rect.Contains(c.PositionSpring.RestingPos))
                c.PositionSpring.RestingPos = slot.transform.position;
        }
    }

    void OnDrop(CardComponent c, PointerEventData e)
    {
        if (c.DragVisual.position.y > discardYThreshold)
        {
            c.gameObject.SetActive(false); //animation here?
            GameSession.OfferedCard = null;
            GameSession.GameState = GameSession.State.PRE_COMBAT;
            return;
        }
        else for (int i = 0; i < cardHand.CardSlots.Length; i++)
            {
                RectTransform slot = cardHand.CardSlots[i];
                var rectTransform = slot.transform as RectTransform;
                var corners = new Vector3[4];
                rectTransform.GetWorldCorners(corners);
                var rect = new Rect(corners[0], corners[2] - corners[0]);
                if (rect.Contains(c.PositionSpring.RestingPos))
                {
                    GameSession.Player.ReplaceCardAt(i, c.Card);
                    GameSession.OfferedCard = null;
                    GameSession.GameState = GameSession.State.PRE_COMBAT;
                    c.gameObject.SetActive(false);
                    break;
                }
            }
    }

    IEnumerator CoroutineTurnOffTreasureSelectionScreen()
    {
        float backgroundFadeDuration = 2f;
        treasureSelectionBackground.DOFade(0, backgroundFadeDuration);
        yield return new WaitForSeconds(backgroundFadeDuration);
        treasureSelectionScreen.SetActive(false);
    }
}
