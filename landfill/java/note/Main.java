import java.util.*;

class Main{
    public static void main (String[] args) {
        Scanner input = new Scanner(System.in);
        int num1, num2, quo;
        
        try {
        System.out.print("Please enter first number: ");
        num1 = input.nextInt();
        System.out.print("Please enter second number: ");
        num2 = input.nextInt();
        quo = num1 / num2;
        System.out.println("The quotient is: " + quo);
        } catch(ArithmeticException a) {
            System.out.println("DIVISION BY ZERO IS NOT ALLOWED!");
        } catch(InputMismatchException b) {
            System.out.println("ONLY INTEGERS ARE ALLOWED!");
        } catch(Exception c) {
            System.out.println("AN ERROR HAS OCCURED...");
        } finally {
            System.out.println("The program will now terminate.");
        }
    }
}