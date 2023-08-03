using System.Reflection;

namespace DirectPrintFromWebUsingDesktopCli
{
    public static class AssemblyDirectory
    {
        private static string GetExecutableFolderPath()
        {
            // Get the folder path where the executable is located.
            try
            {
                return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            }
            catch (Exception e)
            {
                Console.WriteLine("Directory Error. Message: " + e.Message);
            }

            return "";
        }

        public static string GetFilePath()
        {
            try
            {
                string filePath = Path.Combine(GetExecutableFolderPath(), "Pdf");

                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                    Console.WriteLine("Directory Created.");
                }

                return filePath + "\\invoice.pdf";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Directory Error. Message: " + ex.Message);
            }

            return "";
        }
    }
}