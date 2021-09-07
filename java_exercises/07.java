import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner number = new Scanner (System.in);
    System.out.print ("Enter a number: ");
    int n = number.nextInt ();
    int prime = 0;
    while (prime <= n)
      {
	    if (prime % 2 != 0)
	    {
	    System.out.println (prime);
	    }
	    ++prime;
      }
  }
}
