namespace Translator.Code.Exception
{
    public class DuplicateUserException : System.Exception
    {
        public DuplicateUserException(string username):
            base(System.String.Format("User with name '{0}' is already exists", username))
        {

        }
    }
}