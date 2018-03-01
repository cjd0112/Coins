import CoinDefinitions.{BranchCoin, LeafCoin, RuntimeCoin}
import Combos._
import Util.StopWatch

object MagicPurse {

  def Coins()  = BranchCoin("HC",60,BranchCoin("FL",48,BranchCoin("SH",24, BranchCoin("6P",12,BranchCoin("3P",6,BranchCoin("1P",2,LeafCoin("HP")))))))

  def GenerateCombinations(i:Int) :Iterable[Combination] = Combinations(i)

  def Run(i:Int,withPruning:Boolean):Int = {

    def PP(s:(Combination,(Iterable[RuntimeCoin],Iterable[RuntimeCoin]))) ={

      val n = s._1.MinMax(Coins())

      println(s"Combo = ${s._1}, Min-MaxCoins = ($n)")
      println("\tlhs:")
      s._2._1.foreach(x=>x.Print(1))
      println("\trgs:")
      s._2._2.foreach(x=>x.Print(1))
    }

    val t = StopWatch.Time[List[(Combination,(IndexedSeq[RuntimeCoin],IndexedSeq[RuntimeCoin]))]](()=>
      Combinations(i).toList.map(x=>(x,x.GenerateRuntimeCoins(Coins(),withPruning))),"Initial Generation of combinations")
    t.foreach(PP)

    //t.map(x=>x._1.CalculateMatchingNumberOfCoins(x._2)).foreach(println)

    var res = StopWatch.Time[Int](()=>t.map(x=>x._1.CalculateMatchingNumberOfCoins(x._2)).sum,"Calculating matching coins from combinations")

    println("Final sum is: " + res)

    println("Number nodes is " + t.map(x=>x._1.CountNodes(x._2)).sum)


    res


  }
}
