import java.util.Scanner;

public class gaaf{
	public static void main(String[]args){
		int attempts = 0;
		
		boolean iterated = false;
		String answer = "";
		String hold = "";
		
		Scanner scanner = new Scanner(System.in);
		String words = ("lollipop");
		
		do{
			System.out.println("Enter your letter guess!");
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
			attempts++;
			iterated = true;
			System.out.println(answer);
			if(answer.equals(words)){
				System.out.println("You guessed it right in " + attempts + " attempts out of " + words.length + 2 + "!");
				break;
			}
		} while (attempts < words.length() + 2);
	}
}