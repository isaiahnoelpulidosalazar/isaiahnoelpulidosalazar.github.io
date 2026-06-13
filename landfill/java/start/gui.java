package start;

import javax.swing.*;

public class gui {
    public static void createAndShowGUI() {
        JFrame frame = new JFrame("0 & 1");
		frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setSize(500, 500);
		JLabel label = new JLabel("Yooooooooooooooooooo");
		frame.getContentPane().add(label);
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