using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface ICardTarget : IDropHandler, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

    public static Type CurrentSelectionType;

    public static void DoSelection(Vector2 cursorOrigin, Type targetType)
    {
        CurrentSelectionType = targetType;
        OnBeginSelection?.Invoke(cursorOrigin);
    }
    public static event Action<Vector2> OnBeginSelection; //param: start position
    public static event Action<object> OnFinishSelection;


    public static event Action<Vector2> OnEnter;
    public static event Action OnExit;

    protected static void Select(object obj)
    {
        OnFinishSelection?.Invoke(obj);
        CurrentSelectionType = null;
    }
    protected static void InvokeEnter(Vector2 centerPos) => OnEnter?.Invoke(centerPos);
    protected static void InvokeExit() => OnExit?.Invoke();

}
