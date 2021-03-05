using System;

namespace ConsoleApp1
{
    using System.Reflection.Metadata;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var dogOne = new GermanShepherd();

            MakeTalk(dogOne);

            dogOne.Weight = 51;

            Console.WriteLine(dogOne.Weight);

            Console.ReadLine();

            var catOne = new Cat();

            MakeTalk(catOne);

            Console.ReadLine();
        }

        static void MakeTalk(IAnimal animal)
        {
            Console.WriteLine(animal.Speak());
        }
    }

    class Animal : IAnimal
    {
        private readonly string _words;
        public Animal(string words)
        {
            this._words = words;
        }

        public virtual string Speak()
        {
            return this._words;
        }

        public int NumberOfLegs { get; set; }
    }

    class Dog : Animal
    {
        public Dog() : base("woof")
        {

        }

        public int Weight
        {
            get;
            set;
        }
    }

    class Cat : Animal
    {

        public Cat() : base("miaow")
        {

        }
        public override string Speak()
        {
            return "Purrrrrr";
        }
    }

    class GermanShepherd : Dog
    {

    }

    public interface IAnimal
    {
        string Speak();
    }
}
