import javax.swing.*;
import java.awt.*;

class Paint2 extends JFrame{
	/*Font bigFont = new Font("Comic Sans MS", Font.ITALIC, 55);
	String string = "NOBITA";*/
	
	public void paint(Graphics g){
		super.paint(g);
		/*g.setFont(bigFont);
		g.setColor(new Color(102, 51, 0));
		g.drawString(string, 100, 100);
		g.setColor(Color.BLUE);
		g.drawLine(50, 100, 550, 50);
		g.drawRect(20, 50, 180, 80);
		g.fillRect(220, 50, 180, 80);
		g.drawOval(20, 50, 180, 80);
		g.fillOval(220, 50, 180, 80);
		g.drawRoundRect(20, 50, 180, 80, 40, 40);
		g.fillRoundRect(220, 50, 180, 80, 40, 40);
		g.draw3DRect(420, 50, 180, 80, true);
		g.fill3DRect(620, 50, 180, 80, true);
		g.setColor(new Color(72, 210, 176));
		g.fillArc(100, 100, 200, 200, 45, 90);*/
		
		g.setColor(new Color(72, 210, 176));
		int[] xPoints = {42, 50, 72, 58, 68, 42, 14, 24, 12, 34, 42};
		int[] yPoints = {38, 58, 58, 75, 98, 83, 98, 75, 58, 58, 38};
		//g.drawPolygon(xPoints, yPoints, xPoints.length);
		g.fillPolygon(xPoints, yPoints, xPoints.length);
		
		/*int[] xPoints = {50, 100, 25, 50};
		int[] yPoints = {50, 100, 25, 50};
		g.drawPolygon();*/
	}
	
	public static void main(String[] args){
		Paint2 paint2 = new Paint2();
		paint2.getContentPane().setBackground(new Color(155, 89, 182));
		paint2.setSize(500, 500);
		paint2.setVisible(true);
		paint2.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
	}
}