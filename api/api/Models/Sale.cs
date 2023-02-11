using api.ADO.NET;

namespace api.Models
{
    public class Sale : SaleHandler
    {
        // delegados
        Verificator rightType = new Verificator(NonNullable);

        // instance variables
        private long _id;
        private string _comments;
        private long _userId;

        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = (string)rightType(value);
            }
        }
        public long UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                rightType = new Verificator(IsLong);
                _userId = (long)rightType(value);
            }
        }

        // constructor
        public Sale(long id, string comments, long userId)
        {
            _id = id;
            _comments = comments;
            _userId = userId;
        }
        public Sale()
        {

        }
        public override string ToString()
        {
            return String.Format("{0, -10}{1, -50}{2, -10}", _id, _comments, _userId);
        }
    }
}
