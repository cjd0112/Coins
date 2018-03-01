package CoinDefinitions


case class RuntimeCoin(Coin:CoinBase,numCoinsAtThisLevel:Int,remainder:Int,totalCoins:Int,Next:IndexedSeq[RuntimeCoin])
{
  def GetTotalCoinsForLeafNodes(): IndexedSeq[Int] =
  {
    if (Next.isEmpty)
      {
        if (remainder == 0)
          return Array(totalCoins)
        else
          return Array(0)
      }
    return Next.flatMap(x=>x.GetTotalCoinsForLeafNodes())
  }

  def CountNodes(): Int =
  {
    if (Next.isEmpty)
      return 1;
    return Next.map(x=>x.CountNodes()+1).sum

  }

  def Print(depth:Int) :Unit  = {
    for (i <- 0 to depth)
      print("\t")
    println(s"${Coin.Name} - thisLevel=$numCoinsAtThisLevel rem=$remainder total = $totalCoins")
    Next.foreach(x=>x.Print(depth+1))
  }
}