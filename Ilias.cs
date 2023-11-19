using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using System;

namespace IliasMatterOfWisdomAndGlory
{
    /* 
         
         ILIAS: MATTER OF WISDOM AND GLORY

         Phanerozoic Games
         Jakub Raida
         2023
     
    */ 
    public class Ilias : Game
    {
        /* 
             
             Zde budou všechny globální atributy/proměnné
                
                   ↓               ↓               ↓
         
        */

        // Základní proměnné pro grafiku
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        // Vstup
        public KeyboardState Ovladani_Klavesnice { get; private set; }
        public MouseState Ovladani_Mysi { get; private set; }
        public Keys AktivniKlavesa { get; private set; }
        public int ProtiPretizeniKlaves { get; private set; }
        public int ProtiPretizeniMysi { get; private set; }

        // Stavy hry
        public enum StavHry { Uvod, HraJede, ZaverVyhra, ZaverProhra, Informace };
        public StavHry AktualniStavHry { get; private set; }
        public enum Jazyk { en, cz };
        public Jazyk JazykHry { get; private set; }

        // Herní proměnné
        public int Drachmy { get; private set; }
        public int Skore { get; private set; }
        public int CiloveDrachmy { get; private set; }
        public int CiloveSkore { get; private set; }
        public int Pocitadlo { get; private set; }
        public int PomocneCislo { get; private set; }
        public bool OtazkaPolozena { get; private set; }
        public int PoziceSpravneOdpovedi { get; private set; }
        public int[] CislaOtazek { get; private set; }
        public int CenaZaOdpoved { get; private set; }
        public int CasovacProUdalosti { get; private set; }
        public int CasovacProSavlicku { get; private set; }
        public int OdpocetDoPristyOtazky { get; private set; }
        public bool PosledniSpravne { get; private set; }
        public string SvisleCarky { get; private set; }
        public List<Vojak> Vojaci { get; private set; }
        public int VybranyVojakCislo { get; private set; }
        public float DelkaSvihu { get; private set; }
        public bool MaPredSebouSoupere { get; private set; }
        public int ZdraviIndex { get; private set; }
        public int ReckeRezervy { get; private set; }
        public int TrojskeRezervy { get; private set; }
        public int RychlostPochodu { get; private set; }
        public int CenaLeceni { get; private set; }
        public int CenaHnevu { get; private set; }
        public int MoznoLeceni { get; private set; }
        public int MoznoHnevu { get; private set; }
        public int PocitadloZasahu { get; private set; }
        public int PocitadloVarovaniOZdrojich { get; private set; }
        public int BonusZaVitezstvi { get; private set; }
        public int BojujeSe { get; private set; }

        // Rozměry okna
        public int SirkaOkna { get; private set; }
        public int VyskaOkna { get; private set; }

        // Grafika pozadí
        public Texture2D Gfx_PozadiPlaz { get; private set; }
        public enum VolbaPozadi { Plaz, TemnaPlaz };
        public VolbaPozadi AktualniPozadi { get; private set; }
        public Texture2D Gfx_UvodniCZ { get; private set; }
        public Texture2D Gfx_UvodniEN { get; private set; }
        public Texture2D Gfx_PozadiTemnaPlaz { get; private set; }
        public Texture2D Gfx_PozadiTemnaPlaz_Vyhra { get; private set; }
        public Texture2D Gfx_PozadiTemnaPlaz_Prohra { get; private set; }

        // Další grafika
        public Texture2D Gfx_ZaTextem { get; private set; }
        public Texture2D[] Gfx_UkazatelZdravi { get; private set; }

        // Písma
        public SpriteFont Pismo_Normalni { get; private set; }
        public SpriteFont Pismo_Mensi { get; private set; }
        public Vector2 VelikostRameckuZaTextem { get; private set; }
        public Rectangle PozadiZaTextem { get; private set; }
        public int OdsazeniRameckuZaTextem { get; private set; }

        // Řetězce
        public Retezce Text_Drachmy { get; private set; }
        public Retezce Text_Skore { get; private set; }
        public Retezce Text_Kdo { get; private set; }
        public Retezce Text_Jazyk { get; private set; }
        public Retezce Text_Spravne { get; private set; }  
        public Retezce Text_Spatne { get; private set; }
        public Retezce Text_CasDoOtazky { get; private set; }
        public Retezce Text_ReckeRezervy { get; private set; }
        public Retezce Text_TrojskeRezervy { get; private set; }
        public Retezce Text_SeslatLeceni { get; private set; }
        public Retezce Text_SeslatHnev { get; private set; }
        public Retezce Text_SeslatLeceniKompakt { get; private set; }
        public Retezce Text_SeslatHnevKompakt { get; private set; }
        public Retezce Text_Drachem { get; private set; }
        public Retezce Text_MoznychSeslani { get; private set; }
        public Retezce Text_NedostatecneZdroje { get; private set; }
        public Retezce Text_VyhralJsi { get; private set; }
        public Retezce Text_ProhralJsi { get; private set; }
        public Retezce Text_TveSkoreJe { get; private set; }
        public Retezce Text_StiskniR { get; private set; }
        public Retezce Text_Credits_VrchniRadek { get; private set; }
        public Retezce Text_Credits_Akryl { get; private set; }
        public Retezce Text_Credits_AnimaceVojaku { get; private set; }
        public Retezce Text_Credits_ZvlastniGrafickeFX { get; private set; }
        public Retezce Text_Credits_ZvlastniZvukoveFX { get; private set; }
        public Retezce Text_Credits_HudbaFireThunder { get; private set; }
        public Retezce Text_Credits_HudbaMinotaur { get; private set; }
        public Retezce Text_Credits_ObrazekAchilles { get; private set; }
        public Retezce Text_Credits_ObrazekAthena { get; private set; }
        public Retezce Text_Credits_SpodniRadek { get; private set; }

        // Otázky
        public List<Otazky> Souhrn_Otazek { get; private set; }

        // Náhoda
        public Random Nahoda { get; private set; }
        
        // Animace
        public Texture2D SpriteS_TrojskyLehky { get; private set; }
        public Texture2D SpriteS_TrojskyHoplita { get; private set; }
        public Texture2D SpriteS_ReckyLehky { get; private set; }
        public Texture2D SpriteS_ReckyHoplita { get; private set; }
        public Texture2D SpriteS_ReckyAchilles { get; private set; }
        public Texture2D Efekt_Sekani { get; private set; }
        public Texture2D Efekt_Obvazy { get; private set; }
        public Rectangle ObdelnikProAnimaci { get; private set; }
        public int PoziceFramuX { get; private set; }
        public int PoziceFramuY { get; private set; }
        public int SirkaFramu { get; private set; }
        public int VyskaFramu { get; private set; }
        public int PoziceVarovaniOZdrojichX { get; private set; }
        public int PoziceVarovaniOZdrojichY { get; private set; }
        public float ZvlastniEfekty_PoziceX { get; private set; }
        public float ZvlastniEfekty_PoziceY { get; private set; }
        public int ZvlastniEfekty_Pocitadlo { get; private set; }
        public enum TypEfektu { Nic, Leceni, Zranovani };
        public TypEfektu ZvlastniEfekty_Typ { get; private set; }

        // Zvukové efekty
        public SoundEffect Zvuk_SlabySek { get; private set; }
        public SoundEffect Zvuk_AtheninSek { get; private set; }
        public SoundEffect Zvuk_Lecba { get; private set; }
        public Song Hudba_FireThunder { get; private set; }
        public Song Hudba_Minotaur { get; private set; }

        /* 
             
             Zde začínají metody hry
                
                ↓               ↓
         
        */

        public Ilias()
        {
            // Rozměry okna
            SirkaOkna = 1920;
            VyskaOkna = 1080;

            // Nastavení grafiky
            _graphics = new GraphicsDeviceManager(this);

            // Cesta ke složce s grafikou, zvuky apod.
            Content.RootDirectory = "Content";

            // Nastavení kurzoru myši
            IsMouseVisible = true;
        }

        /// <summary>
        /// Připraví hru na začátku.
        /// </summary>
        protected override void Initialize()
        {
            // Nastavení výchozích proměnných
            NastavPromenneNaZacatekHry();

            // Nastavení grafiky
            _graphics.PreferredBackBufferWidth = SirkaOkna;
            _graphics.PreferredBackBufferHeight = VyskaOkna;
            _graphics.IsFullScreen = true;
            _graphics.ApplyChanges();

            // Inicializování hry
            base.Initialize();
        }

