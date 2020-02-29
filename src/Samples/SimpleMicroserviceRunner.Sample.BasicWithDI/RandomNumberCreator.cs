using System;

namespace SimpleMicroserviceRunner.Sample.BasicWithDI
{
    public class RandomNumberCreator : IRandomNumberCreator
    {
        private readonly IRandomNumberRangeProvider randomNumberRangeProvider;

        public RandomNumberCreator(IRandomNumberRangeProvider randomNumberRangeProvider)
        {
            this.randomNumberRangeProvider = randomNumberRangeProvider;
        }

        public int CreateRandomNumner()
        {
            Random random = new Random();
            var min = this.randomNumberRangeProvider.GetMin();
            var max = this.randomNumberRangeProvider.GetMax();

            Console.WriteLine($"New Random Number Range: {min}-{max}.");
            return random.Next(min, max);
        }
    }
}
