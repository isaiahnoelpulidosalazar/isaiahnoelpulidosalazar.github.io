import javax.swing.*;
import java.awt.*;

class LayoutSample extends JFrame{
	
	JButton b1, b2, b3, b4;
	
	public static void main(String[] args){
		LayoutSample ls = new LayoutSample();
		ls.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
	
	public LayoutSample(){
		super("FLOW LAYOUT");
		Container container = getContentPane();
		container.setLayout(new GridLayout(2, 2));
		
		b1 = new JButton("1");
		b2 = new JButton("2");
		b3 = new JButton("3");
		b4 = new JButton("4");
		
		container.add(b1);
		container.add(b2);
		container.add(b3);
		container.add(b4);
		
		container.setSize(150, 150);
		
		pack();
		show();
	}
}