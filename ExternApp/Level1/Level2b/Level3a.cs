using ExternApp.Level1.Level2a;
using System;

namespace ExternApp.Level1.Level2b
{
    public class Level3a
    {
        public void Level4a()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void Level4b()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }

        public void Level4c()
        {
            Console.WriteLine(this.GetType().FullName + " - " + Helpers.ExecutedMethod);
        }
    }
}
