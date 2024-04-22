namespace SkeletonApi.IotHub.DTOs
{
    public class TaskDto
    {
        public string Zona { get; set; }
        public string Type { get; set; }
        public string TaskDuration { get; set; }
        public List<Operators> Operator { get; set; }
    }

    public class Operators
    {
        public string OperatorNumber { get; set; }
        public string TaskNumber { get; set; }
        public string TaskName { get; set; }
    }
}