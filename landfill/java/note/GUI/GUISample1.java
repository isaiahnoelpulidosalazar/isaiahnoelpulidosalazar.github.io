import javax.swing.*;
import java.awt.*;

public class GUISample1 extends JFrame{
	public static void main(String[] args){
		JFrame jf = new JFrame();
		jf.setSize(400, 400);
		
		jf.setDefaultCloseOperation(EXIT_ON_CLOSE);
		jf.setResizable(false);
		
		jf.setTitle("My First GUI Program");
		
		Button b1 = new Button("This is an AWT Button");
		JButton b2 = new JButton("This is a Swing Button");
		
		jf.setLayout(new FlowLayout());
		jf.add(b1);
		jf.add(b2);
		
		Label l1 = new Label("This is an AWT Label");
		JLabel l2 = new JLabel("This is a Swing Label");
		jf.add(l1);
		jf.add(l2);
		
		TextField t1 = new TextField(20);
		JTextField t2 = new JTextField(20);
		jf.add(t1);
		jf.add(t2);
		
		Checkbox c1 = new Checkbox("AWT Checkbox");
		JCheckBox c2 = new JCheckBox("Swing Checkbox");
		jf.add(c1);
		jf.add(c2);
		
		jf.setVisible(true);
	}
}