import java.util.*;

class adventure{
	public static void main(String[]args){
		System.out.println("Please input your name.");
		Scanner name = new Scanner(System.in);
		String name1 = name.nextLine();
		System.out.println("________________________________");
		System.out.println("");
		System.out.println("           Adventure            ");
		System.out.println("");
		System.out.println("________________________________");
		System.out.println("\nWelcome " + name1 + "! Today, we shall embark on a brand new journey!");
		System.out.print("Shall we proceed? Y/N");
		while(true){
		    Scanner confirm = new Scanner(System.in);
		    while(true){
		        String confirm1 = confirm.nextLine();
		        if(confirm1.equals("N")||confirm1.equals("n")){
			        System.out.println("Alrighty then. Have a nice day!");
			        System.exit(1);
		        }else{
		            if(confirm1.equals("Y")||confirm1.equals("y")){
		                System.out.println(("Alright then! Would you like to view the tutorial? Y/N"));
		                break;
		            }else{
		                if(!confirm1.equals("N")||!confirm1.equals("n")||!confirm1.equals("Y")||!confirm1.equals("y")){
		                    System.out.println("Please type Y/N.");
		                }
		            }
		        }
		    }
		    while(true){
		        String confirm2 = confirm.nextLine();
		        if(confirm2.equals("Y")||confirm2.equals("y")){
			        System.out.println("Okay then! Remember, the choices are always labeled by number! Choice 1 is '1', and so on. Let's start!\n");
			        System.out.println("________________________________\n");
			        break;
		        }else{
		            if(confirm2.equals("N")||confirm2.equals("n")){
		                System.out.println("Okay let's start!\n");
		                System.out.println("________________________________\n");
		                break;
		            }else{
		                if(!confirm2.equals("N")||!confirm2.equals("n")||!confirm2.equals("Y")||!confirm2.equals("y")){
		                    System.out.println("Please type Y/N.");
		                }
		            }
		        }
		    }
		    System.out.println("You wake up and see yourself in an unknown area filled with grass.\n");
		    System.out.println("> Look at the ground.");
		    System.out.println("> Look at the sky.\n");
		    Scanner choice = new Scanner(System.in);
		    while(true){
		        try{
		            int choice1 = choice.nextInt();
		            if(choice1 == 1){
		                System.out.println("\nYou looked at the ground and confirmed that you are stepping on grass.\n");
		                break;
		            }else{
		                if(choice1 == 2){
		                    System.out.println("\nYou looked at the sky and you see the clouds freely passing by.\n");
		                    break;
		                }else{
		                    if(choice1 != 1||choice1 != 2){
		                        System.out.println("There are only 2 choices.");
		                    }
		                }
		            }
		        }catch(InputMismatchException num){
		            System.out.println("Please input a number.");
		            choice.next();
		            continue;
		        }
		    }
		    System.out.println("You then looked in front of you and see a dirt road going to the left and to the right.\n");
		    System.out.println("> Go to the left path.");
		    System.out.println("> Go to the right path.");
		    System.out.println("> Go straight.\n");
		    String a1 = "> Turn around.";
		    break;
		}
	}
}