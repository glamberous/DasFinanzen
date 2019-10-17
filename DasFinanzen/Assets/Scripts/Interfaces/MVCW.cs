


public interface IModel { }

public interface IController {
    void Close();
}

public interface IWindow {
    IWindow Activate();
    void Deactivate();
}

public interface IView {
    void Refresh();

    void Activate();
    void Deactivate();
}