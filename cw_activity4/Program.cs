using System;

namespace MyConsoleApplication
{
    public delegate void IncrementEventHandler(object source, EventArgs args);

    interface TestInterface
    {
        string TestString { get; set; }
        void PrintTest();
        event IncrementEventHandler Incremented;
        int this[int i] { get; set; }
        TestInterface CreateTest();
    }

    class TestClass: TestInterface
    {
        public string TestString { get => TestString; set => TestString = value; }
        public void PrintTest(){ Console.WriteLine(message); }

        public event IncrementEventHandler Incremented;

        private int[] arr = new int[10];
        public int this[int i] { get => arr[i]; set => arr[i] = value; }

        public TestInterface CreateTest()
        {
            var new_test = new TestClass();
            return new_test;
        }

        private int counter = 1;
        private string message = "This is a test";


        public void setMessage(string newMessage)
        {
            message = newMessage;
        }
        public string getMessage()
        {
            return message;
        }
        public int Counter
        {
            get { return counter; }
            set { counter = value; }
        }
        public void Increment()
        {
            counter++;

            OnIncremented();
        }

        protected virtual void OnIncremented()
        {
            if (Incremented != null)
                Incremented(this, EventArgs.Empty);
        }
    }

    public class PrintService
    {
        public void OnIncremented(object source, EventArgs e)
        {
            Console.WriteLine("Counter was incremented");
        }
    }


    class Program
    {
        static void Main()
        {
            //Creates an instance of TestClass
            var test_inst = new TestClass();

            //Grabs and outputs default parameters in TestClass
            var msg = test_inst.getMessage();
            var count = test_inst.Counter;
            Console.WriteLine(msg + " " + count.ToString());

            //Tests the indexer
            for (int i = 0; i < 10; i++) { test_inst[i] = i; }
            for (int i = 0; i < 10; i++) { Console.Write(test_inst[i].ToString() + " "); }
            Console.Write("\n");

            //Changes and outputs new values of the parameters
            test_inst.setMessage("new test");
            msg = test_inst.getMessage();
            test_inst.Counter = 2;
            count = test_inst.Counter;
            Console.WriteLine(msg + " " + count.ToString());

            //Tests the event
            var printservice = new PrintService();
            test_inst.Incremented += printservice.OnIncremented;
            test_inst.Increment();
        }
    }
}