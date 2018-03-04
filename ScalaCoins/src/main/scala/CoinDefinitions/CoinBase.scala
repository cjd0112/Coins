package CoinDefinitions

import scala.collection.mutable.Map

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

  def cache:Map[Int,Int] = Map[Int,Int]()

  def GetFromCache(i:Int,g:(Int)=>Int): Int =
  {
    g(i)
      //cache.getOrElseUpdate(i,g(i))
  }

  def MinNumCoins(value:Int) :Int = this match {
    case _:LeafCoin => value
    case _:BranchCoin => value/Units + Next.MinNumCoins(value - (value/Units*Units))
  }

  def GetNumberCombinations(value:Int): Int


  def MaxNumCoins(value:Int) :Int = value

  def GenerateOrderedCombinations(value:Int,totalCoins:Int,pruneBranch:runtimeCoinEvents) : IndexedSeq[RuntimeCoin]

  def BreadthFirstStream(value:Int,totalCoins:Int,events:runtimeCoinEvents): Unit

}

