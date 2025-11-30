using System.Diagnostics;

public class ProgramPdf24Wrapper
{
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(2, 4);



    public static async Task<int> RunProcessAsync(string programPath, string arguments)
    {
        await semaphore.WaitAsync();
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                bool exited = await Task.Run(() => process.WaitForExit(60000));

                if (!exited)
                {
                    process.Kill();
                    Console.WriteLine($"Процесс {programPath} был уничтожен из-за тайм-аута.");
                    return -1;
                }
                else
                {
                    string output = await process.StandardOutput.ReadToEndAsync();
                    Console.WriteLine($"Вывод из {programPath}:");
                    Console.WriteLine(output);
                    return process.ExitCode;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при запуске процесса: {ex.Message}");
            return -1;
        }
        finally
        {
            semaphore.Release();
        }
    }
}