namespace AgileStudioServer.Dto
{
    public class ProjectPostDto
    {
        public string Title { get; set; }

        public string? Description { get; set; }

        public ProjectPostDto(string title)
        {
            Title = title;
        }
    }
}
