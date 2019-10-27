


public interface IModel {
    void Save();
    void Delete();
}

public interface IModelCollection {

}

public interface IController {
    void TriggerCommand(int commandID, string input = null);
}

public interface IControllerElement {
    void SetController(IController controller);
    void SetCommandID(int commandID);
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