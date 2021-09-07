import java.util.Scanner;

class Main
{
  public static void main (String[]args)
  {
    Scanner userName = new Scanner (System.in);
    System.out.print ("Enter your name: ");
    String name = userName.nextLine ();
    if (name.equalsIgnoreCase("Alice") || name.equalsIgnoreCase("Bob")) 
    {
      System.out.println ("Username: " + name);
    }
    else
    {
      System.out.println ("Invalid username!");
    }
  }
}
