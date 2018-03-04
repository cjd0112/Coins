package CoinDefinitions


case class BranchCoin(val Name:String,Units:Int,Next:CoinBase) extends CoinBase(Units,Next)
{
  import CoinDefinitions.CoinTypes._
  def GenerateOrderedCombinations(value:Int,totalCoins:Int,pruneBranch:runtimeCoinEvents) : IndexedSeq[RuntimeCoin] =
  {
    if (value == 0)
      Array[RuntimeCoin]()
    else
      for {
        (numCoins,remainder,totalCoins) <- (value to 0 by -Units).map(n=>(n/Units,value-(n/Units*Units),totalCoins+n/Units))
        if !pruneBranch(this,numCoins,remainder,totalCoins)
      }
        yield RuntimeCoin(this,value,numCoins,remainder,totalCoins,Next.GenerateOrderedCombinations(remainder,totalCoins,pruneBranch))
  }

  def BreadthFirstStream(value:Int,totalCoins:Int,events:runtimeCoinEvents): Unit = {
    if (value > 0) {
      val z = for {
        (numCoins, remainder, totalCoins) <- (0 to value by Units).map(n => (n / Units, value - (n / Units * Units), totalCoins + n / Units))
        if !events(this, numCoins, remainder, totalCoins)
      }
        yield (numCoins, remainder, totalCoins)

      z.foreach(x => Next.BreadthFirstStream(x._2, x._3, events))
    }
  }


  def GetNumberCombinations(value:Int): Int =
  {
    var s = 0

    for(i <- 0 to value by Units) {
   //   println(i)
      s = s + Next.GetNumberCombinations(value - (i/Units * Units))
    }

    s
  }






  def printableFunc(max:Int)(numCoins:Int,remainder:Int) : Boolean =
  {
    println(numCoins + " " + remainder)
    numCoins <= max
  }

}
