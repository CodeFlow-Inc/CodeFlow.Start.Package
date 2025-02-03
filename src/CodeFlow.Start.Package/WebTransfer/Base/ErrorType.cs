using System.ComponentModel;

namespace CodeFlow.Start.Package.WebTransfer.Base;

/// <summary>
/// Enum to categorize error types.
/// </summary>
public enum ErrorType
{
    /// <summary>
    /// Represents an internal system error.
    /// </summary>
    [Description("Internal error")]
    InternalError = 500,

    /// <summary>
    /// Represents a business rule error.
    /// </summary>
    [Description("Business rule error")]
    BusinessRuleError = 400,

    /// <summary>
    /// Represents an error when the user is not authenticated.
    /// </summary>
    [Description("Invalid credentials")]
    InvalidCredentials = 401
}
