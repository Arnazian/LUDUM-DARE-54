using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

public class PlayerHUD : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthImage;
    [SerializeField] private StatusEffectList statusEffectList;
    BaseSpring HealthSpring;
    Spring.Config springConfig = new(20, 2f);
    void Start()
    {
        HealthSpring = new(springConfig)
        {
            Position = GameSession.Player.ReadOnlyHealth.Normalized,
            RestingPos = GameSession.Player.ReadOnlyHealth.Normalized
        };
        healthImage.material = Instantiate(healthImage.material);
        healthText.text = GameSession.Player.Health.Value.ToString();
        HealthSpring.OnSpringUpdated += UpdateMaterial;

        GameSession.OnStateChanged += OnStateChanged;
        Combat.OnEventLogChanged += OnEventLogChanged;
        statusEffectList.Target = GameSession.Player;
    }

    private void UpdateMaterial(float fill)
    {
        healthImage.material.SetFloat("_Fill", fill);
    }

    private void OnStateChanged(GameSession.State state)
    {
        if (state != GameSession.State.COMBAT)
        {
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
    }

    private void OnEventLogChanged(CombatEvent e)
    {
        if (e.Target != GameSession.Player) return;
        switch (e.Type)
        {
            case CombatEvent.EventType.Damaged:
            case CombatEvent.EventType.Healed:
                UpdateHealthUI(e);
                break;
        };
    }

    void UpdateHealthUI(CombatEvent e)
    {
        healthText.text = e.Args[0].ToString();
        HealthSpring.RestingPos = ((int)e.Args[0]) / (float)GameSession.Player.ReadOnlyHealth.Max;
    }

    void Update() => HealthSpring.Step(Time.deltaTime);
}
