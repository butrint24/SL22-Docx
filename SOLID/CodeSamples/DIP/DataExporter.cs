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
      catch(SqlException ex)  
      {  
         _exceptionLogger = new ExceptionLogger(new EventLogger());  
         _exceptionLogger.LogException(ex);  
      }  
      catch(Exception ex)  
      {  
         _exceptionLogger = new ExceptionLogger(new FileLogger());  
         _exceptionLogger.LogException(ex);  
      }  
   }  
} 