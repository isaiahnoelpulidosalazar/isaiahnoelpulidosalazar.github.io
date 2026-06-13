public class Poly{
    static int compSum(int x, int y){
        return x + y;
    }
    static int compSum(int x, int y, int z){
        return x + y + z;
    }
    static double compSum(double x, double y){
        return x + y;
    }
    public static void main (String[] args) {
        System.out.println("The sum is: " + compSum(9.5, 2.4));
    }
}