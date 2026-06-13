public class SWorks extends Bike{
    public void run(){
        System.out.println("Running safely at 60 kmph");
    }
    public static void main (String[] args) {
        Bike b = new SWorks();
        b.run();
    }
}