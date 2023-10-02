using System.Collections;
using System.Collections.Generic;
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

    private ActionCoolDownVisuals cdVisuals;
    private ControlSelectionRotator selectionRotator;
    private EnemyAttackVisuals attackVisuals;
    private EnemyGetHitEffects getHitEffects;

    void Start()
    {
        cdVisuals = GetComponent<ActionCoolDownVisuals>();
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
                // StartCoroutine(CoroutineDoTurn());
                break;

            case CombatEvent.EventType.TurnStarted:
                e.Accept();
                StartCoroutine(CoroutineDoTurn(e));                
                break;
            case CombatEvent.EventType.CooldownChanged:
                //Cooldown update here
                break;
            case CombatEvent.EventType.TakenAction:
                e.Accept();
                StartCoroutine(AnimateAction(e));
                break;
        }
    }

    IEnumerator CoroutineDoTurn(CombatEvent e)
    {
        float waitBeforeAction = 0.5f;
        float waitAfterAction = 0.25f;

        selectionRotator.EnableRotatorVisuals();
        yield return new WaitForSeconds(waitBeforeAction);

        cdVisuals.AnimatedCoolDownChange();
        yield return new WaitForSeconds(0.5f);
        ActionCooldown.text = enemy.ReadOnlyActCooldown.Value > 0 ? enemy.ReadOnlyActCooldown.Value.ToString() : "<color=red>0</color>";
        yield return new WaitForSeconds(waitAfterAction);
        // if(actionCD > 1)
        {
            // do action
            yield return new WaitForSeconds(waitAfterAction);
            selectionRotator.DisableRotatorVisuals();
            e.Consume();
        }
        // else
        {
            selectionRotator.DisableRotatorVisuals();
            e.Consume();
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
