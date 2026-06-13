import java.io.*;

public class TextCreation{
	public static void main(String[] args) throws IOException{
		try {
			File file = new File("note.txt");
		} catch (Exception a){
			a.printStackTrace();
		}
	}
}