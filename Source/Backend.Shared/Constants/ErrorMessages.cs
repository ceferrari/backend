namespace Backend.Shared.Constants
{
    public static class ErrorMessages
    {
        public const string UnexpectedError                     = "oops! unexpected error";
        public const string ValidationError                     = "oops! validation error";
        public const string ExecutionError                      = "oops! execution error";

        public static class Arguments
        {
            public const string HandlerNotRegisteredForType     = "handler not registered for type";
            public const string MethodNotRegisteredForHandler   = "method not registered for handler";
        }

        public static class Validations
        {
            public const string InvalidId                       = "'id' is not valid";
        }

        public static class Cart
        {
            public const string AlreadyCanceled                 = "cart is already canceled";
            public const string AlreadyDone                     = "cart is already done";
            public const string AlreadyExistsById               = "cart with specified id already exists";
            public const string AlreadyExistsByCustomerId       = "cart with specified customer id already exists";
            public const string DoesNotExistsById               = "cart with specified id does not exists";
            public const string DoesNotExistsByCustomerId       = "cart with specified customer id does not exists";
            public const string DoesNotContainsItemById         = "cart does not contain item with specified id";
        }

        public static class Product
        {
            public const string AlreadyExistsById               = "product with specified id already exists";
            public const string AlreadyExistsBySku              = "product with specified sku already exists";
            public const string DoesNoExistsById                = "product with specified id does not exists";
            public const string DoesNoExistsBySku               = "product with specified sku does not exists";
        }
    }
}
