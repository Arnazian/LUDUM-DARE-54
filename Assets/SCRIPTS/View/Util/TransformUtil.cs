using UnityEngine;

public static class TransformUtil
{
    public static T GetRootComponent<T>(this Component component) where T : Component
    {
        var current = component.GetComponentInParent<T>();
        while (current != null)
        {
            var parent = current.transform.parent?.GetComponentInParent<T>();
            if (parent == null) return current;
            current = parent;
        }
        return null;
    }
}