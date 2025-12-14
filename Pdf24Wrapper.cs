using System.Diagnostics;

public class ProgramPdf24Wrapper
{
    private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);



    public static async Task<int> RunProcessAsync(string programPath, string arguments)
    {
        semaphore.Wait();
        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = programPath,
                Arguments = arguments,
                RedirectStandardOutput = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                CreateNoWindow = true,
                LoadUserProfile = true, // important for windows service
            };

            using (Process process = Process.Start(startInfo))
            {
                bool exited = await Task.Run(() => process.WaitForExit(60000));

                if (!exited)
                {
                    process.Kill(true);
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