        protected override void LoadContent()
        {
            // Nastavení grafiky
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // Založení grafiky pozadí
            ZalozSpritePozadi();
            ZalozSprityVojakuAEfektu();
            ZalozPisma();
            ZalozRetezce();
            ZalozOtazky();
            ZalozVojaky();
            ZalozUkazateleZdravi();
            ZalozZvuky();

            // Hudba pro intro na začátku hry
            PrepniHudbu(Hudba_FireThunder);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Spustí se metoda, která zkontroluje, jaký je stav hry, podle toho spustí další metody
            KontrolaHernihoStavu();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // Podkladová barva (na málo širokých nebo moc vysokých monitorech dole černá)
            GraphicsDevice.Clear(Color.Black);

            // Tady se začínají vkládat sprity do herní plochy.
            _spriteBatch.Begin();

            // Vykreslování hry
            KresliHru();

            // Tady se končí se vkládání spritů.
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /*
        
            ####################
            ####################
            ####################
            ####################


            Inicializační a nahrávací metody

                ↓       ↓       ↓

        */

        /// <summary>
        /// Založí zvukové efekty
        /// </summary>
        public void ZalozZvuky()
        {
            Zvuk_SlabySek = Content.Load<SoundEffect>(@"Zvuky\mec-bezny");
            Zvuk_AtheninSek = Content.Load<SoundEffect>(@"Zvuky\mec-athena");
            Zvuk_Lecba = Content.Load<SoundEffect>(@"Zvuky\heal");
            Hudba_FireThunder = Content.Load<Song>(@"Zvuky\fire-thunder");
            Hudba_Minotaur = Content.Load<Song>(@"Zvuky\minotaur");

            // Opakování hudby
            MediaPlayer.IsRepeating = true;
        }

        /// <summary>
        /// Založí grafiku pozadí.
        /// </summary>
        public void ZalozSpritePozadi()
        {
            // Načteme akrylem malovaný obrázek s trójskou pláží
            Gfx_PozadiPlaz = Content.Load<Texture2D>(@"Grafika\trojska-plaz-orez-siroke");
            Gfx_PozadiTemnaPlaz = Content.Load<Texture2D>(@"Grafika\trojska-plaz-orez-siroke-temne");
            Gfx_PozadiTemnaPlaz_Vyhra = Content.Load<Texture2D>(@"Grafika\trojska-plaz-orez-siroke-temne-victory");
            Gfx_PozadiTemnaPlaz_Prohra = Content.Load<Texture2D>(@"Grafika\trojska-plaz-orez-siroke-temne-defeat");
            Gfx_ZaTextem = Content.Load<Texture2D>(@"Grafika\bile-pozadi-za-text");
            
            // Načteme úvodní obrazovky
            Gfx_UvodniCZ = Content.Load<Texture2D>(@"Grafika\uvodni-obrazovka-cz");
            Gfx_UvodniEN = Content.Load<Texture2D>(@"Grafika\uvodni-obrazovka-en");
        }

        /// <summary>
        /// Založí grafiku pro zobrazování zdraví vojáků
        /// </summary>
        public void ZalozUkazateleZdravi()
        {
            Gfx_UkazatelZdravi = new Texture2D[10];

            Gfx_UkazatelZdravi[0] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-1");
            Gfx_UkazatelZdravi[1] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-2");
            Gfx_UkazatelZdravi[2] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-3");
            Gfx_UkazatelZdravi[3] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-4");
            Gfx_UkazatelZdravi[4] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-5");
            Gfx_UkazatelZdravi[5] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-6");
            Gfx_UkazatelZdravi[6] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-7");
            Gfx_UkazatelZdravi[7] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-8");
            Gfx_UkazatelZdravi[8] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-9");
            Gfx_UkazatelZdravi[9] = Content.Load<Texture2D>(@"Grafika\ukazatel-zdravi-10");
        }

        /// <summary>
        /// Sprite sheety pro vojáky
        /// </summary>
        public void ZalozSprityVojakuAEfektu()
        {
            // Načteme sprite sheety pro různé typy vojáků
            SpriteS_ReckyLehky = Content.Load<Texture2D>(@"Grafika\velke-sprity-recky-lehky");
            SpriteS_ReckyHoplita = Content.Load<Texture2D>(@"Grafika\velke-sprity-recky-hoplita");
            SpriteS_TrojskyLehky = Content.Load<Texture2D>(@"Grafika\velke-sprity-trojsky-lehky");
            SpriteS_TrojskyHoplita = Content.Load<Texture2D>(@"Grafika\velke-sprity-trojsky-hoplita");
            SpriteS_ReckyAchilles = Content.Load<Texture2D>(@"Grafika\velke-sprity-recky-achilles");

            // Načteme sprity pro zvláštní efekty
            Efekt_Obvazy = Content.Load<Texture2D>(@"Grafika\bandages");
            Efekt_Sekani = Content.Load<Texture2D>(@"Grafika\athena-slash");
        }

        /// <summary>
        /// Vytvoří popiskové řetězce ve dvou jazycích
        /// </summary>
        public void ZalozRetezce()
        {
            Text_Drachmy = new Retezce("Drachmae", "Drachmy");
            Text_Skore = new Retezce("Score", "Skóre");
            Text_Kdo = new Retezce("Who is", "Kdo je");
            Text_Jazyk = new Retezce("[L]: English", "[L]: Čeština");
            Text_Spravne = new Retezce("Right answer!", "Správná odpověď!");
            Text_Spatne = new Retezce("Wrong answer.", "Špatná odpověď!");
            Text_CasDoOtazky = new Retezce("Countdown to next question", "Odpočet do další otázky");
            Text_ReckeRezervy = new Retezce("Greek troops", "Řecké zálohy");
            Text_TrojskeRezervy = new Retezce("Trojan troops", "Trójské zálohy");
            Text_SeslatHnev = new Retezce("Left-click on enemy = Athena's Wrath (damage)", "Levý klik na nepřítele = Hněv Athény (poškození)");
            Text_SeslatLeceni = new Retezce("Left-click on ally = Field bandages (heal)", "Levý klik na spojence = Polní bandáže (léčení)");
            Text_SeslatHnevKompakt = new Retezce("Wrath (dmg)", "Hněv (zraň.)");
            Text_SeslatLeceniKompakt = new Retezce("Bandage (heal)", "Obvazy (léč.)");
            Text_Drachem = new Retezce("drachmae", "drachem");
            Text_MoznychSeslani = new Retezce("remaining", "zbývá");
            Text_NedostatecneZdroje = new Retezce("Not enough drachmae!", "Nedostatek drachem!");
            Text_ProhralJsi = new Retezce("Greeks lost. Athena is disappointed.", "Řekové prohráli. Athéna je zklamaná.");
            Text_VyhralJsi = new Retezce("Greeks won and took control of Troy! (+500 Score)", "Řekové vyhráli a získali kontrolu nad Trójou! (+500 bodů)");
            Text_TveSkoreJe = new Retezce("Your score is", "Tvé skóre je");
            Text_StiskniR = new Retezce("Press R to restart game (or Esc to end it).", "Stiskni R pro restart hry (nebo Esc pro konec).");
            Text_Credits_VrchniRadek = new Retezce("Credits for graphics and sounds. More detailed with direct links on game webpage.", "Zdroje grafiky a zvuků. Více detailní a s přesnými odkazy na stránce hry.");
            Text_Credits_Akryl = new Retezce("Background of the game: my acrylic on canvas", "Pozadí hry: můj akryl na plátně");
            Text_Credits_AnimaceVojaku = new Retezce("Spritesheets of troops: https://sanderfrenken.github.io/Universal-LPC-Spritesheet-Character-Generator/", "Spritesheety vojáků: https://sanderfrenken.github.io/Universal-LPC-Spritesheet-Character-Generator/");
            Text_Credits_ZvlastniGrafickeFX = new Retezce("Graphical effects (slash, heal): https://www.pngegg.com/", "Grafické efekty (sek, náplast): https://www.pngegg.com/");
            Text_Credits_ZvlastniZvukoveFX = new Retezce("Sound effects: https://pixabay.com/sound-effects/", "Zvukové efekty: https://pixabay.com/sound-effects/");
            Text_Credits_HudbaFireThunder = new Retezce("Music #1: https://bit.ly/fire-and-thunder-song", "Hudba #1: https://bit.ly/fire-and-thunder-song");
            Text_Credits_HudbaMinotaur = new Retezce("Music #2: https://bit.ly/the-army-minotaur-song", "Hudba #2: https://bit.ly/the-army-minotaur-song");
            Text_Credits_ObrazekAchilles = new Retezce("Picture of Achilles: https://commons.wikimedia.org/wiki/", "Obrázek Achillea: https://commons.wikimedia.org/wiki/");
            Text_Credits_ObrazekAthena = new Retezce("Picture of Athena: http://www.shutterstock.com/pic-101997016/", "Obrázek Athény: http://www.shutterstock.com/pic-101997016/");
            Text_Credits_SpodniRadek = new Retezce("Thanks to the authors of all the resources. The full embellishments are on the game's website. Press [SPACE] to return to the intro.", "Děkuji autorům všech zdrojů. Úplné ozdrojování je na webu hry. Stiskněte [MEZERNÍK] pro návrat do intra.");
        }

        /// <summary>
        /// Vytvoří používaná písma
        /// </summary>
        public void ZalozPisma()
        {
            // Načteme fonty
            Pismo_Normalni = Content.Load<SpriteFont>(@"Fonty\infoText");
            Pismo_Mensi = Content.Load<SpriteFont>(@"Fonty\mensiText");
        }

        /// <summary>
        /// Vytvoří testové otázky
        /// </summary>
        public void ZalozOtazky()
        {
            Souhrn_Otazek = new List<Otazky>()
            {
                new Otazky("Athena", "Athéna", "Deity of Wisdom and War, offspring of Zeus", "Bohyně moudrosti a války, dcera Dia", 0),
                new Otazky("Ares", "Arés", "Deity of War, Violence and Rage, offspring of Zeus", "Bůh války, násilí a zuřivosti, syn Dia", 1),
                new Otazky("Zeus", "Zeus", "Lord of all gods, deity of Thunder, offspring of Rheia and Kronos", "Vládce všech bohů, božstvo hromu, potomek Rheiy a Krona", 2),
                new Otazky("Aphrodite", "Afrodita", "Deity of Love and Beauty, symbol is seashell, born from sea foam", "Bohyně lásky a krásy, symbolem mořská mušle, zrozena z mořské pěny", 3),
                new Otazky("Demeter", "Démétér", "Deity of Land, Agriculture and Fertility, offspring of Kronos and Rheia", "Bohyně země, zemědělství a úrodnosti, dcera Krona a Rheiy", 4),
                new Otazky("Abderus", "Abdéros", "Offspring of the deity Hermes, befriended Heracles, big city was founded in his honour", "Potomek boha Herma, přátelil se s Héraklem, na jeho počest vzniklo velké město", 5),
                new Otazky("Adikia", "Adikia", "Sibling of the deity Dike, represents injustice", "Sestra bohyně Diké, reprezentuje bezpráví", 6),
                new Otazky("Adonis", "Adónis", "The most beautiful young man, Aphrodite fell in love with him, but in the end he was killed by a wild boar", "Nejkrásnější mladík, zamilovala se do něj Afrodita, ale nakonec ho usmrtil divočák", 7),
                new Otazky("Adrasteia", "Adrásteia", "The nymph who fed Zeus goat's milk and bee honey in Crete", "Nymfa, která na Krétě krmila Dia kozím mlékem a včelím medem", 8),
                new Otazky("Aerope", "Áeropé", "Wife of King Atreus, mother of the warlike kings Agamemnon and Menelaus", "Manželka krále Átrea, matka bojechtivých králů Agamemnóna a Meneláa", 9),
                new Otazky("Agamemnon", "Agamemnón", "The powerful Mycenaean king who led the Greek forces in the war against Troy", "Mocný mykénský král, který vedl síly Řeků do války proti Tróji", 10),
                new Otazky("Agaue", "Agaué", "Daughter of Queen Harmonia, lover of the deity Dionysus, accidentally killed her son Pentheus", "Dcera královny Harmonie, milovnice boha Dionýsa, omylem usmrtila svého syna Penthea", 11),
                new Otazky("Achilles", "Achilles", "The offspring of Peleus and the deity Thetis, the best warrior of the Trojan War", "Potomek Pélea a bohyně Thetis, nejlepší válečník v době trójské války", 12),
                new Otazky("Ajax", "Aiás", "One of Agamemnon's best warriors, he protected Achilles' body", "Jeden z nejlepších Agamemnónových bojovníků, ochránil Achilleovo tělo", 13),
                new Otazky("Aeëtes", "Aiétés", "The offspring of the deity Helios, king of Colchis, possessed a golden fleece and gave tasks to Iason", "Potomek boha Helia, kolchidský král, vlastnil zlaté rouno a dával úkoly Iásonovi", 14),
                new Otazky("Aeneas", "Aineiás", "Hero of the Trojans, after the fall of Troy he founded the empire that eventually became Rome", "Hrdina z řad Trójanů, po pádu Tróji založil říši, ze které nakonec vznikl Řím", 15),
                new Otazky("Aiolos", "Aiolos", "Lord of the winds, alleged inventor of the sea sail", "Vládce větrů, údajný vynálezce námořní plachty", 16),
                new Otazky("Aether", "Aithér", "Offspring of the deity Erebus and the deity Nykta, deity of bright light, identified with celestial fire", "Potomek boha Ereba a bohyně Nykty, božstvo jasného světla, ztotožňován s nebeským ohněm", 17),
                new Otazky("Alkinoos", "Alkinoos", "The king on the island of the Phaeacians, who housed and the wandering Odysseus", "Král na ostrově Fajáků, který ubytoval a pohostil bloudícího Odyssea", 18),
                new Otazky("Alcmene", "Alkména", "A Mycenaean princess who was seduced by Zeus and thus became the mother of Heracles", "Mykénská princezna, kterou svedl Zeus, a tak se stala matkou Hérakla", 19),
                new Otazky("Alecto", "Alléktó", "One of the Erinyes, or deityes of vengeance", "Jedna z Erinyí, neboli bohyň pomsty", 20),
                new Otazky("Andromache", "Andromacha", "Originally a Theban princess, later the wife of Prince Hector of Troy", "Původně thébská princezna, později manželka trojského prince Hektora", 21),
                new Otazky("Andromeda", "Andromeda", "Offspring of the Ethiopian king and wife of the hero Perseus", "Dcera etiopského krále a manželka hrdiny Persea", 22),
                new Otazky("Antigone", "Antigona", "Daughter of King Oedipus and his wife and mother Iocasta, she defied the will of King Creon", "Dcera krále Oidipa a jeho manželky i matky Iocasty, vzepřela se vůli krále Kreóna", 23),
                new Otazky("Apollo", "Apollón", "Offspring of Zeus and Lethe, deity of the sun and music, defeater of Python", "Syn Dia a Léty, božstvo slunce a hudby, porazitel Pythóna", 24),
                new Otazky("Arachne", "Arachné", "Weaver from Colophon, challenged Athena to a weaving contest, lost and was turned into a spider", "Tkadlena z Kolofónu, vyzvala na soutěž ve tkaní Athénu, prohrála a byla proměněna na pavouka", 25),
                new Otazky("Arete", "Árété", "Wife of King Alkinoos, Queen of the Island of the Phaeacians", "Manželka krále Alkinoa, královna na ostrově Fajáků", 26),
                new Otazky("Ariadne", "Ariadna", "Daughter of King Minos of Crete, she gave Théseus the thread that kept him from getting lost in the labyrinth", "Dcera krétského krále Mínóa, Théseovi dala nit, díky které se neztratil v labyrintu", 27),
                new Otazky("Artemis", "Artemis", "Offspring of Zeus and Lethe, deity of the moon and the hunt", "Dcera Dia a Léty, bohyně měsíce a lovu", 28),
                new Otazky("Asclepius", "Asklépios", "Offspring of the deity Apollo, physician and later deity of medicine", "Potomek boha Apollóna, lékař a později božstvo lékařství", 29),
                new Otazky("Atalanta", "Atalanta", "The offspring of a Boiotic king who was dumped in the woods but was raised by a bear", "Dcera boiótského krále, kterou pohodili v lese, ale ujala se jí medvědice", 30),
                new Otazky("Atlas", "Atlás", "The offspring of the titan Íapetus, the giant who carries the whole sky on his shoulders", "Potomek titána Íapeta, obr, který nese na ramenou celé nebe", 31),
                new Otazky("Atreus", "Átreus", "King of Mycenae, father of the famous kings Agamemnon and Menelaus", "Mykénský král, otec slavných králů Agamemnóna a Meneláa", 32),
                new Otazky("Boreas", "Boreás", "Deity of the north wind, his father was the titan Astraios, his mother the deity Éós", "Božstvo severního větru, jeho otcem byl titán Astraios, matkou bohyně Éós", 33),
                new Otazky("Briseis", "Bríseovna", "The lover of Achilles, a priestess from Apollo's temple, was at the heart of Agamemnon's quarrel with Achilles", "Milenka Achillea, kněžka z Apollónova chrámu, byla v jádru sporu Agamemnóna s Achilleem", 34),
                new Otazky("Daphne", "Dafné", "The most beautiful nymph, Apollo fell in love with her, she turned into a laurel", "Nejkrásnější nymfa, zamiloval se do ní Apollón, proměnila se ve vavřín", 35),
                new Otazky("Daphnis", "Dafnis", "Offspring of the deities Hermes, mythical founder of the genre of shepherd (pastoral) songs", "Potomek boha Herma, bájný zakladatel žánru pastýřských písní", 36),
                new Otazky("Daedalus", "Daidalos", "The mythical great Greek inventor, among other things he fled Crete on the wings he created", "Bájný velký řecký vynálezce, mimo jiné uprchl z Kréty na křídlech, která vytvořil", 37),
                new Otazky("Damocles", "Damoklés", "A man from Syracuse who wanted to be ruler, had to sit under a hanging sword at a banquet", "Muž ze Syrakus, který chtěl být panovníkem, na hostině musel sedět pod visícím mečem", 38),
                new Otazky("Danae", "Danaé", "The beautiful daughter of Eurydice, to whom Zeus get like golden rain and together they had a offspring Perseus", "Krásná dcera Eurydiky, ke které pronikl Zeus jako zlatý déšť a spolu měli potomka Persea", 39),
                new Otazky("Dike", "Diké", "Offspring of the deity Zeus, deity of Just Laws and Efficient Justice", "Potomek boha Dia, božstvo spravedlivých zákonů a účinné spravedlnosti", 40),
                new Otazky("Diomedes", "Diomédés", "King of Argos, the bravest fighter for the Greeks in the days when Achilles refused to fight", "Král z Argu, nejstatečnější bojovník za Řeky ve dnech, kdy Achilles odmítal bojovat", 41),
                new Otazky("Diona", "Dióna", "Deity of Rain and Springs", "Božstvo deště a pramenů", 42),
                new Otazky("Dionysus", "Dionýsos", "Offspring of the deity Zeus, deity of Wine, Fun and Dancing", "Syn boha Dia, bůh vína, zábavy a tance", 43),
                new Otazky("Doris", "Dóris", "Offspring of Okean and Thetis, sea nymph and wife of the deity Nereus", "Dcera Ókeana a Téthys, mořská nymfa a manželka boha Nérea", 44),
                new Otazky("Echidna", "Echidna", "Half woman and half serpent, her husband the terrible giant Typhon", "Napůl žena a napůl had, její manžel hrozný obr Tyfón", 45),
                new Otazky("Echo", "Echó", "Mountain nymph, as punishment she was never allowed to speak first", "Horská nymfa, za trest nesměla nikdy mluvit jako první", 46),
                new Otazky("Eirene", "Eiréné", "Offspring of Zeus and Themida, deity of peace and order", "Potomek boha Dia a bohyně Themidy, božstvo míru a pořádku", 47),
                new Otazky("Elpenor", "Elpénór", "The young Odysseus' warrior who fell off the roof on the island of the witch Kirke", "Mladý Odysseův bojovník, který na ostrově čarodějky Kirké spadl ze střechy", 48),
                new Otazky("Endymion", "Endymión", "A young man punished for his love of Hera with eternal sleep, the moon deity Selene fell in love with him", "Mladík, potrestaný za lásku k Héře věčným spánkem, zamilovala se do něj bohyně měsíce Seléné", 49),
                new Otazky("Enceladus", "Enkelados", "The Titan who led a rebellion against the deities who threw the island of Sicily at him", "Titán, který vedl vzpouru proti bohům, kteří na něj hodili ostrov Sicílii", 50),
                new Otazky("Eos", "Éós", "Offspring of Hyperion, deity of the Morning Red Clouds", "Dcera Hyperíona, bohyně ranních červánků", 51),
                new Otazky("Erebus", "Erebos", "Offspring of Chaos, deity of Eternal Darkness", "Potomek Chaosu, božstvo věčné tmy", 52),
                new Otazky("Eris", "Eris", "The offspring of the deity Nyx, deity of Discord, initiated the Trojan War with her apple", "Potomek bohyně Nyx, božstvo sváru, svým jablkem inicovala trójskou válku", 53),
                new Otazky("Eros", "Erós", "Offspring of Ares and Aphrodite, deity of Love and Lust", "Potomek Área a Afrodity, božstvo lásky a chtíče", 54),
                new Otazky("Euphemus", "Eufémos", "The offspring of Poseidon, could walk on water", "Potomek Poseidona, dokázal kráčet po vodě", 55),
                new Otazky("Eurydice", "Eurydika", "The wife of the singer Orpheus, for whom he went to the underworld", "Manželka pěvce Orfea, pro kterou se vydal do podsvětí", 56),
                new Otazky("Faethon", "Faethón", "The offspring of the sun deity Helios, when his father lent him a solar chariot, nearly burned the Earth", "Potomek slunečního boha Hélia, když mu otec půjčil sluneční vůz, málem spálil Zemi", 57),
                new Otazky("Faidra", "Faidra", "The Cretan princess, sister of Ariadne, married Theseus, but then fell in love with his offspring", "Krétská princezna, sestra Ariadny, vzala si Thésea, ale pak se zamilovala do jeho potomka", 58),
                new Otazky("Philemon", "Filémón", "Old husband to Baukis, feasted the deities, eventually they turned into trees", "Stařičký manžel Baukis, pohostili bohy, nakonec byli proměněni na stromy", 59),
                new Otazky("Phobos", "Fobos", "Offspring of Ares and Aphrodite, deity of Fear", "Potomek Área a Afrodity, božstvo strachu", 60),
                new Otazky("Phosphoros", "Fósforos", "Offspring of the deity Eos, bringer of light, in Latin called Lucifer", "Potomek bohyně Éos, nositel světla, latinsky nazýván Lucifer", 61),
                new Otazky("Gaia", "Gaia", "The primordial deity of the Earth, her husband was the deity Uranos", "Prapůvodní božstvo Země, jejím manželem byl božstvo Úranos", 62),
                new Otazky("Ganymed", "Ganymédés", "A handsome young man, he was a waiter on Olympus, where he was carried off by Zeus as an eagle", "Krásný mladík, dělal číšníka na Olympu, kam jej unesl Zeus jako orel", 63),
                new Otazky("Creusa", "Glauka", "The princess of Corinth, whom Iason wanted to marry out of calculation, but his wife Medea killed her", "Korinthská princezna, kterou si chtěl Íáson z vypočítavosti vzít, ale jeho žena Médeia ji zabila", 64),
                new Otazky("Hades", "Hádés", "Offspring of the titan Kronos, brother of Zeus, deity of the Underworld", "Syn titána Krona, bratr Dia, božstvo podsvětí", 65),
                new Otazky("Haemon", "Haimón", "Offspring of Creontes and Eurydice, fiancé of the rebellious Antigone", "Syn Kreonta a Eurydiky, snoubenec vzpurné Antigony", 66),
                new Otazky("Hebe", "Hébé", "Offspring of Zeus and Hera, deity of Eternal Youth", "Dcera Dia a Héry, bohyně věčného mládí", 67),
                new Otazky("Hephaistos", "Héfaistos", "Offspring of Zeus and Hera, deity of Fire and Blacksmithing", "Potomek Dia a Héry, bůh ohně a kovářství", 68),
                new Otazky("Hecate", "Hekaté", "Deity of Magic and Magical Creatures", "Bohyně kouzel a magických bytostí", 69),
                new Otazky("Hector", "Hektór", "King Priam's offspring, the best Trojan warrior, killed Patroclus and was killed by Achilles", "Potomek krále Priama, nejlepší trójský bojovník, zabil Patrocla a byl zabit Achilleem", 70),
                new Otazky("Helene", "Helena", "But one of the most beautiful women, the wife of King Menelaus, fled with Paris to Troy", "Jedna z nejkrásnějších žen, manželka krále Menelaa, uprchla však s Paridem do Tróji", 71),
                new Otazky("Helios", "Hélios", "Offspring of the titan Hyperion, deity of the Sun", "Potomek titána Hyperíona, božstvo slunce", 72),
                new Otazky("Hera", "Héra", "Offspring of Cronus and sister and wife of Zeus", "Potomek Krona a sestra i manželka Dia", 73),
                new Otazky("Heracles", "Héraklés", "The offspring of Zeus and Alcmene, the greatest strongman of Greek myths, completed twelve quests", "Potomek Dia a Alkmény, největší silák řeckých mýtů, splnil dvanáct úkolů", 74),
                new Otazky("Hermafroditos", "Hermafrodítos", "Offspring of Hermes and Aphrodite, his body literally became one with the nymph Salmakis", "Potomek Herma a Afrodíty, jeho tělo se doslova spojilo v jedno s nymfou Salmakis", 75),
                new Otazky("Hermes", "Hermés", "Messenger of the Deitys", "Posel bohů", 76),
                new Otazky("Hermione", "Hermiona", "Offspring of Menelaus and Helen, betrothed to a Mycenaean prince, but eventually married the offspring of Achilles", "Potomek Meneláa a Heleny, zasnoubena s mykénským princem, nakonec si ale vzala potomka od Achillea", 77),
                new Otazky("Hestia", "Hestia", "Offspring of Kronos and Rhea, deity of the Domestic Hearth", "Potomek Krona a Rheiy, božstvo domácího krbu", 78),
                new Otazky("Hippolyta", "Hippolyta", "Offspring of the deity Ares, queen of the fighting Amazons", "Dcera boha Área, královna bojovných Amazonek", 79),
                new Otazky("Hydra", "Hydra", "Offspring of the giant Typhon and the monstrous Echidna, the dragon with nine heads", "Potomek obra Tyfóna a zrůdné Echidny, drak s devíti hlavami", 80),
                new Otazky("Hygieia", "Hygieia", "Offspring of the deity Asclepius, deity of Health", "Potomek boha Asklépia, božstvo zdraví", 81),
                new Otazky("Hyperion", "Hyperíón", "Titan, offspring of the primordial deities Gaia and Uranus, father of Helios, Selene and Eos", "Titán, syn původních bohů Gaiy a Urana, otece Hélia, Selény a Éós", 82),
                new Otazky("Chaos", "Chaos", "The oldest deity or principle, the disordered beginning of everything", "Nejstarší božstvo či princip, neuspořádaný počátek všeho", 83),
                new Otazky("Charon", "Charón", "A ferryman across the rivers Styx and Acheron in the underworld", "Převozník přes řeky Styx a Acheron v podsvětí", 84),
                new Otazky("Charybdis", "Charybda", "A monster that was creating a dangerous whirlpool that was sinking ships", "Nestvůra, které vytvářela nebezpečný vír, který potápěl lodě", 85),
                new Otazky("Cheiron", "Cheirón", "One of the most famous centaurs", "Jeden z nejslavnějších kentaurů", 86),
                new Otazky("Chimera", "Chiméra", "Offspring of Typhon and Echidna, part lion and part serpent", "Potomek Tyfóna a Echidny, částečně lev a částečně had", 87),
                new Otazky("Chryseis", "Chrýseovna", "Daughter of a priest of Apollo, she was captured by Agamemnon, so the Greek army was punished by the plague", "Dcera Apollónova kněze, byla zajata Agamemnónem, proto byla řecká armáda potrestána morem", 88),
                new Otazky("Iapetos", "Íapetos", "Titan, offspring of Uranus and Gaia, father of Prometheus or Atlas", "Titán, potomek Úrana a Gaiy, otec Prométhea či Atlase", 89),
                new Otazky("Jason", "Iásón", "Prince of Iolus, heroic leader of the Argonauts, husband of Medea", "Iolský princ, hrdinný vůdce Argonautů, manžel Médeie", 90),
                new Otazky("Iphigenia", "Ífigenie", "The offspring of Agamemnon and Clytaimnestra, whom her father sacrificed to propitiate Artemis", "Dcera Agamemnóna a Klytaimnéstry, kterou otec obětoval, aby odprosil Artemidu", 91),
                new Otazky("Ikaros", "Íkaros", "Offspring of Daedalus, he flew too high and crashed", "Potomek Daidala, letěl příliš vysoko a zřítil se", 92),
                new Otazky("Iokaste", "Iokasté", "Queen of Thebes, wife of Laius and later wife of their offspring Oedipus", "Thébská královna, manželka Láia a později i manželka jejich potomka Oidipa", 93),
                new Otazky("Ion", "Íón", "Offspring of the deity Apollo, mythical ancestor of the tribe of the Ionians", "Potomek boha Apollóna, mýtický předek kmene Iónů", 94),
                new Otazky("Iris", "Íris", "Offspring of Electra, deity of the Rainbow", "Potomek Élektry, božstvo duhy", 95),
                new Otazky("Ismene", "Isména", "Offspring of King Oedipus and Iokasta, the more obedient sister of Antigone", "Dcera krále Oidipa a Iokasty, poslušnější sestra Antigony", 96),
                new Otazky("Callisto", "Kallistó", "Daughter of King Lycaon, Zeus came to her in the form of Artemis, to whose company she belonged", "Dcera krále Lykáona, Zeus se k ní dostal v podobě Artemidy, do jejíž družiny patřila", 97),
                new Otazky("Calypso", "Kalypsó", "Daughter of Atlas, she held Ulysses prisoner on her island for seven years", "Atlasův potomek, sedm let na svém ostrově věznila Odyssea", 98),
                new Otazky("Kassandra", "Kassandra", "King Priam's offspring, the oracle, rejected the amorous advances of Apollo", "Potomek krále Priama, věštkyně, odmítla milostné úklady Apollóna", 99),
                new Otazky("Cerberus", "Kerberos", "The offspring of Typhon and Echidna, the three-headed dog he guards at the exit of the underworld", "Potomek Tyfóna a Echidny, trojhlavý pes, které hlídá u východu z podsvětí", 100),
                new Otazky("Circe", "Kirké", "The offspring of the deity Helios, a sorceress, turned Odysseus' men into pigs", "Potomek boha Hélia, kouzelnice, proměnila Odysseovy muže na vepře", 101),
                new Otazky("Clytemnestra", "Klytaimnéstra", "Agamemnon's wife, who together with Aigisthus planned her husband's death", "Manželka Agamemnóna, která spolu s Aigisthem naplánovala smrt svého manžela", 102),
                new Otazky("Cronus", "Kronos", "Offspring of Uranus and Gaia, a titan who devoured his children, including Zeus", "Potomek Úrana a Gaiy, titán, který požíral své děti, a to včetně Dia", 103),
                new Otazky("Laocoon", "Láokoón", "Trojan priest of the deity Apollo, vainly urged that the horse donated by the Greeks be burned", "Trójský kněz boha Apollóna, marně vyzýval, aby byl kůň darovaný Řeky spálen", 104),
                new Otazky("Leto", "Létó", "Mother of Apollo or Artemis", "Matka Apollóna či Artemidy", 105),
                new Otazky("Lycaon", "Lykáón", "The king who presented Zeus with human flesh and was turned into a wolf as punishment", "Král, který předložil Diovy lidské maso a za trest byl proměněn na vlka", 106),
                new Otazky("Marsyas", "Marsyás", "Satyr, who challenged Apollo in a flute game, lost and was flayed", "Satyr, který ve hře na flétnu vyzval Apollóna, prohrál a byl stažen z kůže", 107),
                new Otazky("Medea", "Médeia", "The Princess of Colchis, powerful sorceress and disgraced wife of Jason", "Princezna z Kolchidy, mocná čarodějka a zhrzená Íasonova manželka", 108),
                new Otazky("Medusa", "Medúsa", "The most famous of the Gorgons, she had snakes for hair and her gaze turned people to stone", "Nejslavnější z Gorgon, místo vlasů měla hady a její pohled měnil lidi v kámen", 109),
                new Otazky("Megara", "Megara", "Offspring of King Creontus and wife of Heracles", "Dcera krále Kreonta a manželka Hérakla", 110),
                new Otazky("Menelaus", "Meneláos", "Offspring of King Atreus, brother of Agamemnon and king of Sparta", "Syn krále Átrea, bratr Agamemnóna a spartský král", 111),
                new Otazky("Midas", "Mídás", "King of Phrygia, who had donkey ears or everything he touched turned to gold", "Král z Frýgie, který měl oslí uši či se vše, čeho se dotkl, změnilo ve zlato", 112),
                new Otazky("Minos", "Mínós", "The offspring of Zeus and Europa, the mythical king of Crete, after whom the entire culture is named", "Potomek Dia a Európy, bájný král na Krétě, podle něhož se jmenuje celá kultura", 113),
                new Otazky("Minotaur", "Mínótauros", "A man with a bull's head", "Muž s býčí hlavou", 114),
                new Otazky("Morpheus", "Morfeus", "Offspring of the deity of sleep Hypnos, deity of Dreams", "Potomek boha spánku Hypna, božstvo snů", 115),
                new Otazky("Neoptolemus", "Neoptolemos", "Offspring of Achilles, married Menelaus' offspring Hermione", "Achilleův potomek, oženil se s Meneláovou dcerou Hermionou", 116),
                new Otazky("Nike", "Níké", "The deity of Victory and the Personification of Victory itself", "Božstvo vítězství a zosobněné vítězství samo", 117),
                new Otazky("Niobe", "Niobé", "The unfortunate queen of Thebes, whose children were killed by Apollo and Artemis", "Nešťastná thébská královna, jejíž děti pobili Apollón s Artemidou", 118),
                new Otazky("Nyx", "Nyx", "Offspring of Chaos, deity of the Night", "Potomek Chaosu, božstvo noci", 119),
                new Otazky("Ulysses", "Odysseus", "The ingenious king of Ithaca, who had been returning home from the Trojan War for ten years", "Důmyslný ithacký král, který se deset let vracel domů z trójské války", 120),
                new Otazky("Oidipus", "Oidipús", "A Theban prince who accidentally killed his father Laius and married his mother Iokasta", "Thébský princ, který omylem zabil svého otce Láia a oženil se se svou matkou Íokastou", 121),
                new Otazky("Oceanus", "Ókeanos", "Offspring of Uranus and Gaia, a titan who represented the endless waters surrounding the world", "Potomek Úrana a Gaie, titán, který představoval nekonečné vody obepínající svět", 122),
                new Otazky("Orestes", "Orestés", "Offspring of Agamemnon, brother of Electra", "Potomek Agamemnóna, bratr Élektry", 123),
                new Otazky("Pallas", "Pallas", "Surname of the deity Athena from her old friend", "Přízvisko bohyně Athény dle dávné přítelkyně", 124),
                new Otazky("Pandora", "Pandóra", "Wife of the titan Epimetheus, famous for her box full of disasters", "Manželka titána Epiméthea, proslula svou skříňkou s uschovanými pohromami", 125),
                new Otazky("Paris", "Paris", "Offspring of the Trojan King Priam, he kidnapped Helen and thus started the Trojan War", "Potomek trójského krále Priama, unesl Helenu a tím započal trójskou válku", 126),
                new Otazky("Pasiphae", "Pásifaé", "Offspring of the deity Helios, wife of King Minos and mother of Ariadne and Phaedra", "Dcera boha Hélia, manželka krále Mínóa a matka Ariadny a Faidry", 127),
                new Otazky("Patroclus", "Patroklos", "Achilles' best friend, he donned his armor and was killed by Hector in it", "Nejlepší Achilleův přítel, vzal na sebe jeho zbroj a byl v ní zabit Hektorem", 128),
                new Otazky("Pegasus", "Pégasos", "Winged horse", "Okřídlený kůň", 129),
                new Otazky("Peleus", "Péleus", "A Greek hero and also the father of Achilles", "Řecký hrdina a také otec Achillea", 130),
                new Otazky("Penelope", "Pénelopa", "Faithful wife of Odysseus who rejected all suitors", "Věrná manželka Odyssea, která odmítala všechny nápadníky", 131),
                new Otazky("Persephone", "Persefona", "Daughter of Zeus and Demeter, abducted by Hades to the underworld, she is only allowed out in the spring", "Dcera Dia a Déméter, byla unesena Hádem do podsvětí, smí ven pouze na jaře", 132),
                new Otazky("Perseus", "Perseus", "A great hero who defeated many monsters including Medusa", "Velký hrdina, která porazil mnoho nestvůr včetně Medúsy", 133),
                new Otazky("Polybus", "Polybos", "King of Corinth who raised Oedipus when his parents left him in the forest", "Korinthský král, který vychovával Oidipa, když ho jeho rodiče zanechali v lese", 134),
                new Otazky("Polyphemus", "Polyfémos", "A terrible one-eyed giant, the offspring of the deity Poseidon, wanted to eat all of Odysseus' sailors", "Strašlivý jednooký obr, potomek boha Poseidóna, chtěl sežrat všechny Odysseovy námořníky", 135),
                new Otazky("Polyneikus", "Polyneikés", "Offspring of Oedipus the King, he led a rebellion against Thebes, only Antigone then dared to bury his body", "Potomek krále Oidipa, vedl povstání proti Thébám, jediná Antigona se potom odvážila pohřbít jeho tělo", 136),
                new Otazky("Poseidon", "Poseidón", "Offspring of Cronus and Rheia, deity of the Seas", "Potomek Krona a Rheiy, božstvo moří", 137),
                new Otazky("Priamus", "Priamos", "Last king of Troy, father of Hector and Paris", "Poslední trójský král, otec Hektora a Parida", 138),
                new Otazky("Priapus", "Priápos", "Offspring of Dionysus and Aphrodite, deity of Fertility", "Potomek Dionýsa a Afrodíty, božstvo plodnosti", 139),
                new Otazky("Prometheus", "Prométheus", "A titan who gave people fire, for which he was cruelly punished", "Titán, který daroval lidem oheň, za což byl krutě potrestán", 140),
                new Otazky("Psyche", "Psýché", "Wife of the deity Eros, personification of the Soul", "Manželka boha Erota, zosobnění duše", 141),
                new Otazky("Pygmalion", "Pygmalión", "A sculptor, whose statue, he fell in love with, was brought to life by Aphrodite", "Sochař, jehož sochu, do níž se zamiloval, oživila Afrodíta", 142),
                new Otazky("Python", "Pýthón", "A snake-like dragon", "Hadovi podobný drak", 143),
                new Otazky("Rhea", "Rheia", "Wife and sister of the titan Cronus", "Manželka a sestra titána Kronose", 144),
                new Otazky("Selene", "Seléné", "Offspring of Hyperion, deity of the Moon", "Potomek Hyperíona, božstvo měsíce", 145),
                new Otazky("Sibyll", "Sibylla", "One of the most famous fortune tellers of ancient Greece", "Jedna z nejznámějších věštkyň starověkého Řecka", 146),
                new Otazky("Scylla", "Skylla", "A six-headed monster that threatened Odysseus' sailors", "Šestihlavá příšera, která ohrožovala Odysseovy námořníky", 147),
                new Otazky("Tantalus", "Tantalos", "A mortal who lied to the deities and was cruelly punished in the underworld for it", "Smrtelník, který lhal bohům a byl za to krutě v podsvětí potrestán", 148),
                new Otazky("Tartarus", "Tartaros", "Deity of the Deepest Abyss in the Underworld", "Božstvo nejhlubší propasti v podsvětí", 149),
                new Otazky("Telemachus", "Télemachos", "The offspring of Odysseus and Penelope, who helped his father recapture his kingdom", "Potomek Odyssea a Pénelopy, který svému otci pomáhal opět uchvátit své království", 150),
                new Otazky("Theseus", "Théseus", "Athenian hero who defeated the Minotaurus", "Athénský hrdina, který porazil Mínotaura", 151),
                new Otazky("Tyche", "Tyché", "Offspring of Ocean, deity of Chance and Luck", "Potomek Ókeana, božstvo náhody a štěstí", 152),
                new Otazky("Uranus", "Úranos", "The primordial deity of the Heavens and the personification of the Heavens themselves", "Prapůvodní božstvo nebes a zosobnění nebes samotných", 153),
                new Otazky("Sisyphus", "Sísyfos", "The offspring of Aiolus, he is famous for his punishment, where he has to keep rolling a heavy stone around", "Potomek Aiola, je známý svým trestem, kdy musí pořád dokola valit těžký kámen", 154)
            };
        }

        /// <summary>
        /// Vytvoří vojáky na začátku hry
        /// </summary>
        public void ZalozVojaky()
        {
            Vojaci = new List<Vojak>();

            // Vytvoření vojáků a jejich rozestavění do základních pozic
            for (int i = 0; i < 2;  i++) 
            {
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.ReckyLehky, 80 + i * 55, 420 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.ReckyHoplita, 370 + i * 55, 450 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.TrojskyHoplita, 1100 + i * 55, 500 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.TrojskyHoplita, 1210 + i * 55, 550 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.TrojskyLehky, 1310 + i * 55, 420 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.TrojskyLehky, 1410 + i * 55, 520 + i * 130));
                Vojaci.Add(new Vojak(Vojak.TypyVojaku.TrojskyLehky, 1510 + i * 55, 620 + i * 130));
            }

