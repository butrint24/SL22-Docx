public class Manager: ILead  
{  
   public void AssignTask()  
   {  
      //Code to assign a task.  
   }  
   public void CreateSubTask()  
   {  
      //Code to create a sub task.  
   }  
   public void WorkOnTask()  
   {  
      throw new Exception("Manager can't work on Task");  
   }  
}