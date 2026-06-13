import java.util.Scanner;

public class game{
	public static void main(String[]args){
		int attempts = 0;
		int attempt = 10;
		boolean iterated = false;
		String answer = "";
		String hold = "";
		Scanner scanner = new Scanner(System.in);
		String words = ("lollipop");
		int att = 10;
		
		do{
			System.out.println("Guess the word! Enter a letter! " + att + " attempts left.\n");
			String guess = scanner.nextLine();
			
			for(int i = 0; i < words.length(); i++){
				if(guess.equals(Character.toString(words.charAt(i)))){
					if(!iterated)
					answer += Character.toString(words.charAt(i));
					 else {
					hold = Character.toString(answer.charAt(i)).replace("-", guess);
					answer = answer.substring(0, i) + hold + answer.substring(i + 1, answer.length());
					}
				} else {
					if(!iterated){
						answer += "-";
					}
				}
			}
			attempts += 1;
			att -= 1;
			iterated = true;
			System.out.println(answer);
			if(answer.equals(words)){
				System.out.println("You guessed it right in " + attempts + " attempts out of " + attempt + "!\n");
				break;
			}
		} while (attempts < attempt);
		if (attempts == 10){
		    System.out.println("You ran out of attempts.\n");
		}
	}
}