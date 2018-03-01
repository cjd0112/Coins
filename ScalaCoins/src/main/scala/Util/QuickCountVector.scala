package Util

object QuickCountOrderedVector {



  def microLength(n:IndexedSeq[Int],start:Int): Int =
  {
    val q = n(start)
    var cnt = 0
    while (start + cnt < n.length && n(start+cnt) == q)
      {
        cnt = cnt + 1
      }
    cnt
  }

  def SumSameNums(lhs:IndexedSeq[Int],rhs:IndexedSeq[Int]):Int =
  {
    var sum = 0;
    var i,j:Int = 0
    while (i < lhs.length && j < rhs.length) {
      if (lhs(i) == rhs(j)) {
        var lhs_length = microLength(lhs,i)
        var rhs_length = microLength(rhs,j);
        sum = sum + (lhs_length*rhs_length)
        i = i + lhs_length
        j = j + rhs_length
      }
      else if (lhs(i) < rhs(j))
        {
          i = i + 1

        }
      else if (lhs(i) > rhs(j))
        {
          j = j + 1
        }
    }
    sum
  }


}
