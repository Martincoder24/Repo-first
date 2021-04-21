using System;


namespace Bussen
{
    class Buss
    {
        #region Variabler
        public Passagerare[] passagerare = new Passagerare[25];
        public int counter;//Håller koll på lediga platser i bussen
        string kön;             //Dessa används i påstigningsmetoden
        string humör;
        public int dagsKassa = 0;
        bool ejHeltal = true;//Kontrollerar att antalet passagerare är en siffra mellan 1-25.
        int svar;//Sparar svaret i påstigningen
        int totalÅlder;
        int alternativ;//För menyn
        public int svaret = 0;
        double medelÅlder;//Används till att räkna ut medelåldern.
        int maxÅlder;//Använder dessa 2 för att räkna ut maxålder
        int maxPosition;
        int hittaÅldersSpann1;//Använder dessa 2 för att hålla koll på spannet för åldrarna.
        int hittaÅldersSpann2;
        bool körMeny = true; //För menyn tills användaren väljer avsluta-programmet-metoden.
        #endregion

        #region Menyn
        public void Run()
        {
            do
            {
                Console.Clear();
                Console.WriteLine("=========================================");
                Console.WriteLine("     Välkommen till Buss-simulatorn      ");
                Console.WriteLine("=========================================");
                Console.WriteLine();
                Console.WriteLine("Du kan nu väja följande alternativ:");
                Console.WriteLine();
                Console.WriteLine("1. Påstigning");
                Console.WriteLine("2. Ta reda på vilka som sitter i bussen för tillfället");
                Console.WriteLine("3. Ta reda på den totala åldern i bussen");
                Console.WriteLine("4. Ta reda på medelåldern i bussen");
                Console.WriteLine("5. Ta reda på den äldsta passageraren i bussen");
                Console.WriteLine("6. Ta reda på vart passagerarena inom ett visst ålderspann sitter");
                Console.WriteLine("7. Sortera bussen efter åldern på passagerarna");
                Console.WriteLine("8. Ta reda på hur många personer i bussen som är kvinnor respektive män");
                Console.WriteLine("9. Peta på någon i bussen");
                Console.WriteLine("10. Avstigning");
                Console.WriteLine("11. Avsluta bussprogrammet och skriv ut dagskassan");
                


                try
                {
                    int alternativ = int.Parse(Console.ReadLine());

                    switch (alternativ)//Användaren får mata in en siffra som motsvarar ett val i menyn.
                    {
                        case 1:
                            Påstigning();
                            break;

                        case 2:
                            PrintBuss();
                            break;

                        case 3:
                            BeräknaTotalÅldern();
                            Console.WriteLine("Den totala åldern på bussen är {0} år.", totalÅlder);
                            Console.ReadKey();
                            break;

                        case 4:
                            BeräknaMedelvärdet();
                            Console.WriteLine("Medelåldern i bussen är {0} år.", medelÅlder);
                            Console.ReadKey();
                            break;

                        case 5:
                            MaxÅlder();
                            Console.WriteLine("Den äldsta passageraren är {0} år och sitter på plats {1}.", maxÅlder, maxPosition);
                            Console.ReadKey();
                            break;

                        case 6:
                            HittaÅldersSpannet();
                            Console.ReadKey();
                            break;

                        case 7:
                            SorteraBuss();                                                        
                            break;

                        case 8:
                            VilketKönPåVilkenPlats();                            
                            break;

                        case 9:
                            PetaPå();
                            break;

                        case 10:
                            Avstigning();
                            break;

                        case 11:
                            Avsluta();
                            
                            break;
                        default:
                            Console.WriteLine("Välj ett alternativ i listan:");
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste skriva in ett heltal som matchar det alternativet du vill välja!");// Fångar upp användaren ifall hen inte använder sig utav ett heltal.
                }
            } while (körMeny);
        }


        #endregion

        #region Påstigning
        public void Påstigning()
        {
            counter = 0;//Nollställa lediga-platser-räknaren.
            for (int i = 0; i < passagerare.Length; i++)//Räkna antalet lediga platser i vektorn
            {
                if (passagerare[i] == null)
                {
                    counter++;
                }
            }
            Console.WriteLine("Bussen har stannat. Hur många passagerare stiger på?");
            do
            {
                try
                {
                    svar = int.Parse(Console.ReadLine());
                    ejHeltal = false;
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste skriva in ett heltal");
                }
            } while (ejHeltal); //Try-catch-loop som ser till att ett heltal blir inmatat
            ejHeltal = true; //Vid ytterligare kontroll av påstigningen så skall try-catch-loop fungera

            svaret += svar;
            if (svar <= counter)//Det måste finnas plats för alla påstigande passagerare
            {
                Console.WriteLine("kliv på bussen");
                Console.WriteLine("{0}st passagerare har stigit på bussen. Nu finns det bara {1} lediga platser kvar.", svar, 25 - svaret);

                for (int i = 0; i < svaret; i++)//Skapar det antal passagerare som användaren matat in.
                {
                    int randÅlder = new Random().Next(1, 101);// En ålder mellan 1-100
                    int randKön = new Random().Next(1, 3);
                    if (randKön == 1 && randÅlder >= 18)
                    {
                        kön = "man";
                    }
                    else if (randKön == 1 && randÅlder < 18)
                    {
                        kön = "pojke";
                    }
                    else if (randKön == 2 && randÅlder >= 18)
                    {
                        kön = "kvinna";
                    }
                    else
                    {
                        kön = "flicka";
                    }
                    int randHumör = new Random().Next(1, 3);
                    if (randHumör == 1)
                    {
                        humör = "glad";
                    }
                    else
                    {
                        humör = "sur";
                    }
                    if (passagerare[i] == null)//Måste se till att kommande objekt hamnar efter nuvarande objekt
                    {
                        passagerare[i] = new Passagerare(randÅlder, kön, humör);

                        dagsKassa += BiljettPris(randÅlder); //Lägger till biljettenspris till dagskassan.
                    }



                }
            }
            else//Annars så skrivs detta ut och terminerar påstigningen
            {
                Console.WriteLine("Det finns inte tillräckligt med platser, var god och vänta på nästa buss.");
            }
            Console.ReadKey();
        }
        #endregion

        #region BiljettPris Metod
        public int BiljettPris(int Ålder)
        {
            if (Ålder < 18)//Barn/Ungdom
            {
                return 10;
            }
            else if (Ålder > 18 && Ålder < 65)//Vuxen
            {
                return 30;
            }
            else //Pensionär
            {
                return 15;
            }
        }
        #endregion

        #region PrintBuss
        public void PrintBuss()
        {
            Console.Clear();//Rensar kosollen på överflödig output
            for (int i = 0; i < passagerare.Length; i++)
            {

                if (passagerare[i] != null)//SKriver ut alla passagerare.
                {
                    Console.WriteLine("På plats {0} sitter en {1} {2} som är {3} år gammal.", i + 1,passagerare[i].humör, passagerare[i].kön, passagerare[i].ålder);
                }
                else
                {
                    Console.WriteLine("Plats {0} är ledig.", i + 1);//Är inte bussen full så skrivs detta ut på första lediga plats.
                }
            }
            Console.ReadKey();
            //Skriv ut alla värden ur vektorn. Alltså - skriv ut alla passagerare
        }
        #endregion

        #region BeräknaTotalÅldern
        public int BeräknaTotalÅldern()
        {
            totalÅlder = 0;
            foreach (var temp in passagerare)
            {
                if (temp != null)
                {
                    totalÅlder += temp.ålder;//Sparar in alla passagerares ålder och summerar detta till en totalålder.
                }

            }
            return totalÅlder;           
        }
        #endregion

        #region BeräknaMedelvärdet
        public double BeräknaMedelvärdet()
        {
            medelÅlder = 0;
            int antal = 0;
            foreach (var temp in passagerare)
            {
                if (temp != null)
                {
                    medelÅlder += temp.ålder;//Samlar på sig åldervärdet av alla befintliga passagerare
                    antal++;//Adderar ett till antal-variabeln för varje befintlig passagerare.
                }
                else
                {
                    medelÅlder = medelÅlder / (double)antal;//Delar den totala åldern på antalet befintliga passagerare.
                    break;
                }
            }
            return medelÅlder;
        }
        #endregion

        #region MaxÅlder
        public int MaxÅlder()
        {
            maxÅlder = 0;//Förnyar maxåldern så att man kan göra en ny beräkning vid eventuellt nya passagerare
            maxPosition = 0;//Samma som ovan för att återställa platserna
            for (int i = 0; i < passagerare.Length; i++)
            {
                if (maxÅlder < passagerare[i].ålder && passagerare[i] != null)
                {
                    maxÅlder = passagerare[i].ålder;
                    maxPosition = i + 1;//Anger platsen för högsta åldern.
                }

            }
            return maxÅlder;
        }
        #endregion

        #region HittaÅldersSPannet
        public void HittaÅldersSpannet()
        {
            do
            {
                Console.WriteLine("Markera ett åldersspann genom att mata in 2 positiva heltal. Mata in det minsta först:");
                try
                {
                    hittaÅldersSpann1 = int.Parse(Console.ReadLine());
                    if (hittaÅldersSpann1 > 0)//Ser till att det första talet alltid är över 0 dvs. positivt.
                    {
                        Console.WriteLine("Mata nu in det större heltalet:");
                        hittaÅldersSpann2 = int.Parse(Console.ReadLine());
                    }
                    
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste skriva in ett åldersspann med två positiva heltal! Det minsta talet först.");
                    Console.WriteLine("Var god och försök igen.");
                }
                if (hittaÅldersSpann1 > hittaÅldersSpann2)
                {
                    Console.WriteLine("Din första inmatning måste vara mindre än ditt andra inmatade tal!");
                    Console.WriteLine("Var god och försök igen.");
                }
            } while (hittaÅldersSpann1 > hittaÅldersSpann2 || hittaÅldersSpann1 < 0 || hittaÅldersSpann2 < 0);

            for (int i = 0; i < passagerare.Length; i++)
            {
                //Här kollar for-loopen genom hela objektvektorn och tar ut de som har en ålder i det ålderspann man satt upp. 
                if (passagerare[i].ålder >= hittaÅldersSpann1 && passagerare[i].ålder <= hittaÅldersSpann2 && passagerare[i] != null)
                {
                    Console.WriteLine("På plats {0} sitter en {1} {2} som är {3} år gammal.", i + 1, passagerare[i].humör, passagerare[i].kön, passagerare[i].ålder);
                }
            }
        }
        #endregion

        #region SorteraBussen
        public void SorteraBuss()
        {
            for (int i = 0; i < passagerare.Length; i++)
            {
                if (passagerare[i] != null)//Körs enbart på aktuella objekt
                {
                    for (int j = 0; j < passagerare.Length; j++)
                    {
                        if (passagerare[j] != null)//Körs enbart på aktuella objekt
                        {
                            if (passagerare[j].ålder < passagerare[i].ålder) //Byter bara plats ifall det kommande objektets ålder är större än det nuvarande objektets ålder.
                            {
                                Passagerare temp = passagerare[j];
                                passagerare[j] = passagerare[i];
                                passagerare[i] = temp;//Byter plats på objekten baserat på åldern. Störst först.
                            }
                        }
                    }
                }
            }
            Console.WriteLine("Nu är bussen sorterad med avseende på passagerarnas ålder. Äldst sitter längst fram.");
            Console.ReadKey();
        }
        #endregion

        #region VilketKönPåVilkenPlats
        public void VilketKönPåVilkenPlats()
        {
            Console.WriteLine("Kvinnor och flickor sitter på följande platser: ");
            Console.WriteLine();
            for (int i = 0; i < passagerare.Length; i++)//Kör genom loopen för att kolla kvinnliga passagerare.
            {
                if (passagerare[i].kön == "kvinna" || passagerare[i].kön == "flicka" && passagerare[i] != null)
                {
                    Console.WriteLine("På plats {0} sitter en {1}.", i + 1, passagerare[i].kön);
                }
            }
            Console.WriteLine();
            Console.WriteLine("Män och pojkar sitter på följande platser: ");
            Console.WriteLine();
            for (int i = 0; i < passagerare.Length; i++)//Kör en ny loop för att kolla alla manliga passagerare.
            {
                if (passagerare[i].kön == "man" || passagerare[i].kön == "pojke" && passagerare[i] != null)
                {
                    Console.WriteLine("På plats {0} sitter en {1}.", i + 1, passagerare[i].kön);
                }
            }
            Console.ReadKey();
        }
        #endregion

        #region PetaPå
        public void PetaPå()
        {
            bool felhantering = true;

            PrintBuss();//Printar bussen för att man ska kunna välja passagerare man vill interagera med.
            Console.WriteLine();
            do
            {
                Console.WriteLine("Ange platsen på den passagerare du vill peta på?");

                try
                {
                    int petning = int.Parse(Console.ReadLine());//Användaren matar in platsen på den passagerare som hen vill interagera med.
                    passagerare[petning - 1].Interagera();//Matchar indexet i vektorn.
                    Console.WriteLine("Tryck enter för att peta på en till passagerare");
                     
                    if(Console.ReadLine() != "")
                    {
                        felhantering = false;//Stänger loopen om man inte vill interagera mer.
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Du kan bara interagera med en existerande passagerare.");
                }
            }while (felhantering);    
            Console.ReadKey();
        } 
        #endregion

        #region Avstigning
        public void Avstigning()
        {
            bool flerAvstigande = true;
            do
            {
                flerAvstigande = true;
                Console.WriteLine("Mata in platsen på den passagerare som skall stiga av? ");
                try
                {
                    int svar = int.Parse(Console.ReadLine());
                    for (int i = svar - 1; i < passagerare.Length; i++)//Iochmed att arrayer börjar med [0] så måste jag ta minus 1 från svaret.
                    {
                        passagerare[i] = null;//Passageraren stiger av.
                        if (i != 24)//Om i inte är 24 så görs följande kodblock
                        {                            
                            if (passagerare[i] == null)//När passageraren gått av så flyttar passageraren bakom framåt ett steg.
                            {
                                passagerare[i] = passagerare[i + 1];
                            }
                        }                        
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Du måste mata in en siffra mellan 1-25");
                }
                Console.WriteLine("Mata in \"nej\" ifall ingen mer skall stiga av. Annars klicka valfri tangent:");//Vid avstigning av flera passagerare görs detta.
                string merAvstigande = Console.ReadLine(); //Kör om loopen tills man skriver in nej.
                if (merAvstigande.ToLower() == "nej")
                {
                    flerAvstigande = false;
                }
            } while (flerAvstigande);

        }
        #endregion

        #region Avsluta
        public void Avsluta()
        {
            Console.WriteLine("Tack för användning utav buss-simulatorn. Dagens dagskassa landade på {0}kr.", dagsKassa);
            körMeny = false;//Avslutar menyn och räknar ihop dagskassan.
        } 
        #endregion

    }

    class Program
    {
        static void Main(string[] args)
        {
            var minbuss = new Buss();
            minbuss.Run();

            Console.Write("Press any key to continue . . . ");
            Console.ReadKey(true);
        }
    }
}
 


