using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewsAPI.Models
{
    public enum ErrorCodes
    {
        ApiKeyExhausted,
        ApiKeyMissing,
        ApiKeyInvalid,
        ApiKeyDisabled,
        RateLimited,
        ParametersMissing,
        SourcesTooMany,
        SourceDoesNotExist,
        SourceUnavailableSortedBy,
        SourceTemporarilyUnavailable,
        UnexpectedError,
        ParameterInvalid,
        RequestTimeout,
        UnknownError
    }
}
