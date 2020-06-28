namespace LetsCodeApp.Models.BaseModels
{
    public class Group : DbEntityBase
    {
        public long TelegramId { get; set; }

        public string Title { get; set; }

        public int OwnerId { get; set; }

        public string About { get; set; }

        public string WelcomeMessage { get; set; }
    }
}
