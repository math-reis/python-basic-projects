import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner num = new Scanner (System.in);
    System.out.print ("Enter a number: ");
    int n = num.nextInt ();
    int x = 0;

    while (x <= 12){
        int mult = n * x;
        String result = String.format ("%d x %d = %d", n, x, mult);
	    System.out.println (result);
        x = x + 1;
    }  
  }
}
