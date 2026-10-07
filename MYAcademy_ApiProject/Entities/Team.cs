namespace MYAcademy_ApiProject.Entities
{
    public class Team
    {
        public int TeamId { get; set; }

        public string Name { get; set; }
        public string ShortName { get; set; }

        public string City { get; set; }

        public string Stadium { get; set; }

        public int Capacity { get; set; }

        public int FoundationYear { get; set; }

        public string LogoUrl { get; set; }
        public bool Status { get; set; }


    }
}