            // A ještě Achillea
            Vojaci.Add(new Vojak(Vojak.TypyVojaku.ReckyAchilles, 20, 600));
        }

        /// <summary>
        /// Nastaví proměnné do výchozích hodnot
        /// </summary>
        public void NastavPromenneNaZacatekHry()
        {
            AktualniPozadi = VolbaPozadi.Plaz;
            AktualniStavHry = StavHry.Uvod;
            JazykHry = Jazyk.en;

            Nahoda = new Random();

            CislaOtazek = new int[3];
            OtazkaPolozena = false;
            PosledniSpravne = false;

            MaPredSebouSoupere = false;

            ProtiPretizeniKlaves = 0;
            ProtiPretizeniMysi = 0;
            OdpocetDoPristyOtazky = 0;
            OdsazeniRameckuZaTextem = 6;
            CasovacProUdalosti = 0;
            CasovacProSavlicku = 0;
            ZdraviIndex = 0;
            RychlostPochodu = 1;
            PocitadloZasahu = 0;

            ReckeRezervy = 120;
            TrojskeRezervy = 240;

            CenaLeceni = 2;
            CenaHnevu = 3;
            MoznoHnevu = 0;
            MoznoLeceni = 0;

            PocitadloVarovaniOZdrojich = 0;
            PoziceVarovaniOZdrojichX = 0;
            PoziceVarovaniOZdrojichY = 0;

            PoziceFramuX = 0;
            PoziceFramuY = 0;
            SirkaFramu = 95;
            VyskaFramu = 96;

            DelkaSvihu = 50;

            CiloveDrachmy = 60;
            CiloveSkore = 0;

            CenaZaOdpoved = 35;
            Drachmy = 0;
            Skore = 0;
            BonusZaVitezstvi = 800;

            ZvlastniEfekty_PoziceX = 0;
            ZvlastniEfekty_PoziceY = 0;
            ZvlastniEfekty_Pocitadlo = 0;
            ZvlastniEfekty_Typ = TypEfektu.Nic;

            BojujeSe = 0;
        }

