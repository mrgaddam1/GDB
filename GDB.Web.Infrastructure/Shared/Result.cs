
namespace GDB.Web.Infrastructure.Shared
{
    public class Result
    {
        public bool IsSuccess => Errors == null || Errors.Length == 0;
        public string[] Errors { get; set; }
        
        protected Result() { }

        protected static Result Success()
        {
            return new Result();
        }
        protected static Result Failure(params string[] errors)
        {
            return new Result { Errors = errors };
        }   
    }


    public class Result<T> : Result
    {
        public T? Value { get; init; } = default;
        protected Result() { }
        public static Result<T> Success(T value)
        {
            return new Result<T> { Value = value };
        }
        public new static Result<T> Failure(T value, params string[] errors)
        {
            return new Result<T> { Value = value, Errors = errors };
        }

        public static implicit operator Result<T>(T value)
        {
            return Success(value);
        }
    }
}
