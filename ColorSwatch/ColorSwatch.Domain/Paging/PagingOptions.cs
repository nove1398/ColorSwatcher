namespace ColorSwatch.Domain.Paging
{
    public class PagingOptions
    {
        private int _pageNumber;
        private int _perPage;

        public string SearchTerm { get; set; }

        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                if (value < 1)
                {
                    _pageNumber = 1;
                }
                else
                {
                    _pageNumber = value;
                }
            }
        }

        public int PerPage
        {
            get
            {
                return _perPage;
            }

            set
            {
                if (value > 50 || value < 1)
                {
                    _perPage = 50;
                }
                else
                {
                    _perPage = value;
                }
            }
        }
    }
}