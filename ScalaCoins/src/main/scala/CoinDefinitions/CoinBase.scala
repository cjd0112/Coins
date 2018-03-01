package CoinDefinitions


object CoinTypes
{
  type numCoins = Int
  type totalCoins = Int
  type remainder = Int
  type runtimeCoinEvents = (CoinBase,numCoins,remainder,totalCoins)=>Boolean

}

abstract class CoinBase(Units:Int,Next:CoinBase)
{
  import CoinDefinitions.CoinTypes._

  val Name:String

  def MinNumCoins(value:Int) :Int = this match {
    case _:LeafCoin => value
    case _:BranchCoin => value/Units + Next.MinNumCoins(value - (value/Units*Units))
  }

  def MaxNumCoins(value:Int) :Int = value

  def GenerateOrderedCombinations(value:Int,totalCoins:Int,pruneBranch:runtimeCoinEvents) : IndexedSeq[RuntimeCoin]
}

