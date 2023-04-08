namespace FamilyTreeLib.Exceptions;

public class MotherNotFoundException : Exception
{
    public MotherNotFoundException() : base("A child must have a mother")
    {
        
    }

    public MotherNotFoundException(string message) : base(message)
    {

    }

    public MotherNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        
    }

}