using System;
using System.Runtime.Serialization;

namespace Kapitalist.Services.Prozorro.Exceptions
{
    public class BadRequestException : Exception, ISerializable
    {
    }
}