


namespace UI {
    public class Button_String : GenericElement<string>, IButton<string> {
        public void OnMouseUpAsButton() => Action(Variable);
    }
}
