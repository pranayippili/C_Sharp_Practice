namespace CSharpExpressions
{
    public class Button
    {
        public event Action Click;

        public void SimulateClick()
        {
            Console.WriteLine("Button was clicked!");
            Click?.Invoke();
		}
    }
}