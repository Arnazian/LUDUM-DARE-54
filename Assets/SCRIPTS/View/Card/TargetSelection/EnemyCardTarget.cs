using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyCardTarget : MonoBehaviour, ICardTarget
{
    EnemyComponent cached_enemy;
    EnemyComponent Enemy => cached_enemy ??= GetComponent<EnemyComponent>();
    public void OnDrop(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractEnemy)) return;
        ICardTarget.Select(Enemy.enemy);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractEnemy)) return;
        ICardTarget.Select(Enemy.enemy);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractEnemy)) return;
        ICardTarget.InvokeEnter(Camera.main.WorldToScreenPoint(transform.position));
        Debug.Log(transform.position);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (ICardTarget.CurrentSelectionType != typeof(AbstractEnemy)) return;
        ICardTarget.InvokeExit();
    }
}
