namespace Team2Application.Controllers
{
    public class UdemyCoursesRecord
    {
        public UdemyCoursesRecord(string title, string url, string description)
        {
            Title = title;
            Url = url;
            Description = description;
        }

        public string Title { get; set; }

        public string Url { get; set; }

        public string Description { get; set; }
    }
}