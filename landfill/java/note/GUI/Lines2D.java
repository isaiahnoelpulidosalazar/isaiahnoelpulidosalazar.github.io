import java.awt.*;
import javax.swing.*;
import java.awt.Graphics2D.*;
import java.awt.geom.*;

class Lines2D extends JFrame{
	
	public Lines2D(){
		super("2D Line Examples");
		setSize(480, 200);
		setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		show();
	}
	
	void drawLines(Graphics g){
		Graphics2D g2d = (Graphics2D) g;
		Stroke stroke = new BasicStroke(30f);
		g2d.setStroke(stroke);
		g2d.setColor(new Color(72, 210, 176));
		g2d.drawLine(120, 50, 360, 50);
		
		g2d.setStroke(new BasicStroke(4f));
		g2d.setColor(Color.GREEN);
		g2d.draw(new Line2D.Double(59.2d, 99.8d, 419.1d, 99.8d));
		
		g2d.setStroke(new BasicStroke(9f));
		g2d.setColor(new Color(155, 89, 182));
		g2d.draw(new Line2D.Float(21.50f, 132.50f, 459.50f, 132.50f));
	}
	
	public void paint(Graphics g){
		super.paint(g);
		drawLines(g);
	}
	
	public static void main(String[] args){
		new Lines2D();
	}
}