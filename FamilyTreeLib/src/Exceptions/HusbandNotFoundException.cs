namespace FamilyTreeLib.Exceptions;

public class HusbandNotFoundException : Exception
{
    public HusbandNotFoundException() : base("Wife must have a husband")
    {

    }

    public HusbandNotFoundException(string message) : base(message)
    {
        
    }

    public HusbandNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        
    }

}