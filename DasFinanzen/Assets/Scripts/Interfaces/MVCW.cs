


public interface IModel { }

public interface IController { }

public interface IWindow {
    void Refresh();

    IWindow Activate();
    void Deactivate();
}

public interface IView {
    void Refresh();

    void Activate();
    void Deactivate();
}