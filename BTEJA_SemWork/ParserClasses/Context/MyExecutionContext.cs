namespace BTEJA_SemWork.ParserClasses.Context
{
    public class MyExecutionContext
    {
        public ProgramContext ProgramContext { get; set; }
        public Variables Variables { get; set; }
        public MyExecutionContext ExecutionContext { get; set; }
    }
}