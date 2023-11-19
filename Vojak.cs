using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace IliasMatterOfWisdomAndGlory
{
    public class Vojak
    {
        // Základní vlastnosti vojáka
        public enum TypyVojaku { ReckyLehky, ReckyHoplita, TrojskyLehky, TrojskyHoplita, ReckyAchilles };
        public TypyVojaku TypTohotoVojaka { get; private set; }
        public enum StranyKonfliktu { Rekove, Trojane };
        public StranyKonfliktu VernostVojaka { get; private set; }
        public int MaxZdravi { get; private set; }
        public int Zdravi { get; private set; }
        public int Utok { get; private set; }
        public float PoziceX { get; private set; }
        public float PoziceY { get; private set; }
        public float CilPohybuX { get; private set; }
        public float CilPohybuY { get; private set; }
        public enum CinnostiRozkazy { Cekej, JdiDoUtoku, Utoc, Zhyn };
        public CinnostiRozkazy CoDelaRozkazy { get; private set; }

        // Animace vojáka
        public enum CinnostiAnimace { StojiOtocenVpravo, StojiOtocenVlevo, JdeNahoru, JdeVlevo, JdeDolu, JdeVpravo, UtociNahoru, UtociVlevo, UtociDolu, UtociVpravo, Hyne };
        public CinnostiAnimace CoDelaAnimace { get; private set; }
        public int PocitadloAnimace { get; private set; }
        public Random Nahoda { get; private set; }

        public Vojak(TypyVojaku typ, float poziceX, float poziceY) 
        {
            // Zadané vlastnosti
            TypTohotoVojaka = typ;
            CilPohybuX = PoziceX = poziceX;
            CilPohybuY = PoziceY = poziceY;
            CoDelaRozkazy = CinnostiRozkazy.Cekej;
            PocitadloAnimace = 0;

            Nahoda = new Random();

            // Vlastnosti dle typu
            switch (TypTohotoVojaka)
            {
                case TypyVojaku.ReckyLehky:
                    VernostVojaka = StranyKonfliktu.Rekove;
                    CoDelaAnimace = CinnostiAnimace.StojiOtocenVpravo;
                    MaxZdravi = 400;
                    Utok = 4;
                    break;

                case TypyVojaku.ReckyHoplita:
                    VernostVojaka = StranyKonfliktu.Rekove;
                    CoDelaAnimace = CinnostiAnimace.StojiOtocenVpravo;
                    MaxZdravi = 1000;
                    Utok = 9;
                    break;

                case TypyVojaku.ReckyAchilles:
                    VernostVojaka = StranyKonfliktu.Rekove;
                    CoDelaAnimace = CinnostiAnimace.StojiOtocenVpravo;
                    MaxZdravi = 4000;
                    Utok = 16;
                    break;

                case TypyVojaku.TrojskyLehky:
                    VernostVojaka = StranyKonfliktu.Trojane;
                    CoDelaAnimace = CinnostiAnimace.StojiOtocenVlevo;
                    MaxZdravi = 750;
                    Utok = 6;
                    break;

                case TypyVojaku.TrojskyHoplita:
                    VernostVojaka = StranyKonfliktu.Trojane;
                    CoDelaAnimace = CinnostiAnimace.StojiOtocenVlevo;
                    MaxZdravi = 1450;
                    Utok = 8;
                    break;
            }

            // Zdraví začíná na maximu
            Zdravi = MaxZdravi;
        }

        public void ZranVojaka(int zraneni)
        {
            Zdravi -= zraneni;

            // Kdyby zemřel
            if (Zdravi <= 0)
            {
                Zdravi = 0;

                CoDelaRozkazy = CinnostiRozkazy.Zhyn;
                CoDelaAnimace = CinnostiAnimace.Hyne;
            }

            // Toto je zde z důvodu, že metoda se dá pomocí záporné hodnoty použít i k léčení
            if (Zdravi > MaxZdravi)
            {
                Zdravi = MaxZdravi;
            }
        }

        public void ZmenAnimacniCinnost(CinnostiAnimace novaAnimacniCinnost)
        {
            CoDelaAnimace = novaAnimacniCinnost;
        }

        public void PosunVojakaX(float oKolik)
        {
            PoziceX += oKolik;
        }

        public void PosunVojakaY(float oKolik)
        {
            PoziceY += oKolik;
        }

        public void ZmenRozkazy(CinnostiRozkazy noveRozkazy, float cilX, float cilY)
        {
            CoDelaRozkazy = noveRozkazy;

            if (CoDelaRozkazy != CinnostiRozkazy.Cekej && CoDelaRozkazy != CinnostiRozkazy.Zhyn)
            {
                CilPohybuX = cilX;
                CilPohybuY = cilY;
            }
        }

        public void Vzkriseni()
        {
            if (VernostVojaka == StranyKonfliktu.Rekove)
            {
                PoziceX = -50;
            }
            else
            {
                if (Nahoda.Next(1, 6) == 3)
                {
                    PoziceX = -50;
                }
                else
                {
                    PoziceX = 1970;
                }
            }

            PoziceY = Nahoda.Next(400, 920);

            Zdravi = MaxZdravi;

            CoDelaRozkazy = CinnostiRozkazy.Cekej;
            CoDelaAnimace = CinnostiAnimace.StojiOtocenVlevo;
        }

        public void PosunPocitadloAnimace()
        {
            PocitadloAnimace++;

            if (PocitadloAnimace > 4 && CoDelaAnimace == CinnostiAnimace.Hyne)
            {
                PocitadloAnimace = 4;

                if (Nahoda.Next(1, 5) == 2)
                {
                    Vzkriseni();
                }
            }

            if (PocitadloAnimace > 4 && (CoDelaAnimace == CinnostiAnimace.UtociNahoru 
                || CoDelaAnimace == CinnostiAnimace.UtociDolu 
                || CoDelaAnimace == CinnostiAnimace.UtociVlevo 
                || CoDelaAnimace == CinnostiAnimace.UtociVpravo))
            {
                PocitadloAnimace = 0;
            }

            if (PocitadloAnimace > 8)
            {
                PocitadloAnimace = 0;
            }
        }
    }
}
