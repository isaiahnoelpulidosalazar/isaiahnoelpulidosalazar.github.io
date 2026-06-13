import javax.swing.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.*;
public class Main extends JPanel implements ActionListener {
      double firstnum;
        double secondnum;
        double answer;
        String sanswer;
        int operation = 0;
    protected JButton zero, one, two,three,four,five,six,seven,eight,nine,equal,multiply,decmal,minus,plus,divide;
    JTextField screen;
    public Main() {
         screen = new JTextField(20);    
        add(screen);
        setLayout(new GridLayout(4,4));
        seven = new JButton("7");
        seven.setActionCommand("eseven");
        add(seven);
        eight = new JButton("8");
        eight.setActionCommand("eeight");
        add(eight);
        nine = new JButton("9");
        nine.setActionCommand("enine");
        add(nine);
        divide = new JButton("/");
        divide.setActionCommand("edivide");
        add(divide);
        four = new JButton("4");
        four.setActionCommand("efour");
        add(four);
        five = new JButton("5");
        five.setActionCommand("efive");
        add(five);
        six = new JButton("6");
        six.setActionCommand("esix");
        add(six);
        multiply = new JButton("*");
        multiply.setActionCommand("emultiply");
        add(multiply);
        one = new JButton("1");
        one.setActionCommand("eone");
        add(one);
        two = new JButton("2");
        two.setActionCommand("etwo");
        add(two);
        three = new JButton("3");
        three.setActionCommand("ethree");
        add(three);
        minus = new JButton("-");
        minus.setActionCommand("eminus");
        add(minus);
        zero = new JButton("0");
        zero.setActionCommand("ezero");
        add(zero);
        decmal = new JButton(".");
        decmal.setActionCommand("edecmal");
        add(decmal);
        equal = new JButton("=");
        equal.setActionCommand("eequals");
        add(equal);
        plus = new JButton("+");
        plus.setActionCommand("eplus");
        add(plus);


        one.addActionListener(this);
        two.addActionListener(this);
        three.addActionListener(this);
        four.addActionListener(this);
        five.addActionListener(this);
        six.addActionListener(this);
        seven.addActionListener(this);
        eight.addActionListener(this);
        nine.addActionListener(this);
        zero.addActionListener(this);
        plus.addActionListener(this);
        minus.addActionListener(this);
        equal.addActionListener(this);
        divide.addActionListener(this);
        multiply.addActionListener(this);
        decmal.addActionListener(this);
 
    }

    public void actionPerformed(ActionEvent e) {
        if ("eone".equals(e.getActionCommand())) {
            String pnum = screen.getText();
            screen.setText(pnum + '1');
        }
        if ("etwo".equals(e.getActionCommand())) {
                      String pnum = screen.getText();
            screen.setText(pnum + '2');  
        }
        if ("ethree".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '3');
        }
        if ("efour".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '4');
        }
        if ("efive".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '5');
        }
        if ("esix".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '6');
        }
        if ("eseven".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '7');
        }
        if ("eeight".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '8');
        }
        if ("enine".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '9');
        }
        if ("ezero".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '0');
        }
        if ("eplus".equals(e.getActionCommand())) {
            firstnum = (Double.parseDouble(screen.getText()));
            operation = 1;
            screen.setText("");
        }
        if ("eminus".equals(e.getActionCommand())) {
            firstnum = (Double.parseDouble(screen.getText()));
            operation = 2;
            screen.setText("");
        }
        if ("emultiply".equals(e.getActionCommand())) {
                        firstnum = (Double.parseDouble(screen.getText()));
            operation = 3;
            screen.setText("");
        }
        if ("edivide".equals(e.getActionCommand())) {
            firstnum = (Double.parseDouble(screen.getText()));
            operation = 4;
            screen.setText("");
        }
        if ("eequals".equals(e.getActionCommand())) {
            secondnum = (Double.parseDouble(screen.getText()));
            
                if (operation == 1)
                {
                screen.setText("");
                answer = firstnum + secondnum;
                sanswer = Double.toString(answer);
                screen.setText(sanswer);
                }
 if (operation == 2)
                {
                screen.setText("");
                answer = firstnum - secondnum;
                sanswer = Double.toString(answer);
                screen.setText(sanswer);
 }
          if (operation == 3)
                {
                screen.setText("");
                answer = firstnum * secondnum;
                sanswer = Double.toString(answer);
                screen.setText(sanswer);
          }
          if (operation == 4)
                {
                screen.setText("");
                answer = firstnum / secondnum;
                sanswer = Double.toString(answer);
                screen.setText(sanswer);
           
                }
            }
        if ("edecmal".equals(e.getActionCommand())) {
                        String pnum = screen.getText();
            screen.setText(pnum + '.');
        }
    }
    public static void createAndShowGUI() {

        //Create and set up the window.
        JFrame frame = new JFrame("Main");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

        //Create and set up the content pane.
        Main newContentPane = new Main();
        newContentPane.setOpaque(true); //content panes must be opaque
        frame.setContentPane(newContentPane);
        //Display the window.
        frame.pack();
        frame.setVisible(true);
        
    }

    public static void main(String[] args) {
        javax.swing.SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                createAndShowGUI(); 
            }
        });
    }
}