case class RuntimeCoin(Coin:CoinBase,TotalCoins:Int,Next:Iterable[RuntimeCoin])
{
    def GenerateCoinsPerCombination() : Iterable[Int] = Coin match
    {
        case _:LeafCoin => Array(TotalCoins)
        case _:BranchCoin => Next.flatMap(x=>x.GenerateCoinsPerCombination())
    }
    
    def Print(depth:Int) :Unit  = {
        for (i <- 0 to depth)
            print("\t")
        println(s"${Coin.Name} - $TotalCoins") 
        Next.foreach(x=>x.Print(depth+1))
    }
}
 
abstract class CoinBase(Units:Int,Next:CoinBase)
{
    val Name:String
    
    def MinNumCoins(value:Int) :Int = this match {
        case _:LeafCoin => value
        case _:BranchCoin => (value/Units + Next.MinNumCoins(value - (value/Units*Units)))
    }
   
    def MaxNumCoins(value:Int) :Int = value
   
    def GenerateOrderedCombinations(value:Int,totalCoins:Int,continue:(Int,Int)=>Boolean) : Iterable[RuntimeCoin]
   
    
}
 
case class LeafCoin(Name:String) extends CoinBase(1,null)
{
    def GenerateOrderedCombinations(value:Int,totalCoins:Int,continue:(Int,Int)=>Boolean) :Iterable[RuntimeCoin] = Array(RuntimeCoin(this,value+totalCoins,Array[RuntimeCoin]()))
}
 
case class BranchCoin(val Name:String,Units:Int,Next:CoinBase) extends CoinBase(Units,Next)
{
    def GenerateOrderedCombinations(value:Int,totalCoins:Int,continue:(Int,Int)=>Boolean) : Iterable[RuntimeCoin] =
    {
        for {
            (numCoins,remainder) <- (value to 0 by -Units).map(n=>(n/Units +totalCoins,value-(n/Units*Units))) 
            if continue(numCoins,remainder)
        }
            yield RuntimeCoin(this,numCoins,Next.GenerateOrderedCombinations(remainder,numCoins,continue))
    }
    
    def printableFunc(max:Int)(numCoins:Int,remainder:Int) : Boolean = 
    {
        println(numCoins + " " + remainder)
        numCoins <= max
    }
   
    def EqualNumCoins(combo:Combination) : (Iterable[RuntimeCoin],Iterable[RuntimeCoin]) = {
        val (min,max) = combo.IntersectRange()
        val pf = printableFunc(max) _
        val lhs = GenerateOrderedCombinations(combo.lhs.Value,0,pf)
        val rhs = GenerateOrderedCombinations(combo.rhs.Value,0,pf)
        (lhs,rhs)
    }
}
 
case class CombinationValue(Value:Int,Max:Int,Min:Int)
 
case class Combination(lhs:CombinationValue,rhs:CombinationValue)
{
    def IntersectRange():(Int,Int) = {
        (Math.max(lhs.Min,rhs.Min),Math.min(lhs.Max,rhs.Max))
    }
}
 
object Combinations
{
    def apply(Max:(Int)=>Int,Min:(Int)=>Int)(Value:Int):Iterable[Combination] =
        for (i <- 1 to Value-1)
            yield new Combination(CombinationValue(i,Max(i),Min(i)),CombinationValue(Value-i,Max(Value-i),Min(Value-i)))
          
}
 
object MagicPurse {

    def Coins()  = BranchCoin("3P",6,BranchCoin("1P",2,LeafCoin("HP")));
    
    def GenerateCombinations(i:Int) :Iterable[Combination] = Combinations(Coins().MinNumCoins,Coins().MaxNumCoins)(i) 
   
   def Run(i:Int) = {
   
    var coins = MagicPurse.Coins()
    
    def PP(s:(Iterable[RuntimeCoin],Iterable[RuntimeCoin])) ={
        println("lhs:")
        s._1.foreach(x=>x.Print(0))
        println("rgs:")
        s._2.foreach(x=>x.Print(0))
    }
    
    Combinations(coins.MinNumCoins,coins.MaxNumCoins)(i).toList.map(coins.EqualNumCoins).foreach(PP)
       
   }
   
   def RunCombinationTests()
   {
        var m = MagicPurse.GenerateCombinations(1).toSeq
        assert(m.length == 0,s" length is ${m.length}")
        m = MagicPurse.GenerateCombinations(2).toSeq
        assert(m.length == 1,s" length is ${m.length}")
        assert(m.head.lhs.Value == 1,s"value is ${m.head.lhs.Value}")
        assert(m.head.rhs.Value == 1,s"value is ${m.head.rhs.Value}")
        m = MagicPurse.GenerateCombinations(3).toSeq
        assert(m.length == 2,s" length is ${m.length}")
        assert(m.find(x=>x.lhs.Value == 1 && x.rhs.Value == 2  && x.lhs.Min == 1 && x.rhs.Min == 2) != None)
        assert(m.find(x=>x.lhs.Value == 2 && x.rhs.Value == 1 && x.lhs.Min == 2 && x.rhs.Min == 1) != None)
        assert(m.find(x=>x.lhs.Value == 1 && x.rhs.Value == 1) == None)
      
        m = MagicPurse.GenerateCombinations(4).toSeq
        assert(m.length == 3)

        m = MagicPurse.GenerateCombinations(5).toSeq
        assert(m.length == 4)
        assert(m.find(x=>x.lhs.Value == 2 && x.rhs.Value == 3 && x.lhs.Min == 2 && x.rhs.Min == 3) != None)
        
        println(m)     
   }
   
}