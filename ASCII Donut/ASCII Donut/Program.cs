using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Numerics;
using ASCII_Donut.NewGraphic;


namespace ASCII_Donut
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Graphic.SetConsoleSize(300, 75);
            Graphic.Draw();
        }
    }
}
