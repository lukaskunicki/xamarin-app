namespace TodoList.Services.Business
{
    public class Report
    {
        public string Name { get; set; }
        public string Action { get; set; }

        public Report(string name, string action)
        {
            Name = name;
            Action = action;
        }

    }
}
