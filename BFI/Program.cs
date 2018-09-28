using System;
using System.Collections.Generic;
using static BFI.Cities;

namespace BFI
{
    public class Program
    {

        static void Main(string[] args)
        {
            Program m_objProgram = new Program();

            int m_intChoice = 0;
            int m_intExit = 4;
            string m_strCountry = "ID";
            Cities Cities = new Cities();
            Cities.PrepareCities(m_strCountry);

            while (m_intChoice != m_intExit)
            {
                Console.Clear();
                Console.WriteLine("Please choose the number:");
                Console.WriteLine("1. Find City");
                Console.WriteLine("2. Text Generator");
                Console.WriteLine("3. XYZ Number");
                Console.WriteLine(m_intExit.ToString() + ". Exit");
                string m_strChoice = Console.ReadLine();

                if (!int.TryParse(m_strChoice, out m_intChoice) || m_intChoice < 1 || m_intChoice >= m_intExit)
                    continue;

                switch (m_intChoice)
                {
                    case 1:
                        FindCity(Cities);
                        break;
                    case 2:
                        GenerateText();
                        break;
                    case 3:
                        XYZNumber();
                        break;
                }
                Console.WriteLine("Press any key to continue");
                Console.Read();
            }
        }

        public static void FindCity(Cities Cities)
        {
            Console.Clear();
            Console.WriteLine("Please enter city name:");
            string m_strCity = Console.ReadLine();
            if (m_strCity.Length <= 0)
                Console.WriteLine("No city name has been inputed");
            else
            {
                Console.WriteLine(string.Empty);
                List<string> m_lstCities = Cities.Find(m_strCity);
                if (m_lstCities.Count <= 0)
                    Console.WriteLine("No cities found!");
                else
                {
                    Console.WriteLine("Cities found:");
                    Console.WriteLine(String.Join(", ", m_lstCities));
                }
            }
            Console.WriteLine(string.Empty);
        }

        public static void GenerateText()
        {
            Console.Clear();
            TextGenerator m_objTextGenerator = new TextGenerator();
            Console.WriteLine("Text generated:");
            string m_strGeneratedText = m_objTextGenerator.RandomText(12);
            Console.WriteLine(m_strGeneratedText);
            Console.WriteLine(string.Empty);
        }

        public static void XYZNumber()
        {
            Console.Clear();
            Console.WriteLine("Input number (1 - 1000):");
            string m_strNumber = Console.ReadLine();
            int m_intNumber = 0;
            if (!int.TryParse(m_strNumber, out m_intNumber))
                Console.WriteLine("Input is not number");
            else
            {
                XYZNumber m_objXYZNumber = new XYZNumber();
                Console.WriteLine(m_objXYZNumber.GenerateXYZNumber(m_intNumber));
            }
            Console.WriteLine(string.Empty);
        }
    }
}
