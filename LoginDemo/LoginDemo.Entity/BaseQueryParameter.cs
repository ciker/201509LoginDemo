using LoginDemo.Commom;
namespace LoginDemo.Entity
{
    public class BaseQueryParameter
    {
        private int _pageindex = 1;
        private int _pagesize = 10;
        private int _take = 10;
        [IgnoreField]
        public int PageIndex
        {
            get
            {
                return _pageindex;
            }
            set
            {
                _pageindex = value;
            }
        }

        [IgnoreField]
        public int PageSize
        {
            get
            {
                return _pagesize;
            }
            set
            {
                _pagesize = value;
            }
        }

        [IgnoreField]
        public int Skip { get { return PageIndex - 1; } }

        [IgnoreField]
        public int Take
        {
            get
            {
                return _take;
            }
            set
            {
                _take = value;
            }
        }
        public string SearchKeyWord { get; set; }
    }
}
