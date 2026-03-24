namespace Academy.Core.DTOs
{
    /// <summary>
    /// A class used to promote defensive coding and to prevent throwing unnecessary exceptions.
    /// Used for the 4th Wall Academy application.
    /// </summary>
    public class AcademyResult
    {
        public bool Ok { get; }
        public string Message { get; private set; }

        public AcademyResult(bool success, string message)
        {
            Ok = success;
            Message = message;
        }
    }

    /// <summary>
    /// Inherits from Result and allows generic collections to be utilized in results.
    /// </summary>
    /// <typeparam name="T">A generic collection of data.</typeparam>
    public class Result<T> : AcademyResult
    {
        public T? Data { get; set; }

        public Result(T data, bool success, string message) : base(success, message)
        {
            Data = data;
        }
    }

    /// <summary>
    /// A factory class that contains behavior members for instantiating new Result objects.
    /// </summary>
    public class ResultFactory
    {
        public static AcademyResult Success()
        {
            return new AcademyResult(true, string.Empty);
        }

        public static AcademyResult Success(string message)
        {
            return new AcademyResult(true, message);
        }

        public static AcademyResult Fail(string message)
        {
            return new AcademyResult(false, message);
        }

        public static Result<T> Success<T>(T data)
        {
            return new Result<T>(data, true, string.Empty);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default, false, message);
        }
    }
}