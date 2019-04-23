using System;

namespace PlaygroundApps
{
    public class ProgramUI
    {
        public ProgramUI()
        {
            //var num = Convert.ToInt32(null);
        }

        public void Run()
        {
            while (Menu()) { Console.ReadLine(); }
        }

        private bool Menu()
        {
            //var input = Console.ReadKey().KeyChar.ToString();
            //if (input.IsValidInt())
            //{
            //    var selection = int.Parse(input);
            //    Console.WriteLine(selection);
            //}
            //else Console.WriteLine("Invalid input.");

            var enumGuy = MyEnumThing.Long_Sword;
            Console.WriteLine(enumGuy.ReplaceUnderscoreWithSpace());

            return true;
        }
    }

    public enum MyEnumThing { Hi = 1, Mom = 2, Im = 3, On = 4, TV = 5, Long_Sword = 6 }

    public static class ExtensionMethods
    {
        public static string ReplaceUnderscoreWithSpace(this MyEnumThing e)
        {
            var enumName = e.ToString();

            if (enumName.Contains("_"))
                enumName = enumName.Replace('_', ' ');

            return enumName;
        }

        public static bool IsValidInt(this string s)
        {
            if (int.TryParse(s, out _))
                return true;
            return false;
        }
    }
}