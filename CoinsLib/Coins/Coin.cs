using System;
using System.Collections.Generic;
using System.Linq;

namespace CoinsLib.Coins
{
    public class Coin
    {
        public readonly int Units;
        public readonly String Name;
        public readonly Coin Next;
        public Coin(int units,String name,Coin next)
        {
            Units = units;
            Name = name;
            Next = next;
        }

        public bool IsLeaf()
        {
            return Next == null;
        }

        public override bool Equals(object obj)
        {
            return ((Coin) obj).Name == Name;
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

    public class CoinFactory
    {
        public static Coin GenerateCoinStatic()
        {
            return new Coin(60,"HC",new Coin(48,"FL",new Coin(24,"SH",new Coin(12, "6d", new Coin(6, "3d", new Coin(2, "1d", new Coin(1, ".5d", null)))))));
        }
    }
}
