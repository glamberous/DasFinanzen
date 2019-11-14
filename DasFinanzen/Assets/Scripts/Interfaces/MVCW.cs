
using System;

public interface IModel {
    void Save();
    void Delete();
}

public interface IModelCollection {

}

public interface IElement<T> {
    void SetOnClickAction(Action<T> action, T var = default(T));
}

public interface IInputField {
    void SetDisplayText(string input);
}

public interface IWindow {
    IWindow Activate();
    void Deactivate();
    void SetZLayer(float input);
}

public interface IView {
    void Refresh();

    void Activate();
    void Deactivate();
}