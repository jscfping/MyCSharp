using System;

namespace WebCore31
{
    public class DefaultInterfaceMethods
    {
        private readonly IDefaultInterface _defaultInterface1 = new DefaultInterface1();
        private readonly IDefaultInterface _defaultInterface2 = new DefaultInterface2();
        public void Execute()
        {
            _defaultInterface1.Exectue();
            _defaultInterface2.Exectue();
            Console.ReadLine();
        }

        private interface IDefaultInterface
        {
            public void Vaild();
            public void HandleError(Exception ex);
            public void Exectue()
            {
                try
                {
                    Vaild();
                }
                catch (Exception ex)
                {
                    HandleError(ex);
                }
            }
        }

        private class DefaultInterface1 : IDefaultInterface
        {
            public void HandleError(Exception ex)
            {
                Console.WriteLine($"[{nameof(DefaultInterface1)}]{nameof(HandleError)}:{ex.Message}");
            }

            public void Vaild()
            {
                Console.WriteLine($"[{nameof(DefaultInterface1)}]{nameof(Vaild)}");
            }
        }

        private class DefaultInterface2 : IDefaultInterface
        {
            public void HandleError(Exception ex)
            {
                Console.WriteLine($"[{nameof(DefaultInterface2)}]{nameof(HandleError)}:{ex.Message}");
            }

            public void Vaild()
            {
                throw new Exception($"[{nameof(DefaultInterface2)}]{nameof(Vaild)}");
            }
        }
    }
}
