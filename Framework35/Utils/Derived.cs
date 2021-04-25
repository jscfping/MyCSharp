using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework35.Utils
{
    public class TestDerived
    {

        public void Execute()
        {
            Console.WriteLine($"=========={nameof(Execute1)}==========");
            Execute1();

            Console.WriteLine($"=========={nameof(Execute2)}==========");
            Execute2();

            Console.WriteLine($"=========={nameof(Execute3)}==========");
            Execute3();

            Console.WriteLine($"=========={nameof(Execute4)}==========");
            Execute4();

            Console.ReadLine();
        }

        private void Execute1()
        {
            Animal aAnimal = new Animal();
            aAnimal.Active();
            // Eat.
            // Animal Sleep.

            Dog aDog = new Dog();
            aDog.Active();
            // Eat.
            // Animal Sleep.

            Human aHuman = new Human();
            aHuman.Active();
            // Eat.
            // Human Sleep.
        }
        private void Execute2()
        {
            Animal aAnimal = new Animal();
            aAnimal.Active();
            // Eat.
            // Animal Sleep.

            Animal aDog = new Dog();
            aDog.Active();
            // Eat.
            // Animal Sleep.

            Animal aHuman = new Human();
            aHuman.Active();
            // Eat.
            // Human Sleep.
        }
        private void Execute3()
        {
            Dog aDog = new Dog();
            aDog.Sleep();
            // Dog Sleep.

            Human aHuman = new Human();
            aHuman.Sleep();
            // Human Sleep.
        }
        private void Execute4()
        {
            Animal aDog = new Dog();
            aDog.Sleep();
            // Animal Sleep.

            Animal aHuman = new Human();
            aHuman.Sleep();
            // Human Sleep.
        }


        private class Animal
        {
            public virtual void Active()
            {
                Console.WriteLine("Eat.");
                Sleep();
            }

            public virtual void Sleep()
            {
                Console.WriteLine("Animal Sleep.");
            }
        }

        class Dog : Animal
        {
            public new void Sleep()
            {
                Console.WriteLine("Dog Sleep.");
            }
        }

        class Human : Animal
        {
            public override void Sleep()
            {
                Console.WriteLine("Human Sleep.");
            }
        }

    }
}

