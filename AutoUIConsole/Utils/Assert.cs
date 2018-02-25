
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AutoUIConsole.Utils
{
    public class Assert
    {
        public static bool IsNotNull(object objectToValidate, string errorMsg)
        {
            try
            {
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsNotNull(objectToValidate, errorMsg);
            }
            catch (AssertFailedException ex)
            {
                Helper.Log("AssertionError",ex);
                return false;
            }
            return true;
        }
    }
}