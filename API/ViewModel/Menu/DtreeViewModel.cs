namespace API.ViewModel.Menu
{
    public class DtreeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CheckArr { get; set; }

        public int ParentId { get; set; }

        public bool Last { get; set; } = false;

        public bool Spread { get; set; } = false;
    }
}