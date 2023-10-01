using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : MonoBehaviour
{
    public AbstractEnemy enemy;
    [SerializeField] private SpriteRenderer EnemySprite;
    [SerializeField] private Image healthImage;
    BaseSpring HealthSpring;
    Spring.Config springConfig = new(20, 2f);
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text ActionCooldown;

    private EnemyGetHitEffects getHitEffects;

    void Start()
    {
        getHitEffects = GetComponent<EnemyGetHitEffects>();
        Combat.OnEventLogChanged += OnNewCombatEvent;
        HealthText.text = enemy.ReadOnlyHealth.Value.ToString();

        HealthSpring = new(springConfig)
        {
            Position = GameSession.Player.ReadOnlyHealth.Normalized,
            RestingPos = GameSession.Player.ReadOnlyHealth.Normalized
        };
        healthImage.material = Instantiate(healthImage.material);

        HealthSpring.OnSpringUpdated += UpdateMaterial;

        ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "";
    }

    private void UpdateMaterial(float fill)
    {
        healthImage.material.SetFloat("_Fill", fill);
    }

    private void OnNewCombatEvent(CombatEvent e)
    {
        if (e.Target != enemy) return;
        switch (e.Type)
        {
            case CombatEvent.EventType.Killed:
                getHitEffects.DoDeathEffects();
                /// Destroy(gameObject); //death animation
                e.Consume();
                break;
            case CombatEvent.EventType.Damaged:
                getHitEffects.DoGetHitEffects();
                HealthText.text = enemy.ReadOnlyHealth.Value.ToString();
                HealthSpring.RestingPos = enemy.ReadOnlyHealth.Normalized;
                e.Consume();
                break;
            case CombatEvent.EventType.TurnEnded:
            case CombatEvent.EventType.TurnStarted:
                ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "<color=red>0</color>";
                e.Consume();
                break;
        }
    }
    void Update() => HealthSpring.Step(Time.deltaTime);

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnNewCombatEvent;
    }
}
