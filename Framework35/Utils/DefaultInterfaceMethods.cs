using System;

namespace Framework35.Utils
{
    public class DefaultInterfaceMethods
    {
        private readonly AbstractDefaultInterface _defaultInterface1 = new DefaultInterface1();
        private readonly AbstractDefaultInterface _defaultInterface2 = new DefaultInterface2();
        public void Execute()
        {
            _defaultInterface1.Exectue();
            _defaultInterface2.Exectue();
            Console.ReadLine();
        }

        private interface IDefaultInterface
        {
            void Vaild();
            void HandleError(Exception ex);
        }

        private abstract class AbstractDefaultInterface : IDefaultInterface
        {
            public abstract void HandleError(Exception ex);
            public abstract void Vaild();
            public virtual void Exectue()
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

        private class DefaultInterface1 : AbstractDefaultInterface
        {
            public override void HandleError(Exception ex)
            {
                Console.WriteLine($"[{nameof(DefaultInterface1)}]{nameof(HandleError)}:{ex.Message}");
            }

            public override void Vaild()
            {
                Console.WriteLine($"[{nameof(DefaultInterface1)}]{nameof(Vaild)}");
            }
        }

        private class DefaultInterface2 : AbstractDefaultInterface
        {
            public override void HandleError(Exception ex)
            {
                Console.WriteLine($"[{nameof(DefaultInterface2)}]{nameof(HandleError)}:{ex.Message}");
            }

            public override void Vaild()
            {
                throw new Exception($"[{nameof(DefaultInterface2)}]{nameof(Vaild)}");
            }
        }
    }
}
