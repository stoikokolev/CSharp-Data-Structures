using System;
using Wintellect.PowerCollections;

namespace _04.CookiesProblem
{
    public class CookiesProblem
    {
        public int Solve(int k, int[] cookies)
        {
            int count = 0;
            var bagOfCookies = new OrderedBag<int>(cookies);
            for (int i = 0; bagOfCookies.Count>1; i++)
            {
                if (bagOfCookies.GetFirst() > k)
                {
                    return count;
                }
                count++;
                var first = bagOfCookies.RemoveFirst();
                var second = bagOfCookies.RemoveFirst();
                bagOfCookies.Add(first+2*second);
                
            }

            if (bagOfCookies.GetFirst() > k)
            {
                return count;
            }
            else
            {
                return -1;
            }
        }
    }
}
