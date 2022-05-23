public class ExceptionLogger  
{  
   private ILogger _logger;  
   public ExceptionLogger(ILogger aLogger)  
   {  
      this._logger = aLogger;  
   }  
   public void LogException(Exception aException)  
   {  
      string strMessage = GetUserReadableMessage(aException);  
      this._logger.LogMessage(strMessage);  
   }  
   private string GetUserReadableMessage(Exception aException)  
   {  
      string strMessage = string.Empty;  
      //code to convert Exception's stack trace and message to user readable format.  
      //....  
      //....  
      return strMessage;  
   }  
}  
public class DataExporter  
{  
   public void ExportDataFromFile()  
   {  
      ExceptionLogger _exceptionLogger;  
      try {  
         //code to export data from files to database.  
      }  
      catch(IOException ex)  
      {  
         _exceptionLogger = new ExceptionLogger(new DbLogger());  
         _exceptionLogger.LogException(ex);  
      }  
      catch(Exception ex)  
      {  
         _exceptionLogger = new ExceptionLogger(new FileLogger());  
         _exceptionLogger.LogException(ex);  
      }  
   }  
}  