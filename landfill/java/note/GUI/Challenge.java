import java.awt.*;
import javax.swing.*;

class Challenge extends JFrame{
	
	JButton b1;
	JButton b2;
	JButton b3;
	JButton b4;
	JButton b5;
	JButton b6;
	
	JPanel p1;
	
	public Challenge(){
		super("Challenge");
		Container c = getContentPane();
		c.setLayout(new GridLayout(2, 2));
		
		b1 = new JButton("1");
		b2 = new JButton("2");
		b3 = new JButton("3");
		b4 = new JButton("4");
		b5 = new JButton("5");
		b6 = new JButton("6");
		
		p1 = new JPanel();
		p1.setLayout(new GridLayout(1, 3));
		p1.add(b3);
		p1.add(b4);
		p1.add(b5);
		
		c.add(b1);
		c.add(b2);
		c.add(p1);
		c.add(b6);
		
		pack();
		show();
	}
	
	public static void main(String[] args){
		Challenge challenge = new Challenge();
		challenge.setSize(300, 300);
		challenge.setResizable(false);
		challenge.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}