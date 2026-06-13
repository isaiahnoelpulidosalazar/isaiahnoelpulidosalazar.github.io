package start;

import java.util.Random;

public class number{
    public static void main (String[] args) {
		while(true){
			Random num = new Random();
			int numnum = num.nextInt(2147483647);
			System.out.print(numnum);
		}
	}
}