using System;
using System.IO;

namespace Domain.Common
{
    public static class LoggerImage
    {
        private static string _pathLog = string.Empty;
        private const long MaxLogSize = 15 * 1024 * 1024; // 15 MB en bytes

        public static string PathLog
        {
            get
            {
                return _pathLog;
            }
            set
            {
                //current windows folder
                _pathLog = Environment.CurrentDirectory.ToString();
            }
        }

        // Write log in file and add date and time and type of log
        public static void WriteLog(string message, string type)
        {
            try
            {
                string path = Environment.CurrentDirectory.ToString();
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // Nombre del archivo log basado en la fecha actual (uno por día)
                string fileName = string.Format("{0}\\{1}.txt", path, DateTime.Now.ToString("yyyyMMdd"));

                // Verificar si el archivo de log excede los 15 MB
                if (File.Exists(fileName))
                {
                    FileInfo logFileInfo = new FileInfo(fileName);
                    if (logFileInfo.Length > MaxLogSize)
                    {
                        try
                        {
                            // Si el archivo supera los 15 MB, intentamos eliminarlo
                            File.Delete(fileName);
                        }
                        catch (Exception ex)
                        {
                            // Si no podemos eliminar el archivo, capturamos el error y no escribimos más en el log
                            Console.WriteLine($"Error al eliminar el archivo de log: {ex.Message}");
                            return; // Salimos sin escribir en el log si no se pudo eliminar
                        }
                    }
                }
                else
                {
                    // Si el archivo no existe, lo creamos
                    File.Create(fileName).Close();
                }

                // Escribir el log en el archivo
                using (StreamWriter writer = new StreamWriter(fileName, true))
                {
                    writer.WriteLine(string.Format("{0} {1} {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), type, message));
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones generales al escribir el log
                Console.WriteLine($"Error al escribir el log: {ex.Message}");
            }
        }
    }
}
