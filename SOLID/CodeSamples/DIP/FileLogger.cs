public class FileLogger  
{  
   public void LogMessage(string aStackTrace)  
   {  
      //code to log stack trace into a file.  
   }  
}  
public class ExceptionLogger  
{  
   public void LogIntoFile(Exception aException)  
   {  
      FileLogger objFileLogger = new FileLogger();  
      objFileLogger.LogMessage(GetUserReadableMessage(aException));  
   }  
   private GetUserReadableMessage(Exception ex)  
   {  
      string strMessage = string. Empty;  
      //code to convert Exception's stack trace and message to user readable format.  
      //....  
      //....  
      return strMessage;  
   }  
} 