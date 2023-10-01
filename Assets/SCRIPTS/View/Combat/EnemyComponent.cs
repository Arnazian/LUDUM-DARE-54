using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : MonoBehaviour
{
    public AbstractEnemy enemy;
    [SerializeField] private SpriteRenderer EnemySprite;
    [SerializeField] private Image Healthbar;
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text ActionCooldown;

    private EnemyGetHitEffects getHitEffects;

    void Start()
    {
        getHitEffects = GetComponent<EnemyGetHitEffects>();
        GameSession.ActiveCombat.OnEventLogChanged += OnNewCombatEvent;
        HealthText.text = enemy.ReadOnlyHealth.Value.ToString();
        Healthbar.fillAmount = enemy.ReadOnlyHealth.Normalized;
        ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "";
    }
    private void OnNewCombatEvent(CombatEvent e)
    {
        switch (e.Type)
        {
            case CombatEvent.EventType.Killed:
                if (e.Args[0] != enemy) break;
                Destroy(gameObject); //death animation
                e.Consume();
                break;
            case CombatEvent.EventType.Damaged:
                if (e.Args[0] != enemy) break;
                getHitEffects.DoGetHitEffects();
                HealthText.text = enemy.ReadOnlyHealth.Value.ToString();
                Healthbar.fillAmount = enemy.ReadOnlyHealth.Normalized;
                e.Consume();
                break;
            case CombatEvent.EventType.EnemyTurnStarted:
                ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "";
                e.Consume();
                break;

        }


    }
    void OnDestroy()
    {
        if (GameSession.ActiveCombat != null)
            GameSession.ActiveCombat.OnEventLogChanged -= OnNewCombatEvent;
    }
}
