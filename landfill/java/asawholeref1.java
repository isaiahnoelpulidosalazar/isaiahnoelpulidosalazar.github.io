import java.util.Random;
import java.util.Scanner;

class asawholeref1 {
	public asawholeref1(){
		Random rand = new Random();
		Scanner scan = new Scanner(System.in);
		int n = rand.nextInt(100);
		n += 1;
		while (true){
			System.out.println("Guess the number!");
			String string = scan.nextLine();
			if (!string.equals(int)){
				System.out.println("Please type a 'number'.");
			}
		}
	}
}