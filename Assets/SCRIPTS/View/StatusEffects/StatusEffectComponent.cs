using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StatusEffectComponent : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TMP_Text StackCountText;
    [SerializeField] RectTransform Tooltip;
    [SerializeField] TMP_Text TooltipName;
    [SerializeField] TMP_Text TooltipDescr;
    [SerializeField] Image BaseImage;

    public IStatusEffectTarget Target { get; set; }
    private AbstractStatusEffect effect;
    public AbstractStatusEffect Effect
    {
        get => effect;
        set
        {
            effect = value;
            if (value == null) return;
            BaseImage.color = value.Color;
            TooltipName.text = value.Name;
            TooltipDescr.text = value.Description;
            StackCountText.text = value.Stacks > 0 ? value.Stacks.ToString() : "-";
        }
    }

    void Start()
    {
        Combat.OnEventLogChanged += OnCombatEvent;
    }

    private void OnCombatEvent(CombatEvent e)
    {
        if (e.Target != Target) return;
        switch (e.Type)
        {
            case CombatEvent.EventType.StatusApplied:
            case CombatEvent.EventType.StatusRemoved:
                var eventStatusEffect = e.Args[0] as AbstractStatusEffect;
                if (eventStatusEffect != Effect) break;
                var stacks = (int)e.Args[1];
                if (stacks == 0)
                    Destroy(gameObject);
                StackCountText.text = stacks > 0 ? stacks.ToString() : "-";
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
