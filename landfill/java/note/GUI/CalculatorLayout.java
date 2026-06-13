import java.awt.*;
import javax.swing.*;

class CalculatorLayout extends JFrame{
	
	JLabel l1;
	JLabel l2;
	
	JTextField t1;
	
	JButton b1;
	JButton b2;
	JButton b3;
	JButton b4;
	JButton b5;
	JButton b6;
	JButton b7;
	JButton b8;
	JButton b9;
	JButton b0;
	
	JPanel p1;
	JPanel p2;
	
	public CalculatorLayout(){
		super("CalculatorLayout");
		Container c = getContentPane();
		c.setLayout(new GridLayout(2, 1));
		
		t1 = new JTextField();
		
		l1 = new JLabel();
		l2 = new JLabel();
		
		b1 = new JButton("1");
		b2 = new JButton("2");
		b3 = new JButton("3");
		b4 = new JButton("4");
		b5 = new JButton("5");
		b6 = new JButton("6");
		b7 = new JButton("7");
		b8 = new JButton("8");
		b9 = new JButton("9");
		b0 = new JButton("0");
		
		p1 = new JPanel();
		p1.setLayout(new GridLayout(1, 1));
		p1.add(t1);
		
		p2 = new JPanel();
		p2.setLayout(new GridLayout(4, 3));
		p2.add(b7);
		p2.add(b8);
		p2.add(b9);
		p2.add(b4);
		p2.add(b5);
		p2.add(b6);
		p2.add(b1);
		p2.add(b2);
		p2.add(b3);
		p2.add(l1);
		p2.add(b0);
		p2.add(l2);
		
		c.add(p1);
		c.add(p2);
		
		pack();
		show();
	}
	
	public static void main(String[] args){
		CalculatorLayout calculatorLayout = new CalculatorLayout();
		calculatorLayout.setSize(300, 300);
		calculatorLayout.setResizable(false);
		calculatorLayout.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}