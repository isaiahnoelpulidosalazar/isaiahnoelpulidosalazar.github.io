import java.util.Random;
import java.util.Scanner;
import java.util.InputMismatchException;

class guessthenumber1 {
	public guessthenumber1(){
		Random rand = new Random();
		int n = rand.nextInt(100);
		n += 1;
		int attempt = 0;
		while (true){
			Scanner scan = new Scanner(System.in);
			boolean isNumeric = false;
			while (!isNumeric){
				try {
					System.out.println("Guess the number between 1 and 100!");
					int input = scan.nextInt();
					scan.nextLine();
					isNumeric = true;
					attempt++;
					if (input == n){
						System.out.println("You guessed it right after " + attempt + " attempts!\n");
						System.exit(1);
					} else {
						if (input > 100 || input < 1){
						    attempt--;
							System.out.println("Please enter a number between '1 and 100'.\n");
						} else {
							if (input < n){
								System.out.println("Try a higher number!\n");
							} else {
								if (input > n){
									System.out.println("Try a lower number!\n");
								}
							}
						}
					}
				} catch (InputMismatchException ime) {
				    attempt--;
					System.out.println("Please enter a 'number'.\n");
					scan.nextLine();
				}
			}
		}
	}
}