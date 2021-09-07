import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner number = new Scanner (System.in);
      System.out.println ("Enter a number: ");
    int n = number.nextInt ();
    int sum = ((n + 1) * n) / 2;
    String result =
      String.format ("The sum of the numbers 1 to %d is: %d", n, sum);
      System.out.println (result);
  }
}
