namespace FamilyTreeLib.Exceptions;

public class FatherDoesNotExistException : Exception {
  
  public FatherDoesNotExistException() : base("A person must have father")
  {
      
  }

  public FatherDoesNotExistException(string message) : base(message)
  {
      
  }

  public FatherDoesNotExistException(string message, Exception innerException) : base(message, innerException)
  {
      
  }

}
