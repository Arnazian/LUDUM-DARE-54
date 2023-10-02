using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyComponent : MonoBehaviour
{
    public AbstractEnemy enemy;
    [SerializeField] private SpriteRenderer EnemySprite;
    [SerializeField] private SpriteRenderer Hitflash;
    [SerializeField] private Image healthImage;
    BaseSpring HealthSpring;
    Spring.Config springConfig = new(20, 2f);
    [SerializeField] private TMP_Text HealthText;
    [SerializeField] private TMP_Text ActionCooldown;
    [SerializeField] StatusEffectList StatusList;
    [SerializeField] PolygonCollider2D polyCollider;

    private ControlSelectionRotator selectionRotator;
    private EnemyAttackVisuals attackVisuals;
    private EnemyGetHitEffects getHitEffects;

    void Start()
    {
        selectionRotator = GetComponent<ControlSelectionRotator>();
        selectionRotator.DisableRotatorVisuals();
        attackVisuals = GetComponent<EnemyAttackVisuals>();
        getHitEffects = GetComponent<EnemyGetHitEffects>();
        Combat.OnEventLogChanged += OnNewCombatEvent;
        StatusList.Target = enemy;
        HealthText.text = enemy.ReadOnlyHealth.Value.ToString();
        EnemySprite.sprite = Resources.Load<Sprite>($"Enemies/{enemy.GetType().Name}");
        Hitflash.sprite = EnemySprite.sprite;
        var shape = new List<Vector2>();
        EnemySprite.sprite.GetPhysicsShape(0, shape);
        polyCollider.SetPath(0, shape);

        HealthSpring = new(springConfig)
        {
            Position = enemy.ReadOnlyHealth.Normalized,
            RestingPos = enemy.ReadOnlyHealth.Normalized
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
                e.Accept();
                getHitEffects.DoDeathEffects();
                e.Consume();
                break;
            case CombatEvent.EventType.Damaged:
                e.Accept();
                getHitEffects.DoGetHitEffects((int)e.Args[1]);
                HealthText.text = enemy.ReadOnlyHealth.Value.ToString();
                HealthSpring.RestingPos = enemy.ReadOnlyHealth.Normalized;
                e.Consume();
                break;
            case CombatEvent.EventType.TurnEnded:
            case CombatEvent.EventType.TurnStarted:
                // StartCoroutine(CoroutineDoTurn());
                ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "<color=red>0</color>";
                break;
            case CombatEvent.EventType.TakenAction:
                e.Accept();
                StartCoroutine(AnimateAction(e));
                break;
        }
    }

    IEnumerator CoroutineDoTurn()
    {
        float waitBeforeAction = 1f;
        float waitAfterAction = 0.5f;

        selectionRotator.EnableRotatorVisuals();
        yield return new WaitForSeconds(waitBeforeAction);

        // actionCD lowering animation
        yield return new WaitForSeconds(waitAfterAction);
        // if(actionCD > 1)
        {
            // do action
            yield return new WaitForSeconds(waitAfterAction);
            selectionRotator.DisableRotatorVisuals();
        }
        // else
        {
            selectionRotator.DisableRotatorVisuals();
        }
        // end turn
    }
    IEnumerator AnimateAction(CombatEvent e)
    {
        attackVisuals.DoAttackVisuals();
        yield return new WaitForSeconds(0.5f);
        e.Consume();
    }

    void Update() => HealthSpring.Step(Time.deltaTime);

    void OnDestroy()
    {
        Combat.OnEventLogChanged -= OnNewCombatEvent;
    }
}
