public class testt {

  public static void main(String... args) {
    int n = Integer.parseInt(args[0]);
    int e = Integer.parseInt(args[1]);
    for (int a = 0; a < n; a++) {
      System.out.print("Hello World");
      for (int b = 0; b < e; b++) {
        System.out.print("!");
      }
      System.out.println();
    }
  }
}