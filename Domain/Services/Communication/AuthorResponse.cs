using API.Domain.Models;

namespace API.Domain.Services.Communication
{
    public class AuthorResponse : BaseResponse
    {
        public Author Author { get; private set; }

        private AuthorResponse(bool success, string message, Author author) : base(success, message)
        {
            Author = author;
        }

        /// <summary>
        /// Creates a success response.
        /// </summary>
        /// <param name="category">Saved category.</param>
        /// <returns>Response.</returns>
        public AuthorResponse(Author author) : this(true, string.Empty, author)
        { }

        /// <summary>
        /// Creates am error response.
        /// </summary>
        /// <param name="message">Error message.</param>
        /// <returns>Response.</returns>
        public AuthorResponse(string message) : this(false, message, null)
        { }
    }
}