import java.util.Random;

class Guess extends JFrame{
	
	JLabel l1 = new JLabel();
	JTextField t1 = new JTextField(8);
	
	int total_attempts = 0;
	
	String[] words = {"CUPCAKE", "DONUT", "ECLAIR", "FROYO", "GINGERBREAD"};
	
	public Guess(){
		Container c = getContentPane();
		c.setLayout(new GridLayout(2, 1));
		
		c.add(l1);
		c.add(t1);
		
		pack();
		show();
	}
	
	
}