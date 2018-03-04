package Combos

import CoinDefinitions.{CoinBase, RuntimeCoin}
import Util.QuickCountOrderedVector


case class Combination(lhs:Int,rhs:Int)
{
  import CoinDefinitions.CoinTypes._

  def MinMax(b:CoinBase) = {
    (Math.max(b.MinNumCoins(lhs),b.MinNumCoins(rhs)),Math.max(b.MaxNumCoins(lhs),b.MaxNumCoins(rhs)))
  }

  def CalculateMatchingNumberOfCoins(combos:(IndexedSeq[RuntimeCoin],IndexedSeq[RuntimeCoin])) :Int =
  {
    var lhs_coins = combos._1.flatMap(x=>x.GetTotalCoinsForLeafNodes()).filterNot(x=>x==0)
    var rhs_coins = combos._2.flatMap(x=>x.GetTotalCoinsForLeafNodes()).filterNot(x=>x==0)

    println(s"$lhs  " + lhs_coins)
    //println(rhs_coins)
    //println(QuickCountOrderedVector.SumSameNums(lhs_coins,rhs_coins))
    QuickCountOrderedVector.SumSameNums(lhs_coins,rhs_coins)


  }

  def CountNodes(combos:(IndexedSeq[RuntimeCoin],IndexedSeq[RuntimeCoin])) :Int =
  {
    combos._1.map(x=>x.CountNodes()).sum + combos._2.map(x=>x.CountNodes()).sum

  }

  def AssertOrdered(n:Int,i:List[Int]): Unit = i match{
    case Nil => Unit
    case z::rest => {
      assert(z>=n)
      AssertOrdered(z,rest)
    }

  }


  def GenerateRuntimeCoinsFromStream(b:CoinBase): Unit =
  {
    var cnt = 0

    def streamer (num:Int,min:Int,max:Int) : (runtimeCoinEvents) = {
      (c:CoinBase,nc:numCoins,r:remainder,tc:totalCoins)=>{
        cnt = cnt + 1
        //println(s"$nc - $r - $tc")
        if (r == 0)
          {
          }
          tc > max
      }
    }

    val (lhs_min,lhs_max) = (b.MinNumCoins(lhs),b.MaxNumCoins((lhs)))
    val (rhs_min,rhs_max) = (b.MinNumCoins(rhs),b.MaxNumCoins((rhs)))

    (b.BreadthFirstStream(lhs,0,streamer(lhs,rhs_min,rhs_max) ),b.BreadthFirstStream(rhs,0,streamer (rhs,lhs_min,lhs_max)))

    println(cnt)
  }

  def GenerateRuntimeCoins(b:CoinBase,prune:Boolean = true) : (IndexedSeq[RuntimeCoin],IndexedSeq[RuntimeCoin]) = {

    def pruner (min:Int,max:Int) : (runtimeCoinEvents)  = prune match {

      case false => (_, _, _, _) => false
      case true => (x: CoinBase, nc: numCoins, r: remainder, tc: totalCoins) => {
        tc > max

      }
    }
    val (lhs_min,lhs_max) = (b.MinNumCoins(lhs),b.MaxNumCoins((lhs)))
    val (rhs_min,rhs_max) = (b.MinNumCoins(rhs),b.MaxNumCoins((rhs)))

    (b.GenerateOrderedCombinations(lhs,0,pruner (rhs_min,rhs_max) ),b.GenerateOrderedCombinations(rhs,0,pruner (lhs_min,lhs_max)))
  }
}
