import java.io.Console;

public class a {

  public static void main(String[] args) {
    Console console = System.console();

    char passwordArray[] = console.readPassword("Enter your secret password: ");
    console.printf("Password entered was: %s%n", new String(passwordArray));
  }
}