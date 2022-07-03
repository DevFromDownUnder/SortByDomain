using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SortByDomain.Helpers
{
    public class FunctionHelper
    {
        public static void ConsumeException(Action action)
        {
            try
            {
                action();
            }
            catch { }
        }

        public static void ConsumeFinalReleaseNullComObject(object o)
        {
            ConsumeException(() => FinalReleaseNullComObject(o));
        }

        public static void ConsumeReleaseNullComObject(object o)
        {
            ConsumeException(() => ReleaseNullComObject(o));
        }

        public static void FinalReleaseNullComObject(object o)
        {
            if (o != null)
            {
                Marshal.FinalReleaseComObject(o);

                if (o is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            //Need to force collection
            GC.Collect();
        }

        public static void ReleaseNullComObject(object o)
        {
            if (o != null)
            {
                Marshal.ReleaseComObject(o);

                if (o is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }

            //Need to force collection
            GC.Collect();
        }

        public static void Log(string message, [CallerMemberName] string name = "")
        {
            Console.WriteLine($"[{name}] - {message}");
        }
    }
}