namespace BTEJA_SemWork.ParserClasses.Context
{
    public class MyExecutionContext
    {
        public ProgramContext ProgramContext { get; set; }
        public Variables Variables { get; set; }
        //public MyExecutionContext GlobalExecutionContext { get; set; }

        public MyExecutionContext()
        {
            ProgramContext = new ProgramContext();
            Variables = new Variables();
            //GlobalExecutionContext = new MyExecutionContext();
        }
    }
}