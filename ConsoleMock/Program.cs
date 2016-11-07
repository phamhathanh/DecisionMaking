using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleMock
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var speed = new Criterion(1);
            var criteria = new[] { speed };
            var choices = new[] { "VW Golf C", "Renault R9 GTL", "Citroen GSA X1", "Peugeot P305 GR",
                    "Talbot HOR.GLS", "Audi 80 CL", "Renault R18 GTL", "Alfa SUD TI-NR" };
            var carDecision = new Dilemma(criteria, choices);

            carDecision.preferenceThreshold = gs => 0.1f * gs;
            carDecision.indifferenceThreshold = gs => 5;

            var speedByChoice = new Dictionary<string, float>()
            {
                ["VW Golf C"] = 140,
                ["Renault R9 GTL"] = 150,
                ["Citroen GSA X1"] = 160,
                ["Peugeot P305 GR"] = 153,
                ["Talbot HOR.GLS"] = 164,
                ["Audi 80 CL"] = 148,
                ["Renault R18 GTL"] = 155,
                ["Alfa SUD TI-NR"] = 170
            };
            carDecision.AddEvaluation((choice, criterion) => speedByChoice[choice]);

            var allPairs = from car1 in choices
                           from car2 in choices
                           where car1.CompareTo(car2) == -1
                           select new Tuple<string, string>(car1, car2);

            for (int i = 0; i < choices.Length; i++)
            {
                for (int j = 0; j < choices.Length; j++)
                {
                    var preference = carDecision.GetPreference(choices[i], choices[j], speed);
                    Console.Write($"{preference:F3}\t");
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            for (int i = 0; i < choices.Length; i++)
            {
                for (int j = 0; j < choices.Length; j++)
                {
                    var strictPreference = carDecision.GetStrictPreference(choices[i], choices[j], speed);
                    Console.Write($"{strictPreference:F3}\t");
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
