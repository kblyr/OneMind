using System.Text;
using CodeCompanion.Exceptions;
using CodeCompanion.FluentEnumerable;

namespace OneMind.Exceptions
{
    public class UsernameAlreadyExistsException : CodeCompanionException
    {
        public string Username { get; init; } = "";

        protected override void SetClientMessage(StringBuilder builder) => builder.Append($"Username '{Username}' already exists");

        protected override void SetErrorData(IDictionary<string, object?> errorData) => errorData.FluentAdd(nameof(Username), Username);
    }
}