import java.util.Scanner;

class jaja {
	public static void main(String[]args){
		Scanner myObj = new Scanner(System.in);
		System.out.println("Who dare interrupt my slumber?\n");
		
		String code = myObj.nextLine();
		if(code.equals("IT IS I, ZAKUSA!")){
		    System.out.println("\n\nOh, welcome Zakusa.");
		} else {
		    System.out.println("\n\nYOU BASTARD, WHO ARE YOU?!");
		}
	}
}