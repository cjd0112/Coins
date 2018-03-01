import Util.{QuickCountOrderedVector, StopWatch}

class CoinsMain {

}

object CoinsMain {
  def main(a: Array[String]): Unit = {

    var z = Vector(1,2,2,2,3,4,4,5,9,12)
    var z2 = Vector(5,5,9,10)

    println(QuickCountOrderedVector.SumSameNums(z,z2))



    System.currentTimeMillis()
    StopWatch.Time(() => {

//      println("S1 is " + MagicPurse.Run(98, false))
//      println("S2 is " + MagicPurse.Run(98, true))

    //  println("S1 is " + MagicPurse.Run(20, false))
      println("S2 is " + MagicPurse.Run(50, true))


    },"Whole Operation")

  }
}