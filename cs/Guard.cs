using System;

namespace Utils
{
    public class Guard
    {
        public static void ThrowIf(Boolean condition, Exception exception)
        {
            if(condition)
            {
                throw exception;
            }
        }
        public static void ThrowUnless(Boolean condition, Exception exception)
        {
            ThrowIf(!condition, exception);
        }
        public static void ThrowIfArgumentNull(object arg)
        {
            ThrowIf(arg == null, new ArgumentNullException(nameof(arg)));
        }
    }
}
