class Pain{
    public static void main (String[] args) {
        try {
            throw new MyException("Sample");
        } catch(MyException e) {
            System.out.println("Caught!");
            System.out.println("Error Message: " + e.getMessage());
        }
    }
}