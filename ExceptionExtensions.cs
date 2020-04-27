using System;
using System.Data.SqlClient;
using System.Text;

namespace Extensions
{
    /// <summary>
    /// Extensions for Exception class
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Gets a string with the message of an exception and all of its inner exception, separated by the given delimiter
        /// </summary>
        /// <param name="ex">The top level exception</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <returns>The messages of all levels of exceptions, delimited, starting from the top one and moving to the inner ones</returns>
        public static string GetMessagesDeep(this Exception ex, string delimiter)
        {
            StringBuilder sb = new StringBuilder();
            if (ex != null)
            {
                sb.Append(ex.Message);
                Exception inner = ex;
                while (inner != null)
                {
                    sb.Append((delimiter == null ? "" : delimiter) + inner.Message);
                    inner = inner.InnerException;
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// Gets the message of the most inner exception (if any), or the actual exception
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns>The innermost exception message</returns>
        public static string GetInnermostMessage(this Exception ex)
        {
            string message = null;
            if (ex != null)
            {
                message = ex.Message;
                Exception inner = ex;
                while (inner != null)
                {
                    message = inner.Message;
                    inner = inner.InnerException;
                }
            }
            return message;
        }

        /// <summary>
        /// Gets the SQL exception number.
        /// </summary>
        /// <param name="ex">The exception.</param>
        /// <returns>System.Nullable&lt;System.Int32&gt;.</returns>
        public static int? GetSqlExceptionNumber(this Exception ex)
        {
            int? no = null;

            Exception rootException = null;
            Exception innerException = ex;
            while (innerException != null)
            {
                rootException = innerException;
                innerException = innerException.InnerException;
            }
            if (rootException != null)
            {
                SqlException rootCause = rootException as SqlException;
                if (rootCause != null)
                {
                    no = rootCause.Number;
                }
            }

            return no;
        }

        /// <summary>
        /// Duplicate record sql error investigation.
        /// </summary>
        /// <param name="ex"></param>
        public static void CheckIfDuplicateKeyException(this Exception /*DbUpdateException*/ ex)
        {
            Exception innerEx = null;
            while (ex.InnerException != null) innerEx = ex.InnerException;
            if (innerEx is SqlException && (((SqlException)innerEx).Number == 2601 || ((SqlException)innerEx).Number == 2627))
            {
                throw new Exception("Duplicate key found.", ex);
            }
        }
    }
}
