public interface IManager {
    ManagerStatus status { get; }

    void Startup();
}
