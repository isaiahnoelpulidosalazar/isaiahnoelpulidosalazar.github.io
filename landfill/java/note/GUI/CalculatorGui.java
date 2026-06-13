import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

class CalculatorGui extends JFrame implements ActionListener{
	
	JLabel label;
	JLabel label2;
	JLabel label3;
	JLabel label4;
	
	JTextField textField;
	JTextField textField1;
	JTextField textField2;
	JTextField textField3;
	
	JButton button1;
	JButton button2;
	JButton button3;
	JButton buttondivide;
	JButton button4;
	JButton button5;
	JButton button6;
	JButton buttonmultiply;
	JButton button7;
	JButton button8;
	JButton button9;
	JButton buttonminus;
	JButton buttondot;
	JButton button0;
	JButton buttonplus;
	JButton buttonequals;
	
	public static void main(String[] args){
		CalculatorGui ls = new CalculatorGui();
		ls.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
	
	public CalculatorGui(){
		super("CalculatorGui");
		Container container = getContentPane();
		container.setLayout(new GridLayout(6, 4));
		
		label = new JLabel("First Input");
		label2 = new JLabel("Operator");
		label3 = new JLabel("Second Input");
		label4 = new JLabel("Result");
		
		textField = new JTextField(5);
		textField1 = new JTextField(1);
		textField2 = new JTextField(5);
		textField3 = new JTextField(5);
		
		button1 = new JButton("1");
		button2 = new JButton("2");
		button3 = new JButton("3");
		buttondivide = new JButton("/");
		button4 = new JButton("4");
		button5 = new JButton("5");
		button6 = new JButton("6");
		buttonmultiply = new JButton("*");
		button7 = new JButton("7");
		button8 = new JButton("8");
		button9 = new JButton("9");
		buttonminus = new JButton("-");
		buttondot = new JButton(".");
		button0 = new JButton("0");
		buttonplus = new JButton("+");
		buttonequals = new JButton("=");
		
		container.add(label);
		container.add(label2);
		container.add(label3);
		container.add(label4);
		
		container.add(textField);
		container.add(textField1);
		container.add(textField2);
		container.add(textField3);
		
		container.add(button1);
		container.add(button2);
		container.add(button3);
		container.add(buttondivide);
		container.add(button4);
		container.add(button5);
		container.add(button6);
		container.add(buttonmultiply);
		container.add(button7);
		container.add(button8);
		container.add(button9);
		container.add(buttonminus);
		container.add(buttondot);
		container.add(button0);
		container.add(buttonplus);
		container.add(buttonequals);
		
		button1.addActionListener(this);
		button2.addActionListener(this);
		button3.addActionListener(this);
		buttondivide.addActionListener(this);
		button4.addActionListener(this);
		button5.addActionListener(this);
		button6.addActionListener(this);
		buttonmultiply.addActionListener(this);
		button7.addActionListener(this);
		button8.addActionListener(this);
		button9.addActionListener(this);
		buttonminus.addActionListener(this);
		buttondot.addActionListener(this);
		button0.addActionListener(this);
		buttonplus.addActionListener(this);
		buttonequals.addActionListener(this);

		container.setSize(400, 400);
		
		pack();
		show();
	}
	
	public void actionPerformed(ActionEvent event){
		textField.addFocusListener(new FocusListener(){
			
			@Override
			public void focusGained(FocusEvent e){
				if(event.getSource() == button1){
					textField.setCaretPosition(textField.getDocument().getLength());
					textField.replaceSelection("1");
				}
			}
			
			@Override
			public void focusLost(FocusEvent e){
				
			}
		});
	}
}