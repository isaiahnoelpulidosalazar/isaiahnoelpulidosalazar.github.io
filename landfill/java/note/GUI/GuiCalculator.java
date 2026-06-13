import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

class GuiCalculator extends JFrame implements ActionListener{
	
	JLabel label;
	JLabel label2;
	JLabel label3;
	JLabel spaceleft;
	JLabel spaceright;
	
	JTextField textField;
	JTextField textField1;
	JTextField textField2;
	
	JButton buttondivide;
	JButton buttonmultiply;
	JButton buttonminus;
	JButton buttonplus;
	
	public static void main(String[] args){
		GuiCalculator ls = new GuiCalculator();
		ls.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		ls.setResizable(false);
		ls.setSize(300, 300);
	}
	
	public GuiCalculator(){
		super("GuiCalculator");
		Container container = getContentPane();
		container.setLayout(new GridLayout(4, 3));
		
		label = new JLabel("First Input");
		label2 = new JLabel("Second Input");
		label3 = new JLabel("Result");
		spaceleft = new JLabel();
		spaceright = new JLabel();
		
		textField = new JTextField(8);
		textField1 = new JTextField(8);
		textField2 = new JTextField(8);
		
		textField2.setEditable(false);
		
		buttonplus = new JButton("PLUS");
		buttondivide = new JButton("DIVIDE");
		buttonmultiply = new JButton("MULTIPLY");
		buttonminus = new JButton("MINUS");
		
		container.add(label);
		container.add(label2);
		container.add(label3);
		
		container.add(textField);
		container.add(textField1);
		container.add(textField2);
		
		container.add(buttonplus);
		container.add(buttonminus);
		container.add(buttonmultiply);
		
		container.add(spaceleft);
		container.add(buttondivide);
		container.add(spaceright);
		
		buttonplus.addActionListener(this);
		buttonminus.addActionListener(this);
		buttonmultiply.addActionListener(this);
		buttondivide.addActionListener(this);
		
		pack();
		show();
	}
	
	public void actionPerformed(ActionEvent event){
		if(event.getSource() == buttonplus){
			try{
				int num = Integer.parseInt(textField.getText());
				int num1 = Integer.parseInt(textField1.getText());
				int sum = num + num1;
				textField2.setText(String.valueOf(sum));
			} catch (Exception a){
				JOptionPane.showMessageDialog(null, "Please type a number!");
			}
		}
		if(event.getSource() == buttonminus){
			try{
				int num = Integer.parseInt(textField.getText());
				int num1 = Integer.parseInt(textField1.getText());
				int dif = num - num1;
				textField2.setText(String.valueOf(dif));
			} catch (Exception a){
				JOptionPane.showMessageDialog(null, "Please type a number!");
			}
		}
		if(event.getSource() == buttonmultiply){
			try{
				int num = Integer.parseInt(textField.getText());
				int num1 = Integer.parseInt(textField1.getText());
				int pro = num * num1;
				textField2.setText(String.valueOf(pro));
			} catch (Exception a){
				JOptionPane.showMessageDialog(null, "Please type a number!");
			}
		}
		if(event.getSource() == buttondivide){
			try{
				int num = Integer.parseInt(textField.getText());
				int num1 = Integer.parseInt(textField1.getText());
				int div = num / num1;
				textField2.setText(String.valueOf(div));
			} catch (Exception a){
				JOptionPane.showMessageDialog(null, "Please type a number!");
			}
		}
	}
}