namespace ConsoleApp1
{
    using Core;

    public class AccessModifiersTwo : AccessModifiers
    {
        public void testMethod()
        {
            base.AddTwoNumbersProtected(4, 5);
        }
    }
}