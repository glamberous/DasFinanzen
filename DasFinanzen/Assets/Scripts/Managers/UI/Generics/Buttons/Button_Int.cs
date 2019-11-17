

namespace UI {
    public class Button_Int : GenericElement<int>, IButton<int> {
        public virtual void OnMouseUpAsButton() => Action(Variable);
    }
}

