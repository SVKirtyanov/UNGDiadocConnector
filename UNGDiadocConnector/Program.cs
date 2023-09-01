namespace UNGDiadocConnector
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string docEntityId = args[0];
            string signPath = args[1];
            try
            {
                // have to authentificate first
                string token = Authenticate.GetToken();
                ILoger loger = new TxtFileLoger(docEntityId);
                PatchDocument.Run(token, docEntityId, signPath, loger);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occured while running: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
            Console.WriteLine("Patching completed");
            Console.ReadKey();
        }
    }
}