import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

class GuiCalculatorOld extends JFrame implements ActionListener{
	
	Label label;
	Label label2;
	Label label3;
	Label spaceleft;
	Label spaceright;
	
	TextField textField;
	TextField textField1;
	TextField textField2;
	
	Button buttondivide;
	Button buttonmultiply;
	Button buttonminus;
	Button buttonplus;

	public static void main(String[] args){
		GuiCalculatorOld ls = new GuiCalculatorOld();
		ls.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		ls.setResizable(false);
		ls.setSize(300, 300);
	}
	
	public GuiCalculatorOld(){
		super("GuiCalculatorOld");
		Container container = getContentPane();
		container.setLayout(new GridLayout(4, 3));
		
		label = new Label("First Input");
		label2 = new Label("Second Input");
		label3 = new Label("Result");
		spaceleft = new Label();
		spaceright = new Label();
		
		textField = new TextField(8);
		textField1 = new TextField(8);
		textField2 = new TextField(8);
		
		textField2.setEditable(false);
		
		buttonplus = new Button("PLUS");
		buttondivide = new Button("DIVIDE");
		buttonmultiply = new Button("MULTIPLY");
		buttonminus = new Button("MINUS");
		
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