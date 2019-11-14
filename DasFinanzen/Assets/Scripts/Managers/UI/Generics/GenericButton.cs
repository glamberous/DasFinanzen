
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GenericButton<T> : MonoBehaviour, IElement<T> {
    private Action<T> OnClickAction;
    private T Variable;

    public void SetOnClickAction(Action<T> action, T var = default(T)) {
        OnClickAction = action;
        Variable = var;
    }

    public virtual void OnMouseUpAsButton() => OnClickAction(Variable);
}