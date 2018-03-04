import scala.collection.mutable._


class TD(N1:Int,N2:Int,N3:Int) {

  var value = 0
  var ticker = 0
  var N3Vector :ListBuffer[(Int,Int,Int)] = ListBuffer[(Int,Int,Int)]()
  var N2Vector : ListBuffer[(Int,Int,Int)]= ListBuffer[(Int,Int,Int)]()
  var N1Vector :ListBuffer[(Int,Int,Int)]= ListBuffer[(Int,Int,Int)]()


  def incrementCount(): Array[Int] = {
    value = value + 1

    if (value == N1 + N2 + N3) {
      N1Vector = N1Vector :+ (1, 1, 1)
      N2Vector = N2Vector :+ (1, 1, 1)
      N3Vector = N3Vector :+ (1, 1, 1)
      Array(3)
    }
    else
      {
        if (value > N1 + N2 + N3)
        {
          ticker = ticker + 1
          if (ticker % N1 == 0)
            N1Vector(0) = N1Vector(0).copy()

        }
        null
      }

  }






}
