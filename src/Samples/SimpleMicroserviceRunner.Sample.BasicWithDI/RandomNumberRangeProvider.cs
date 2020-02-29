using System;

namespace SimpleMicroserviceRunner.Sample.BasicWithDI
{
    public class RandomNumberRangeProvider : IRandomNumberRangeProvider
    {
        public int GetMax()
        {
            Random random = new Random();
            return Convert.ToInt32(random.Next(100, 1000) * Math.PI);
        }

        public int GetMin()
        {
            Random random = new Random();
            return Convert.ToInt32(random.Next(1, 100) * Math.PI);
        }
    }
}
