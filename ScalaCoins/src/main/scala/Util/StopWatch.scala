package Util

object StopWatch {

  def Start(): ()=>Long =
  {
    val i = System.currentTimeMillis()
    return ()=>(System.currentTimeMillis()-i)/1000
  }

  def Time[T](a:()=>T,s:String):T = {
    val i = System.currentTimeMillis()
    val q = a()

    val g = System.currentTimeMillis()-i
    println(s"$s - $g milliseconds")
    q
  }

}
