import Util.{QuickCountOrderedVector, StopWatch}

class CoinsMain {

}

object CoinsMain {
  def main(a: Array[String]): Unit = {

    System.currentTimeMillis()
    StopWatch.Time(() => {

//      println("S1 is " + MagicPurse.Run(98, false))
//      println("S2 is " + MagicPurse.Run(98, true))

    //  println("S1 is " + MagicPurse.Run(20, false))
   //   println("S2 is " + MagicPurse.Run2(6,false))
      println("S2 is " + MagicPurse.Run2(20,false))


    },"Whole Operation")

  }
}