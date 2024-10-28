using System.Diagnostics;

namespace Delivery.Services
{
    public sealed class RunProcessService
    {
        public void StartProcess(string fileName, string args = null!)
        {
            try
            {
                using var _ = Process.Start(new ProcessStartInfo(fileName, args)
                {
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw;
            }
            finally { }
        }
    }
}
