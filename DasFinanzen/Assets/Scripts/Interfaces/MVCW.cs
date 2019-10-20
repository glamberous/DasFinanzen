


public interface IModel {
    void Save();
    void Delete();
}

public interface IModelCollection {

}

public interface IController {

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