
using System;
using UnityEngine;

public interface IModel {
    void Save();
    void Delete();
}

public interface IModelCollection {

}

public interface IControllerElement<T> {
    void SetAction(Action<T> action, T var = default(T));
}

public interface IInputField<T> : IControllerElement<T> {
    void SetDisplayText(string input);
}

public interface IButton<T> : IControllerElement<T> {
    void OnMouseUpAsButton();
}

public interface IWindow {
    UI.WINDOW GetEnum();
    GameObject GetGameObject();
    IWindow Activate();
    void Deactivate();
    void SetZLayer(float input);
}

public interface IView {
    void Refresh();

    void Activate();
    void Deactivate();
}