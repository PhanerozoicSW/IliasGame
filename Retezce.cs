using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace IliasMatterOfWisdomAndGlory
{
    public class Retezce
    {
        public string Anglicky { get; private set; }
        public string Cesky { get; private set; }
        public enum Jazyk { cz, en };

        public Retezce(string anglicky, string cesky)
        {
            Anglicky = anglicky;
            Cesky = cesky;
        }

        public string Vypis(Jazyk jazyk)
        {
            switch (jazyk)
            {
                case Jazyk.cz:
                    return String.Format(Cesky);

                default:
                case Jazyk.en:
                    return String.Format(Anglicky);
            }
        }
    }
}
