import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

class ActionSample extends JFrame implements ActionListener{
	
	JButton button;
	JTextField textField;
	JTextField textField1;
	
	public ActionSample(){
		super("ActionListener Example");
		Container container = getContentPane();
		container.setLayout(new FlowLayout());
		
		button = new JButton("ADD");
		textField = new JTextField(5);
		textField1 = new JTextField(5);
		
		button.addActionListener(this);
		
		container.add(button);
		container.add(textField);
		container.add(textField1);
		container.setSize(300, 200);
		pack();
		show();
	}
	
	public void actionPerformed(ActionEvent event){
		if (event.getSource() == button){
			//String text1 = textField.getText();
			//JOptionPane.showMessageDialog(null, "Hello, " + text1 + "!");
			int num = Integer.parseInt(textField.getText());
			int num1 = Integer.parseInt(textField1.getText());
			int sum = num + num1;
			JOptionPane.showMessageDialog(null, "Sum is: " + sum);
		}
	}
	
	public static void main(String[] args){
		ActionSample actionSample = new ActionSample();
		actionSample.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}