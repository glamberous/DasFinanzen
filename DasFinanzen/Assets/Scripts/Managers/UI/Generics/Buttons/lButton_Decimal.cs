


namespace UI {
    public class Button_Decimal : GenericElement<decimal>, IButton<decimal> {
        public void OnMouseUpAsButton() => Action(Variable);
    }
}
