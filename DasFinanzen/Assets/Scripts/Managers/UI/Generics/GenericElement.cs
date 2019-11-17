
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class GenericElement<T> : MonoBehaviour, IControllerElement<T> {
    protected Action<T> Action;
    protected T Variable;

    public void SetAction(Action<T> action, T var = default(T)) {
        Action = action;
        Variable = var;
    }
}