package Combos

object Combinations
{
  def apply(Value:Int):Iterable[Combination] =
  {
    for (i <- 1 to Value-1)
      yield new Combination(i,Value-i)
  }

}
