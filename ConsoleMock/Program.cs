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
            var price = new Criterion(1);
            var funness = new Criterion(1);

            var dilemma = new Dilemma(new[] { price, funness });

            var firstChoice = dilemma.CreateChoice();
            var secondChoice = dilemma.CreateChoice();
        }
    }
}
