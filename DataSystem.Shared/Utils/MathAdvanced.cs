namespace DataSystem.Shared.Utils
{
    public static class MathAdvanced
    {
        public static int DivideAndRoundUp(double dividend, double divisor)
        {
            double resultOfDivision = MathBasic.DivideNumbers(dividend, divisor);
            int totalPages = (int)Math.Ceiling(resultOfDivision);

            return totalPages;
        }
    }
}
