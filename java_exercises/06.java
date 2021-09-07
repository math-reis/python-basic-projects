import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner num = new Scanner (System.in);
    System.out.print ("Enter a number: ");
    int n = num.nextInt ();

    Scanner option = new Scanner (System.in);
    System.out.print ("Choose between the sum or product: [S/P]");
    char op = option.next ().charAt (0);

    if (op == 's' || op == 'S')
      {
	int sum = ((n + 1) * n) / 2;
	String result = String.format ("The sum of the numbers 1 to %d is %d.", n, sum);
	System.out.println (result);
      }
    if (op == 'p' || op == 'P')
      {
	int i, fact = 1;
	for (i = 1; i <= n; i++)
	  {
	    fact = fact * i;
	  }
	String result = String.format ("The product of the numbers 1 to %d is %d.", n, fact);
	System.out.println (result);
      }
    else
      {
	System.out.println ("Insert a valid option.");
      }
  }
}
