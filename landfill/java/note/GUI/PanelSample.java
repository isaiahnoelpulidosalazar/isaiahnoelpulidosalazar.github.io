import java.awt.*;
import javax.swing.*;

class PanelSample extends JFrame{
	
	JButton b1;
	JButton b2;
	JButton b3;
	JButton b4;
	JButton b5;
	JButton b6;
	JButton b7;
	JButton b8;
	
	JPanel p1;
	
	public PanelSample(){
		super("Panel Sample 1");
		Container c = getContentPane();
		c.setLayout(new BorderLayout());
		
		b1 = new JButton("1");
		b2 = new JButton("2");
		b3 = new JButton("3");
		b4 = new JButton("4");
		b5 = new JButton("5");
		b6 = new JButton("6");
		b7 = new JButton("7");
		b8 = new JButton("8");
		
		p1 = new JPanel();
		p1.setLayout(new GridLayout(2, 2));
		p1.add(b3);
		p1.add(b4);
		p1.add(b5);
		p1.add(b6);
		
		c.add(b1, BorderLayout.NORTH);
		c.add(b2, BorderLayout.WEST);
		c.add(b7, BorderLayout.EAST);
		c.add(b8, BorderLayout.SOUTH);
		c.add(p1, BorderLayout.CENTER);
		
		pack();
		show();
	}
	
	public static void main(String[] args){
		PanelSample panelSample = new PanelSample();
		panelSample.setSize(300, 300);
		panelSample.setResizable(false);
		panelSample.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}