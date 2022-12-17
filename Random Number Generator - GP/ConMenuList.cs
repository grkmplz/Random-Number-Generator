using System;
using System.Collections.Generic;
using System.Text;


namespace Project_05_Random_Numbers_Generator
{
    class Console_ML
    {
        public string Labelx { get; set; }
        public Action RunAction { get; set; }
        public Console_ML(string label, Action runaction)
        {
            Labelx = label;
            RunAction = runaction;
        }
        public override string ToString()
        {
            return Labelx;
        }
    }
    class ConMenuClass
    {
        private bool notExit = true;
        private string Description { get; set; }
        private Console_ML[] MenuElements { get; set; }
        public int SelectedItem { get; private set; }
        public ConMenuClass(string desc, Console_ML[] menu_List)
        {
            SelectedItem = 0;
            Description = desc;
            MenuElements = menu_List;
        }
        public void MenuDisplay()
        {
            Console.WriteLine(Description);
            for (int i = 0; i < MenuElements.Length; i++)
            {
                if (i == SelectedItem)
                {
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine(MenuElements[i]);
                Console.ResetColor();
            }
        }
        public void RunMenu()
        {
            while (notExit)
            {
                Console.Clear();
                MenuDisplay();
                ConsoleKeyInfo pressedKey = Console.ReadKey();
                switch (pressedKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        SelectedItem = (SelectedItem - 1 + MenuElements.Length) % MenuElements.Length;
                        break;
                    case ConsoleKey.DownArrow:
                        SelectedItem = (SelectedItem + 1) % MenuElements.Length;
                        break;
                    case ConsoleKey.Enter:
                        if (MenuElements.Length == 0) { break; }
                        MenuElements[SelectedItem].RunAction();
                        break;
                    case ConsoleKey.Escape:
                        notExit = false;
                        break;
                }
            }
        }
    }
}
