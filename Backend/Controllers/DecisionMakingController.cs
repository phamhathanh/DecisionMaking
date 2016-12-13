using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DecisionMaking;

namespace Backend.Controllers
{
    public class DecisionMakingController : Controller
    {
        public class Place
        {
            public string name, price, distance, funness, quality;
            // Hacky, cuz not sure if it's needed for json.
        }
        public class Vote
        {
            public Place[] places;
        }

        private readonly TrapezoidalFuzzyNumber[] fuzzyValues = new[]
        {
            new TrapezoidalFuzzyNumber(0, 0.2, 0.2, 0.4),
            new TrapezoidalFuzzyNumber(0.4, 0.6, 0.6, 0.8),
            new TrapezoidalFuzzyNumber(0.6, 0.8, 0.8, 1),
            new TrapezoidalFuzzyNumber(0.4, 0.6, 0.6, 1),
            new TrapezoidalFuzzyNumber(0.8, 1, 1, 1)
        };

        private readonly string[]
            priceLabels = CreateLabels("Expensive", "Cheap"),
            distanceLabels = CreateLabels("Far", "Near"),
            funnessLabels = CreateLabels("Boring", "Fun"),
            qualityLabels = CreateLabels("Bad", "Good");

        private static string[] CreateLabels(string bad, string good)
            => new[] { $"Very {bad}", bad, "Normal", good, $"Very {good}" };

        private Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>(TKey[] keys, TValue[] values)
            => values.ToDictionary(value => keys[Array.IndexOf(values, value)]);

        [HttpPost]
        [Route("api/algorithm")]
        public IEnumerable<string> RunAlgorithm([FromBody] Vote[] votes)
        {
            var price = new Criterion(CreateDictionary(priceLabels, fuzzyValues), 1);
            var distance = new Criterion(CreateDictionary(distanceLabels, fuzzyValues), 0.625);
            var funness = new Criterion(CreateDictionary(funnessLabels, fuzzyValues), 0.625);
            var quality = new Criterion(CreateDictionary(qualityLabels, fuzzyValues), 0.25);
            var criteria = new[] { price, distance, funness, quality };

            var places = new[] { "Sapa", "Tam Dao", "Ba Vi" };

            var evaluation = new Dictionary<string, Dictionary<Criterion, string>>();
            var lowa = new LOWA(5);
            for (int i = 0; i < places.Length; i++)
            {
                var priceIndices = from vote in votes
                                   select Array.IndexOf(priceLabels, vote.places[i].price);
                var distanceIndices = from vote in votes
                                      select Array.IndexOf(distanceLabels, vote.places[i].distance);
                var funnessIndices = from vote in votes
                                     select Array.IndexOf(funnessLabels, vote.places[i].funness);
                var qualityIndices = from vote in votes
                                     select Array.IndexOf(qualityLabels, vote.places[i].quality);
                var placeEvaluation = new Dictionary<Criterion, string>
                {
                    [price] = priceLabels[lowa.Average(priceIndices.ToArray())],
                    [distance] = distanceLabels[lowa.Average(distanceIndices.ToArray())],
                    [funness] = funnessLabels[lowa.Average(funnessIndices.ToArray())],
                    [quality] = qualityLabels[lowa.Average(qualityIndices.ToArray())]
                };
                evaluation.Add(places[i], placeEvaluation);
            }

            var dilemma = new Dilemma(evaluation);
            var clearlyBestPlaces = from place in places
                                    where dilemma.GetNonDominationDegree(place) == 1
                                    select place;

            return clearlyBestPlaces;
        }
    }
}
