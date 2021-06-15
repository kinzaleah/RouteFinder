namespace Core
{
    public class AccessModifiers
    {

        public static int AddTwoNumbersStatic(int a, int b)
        {
            return a + b;
        }

        public int AddTwoNumbersPublic(int a, int b)
        {
            return a + b;
        }

        private int AddTwoNumbersPrivate(int a, int b)
        {
            return a + b;
        }

        protected int AddTwoNumbersProtected(int a, int b)
        {
            return a + b;
        }

        internal int AddTwoNumbersInternal(int a, int b)
        {
            return a + b;
        }

        protected internal int AddTwoNumbersProtectedInternal(int a, int b)
        {
            return a + b;
        }

    }
}