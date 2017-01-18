namespace SoftwareKobo.Utils
{
    public interface IVerifiable
    {
        ValidationResultCollection Errors
        {
            get;
        }

        bool IsValid
        {
            get;
        }
    }
}