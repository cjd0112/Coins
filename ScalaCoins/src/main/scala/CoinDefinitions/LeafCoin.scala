package CoinDefinitions

case class LeafCoin(Name:String) extends CoinBase(1,null)
{
  import CoinDefinitions.CoinTypes.runtimeCoinEvents

  def GenerateOrderedCombinations(value:Int,totalCoins:Int,pruneBranch:runtimeCoinEvents) :IndexedSeq[RuntimeCoin] = {
    if (value == 0 || pruneBranch(this,value,0,totalCoins+value))
      Array[RuntimeCoin]()
    else
      Array(RuntimeCoin(this,value,0,totalCoins+value,Array[RuntimeCoin]()))
  }
}