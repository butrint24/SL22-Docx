public class DbLogger: ILogger  
{  
   public void LogMessage(string aMessage)  
   {  
      //Code to write message in database.  
   }  
}  
public class FileLogger: ILogger  
{  
   public void LogMessage(string aStackTrace)  
   {  
      //code to log stack trace into a file.  
   }  
}