﻿namespace InTouchApi.Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string Message) : base(Message) { }
    }
}
