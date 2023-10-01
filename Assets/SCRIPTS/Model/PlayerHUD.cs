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

        HealthSpring.OnSpringUpdated += UpdateMaterial;

        GameSession.OnStateChanged += OnStateChanged;
        Combat.OnEventLogChanged += OnEventLogChanged;
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
                UpdateHealthUI();
                e.Consume();
                break;
        };
    }

    void UpdateHealthUI()
    {
        healthText.text = GameSession.Player.ReadOnlyHealth.Value.ToString();
        HealthSpring.RestingPos = GameSession.Player.ReadOnlyHealth.Normalized;
    }

    void Update() => HealthSpring.Step(Time.deltaTime);
}
