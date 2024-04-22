namespace SkeletonApi.Application.DTOs.TaskRealtime
{
    public class TaskRealtimeDto
    {
        public string Zona { get; set; }
        public string Type { get; set; }
        public int TaskDuration { get; set; }
        public List<SettingOperator> Operator { get; set; }
        public string Ugjg { get; set; }
    }

    public class SettingOperator
    {
        public string OperatorNumber { get; set; }
        public string TaskNumber { get; set; }
        public string TaskName { get; set; }
    }
}