        /*
        
            ####################
            ####################
            ####################
            ####################
        

            Vykreslovací metody

                ↓       ↓  

        */

        /// <summary>
        /// Základní metoda pro kreslení hry, volá ostatní metody.
        /// </summary>
        public void KresliHru()
        {
            // Dále se rozhodneme podle stavu hry
            switch (AktualniStavHry)
            {
                // Konec hry
                case StavHry.ZaverVyhra:
                case StavHry.ZaverProhra:
                    VykresliKonecHry();
                    break;

                // Informace neboli "credits"
                case StavHry.Informace:
                    VykresliPozadi(Gfx_PozadiTemnaPlaz);
                    VypisZdroje();
                    break;

                // Hra jede
                case StavHry.HraJede:
                    VykresliPozadi(Gfx_PozadiPlaz);
                    KresliJedouciHru();
                    break;

                // Úvodní obrazovka
                default:
                case StavHry.Uvod:
                    VykresliPozadi(Gfx_PozadiTemnaPlaz);
                    KresliUvod();
                    break;
            }
        }

        /// <summary>
        /// Vypíše zkrácenou formou (plná na webu hry) zdroje pro grafiku a zvuky
        /// </summary>
        public void VypisZdroje()
        {
            VypisText(JazykMutace(Text_Credits_VrchniRadek), 50, 40, Color.DarkBlue, Pismo_Normalni);
            VypisText(JazykMutace(Text_Credits_Akryl), 50, 120, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_AnimaceVojaku), 50, 170, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_ZvlastniGrafickeFX), 50, 220, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_ZvlastniZvukoveFX), 50, 270, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_HudbaFireThunder), 50, 320, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_HudbaMinotaur), 50, 370, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_ObrazekAchilles), 50, 420, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_ObrazekAthena), 50, 470, Color.DarkMagenta, Pismo_Mensi);
            VypisText(JazykMutace(Text_Credits_SpodniRadek), 50, 620, Color.DarkSlateBlue, Pismo_Mensi);
        }

        /// <summary>
        /// Vykreslit konec hry
        /// </summary>
        public void VykresliKonecHry()
        {
            switch (AktualniStavHry)
            {
                case StavHry.ZaverVyhra:
                    VykresliPozadi(Gfx_PozadiTemnaPlaz_Vyhra);
                    VypisText(JazykMutace(Text_VyhralJsi), 100, 200, Color.DarkSlateBlue, Pismo_Normalni);
                    break;

                default:
                case StavHry.ZaverProhra:
                    VykresliPozadi(Gfx_PozadiTemnaPlaz_Prohra);
                    VypisText(JazykMutace(Text_ProhralJsi), 100, 200, Color.DarkRed, Pismo_Normalni);
                    break;
            }

            VypisText(String.Format("{0}: {1}", JazykMutace(Text_TveSkoreJe), Skore), 100, 300, Color.Black, Pismo_Normalni);
            VypisText(JazykMutace(Text_StiskniR), 100, 400, Color.DimGray, Pismo_Mensi);
        }

        /// <summary>
        /// Vykreslování v případě, že hra jede
        /// </summary>
        public void KresliJedouciHru()
        {
            VykresliPrepinaniJazyka();
            VykresliDrachmy();
            VykresliSkore();
            VykresliRezervy();
            VykresliInfoOKlikani();
            VykresliTestoveOtazky();
            VykresliVojaky();
            VykresliUkazateleZdravi();
            VykresliVarovaniOZdrojich();
            VykresliZvlastniEfekty();
            ZvukoveEfektyBoje();
        }

        /// <summary>
        /// Přepínač skladby hudby
        /// </summary>
        /// <param name="hudba"></param>
        public void PrepniHudbu(Song hudba)
        {
            if (MediaPlayer.Queue.ActiveSong != hudba) { MediaPlayer.Play(hudba); }
        }

        /// <summary>
        /// Vytváří zvukové efekty souboje prostých vojáků
        /// </summary>
        public void ZvukoveEfektyBoje()
        {
            if (BojujeSe > 0)
            {
                if (CasovacProSavlicku == 1 && Nahoda.Next(1, 10) > 6)
                {
                    Zvuk_SlabySek.Play();
                }

                BojujeSe--;
            } 
        }

        /// <summary>
        /// Vykresluje efekty obvazů či Athénina hněvu
        /// </summary>
        public void VykresliZvlastniEfekty()
        {
            if (ZvlastniEfekty_Typ != TypEfektu.Nic)
            {
                switch (ZvlastniEfekty_Typ)
                {
                    case TypEfektu.Leceni:
                        _spriteBatch.Draw(Efekt_Obvazy, new Vector2(ZvlastniEfekty_PoziceX, ZvlastniEfekty_PoziceY), Color.White);
                        break;

                    default:
                    case TypEfektu.Zranovani:
                        _spriteBatch.Draw(Efekt_Sekani, new Vector2(ZvlastniEfekty_PoziceX, ZvlastniEfekty_PoziceY), Color.White);
                        break;
                }
            }

            if (ZvlastniEfekty_Pocitadlo > 0)
            {
                ZvlastniEfekty_Pocitadlo--;
            }

            if (ZvlastniEfekty_Pocitadlo <= 0)
            {
                ZvlastniEfekty_Typ = TypEfektu.Nic;
            }
        }

        /// <summary>
        /// Zobrazí úvodní obrazovku
        /// </summary>
        public void KresliUvod()
        {
            if (JazykHry == Jazyk.en)
            {
                _spriteBatch.Draw(Gfx_UvodniEN, new Vector2(300, 150), Color.White);
            }
            else
            {
                _spriteBatch.Draw(Gfx_UvodniCZ, new Vector2(300, 150), Color.White);
            }
        }

        /// <summary>
        /// Nad hlavy vojáků vykreslí ukazatele jejich aktuálního zdraví
        /// </summary>
        public void VykresliUkazateleZdravi()
        {
            foreach (Vojak vojakPacient in Vojaci)
            {
                if (vojakPacient.Zdravi > 0)
                {
                    if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.9))
                    {
                        ZdraviIndex = 9;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.8))
                    {
                        ZdraviIndex = 8;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.7))
                    {
                        ZdraviIndex = 7;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.6))
                    {
                        ZdraviIndex = 6;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.5))
                    {
                        ZdraviIndex = 5;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.4))
                    {
                        ZdraviIndex = 4;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.3))
                    {
                        ZdraviIndex = 3;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.2))
                    {
                        ZdraviIndex = 2;
                    }
                    else if (vojakPacient.Zdravi > ((float)vojakPacient.MaxZdravi * 0.1))
                    {
                        ZdraviIndex = 1;
                    }
                    else
                    {
                        ZdraviIndex = 0;
                    }

                    _spriteBatch.Draw(Gfx_UkazatelZdravi[ZdraviIndex], new Vector2(vojakPacient.PoziceX, vojakPacient.PoziceY - 3), Color.White);
                }
            }
        }

        /// <summary>
        /// Vykreslí všechny vojáky
        /// </summary>
        public void VykresliVojaky()
        {
            foreach (Vojak vojak in Vojaci)
            {
                // Jednou za čas se vojákovi posune počítadlo animace
                if (CasovacProUdalosti == 2)
                {
                    vojak.PosunPocitadloAnimace();
                }

                VykresliJednohoVojaka(vojak.CoDelaAnimace, vojak.PocitadloAnimace, vojak.PoziceX, vojak.PoziceY, vojak.TypTohotoVojaka);
            }
        }

        /// <summary>
        /// Vykreslí konkrétního vojáka
        /// </summary>
        /// <param name="cinnostiAnimace">Vyhodnotíme enum z třídy Vojak a podle toho nastavíme výběrový obdélník pro sprite sheet</param>
        /// <param name="pocitadloAnimace">Časovač pro animaci z třídy Vojak</param>
        /// <param name="poziceX">Pozice vojáka na horizontále</param>
        /// <param name="poziceY">Pozice vojáka na vertikále</param>
        /// <param name="typVojaka">Jaký typ vojáka se vykreslí</param>
        public void VykresliJednohoVojaka(Vojak.CinnostiAnimace cinnostiAnimace, int pocitadloAnimace, float poziceX, float poziceY, Vojak.TypyVojaku typVojaka)
        {
            // Nejprve vyhledáme, kde ve sprite sheetu se příslušné políčko (frame) nachází
            switch (cinnostiAnimace)
            {
                default:
                case Vojak.CinnostiAnimace.StojiOtocenVpravo:
                    PouzijMalyFrame();
                    PoziceFramuX = 0;
                    PoziceFramuY = 11 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.StojiOtocenVlevo:
                    PouzijMalyFrame();
                    PoziceFramuX = 0;
                    PoziceFramuY = 9 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.JdeNahoru:
                    PouzijMalyFrame();
                    PoziceFramuX = pocitadloAnimace * SirkaFramu;
                    PoziceFramuY = 8 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.JdeVlevo:
                    PouzijMalyFrame();
                    PoziceFramuX = pocitadloAnimace * SirkaFramu;
                    PoziceFramuY = 9 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.JdeVpravo:
                    PouzijMalyFrame();
                    PoziceFramuX = pocitadloAnimace * SirkaFramu;
                    PoziceFramuY = 11 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.JdeDolu:
                    PouzijMalyFrame();
                    PoziceFramuX = pocitadloAnimace * SirkaFramu;
                    PoziceFramuY = 10 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.UtociVlevo:
                    PouzijMalyFrame();
                    SekaciSekvence(pocitadloAnimace);
                    PoziceFramuY = 9 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.UtociVpravo:
                    PouzijMalyFrame();
                    SekaciSekvence(pocitadloAnimace);
                    PoziceFramuY = 11 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.UtociNahoru:
                    PouzijMalyFrame();
                    SekaciSekvence(pocitadloAnimace);
                    PoziceFramuY = 8 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.UtociDolu:
                    PouzijMalyFrame();
                    SekaciSekvence(pocitadloAnimace);
                    PoziceFramuY = 10 * VyskaFramu;
                    break;

                case Vojak.CinnostiAnimace.Hyne:
                    PouzijMalyFrame();
                    PoziceFramuX = SirkaFramu + SirkaFramu * pocitadloAnimace;
                    PoziceFramuY = 20 * VyskaFramu;
                    break;
            }

            // Nyní podle toho vytvoříme výběrový obdélník
            ObdelnikProAnimaci = new Rectangle(PoziceFramuX, PoziceFramuY, SirkaFramu, VyskaFramu);

            // A teď vykreslíme správný typ vojáka
            switch (typVojaka)
            {
                default:
                case Vojak.TypyVojaku.ReckyLehky:
                    _spriteBatch.Draw(SpriteS_ReckyLehky, new Vector2(poziceX, poziceY), ObdelnikProAnimaci, Color.White);
                    break;

                case Vojak.TypyVojaku.ReckyHoplita:
                    _spriteBatch.Draw(SpriteS_ReckyHoplita, new Vector2(poziceX, poziceY), ObdelnikProAnimaci, Color.White);
                    break;

                case Vojak.TypyVojaku.ReckyAchilles:
                    _spriteBatch.Draw(SpriteS_ReckyAchilles, new Vector2(poziceX, poziceY), ObdelnikProAnimaci, Color.White);
                    break;

                case Vojak.TypyVojaku.TrojskyLehky:
                    _spriteBatch.Draw(SpriteS_TrojskyLehky, new Vector2(poziceX, poziceY), ObdelnikProAnimaci, Color.White);
                    break;

                case Vojak.TypyVojaku.TrojskyHoplita:
                    _spriteBatch.Draw(SpriteS_TrojskyHoplita, new Vector2(poziceX, poziceY), ObdelnikProAnimaci, Color.White);
                    break;
            }
        }

        /// <summary>
        /// Zvláštní (zjednodušený) pohyb při sekání mečem, řeší jen osu X
        /// </summary>
        /// <param name="pocitadloAnimace"></param>
        public void SekaciSekvence(int pocitadloAnimace)
        {
            if (pocitadloAnimace == 0)
            {
                PoziceFramuX = 5 * SirkaFramu;
            }
            else if (pocitadloAnimace == 1 || pocitadloAnimace == 3)
            {
                PoziceFramuX = 2 * SirkaFramu;
            }
            else
            {
                PoziceFramuX = 0;
            }
        }

        /// <summary>
        /// Nastaví základní rozměry framu (jednoho obrázku v animaci)
        /// </summary>
        public void PouzijMalyFrame()
        {
            SirkaFramu = 95;
            VyskaFramu = 96;
        }

        /// <summary>
        /// Vykreslí testové otázky k získávání drachem
        /// </summary>
        public void VykresliTestoveOtazky()
        {
            // Kontrolujeme, jestli už na otázku přišel čas, jinak je odpočet
            if (OdpocetDoPristyOtazky < 1)
            {
                // Nejdříve otázka 
                VypisText(String.Format("{0} {1}?", JazykMutace(Text_Kdo), VratTextOtazkyCiOdpovedi(CislaOtazek[PoziceSpravneOdpovedi], true)), 330, 20, Color.DarkBlue, Pismo_Normalni);

                // Teď odpovědi
                VypisText(String.Format("[Q] {0}", VratTextOtazkyCiOdpovedi(CislaOtazek[0], false)), 350, 80, Color.Black, Pismo_Mensi);
                VypisText(String.Format("[W] {0}", VratTextOtazkyCiOdpovedi(CislaOtazek[1], false)), 350, 130, Color.Black, Pismo_Mensi);
                VypisText(String.Format("[E] {0}", VratTextOtazkyCiOdpovedi(CislaOtazek[2], false)), 350, 180, Color.Black, Pismo_Mensi);
            }
            else
            {
                // Kontrolujeme ještě, jestli poslední otázka byla správně
                if (PosledniSpravne)
                {
                    // Pochvala
                    VypisText(JazykMutace(Text_Spravne), 330, 20, Color.DarkOliveGreen, Pismo_Normalni);
                }
                else
                {
                    // Pohana
                    VypisText(JazykMutace(Text_Spatne), 330, 20, Color.DarkRed, Pismo_Normalni);
                }

                // Odpočet do příští otázky: vytvoření svislých čárek
                SvisleCarky = String.Empty;
                Pocitadlo = 0;

                while (Pocitadlo < OdpocetDoPristyOtazky)
                {
                    SvisleCarky += String.Format("|");
                    Pocitadlo++;
                }

                // Odpočet do příští otázky: vypsání textu a svislých čárek
                VypisText(String.Format("{0}: {1} {2}", JazykMutace(Text_CasDoOtazky), OdpocetDoPristyOtazky, SvisleCarky), 350, 80, Color.DarkGray, Pismo_Mensi);
            }
        }

        public string VratTextOtazkyCiOdpovedi(int zadaneCislo, bool hledameOtazku)
        {
            foreach (Otazky otazka in Souhrn_Otazek)
            {
                if (otazka.Cislo == zadaneCislo)
                {
                    if (JazykHry == Jazyk.cz)
                    {
                        if (hledameOtazku)
                        {
                            return otazka.CeskyOtazka;
                        }
                        else
                        {
                            return otazka.CeskyOdpoved;
                        }
                    }
                    else if (JazykHry == Jazyk.en)
                    {
                        if (hledameOtazku)
                        {
                            return otazka.AnglickyOtazka;
                        }
                        else
                        {
                            return otazka.AnglickyOdpoved;
                        }
                    }
                    else
                    {
                        // Sem bychom se neměli dostat, jestli to půjde dobře
                        return String.Empty;
                    }
                }
            }

            // Ani sem bychom se neměli dostat, jestli to půjde dobře
            return String.Empty;
        }

        /// <summary>
        /// Vrátí nápis ve správném jazyce
        /// </summary>
        /// <param name="retezec">Který řetězec se má převést do jazykové mutace.</param>
        public string JazykMutace(Retezce retezec)
        {
            switch (JazykHry)
            {
                default:
                case Jazyk.en:
                    return String.Format(retezec.Vypis(Retezce.Jazyk.en));

                case Jazyk.cz:
                    return String.Format(retezec.Vypis(Retezce.Jazyk.cz));
            }
        }

        /// <summary>
        /// Pro vypsání řetězce, který není představen v několikajazyčných variantách. Především číselné hodnoty apod.
        /// </summary>
        /// <param name="retezec">Co se má vypsat, přímo string.</param>
        /// <param name="poziceX">Poloha na horizontále.</param>
        /// <param name="poziceY">Poloha na vertikále.</param>
        /// <param name="barva">Barva, kterou se má řetězec vypsat.</param>
        /// <param name="pismo">Typ písma, který má být použit.</param>
        public void VypisText(string retezec, int poziceX, int poziceY, Color barva, SpriteFont pismo)
        {
            VytvorRamecekZaTextem(pismo, retezec, poziceX, poziceY, Color.WhiteSmoke);
            _spriteBatch.DrawString(pismo, retezec, new Vector2(poziceX, poziceY), barva);
        }

        /// <summary>
        /// Pro lepší čitelnost vytvoří světlý rámeček za textem
        /// </summary>
        /// <param name="pismo">Použitý font</param>
        /// <param name="merenyRetezec">Text, který se vypisuje (normálně string)</param>
        /// <param name="poziceX">Pozice na horizontále</param>
        /// <param name="poziceY">Pozice na vertikále</param>
        /// <param name="barvaRamecku">Barva pozadí za textem</param>
        public void VytvorRamecekZaTextem(SpriteFont pismo, string merenyRetezec, int poziceX, int poziceY, Color barvaRamecku)
        {
            VelikostRameckuZaTextem = pismo.MeasureString(merenyRetezec);
            PozadiZaTextem = new Rectangle(poziceX-OdsazeniRameckuZaTextem, 
                poziceY- OdsazeniRameckuZaTextem, 
                (int)VelikostRameckuZaTextem.X+OdsazeniRameckuZaTextem*2, 
                (int)VelikostRameckuZaTextem.Y+OdsazeniRameckuZaTextem*2);
            _spriteBatch.Draw(Gfx_ZaTextem, PozadiZaTextem, barvaRamecku);
        }

        /// <summary>
        /// Upozorní, že hráč může přepnout jazyk
        /// </summary>
        public void VykresliPrepinaniJazyka()
        {
            VypisText(JazykMutace(Text_Jazyk), 20, 20, Color.DarkSlateGray, Pismo_Mensi);
        }

        /// <summary>
        /// Pokud bylo kliknuto bez drachem, vypíše varování
        /// </summary>
        public void VykresliVarovaniOZdrojich()
        {
            if (PocitadloVarovaniOZdrojich > 0)
            {
                VypisText(JazykMutace(Text_NedostatecneZdroje), PoziceVarovaniOZdrojichX, PoziceVarovaniOZdrojichY, Color.IndianRed, Pismo_Mensi);

                PocitadloVarovaniOZdrojich--;
            }
        }

        /// <summary>
        /// Nastaví časovač a pozici varování o zdrojích
        /// </summary>
        public void NastavVarovaniOZdrojich(int pocitadlo, int poziceX, int poziceY)
        {
            PocitadloVarovaniOZdrojich = pocitadlo;

            PoziceVarovaniOZdrojichX = poziceX;
            PoziceVarovaniOZdrojichY = poziceY;
        }

        /// <summary>
        /// Vypíše informace o klikání myší, ceny a odhady zbývajících
        /// </summary>
        public void VykresliInfoOKlikani()
        {
            VypocitejMoznaSeslani();

            // Rozšířené, nebo kompaktní zobrazení
            if (PocitadloZasahu < 10)
            {
                // Informace o léčení
                VypisText(String.Format("{0} | {1} {2} | {3} {4}",
                    JazykMutace(Text_SeslatLeceni), CenaLeceni.ToString(), JazykMutace(Text_Drachem),
                    MoznoLeceni.ToString(), JazykMutace(Text_MoznychSeslani)), 20, 262, Color.DarkSlateBlue, Pismo_Mensi);

                // Informace o poškození
                VypisText(String.Format("{0} | {1} {2} | {3} {4}",
                    JazykMutace(Text_SeslatHnev), CenaHnevu.ToString(), JazykMutace(Text_Drachem),
                    MoznoHnevu.ToString(), JazykMutace(Text_MoznychSeslani)), 20, 306, Color.DarkOrchid, Pismo_Mensi);
            }
            else 
            {
                // Informace o léčení
                VypisText(String.Format("{0} | {1} D. | {2}x",
                    JazykMutace(Text_SeslatLeceniKompakt), CenaLeceni.ToString(), 
                    MoznoLeceni.ToString()), 20, 262, Color.DarkSlateBlue, Pismo_Mensi);

                // Informace o poškození
                VypisText(String.Format("{0} | {1} D. | {2}x",
                    JazykMutace(Text_SeslatHnevKompakt), CenaHnevu.ToString(), 
                    MoznoHnevu.ToString()), 20, 306, Color.DarkOrchid, Pismo_Mensi);
            }
        }

        /// <summary>
        /// Aktualizuje odhady možných seslání léčení a Athénina hněvu
        /// </summary>
        public void VypocitejMoznaSeslani()
        {
            MoznoLeceni = Convert.ToInt32(Math.Floor((float)Drachmy / CenaLeceni));
            MoznoHnevu = Convert.ToInt32(Math.Floor((float)Drachmy / CenaHnevu));
        }

        /// <summary>
        /// Vypíše na obrazovku, kolik zbývá řeckých a trójských vojáků
        /// </summary>
        public void VykresliRezervy()
        {
            VypisText(String.Format("{0}: {1}", JazykMutace(Text_ReckeRezervy), ReckeRezervy.ToString()), 20, 164, Color.CornflowerBlue, Pismo_Mensi);
            VypisText(String.Format("{0}: {1}", JazykMutace(Text_TrojskeRezervy), TrojskeRezervy.ToString()), 20, 208, Color.DarkGoldenrod, Pismo_Mensi);
        }

        /// <summary>
        /// Vypíše množství peněz, které hráč má
        /// </summary>
        public void VykresliDrachmy()
        {
            VypisText(String.Format("{0}: {1}", JazykMutace(Text_Drachmy), Drachmy.ToString()), 20, 63, Color.DarkSlateBlue, Pismo_Normalni);
        }

        /// <summary>
        /// Vypíše množství získaných bodů
        /// </summary>
        public void VykresliSkore()
        {
            VypisText(String.Format("{0}: {1}", JazykMutace(Text_Skore), Skore.ToString()), 20, 113, Color.DarkCyan, Pismo_Normalni);
        }

        /// <summary>
        /// Vykreslí aktuální pozadí.
        /// </summary>
        /// <param name="pozadi">Pozadí, které má být aktuálně vykresleno.</param>
        public void VykresliPozadi(Texture2D pozadi)
        {
            _spriteBatch.Draw(pozadi, new Vector2(0, 0), Color.White);
        }

        /*
        
              ####################
              ####################
              ####################
              ####################
        

            Počítací (update) metody

                ↓           ↓  

        */

        /// <summary>
        /// Rozhodneme se, jaký je aktuální stav hry
        /// </summary>
        public void KontrolaHernihoStavu()
        {
            NactiKlavesnici();
            KontrolaPrepnutiJazyka();
            ZkontrolujMoznyKonec();

            switch (AktualniStavHry)
            {
                // Konec hry
                case StavHry.ZaverVyhra:
                case StavHry.ZaverProhra:
                    PocitejKonecHry();
                    break;

                // Tzv. "credits" pro zdroje (úplné potom na stránce hry)
                case StavHry.Informace:
                    PocitejObrazovkuSInformacemi();
                    break;

                // Hra jede
                case StavHry.HraJede:
                    PocitejJedouciHru();
                    break;

                // Úvodní obrazovka
                default:
                case StavHry.Uvod:
                    PocitejUvod();
                    break;
            }
        }

        /// <summary>
        /// Konec hry, tady je jen možnost restartovat hru
        /// </summary>
        public void PocitejKonecHry()
        {
            if (AktivniKlavesa == Keys.R)
            {
                NastavPromenneNaZacatekHry();
                ZalozVojaky();
                AktualniStavHry = StavHry.Uvod;
            }
        }

        /// <summary>
        /// Zde se jen čeká na mezerník pro návrat do intra
        /// </summary>
        public void PocitejObrazovkuSInformacemi()
        {
            if (AktivniKlavesa == Keys.Space)
            {
                AktualniStavHry = StavHry.Uvod;
            }
        }

        /// <summary>
        /// Vyhodnotí, jestli nejsou splněny podmínky pro konec hry.
        /// </summary>
        public void ZkontrolujMoznyKonec()
        {
            if (ReckeRezervy <= 0)
            {
                ReckeRezervy = 0;
                AktualniStavHry = StavHry.ZaverProhra;
                PrepniHudbu(Hudba_FireThunder);
            }

            if (TrojskeRezervy <= 0)
            {
                TrojskeRezervy = 0;
                ZmenSkore(500);
                AktualniStavHry = StavHry.ZaverVyhra;
                PrepniHudbu(Hudba_FireThunder);
            }
        }

        /// <summary>
        /// Když hra jede, tak se čeká na její spuštění či na "credits"
        /// </summary>
        public void PocitejUvod()
        {
            if (AktivniKlavesa == Keys.Space)
            {
                AktualniStavHry = StavHry.HraJede;
                PrepniHudbu(Hudba_Minotaur);
            }

            if (AktivniKlavesa == Keys.I)
            {
                AktualniStavHry = StavHry.Informace;
            }
        }

        /// <summary>
        /// Základní počty v jedoucí hře
        /// </summary>
        public void PocitejJedouciHru()
        {
            TestoveOtazky();
            AktualizaceDrachem();
            AktualizaceSkore();
            PosunCasovacUdalosti();
            OdpocetCasovaceProOtazky();
            BitkyVojaku();
            ManevryVojaku();
            KlikaniNaVojaky();
        }

        public void NastavZvlastniEfektAPrehrajZvuk(float poziceX, float poziceY, TypEfektu typ)
        {
            // Nastaví vizuální efekt
            ZvlastniEfekty_Typ = typ;
            ZvlastniEfekty_PoziceX = poziceX;
            ZvlastniEfekty_PoziceY = poziceY;
            ZvlastniEfekty_Pocitadlo = 20;

            // Přehraje zvuk
            switch (typ)
            {
                case TypEfektu.Leceni:
                    Zvuk_Lecba.Play();
                    break;

                case TypEfektu.Zranovani:
                    Zvuk_AtheninSek.Play();
                    break;
            }
        }

        /// <summary>
        /// Vyhodnotí, jestli hráč kliká na vojáky, aby je léčil či zraňoval
        /// </summary>
        public void KlikaniNaVojaky()
        {
            // Načteme vstup z myši
            Ovladani_Mysi = Mouse.GetState();

            // Pracujeme s určitým rozměrem jednotek
            PouzijMalyFrame();

            // Vyhodnocení klikání levým myšítkem
            if (Ovladani_Mysi.LeftButton == ButtonState.Pressed)
            {
                if (ProtiPretizeniMysi < 1)
                {
                    foreach (Vojak vojakZasazeny in Vojaci)
                    {
                        if ((Ovladani_Mysi.X >= vojakZasazeny.PoziceX && Ovladani_Mysi.X <= vojakZasazeny.PoziceX + SirkaFramu)
                            && (Ovladani_Mysi.Y >= vojakZasazeny.PoziceY && Ovladani_Mysi.Y <= vojakZasazeny.PoziceY + VyskaFramu))
                        {
                            // Zjistíme, jestli je Řek (-> léčení), nebo Trójan (-> hněv Athény/zranění)
                            if (vojakZasazeny.VernostVojaka == Vojak.StranyKonfliktu.Rekove)
                            {
                                if (vojakZasazeny.Zdravi < vojakZasazeny.MaxZdravi && vojakZasazeny.Zdravi > 0)
                                {
                                    if (Drachmy >= CenaLeceni)
                                    {
                                        // Používáme metodu na zranění, ale se zápornou hodnotou, takže léčí
                                        vojakZasazeny.ZranVojaka(Nahoda.Next(200, 600) * -1);

                                        // Zvláštní efekt
                                        NastavZvlastniEfektAPrehrajZvuk(vojakZasazeny.PoziceX, vojakZasazeny.PoziceY, TypEfektu.Leceni);

                                        // Platba za léčení
                                        ZmenDrachmy(CenaLeceni * -1);

                                        // Nastavíme přetížení myši, aby se muselo chvíli čekat
                                        ProtiPretizeniMysi = 15;

                                        // Počítadlo zásahů
                                        PocitadloZasahu++;
                                    }
                                    else
                                    {
                                        // Varování o nedostatku drachem
                                        NastavVarovaniOZdrojich(90, (int)vojakZasazeny.PoziceX - 5, (int)vojakZasazeny.PoziceY - 5);
                                    }
                                }
                            }
                            else
                            {
                                if (Drachmy >= CenaHnevu)
                                {
                                    // Používáme metodu na zranění
                                    vojakZasazeny.ZranVojaka(Nahoda.Next(200, 700));

                                    // Zvláštní efekt
                                    NastavZvlastniEfektAPrehrajZvuk(vojakZasazeny.PoziceX, vojakZasazeny.PoziceY, TypEfektu.Zranovani);

                                    // Vyhodnocení zabití a bodů
                                    if (vojakZasazeny.Zdravi <= 0)
                                    {
                                        ZmenSkore(2);
                                        TrojskeRezervy--;
                                    }

                                    // Platba za zranění nepřítele
                                    ZmenDrachmy(CenaHnevu * -1);

                                    // Nastavíme přetížení myši, aby se muselo chvíli čekat
                                    ProtiPretizeniMysi = 15;

                                    // Počítadlo zásahů
                                    PocitadloZasahu++;
                                }
                                else
                                {
                                    // Varování o nedostatku drachem
                                    NastavVarovaniOZdrojich(90, (int)vojakZasazeny.PoziceX - 5, (int)vojakZasazeny.PoziceY - 5);
                                }
                            }
                        }
                    }
                }
            }

            // Snižujeme přetížení myši
            if (ProtiPretizeniMysi > 0) { ProtiPretizeniMysi--; }
            if (ProtiPretizeniMysi < 0) { ProtiPretizeniMysi = 0; }
        }

        /// <summary>
        /// Ještě před vyhodnocením chození vojáků vyhodnotíme, jestli nemají poblíž nepřítele
        /// </summary>
        public void BitkyVojaku()
        {
            foreach (Vojak vojakUtocnik in Vojaci)
            {
                MaPredSebouSoupere = false;

                foreach (Vojak vojakBranici in Vojaci)
                {
                    // Ověříme, jestli jsou nepřátelé a jestli útočník ani soupeř už nejsou mrtví
                    if (vojakUtocnik.VernostVojaka != vojakBranici.VernostVojaka && vojakBranici.Zdravi > 0 && vojakUtocnik.Zdravi > 0)
                    {
                        // Nejprve zjistíme, jestli jsou si blízko na vertikále
                        if (vojakUtocnik.PoziceY >= vojakBranici.PoziceY - DelkaSvihu && vojakUtocnik.PoziceY <= vojakBranici.PoziceY + DelkaSvihu)
                        {
                            // Teď zjistíme, jestli jsou si blízcí i na horizontále
                            if (vojakUtocnik.PoziceX >= vojakBranici.PoziceX - DelkaSvihu && vojakUtocnik.PoziceX <= vojakBranici.PoziceX + DelkaSvihu)
                            {
                                // Potvrdíme, že je s kým bojovat
                                MaPredSebouSoupere = true;

                                // Nastavíme, že má útočit
                                vojakUtocnik.ZmenRozkazy(Vojak.CinnostiRozkazy.Utoc, vojakUtocnik.PoziceX, vojakUtocnik.PoziceY);

                                // Zranění nepřítele
                                if (CasovacProUdalosti == 1)
                                {
                                    // Zranění cíle
                                    vojakBranici.ZranVojaka(vojakUtocnik.Utok + Nahoda.Next(1, 6));
                                    
                                    // Potvrzujeme, že probíhá boj
                                    BojujeSe = 10;

                                    // Body za zabití
                                    if (vojakBranici.Zdravi < 1)
                                    {
                                        if (vojakBranici.VernostVojaka == Vojak.StranyKonfliktu.Trojane)
                                        {
                                            ZmenSkore(3);
                                            TrojskeRezervy--;
                                        }
                                        else
                                        {
                                            ZmenSkore(-1);
                                            ReckeRezervy--;
                                        }
                                    }
                                }

                                // Nastavíme správnou animaci
                                switch (vojakUtocnik.CoDelaAnimace)
                                {
                                    default:
                                    case Vojak.CinnostiAnimace.JdeVlevo:
                                    case Vojak.CinnostiAnimace.UtociVlevo:
                                    case Vojak.CinnostiAnimace.StojiOtocenVlevo:
                                        vojakUtocnik.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.UtociVlevo);                                        
                                        break;

                                    case Vojak.CinnostiAnimace.JdeVpravo:
                                    case Vojak.CinnostiAnimace.UtociVpravo:
                                    case Vojak.CinnostiAnimace.StojiOtocenVpravo:
                                        vojakUtocnik.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.UtociVpravo);
                                        break;

                                    case Vojak.CinnostiAnimace.JdeNahoru:
                                    case Vojak.CinnostiAnimace.UtociNahoru:
                                        vojakUtocnik.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.UtociNahoru);
                                        break;

                                    case Vojak.CinnostiAnimace.JdeDolu:
                                    case Vojak.CinnostiAnimace.UtociDolu:
                                        vojakUtocnik.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.UtociDolu);
                                        break;
                                }
                            }
                        }
                    }
                }

                // Pokud nenarazil na soupeře, ale útočí, pak se útok vypne
                if (!MaPredSebouSoupere && vojakUtocnik.CoDelaRozkazy == Vojak.CinnostiRozkazy.Utoc && vojakUtocnik.Zdravi > 0)
                {
                    vojakUtocnik.ZmenRozkazy(Vojak.CinnostiRozkazy.Cekej, 0, 0);
                    vojakUtocnik.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.StojiOtocenVpravo);
                }
            }
        }

        /// <summary>
        /// Nastaví chování (zejména chození) vojáků
        /// </summary>
        public void ManevryVojaku()
        {
            foreach (Vojak vojakManevr in Vojaci)
            {
                switch (vojakManevr.CoDelaRozkazy)
                {
                    // Voják čeká, pošleme ho do útoku
                    default:
                    case Vojak.CinnostiRozkazy.Cekej:
                        if (CasovacProUdalosti == 1)
                        {
                            VybranyVojakCislo = Nahoda.Next(Vojaci.Count);
                            
                            if (vojakManevr.VernostVojaka != Vojaci[VybranyVojakCislo].VernostVojaka)
                            {
                                vojakManevr.ZmenRozkazy(Vojak.CinnostiRozkazy.JdiDoUtoku, Vojaci[VybranyVojakCislo].PoziceX, Vojaci[VybranyVojakCislo].PoziceY);
                            }
                        }
                        break;

                    // U těchto činností se nebude dít nic
                    case Vojak.CinnostiRozkazy.Utoc:
                    case Vojak.CinnostiRozkazy.Zhyn:
                        break;

                    // Voják už jde do útoku, musíme vyhodnotit, kterým směrem
                    case Vojak.CinnostiRozkazy.JdiDoUtoku:
                        // Jesli není, jde se dál, zde nastavujeme animace
                        if (vojakManevr.CilPohybuX == vojakManevr.PoziceX)
                        {
                            // Vertikální polohování pro animaci
                            if (vojakManevr.CilPohybuY < vojakManevr.PoziceY)
                            {
                                // Voják jde nahoru
                                vojakManevr.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.JdeNahoru);
                            }
                            else
                            {
                                // Voják jde dolů
                                vojakManevr.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.JdeDolu);
                            }
                        }
                        else
                        {
                            // Horizontální polohování pro animaci
                            if (vojakManevr.CilPohybuX < vojakManevr.PoziceX)
                            {
                                // Voják jde doleva
                                vojakManevr.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.JdeVlevo);
                            }
                            else
                            {
                                // Voják jde doprava
                                vojakManevr.ZmenAnimacniCinnost(Vojak.CinnostiAnimace.JdeVpravo);
                            }
                        }

                        // A teď nastavíme reálný posun
                        if (vojakManevr.TypTohotoVojaka == Vojak.TypyVojaku.ReckyAchilles)
                        {
                            RychlostPochodu = 2;
                        }
                        else
                        {
                            RychlostPochodu = 1;
                        }

                        if (vojakManevr.CilPohybuX < vojakManevr.PoziceX) { vojakManevr.PosunVojakaX(RychlostPochodu * -1); }
                        if (vojakManevr.CilPohybuX > vojakManevr.PoziceX) { vojakManevr.PosunVojakaX(RychlostPochodu); }
                        if (vojakManevr.CilPohybuY < vojakManevr.PoziceY) { vojakManevr.PosunVojakaY(RychlostPochodu * -1); }
                        if (vojakManevr.CilPohybuY > vojakManevr.PoziceY) { vojakManevr.PosunVojakaY(RychlostPochodu); }

                        // Vyhodnotíme, jestli je v cíli, musíme vytvořit toleranci, a to především kvůli příliš rychlonohému Achilleovi
                        if ((vojakManevr.PoziceX >= vojakManevr.CilPohybuX - 5 && vojakManevr.PoziceX <= vojakManevr.CilPohybuX + 5)
                            && (vojakManevr.PoziceY >= vojakManevr.CilPohybuY - 5 && vojakManevr.PoziceY <= vojakManevr.CilPohybuY + 5))
                        {
                            vojakManevr.ZmenRozkazy(Vojak.CinnostiRozkazy.Cekej, 0, 0);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Zde se zadávají a vyhodnocují testové otázky, skrze které se čerpají dotace (drachmy)
        /// </summary>
        public void TestoveOtazky()
        {
            if (OtazkaPolozena)
            {
                VyhodnocujOtazku();
            }
            else
            {
                PolozOtazku();
            }
        }

        /// <summary>
        /// Pokládá se nová otázka
        /// </summary>
        public void PolozOtazku()
        {
            // Zkontrolujeme ještě odpočet mezi otázkami
            if (OdpocetDoPristyOtazky < 1)
            {
                // Vybere otázku
                CislaOtazek[0] = Nahoda.Next(0, Souhrn_Otazek.Count);

                // Potřebujeme ještě ze dvou jiných náhodných otázek vybrat dvě špatné odpovědi
                // První špatná
                do
                {
                    CislaOtazek[1] = Nahoda.Next(0, Souhrn_Otazek.Count);
                }
                while (CislaOtazek[1] == CislaOtazek[0]);

                // Druhá špatná
                do
                {
                    CislaOtazek[2] = Nahoda.Next(0, Souhrn_Otazek.Count);
                }
                while (CislaOtazek[2] == CislaOtazek[1] || CislaOtazek[2] == CislaOtazek[0]);

                // Ještě musíme zamíchat pořadím, ale zapamatovat si správnou
                Pocitadlo = Nahoda.Next(3, 24);

                // Pocitadlo tady znamená počet posuvů
                PoziceSpravneOdpovedi = 0;

                for (int i = 0; i < Pocitadlo; i++)
                {
                    PomocneCislo = CislaOtazek[2];
                    CislaOtazek[2] = CislaOtazek[1];
                    CislaOtazek[1] = CislaOtazek[0];
                    CislaOtazek[0] = PomocneCislo;

                    PoziceSpravneOdpovedi++;

                    if (PoziceSpravneOdpovedi > 2) { PoziceSpravneOdpovedi = 0; }
                }

                // Zaznačíme, že otázka už byla položena, jinak by se 30krát za sekundu změnila
                OtazkaPolozena = true;
            }
        }

        public void OdpocetCasovaceProOtazky()
        {
            if (CasovacProUdalosti == 4)
            {
                OdpocetDoPristyOtazky--;
            }
        }

        /// <summary>
        /// Vyhodnocuje se správné zodpovězení položené otázky
        /// </summary>
        public void VyhodnocujOtazku()
        {
            if (AktivniKlavesa == Keys.Q)
            {
                VyhodnotHracovuOdpoved(0);
            }
            else if (AktivniKlavesa == Keys.W)
            {
                VyhodnotHracovuOdpoved(1);
            }
            else if (AktivniKlavesa == Keys.E)
            {
                VyhodnotHracovuOdpoved(2);
            }
        }

        /// <summary>
        /// Vyhodnotí, jestli hráč správně zodpověděl otázku
        /// </summary>
        /// <param name="pozice"></param>
        public void VyhodnotHracovuOdpoved(int pozice)
        {
            if (PoziceSpravneOdpovedi == pozice)
            {
                ZmenDrachmy(CenaZaOdpoved);
                ZmenSkore(35);
                PosledniSpravne = true;
            }
            else
            {
                PosledniSpravne = false;
            }

            // Otázka je vyhodnocena, není tedy již položena
            OtazkaPolozena = false;
            OdpocetDoPristyOtazky = 20;
        }

        /// <summary>
        /// Změní množství drachem, které hráč má
        /// </summary>
        public void ZmenDrachmy(int zmena)
        {
            // Nezměníme to hned, protože chceme efekt, že se to načítá
            CiloveDrachmy = CiloveDrachmy + zmena;

            // Zadlužit se naštěstí hráč nemůže
            if (CiloveDrachmy < 0)
            {
                CiloveDrachmy = 0;
            }
        }
        
        /// <summary>
        /// Dynamická změna drachem
        /// </summary>
        public void AktualizaceDrachem()
        {
            // Chceme, aby se to spustilo jen občas
            if (CasovacProUdalosti == 3)
            {
                // Drachmy dohánějí hodnotu CiloveDrachmy, ale ne každý herní tick
                if (Drachmy < CiloveDrachmy) { Drachmy++; }
                if (Drachmy > CiloveDrachmy) { Drachmy--; }
            }
        }

        /// <summary>
        /// Změní množství drachem, které hráč má
        /// </summary>
        public void ZmenSkore(int zmena)
        {
            // Nezměníme to hned, protože chceme efekt, že se to načítá
            CiloveSkore = CiloveSkore + zmena;

            // Jít do mínusu s body naštěstí hráč nemůže
            if (CiloveSkore < 0)
            {
                CiloveSkore = 0;
            }
        }

        /// <summary>
        /// Dynamická změna drachem
        /// </summary>
        public void AktualizaceSkore()
        {
            // Chceme, aby se to spustilo jen občas
            if (CasovacProUdalosti == 4)
            {
                // Drachmy dohánějí hodnotu CiloveDrachmy, ale ne každý herní tick
                if (Skore < CiloveSkore) { Skore++; }
                if (Skore > CiloveSkore) { Skore--; }
            }
        }

        /// <summary>
        /// Hra se aktualizuje 30krát za sekundu, některé věci ale chceme pomaleji, proto máme tento časovač
        /// </summary>
        public void PosunCasovacUdalosti()
        {
            CasovacProUdalosti++;
            if (CasovacProUdalosti > 5) { CasovacProUdalosti = 0; }

            CasovacProSavlicku++;
            if (CasovacProSavlicku > 20) { CasovacProSavlicku = 0; }
        }

        /// <summary>
        /// Načte vstup z klávesnice, ale má zároveň počítadlo proti přetížení kláves
        /// </summary>
        public void NactiKlavesnici()
        {
            if (ProtiPretizeniKlaves <= 0)
            {
                Ovladani_Klavesnice = Keyboard.GetState();

                // Pole všech načtených kláves nás zajímá tehdy, není-li o nulové délce
                if (Ovladani_Klavesnice.GetPressedKeys().Length > 0)
                {
                    // Načte první klávesu z pole všech načtených kláves
                    AktivniKlavesa = Ovladani_Klavesnice.GetPressedKeys()[0];
                    ProtiPretizeniKlaves = 15;
                }
                else
                {
                    AktivniKlavesa = Keys.None;
                }
            }
            else
            {
                AktivniKlavesa = Keys.None;
                ProtiPretizeniKlaves--;
            }
        }

        /// <summary>
        /// Kontrola, jestli nebyla stisknuta klávesa pro přepnutí jazykové varianty
        /// </summary>
        public void KontrolaPrepnutiJazyka()
        {
            if (AktivniKlavesa == Keys.L)
            {
                switch (JazykHry)
                {
                    default:
                    case Jazyk.cz:
                        JazykHry = Jazyk.en;
                        break;

                    case Jazyk.en:
                        JazykHry = Jazyk.cz;
                        break;
                }
            }
        }

        // Konec hlavní třídy
    }
}
