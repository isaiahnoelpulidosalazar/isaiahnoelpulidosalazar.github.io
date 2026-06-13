import java.awt.*;
import java.awt.event.*;
import javax.swing.*;

import java.util.Random;

class Snake extends JFrame implements ActionListener{
	
	int x = 5;
	int y = 5;
	int x1 = 5;
	int y1 = 5;
	
	int a;
	int b;
	int c;
	int d;
	
	int a1 = 5;
	int b1 = 5;
	int a2 = 5;
	int b2 = 5;
	
	int counter = 0;
	int score = 0;
	
	boolean UP = false;
	boolean LEFT = false;
	boolean RIGHT = false;
	boolean DOWN = false;
	
	boolean bUp = false;
	boolean bLeft = false;
	boolean bRight = false;
	boolean bDown = false;
	
	boolean sUp = false;
	boolean sLeft = false;
	boolean sRight = false;
	boolean sDown = false;
	
	boolean started = false;
	boolean complete = false;
	
	JButton up = new JButton("UP");
	JButton left = new JButton("LEFT");
	JButton right = new JButton("RIGHT");
	JButton down = new JButton("DOWN");
	
	JLabel t1 = new JLabel();
	JLabel t2 = new JLabel();
	JLabel t3 = new JLabel();
	JLabel t4 = new JLabel();
	JLabel t5 = new JLabel();
	
	JLabel l1 = new JLabel();
	JLabel l2 = new JLabel();
	JLabel l3 = new JLabel();
	JLabel l4 = new JLabel();
	JLabel l5 = new JLabel();
	
	Snakes snakes_main = new Snakes();
	
	public Snake(){
		
		Random random = new Random();
		
		int[] x_choices = {35, 45, 55, 65, 75, 85, 95, 105, 115, 125, 135, 145, 155, 165};
		int[] y_choices = {35, 45, 55, 65, 75, 85, 95, 105, 115, 125, 135, 145, 155, 165};
		
		a = x_choices[random.nextInt(x_choices.length)];
		b = y_choices[random.nextInt(y_choices.length)];
		c = x_choices[random.nextInt(x_choices.length)];
		d = y_choices[random.nextInt(y_choices.length)];
		
		setLayout(new GridLayout(3, 1));
		
		JPanel p1 = new JPanel();
		p1.add(snakes_main);
		p1.setBackground(new Color(178, 186, 187));
		
		JPanel p2 = new JPanel();
		p2.setLayout(new GridLayout(1, 5));
		p2.add(t1);
		p2.add(t2);
		p2.add(t3);
		p2.add(t4);
		p2.add(t5);
		
		JPanel p3 = new JPanel();
		p3.setLayout(new GridLayout(3, 3));
		p3.add(l1);
		p3.add(up);
		p3.add(l2);
		p3.add(left);
		p3.add(l3);
		p3.add(right);
		p3.add(l4);
		p3.add(down);
		p3.add(l5);
		
		ActionListener listener = new AbstractAction(){
			public void actionPerformed(ActionEvent e){
				
				a1 = a;
				b1 = b;
				a2 = c;
				b2 = d;
				
				if(UP == true && y != -5){
					up();
					if(started == true && y == -5){
						JOptionPane.showMessageDialog(null, "Game Over!\nFinal Score: " + score);
						dispose();
						try {
							Thread.sleep(500);
						} catch (Exception a){
							a.printStackTrace();
						}
						new Snake();
					}
				}
				if(LEFT == true && x != -5){
					left();
					if(started == true && x == -5){
						JOptionPane.showMessageDialog(null, "Game Over!\nFinal Score: " + score);
						dispose();
						try {
							Thread.sleep(500);
						} catch (Exception a){
							a.printStackTrace();
						}
						new Snake();
					}
				}
				if(RIGHT == true && x != 365){
					right();
					if(started == true && x == 365){
						JOptionPane.showMessageDialog(null, "Game Over!\nFinal Score: " + score);
						dispose();
						try {
							Thread.sleep(500);
						} catch (Exception a){
							a.printStackTrace();
						}
						new Snake();
					}
				}
				if(DOWN == true && y != 195){
					down();
					if(started == true && y == 195){
						JOptionPane.showMessageDialog(null, "Game Over!\nFinal Score: " + score);
						dispose();
						try {
							Thread.sleep(500);
						} catch (Exception a){
							a.printStackTrace();
						}
						new Snake();
					}
				}
				if(x == a1 && y == b1){
					score++;
					a = x_choices[random.nextInt(x_choices.length)];
					b = y_choices[random.nextInt(y_choices.length)];
					snakes_main.repaint();
				}
				if(x == a2 && y == b2){
					score++;
					c = x_choices[random.nextInt(x_choices.length)];
					d = y_choices[random.nextInt(y_choices.length)];
					snakes_main.repaint();
				}
				t2.setText("Score:");
				t4.setText(String.valueOf(score));
			}
		};
		Timer timer = new Timer(100, listener);
		timer.start();
		
		add(p1);
		add(p2);
		add(p3);
		
		up.addActionListener(this);
		left.addActionListener(this);
		right.addActionListener(this);
		down.addActionListener(this);
		
		pack();
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		setResizable(false);
		setSize(400, 700);
		setVisible(true);
	}
	
