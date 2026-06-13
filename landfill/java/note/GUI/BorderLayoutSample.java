import javax.swing.*;
import java.awt.*;

class BorderLayoutSample extends JFrame{
	
	JButton b1, b2, b3, b4, b5;
	
	public BorderLayoutSample(){
		super("BORDER LAYOUT");
		Container container = getContentPane();
		container.setLayout(new BorderLayout());
		
		b1 = new JButton("UP");
		b2 = new JButton("LEFT");
		b3 = new JButton("RIGHT");
		b4 = new JButton("DOWN");
		b5 = new JButton("FIRE!");
		
		container.add(b1, BorderLayout.NORTH);
		container.add(b2, BorderLayout.WEST);
		container.add(b3, BorderLayout.EAST);
		container.add(b4, BorderLayout.SOUTH);
		container.add(b5, BorderLayout.CENTER);
		
		container.setSize(150, 150);
		
		pack();
		show();
	}
	
	public static void main(String[] args){
		BorderLayoutSample ls = new BorderLayoutSample();
		ls.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}