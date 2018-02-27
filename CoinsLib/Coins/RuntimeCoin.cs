using System.Collections.Generic;

namespace CoinsLib.Coins
{
    public class RuntimeCoin
    {
        public readonly Coin Coin;
        public readonly IEnumerable<RuntimeCoin> Next;
        public readonly int NumCoins;

        public RuntimeCoin(Coin c,int numCoins,IEnumerable<RuntimeCoin> next)
        {
            Coin = c;
            this.Next = next;
            this.NumCoins = numCoins;
        }

    }
}
