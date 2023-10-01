using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusEffectComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] IStatusEffectTarget target;
    [SerializeField] TMP_Text StackCountText;
    [SerializeField] RectTransform Tooltip;
    [SerializeField] TMP_Text TooltipName;
    [SerializeField] TMP_Text TooltipDescr;
    [SerializeField] Image BaseImage;

    public AbstractStatusEffect effect;
    public AbstractStatusEffect Effect
    {
        get => effect;
        set
        {
            effect = value;
            BaseImage.color = effect.Color;
            TooltipName.text = effect.Name;
            TooltipDescr.text = effect.Description;
            StackCountText.text = effect.Stacks.ToString();
        }
    }

    void Start()
    {
        Combat.OnEventLogChanged += OnCombatEvent;
    }

    private void OnCombatEvent(CombatEvent e)
    {
        if (e.Target != target) return;
        switch (e.Type)
        {
            case CombatEvent.EventType.StatusApplied:
            case CombatEvent.EventType.StatusRemoved:
                var eventStatusEffect = e.Args[0] as AbstractStatusEffect;
                if (eventStatusEffect != Effect) break;
                if (eventStatusEffect.Stacks == 0)
                    Destroy(gameObject);
                StackCountText.text = eventStatusEffect.Stacks.ToString();
                break;
        }
    }

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnCombatEvent;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Tooltip.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Tooltip.gameObject.SetActive(false);
    }
}
