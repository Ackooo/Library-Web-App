namespace biblioteka_api.DTOs
{
    public class PaginationDTO
    {
        public int Page { get; set; }

        private int unitsPerPage;
        private readonly int maxUnitsPerPage = 50;

        public int UnitsPerPage
        {
            get
            {
                return unitsPerPage;
            }
            set
            {
                unitsPerPage = (value>maxUnitsPerPage)? maxUnitsPerPage: value;
            }

        }

    }
}
