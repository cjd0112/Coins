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
        yield RuntimeCoin(this,numCoins,remainder,totalCoins,Next.GenerateOrderedCombinations(remainder,totalCoins,pruneBranch))
  }

  def printableFunc(max:Int)(numCoins:Int,remainder:Int) : Boolean =
  {
    println(numCoins + " " + remainder)
    numCoins <= max
  }

}