	public class Snakes extends JPanel{
		
		public void paint(Graphics g){
			super.paint(g);
			g.fillRect(x, y, 15, 15);
			g.fillRect(x1, y1, 15, 15);
			g.drawRect(a1, b1, 15, 15);
			g.drawRect(a2, b2, 15, 15);
		}
		
		public Dimension getPreferredSize(){
			return new Dimension(375, 210);
		}
	}
	
	public void up(){
		LEFT = false;
		RIGHT = false;
		DOWN = false;
		sUp = true;
		y -= 10;
		y1 -= 10;
		if(counter != 2){
			if(sRight == true){
			counter++;
				x1 += 10;
				y1 += 10;
				complete = true;
			}
			if(sLeft == true){
				counter++;
				x1 -= 10;
				y1 += 10;
				complete = true;
			}
			if(complete && counter == 2){
				sLeft = false;
				sRight = false;
			}
		}
		snakes_main.repaint();
	}
	
	public void left(){
		UP = false;
		RIGHT = false;
		DOWN = false;
		sLeft = true;
		x -= 10;
		x1 -= 10;
		if(counter != 2){
			if(sDown == true){
				counter++;
				x1 += 10;
				y1 += 10;
				complete = true;
			}
			if(sUp == true){
				counter++;
				x1 += 10;
				y1 -= 10;
				complete = true;
			}
			if(complete && counter == 2){
				sUp = false;
				sDown = false;
			}
		}
		snakes_main.repaint();
	}
	
	public void right(){
		UP = false;
		LEFT = false;
		DOWN = false;
		sRight = true;
		x += 10;
		x1 += 10;
		if(counter != 2){
			if(sDown == true){
				counter++;
				x1 -= 10;
				y1 += 10;
				complete = true;
			}
			if(sUp == true){
				counter++;
				x1 -= 10;
				y1 -= 10;
				complete = true;
			}
			if(complete && counter == 2){
				sUp = false;
				sDown = false;
			}
		}
		snakes_main.repaint();
	}
	
	public void down(){
		UP = false;
		LEFT = false;
		RIGHT = false;
		sDown = true;
		y += 10;
		y1 += 10;
		if(counter != 2){
			if(sRight == true){
				counter++;
				x1 += 10;
				y1 -= 10;
				complete = true;
			}
			if(sLeft == true){
				counter++;
				x1 -= 10;
				y1 -= 10;
				complete = true;
			}
			if(complete && counter == 2){
				sLeft = false;
				sRight = false;
			}
		}
		snakes_main.repaint();
	}
	
	@Override
	public void actionPerformed(ActionEvent event){
		if(event.getSource() == up){
			if(DOWN){
				DOWN = true;
				UP = false;
				LEFT = false;
				RIGHT = false;
				counter = 0;
				if(bDown == false){
					bDown = true;
					bRight = true;
					y1 = -15;
				}
				repaint();
			} else {
				if(started){
					UP = true;
					LEFT = false;
					RIGHT = false;
					DOWN = false;
					counter = 0;
					repaint();
				}
			}
		}
		if(event.getSource() == left){
			if(RIGHT){
				RIGHT = true;
				UP = false;
				LEFT = false;
				DOWN = false;
				counter = 0;
				if(bRight == false){
					bRight = true;
					bDown = true;
					x1 = -15;
				}
				repaint();
			} else {
				if(started){
					LEFT = true;
					UP = false;
					RIGHT = false;
					DOWN = false;
					counter = 0;
					repaint();
				}
			}
		}
		if(event.getSource() == right){
			if(LEFT){
				LEFT = true;
				UP = false;
				RIGHT = false;
				DOWN = false;
				counter = 0;
				repaint();
			} else {
				started = true;
				RIGHT = true;
				UP = false;
				LEFT = false;
				DOWN = false;
				counter = 0;
				if(bRight == false){
					bRight = true;
					bDown = true;
					x1 = -15;
				}
				repaint();
			}
		}
		if(event.getSource() == down){
			if(UP){
				UP = true;
				LEFT = false;
				RIGHT = false;
				DOWN = false;
				counter = 0;
				repaint();
			} else {
				started = true;
				DOWN = true;
				UP = false;
				LEFT = false;
				RIGHT = false;
				counter = 0;
				if(bDown == false){
					bDown = true;
					bRight = true;
					y1 = -15;
				}
				repaint();
			}
		}
	}
	public static void main(String[] args){
		new Snake();
	}
}