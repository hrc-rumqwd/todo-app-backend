using System;
using System.Collections.Generic;
using System.Text;

namespace TodoApp.Shared.Commons
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public IEnumerable<string> Errors { get; set; }

        public static Result Success()
        {
            return new Result
            {
                IsSuccess = true
            };
        }

        public static Result Failure(string message, IEnumerable<string> errors = null)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }

    public class Result<T> : Result
    {
        public T Data { get; set; }

        public static Result<T> Success(T data)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static Result<T> Failure(string message, IEnumerable<string> errors = null)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }
    }
}
