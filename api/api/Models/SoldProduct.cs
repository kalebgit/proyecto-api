using api.ADO.NET;

namespace api.Models
{
    public class SoldProduct : SoldProductHandler
    {
        // delegados
        Verificator rightType = new Verificator(IsInt);

        // instance variables
        private long _id;
        private int _quantity;
        private long _productId;
        private long _saleId;


        // properties
        public long Id
        {
            get
            {
                return _id;
            }
        }
        public int Quantity
        {
            get
            {
                return _quantity;
            }
            set
            {
                _quantity = (int)rightType(value);
            }
        }
        public long ProductId
        {
            get
            {
                return _productId;
            }
            set
            {
                rightType = new Verificator(IsLong);
                _productId = (long)rightType(value);
            }
        }
        public long SaleId
        {
            get
            {
                return _saleId;
            }
            set
            {
                _saleId = (long)rightType(value);
            }
        }


        // constructor
        public SoldProduct(int quantity, long productId, long saleId)
        {
            _quantity = quantity;
            _productId = productId;
            _saleId = saleId;
        }


    }
}
