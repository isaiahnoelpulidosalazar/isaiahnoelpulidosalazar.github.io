import javax.swing.*;
import java.awt.*;
import java.awt.event.*;

class Paint1 extends JFrame implements ActionListener{
	
	JButton b1 = new JButton("PRESS");
	
	public Paint1(){
		setTitle("Paint Demo");
		setLayout(new FlowLayout());
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		add(b1);
		b1.addActionListener(this);
		setSize(300, 300);
		pack();
		show();
	}
	
	public void actionPerformed(ActionEvent e){
		System.out.println("Button was pressed.");
		repaint();
	}
	
	public void paint(Graphics g){
		super.paint(g);
		System.out.println("In paint method.");
	}
	
	public static void main(String[] args){
		new Paint1();
	}
}