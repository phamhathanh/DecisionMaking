using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleMock
{
    public class Dilemma
    {
        private List<Choice> choices;
        private List<Criterion> criteria;

        public Dilemma(IEnumerable<Criterion> criteria)
        {

        }

        public Choice CreateChoice()
        {
            throw new NotImplementedException();
        }

        public float GetPreference(Choice choice1, Choice choice2)
        {

        }

        public float GetDiscredit(Choice choice1, Choice choice2)
        {

        }
    }
}
