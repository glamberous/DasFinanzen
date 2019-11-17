

namespace UI {
    public class Button_Void : GenericElement<Void>, IButton<Void> {
        public void OnMouseUpAsButton() => Action(Variable);
    }
}

