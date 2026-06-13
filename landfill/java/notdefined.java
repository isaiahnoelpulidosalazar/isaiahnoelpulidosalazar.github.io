import java.util.Scanner;

public class notdefined {
	public notdefined() {
		Scanner scan1 = new Scanner(System.in);
		String day = "Good day.";
		System.out.println("Hi.");
		
		String string = scan1.nextLine();
		if(string.equals("Hello")) {
			System.out.println(day);
		} else {
			if(string.equals("hello")) {
				System.out.println(day);
			} else {
				if(string.equals("Hello.")) {
					System.out.println(day);
				} else {
					if(string.equals("hello.")) {
						System.out.println(day);
					} else {
						System.out.println("Have a nice day.");
					}
				}
			}
		}
	}
}