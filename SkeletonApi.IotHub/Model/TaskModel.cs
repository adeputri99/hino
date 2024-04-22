namespace SkeletonApi.IotHub.Model
{
    public class TaskModel
    {
        public string Zona { get; set; }
        public string Type { get; set; }
        public string TaskDuration { get; set; }
        public List<Operator> Operator { get; set; }
    }

    public class Operator
    {
        public string OperatorNumber { get; set; }
        public string TaskNumber { get; set; }
        public string TaskName { get; set; }
    }
}