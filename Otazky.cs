using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace IliasMatterOfWisdomAndGlory
{
    public class Otazky
    {
        public string AnglickyOtazka { get; private set; }
        public string CeskyOtazka { get; private set; }
        public string AnglickyOdpoved { get; private set; }
        public string CeskyOdpoved { get; private set; }
        public int Cislo { get; private set; }
        public enum Jazyk { cz, en };

        public Otazky(string anglickyOtazka, string ceskyOtazka, string anglickyOdpoved, string ceskyOdpoved, int cislo) 
        { 
            AnglickyOtazka = anglickyOtazka;
            CeskyOtazka= ceskyOtazka;
            AnglickyOdpoved = anglickyOdpoved;
            CeskyOdpoved = ceskyOdpoved;
            Cislo = cislo;
        }
    }
}
