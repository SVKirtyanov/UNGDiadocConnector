using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNGDiadocConnector 
{
    internal class TxtFileLoger : ILoger
    {
     
        private   string DocEntityId { get; }
        private string LogFilePath { get; }
        public TxtFileLoger(string docEntityId)
        {
            this.DocEntityId = docEntityId;
            this.LogFilePath = $"{System.IO.Path.GetTempPath()}{DocEntityId}.log" ;
        }

       public async void WriteToLog(string msg)
       {
         Console.WriteLine($" Документ с EntityId = {DocEntityId} | {msg};"); 
         await File.WriteAllTextAsync(LogFilePath, msg);
       }
    }
}
