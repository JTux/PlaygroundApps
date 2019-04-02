using System;

namespace PlaygroundApps
{
    public class ProgramUI
    {
        public ProgramUI()
        {
        }

        public void Run()
        {
            while(Menu()) { }
        }

        private bool Menu()
        {
            Console.WriteLine("Cats > Birds");
            return true;
        }
    }
}