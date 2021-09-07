import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner userName = new Scanner (System.in);
    System.out.println ("Enter your name: ");

    String name = userName.nextLine ();
    System.out.println ("Username is: " + name);
  }
}

