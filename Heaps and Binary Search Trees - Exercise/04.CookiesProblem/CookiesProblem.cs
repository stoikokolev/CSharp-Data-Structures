using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            var count = 0;
            var bagOfCookies = new OrderedBag<int>(cookies);
            while (bagOfCookies.Count > 1)
            {
                if (bagOfCookies.GetFirst() > k)
                {
                    return count;
                }
                count++;
                var first = bagOfCookies.RemoveFirst();
                var second = bagOfCookies.RemoveFirst();
                bagOfCookies.Add(first + 2 * second);

            }

            if (bagOfCookies.GetFirst() > k)
            {
                return count;
            }

            return -1;
        }
    }
}
