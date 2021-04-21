using System;
using System.Collections.Generic;
using System.Text;

namespace Bussen
{
    class Passagerare
    {
       public int ålder;
       public string kön;
       public string humör;

        //Konstruktor av classen för att tilläggningen av passagerare.

        public Passagerare(int _ålder, string _kön, string _humör)
        {
            ålder = _ålder;
            kön = _kön;
            humör = _humör;
        }
        public void Interagera()//Behöver inte ta med åldern i beteendet då åldern bestämmer om det är en pojke eller man exempelvis.
        {            
            if(kön == "pojke" && humör == "glad")
            {
                Console.WriteLine("Den glada pojken skrattar lågmält.");
            }
            else if(kön == "pojke" && humör == "sur")
            {
                Console.WriteLine("Den sura pojken grimaserar bittert.");
            }
            else if (kön == "flicka" && humör == "glad")
            {
                Console.WriteLine("Den glada flickan fnissar.");
            }
            else if (kön == "flicka" && humör == "sur")
            {
                Console.WriteLine("Den sura flickan grinar.");
            }
            else if (kön == "man" && humör == "glad")
            {
                Console.WriteLine("Den glada mannen startar en livlig diskussion.");
            }
            else if (kön == "man" && humör == "sur")
            {
                Console.WriteLine("Den sura mannen ignorerar petandet.");
            }
            else if (kön == "kvinna" && humör == "glad")
            {
                Console.WriteLine("Den glada kvinnan börjar prata om vädret.");
            }
            else
            {
                Console.WriteLine("Den sura kvinnan blir förnärmad och utbrister avsky.");
            }
        }

    }
}
