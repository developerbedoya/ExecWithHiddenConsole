using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ExecWithHiddenConsole;

class Program
{
    static void Main(string[] args) => VerifyArgs(args, () => ExecWithHiddenConsole(args[0], args.Skip(1).ToArray()));

    [DllImport("User32.dll", CharSet = CharSet.Unicode)]
    public static extern int MessageBox(IntPtr h, string m, string c, int type);

    private static void VerifyArgs(string[] args, Action action)
    {
        if (args.Length < 1)
        {
            MessageBox((IntPtr)0,"Usage: ExecWithHiddenConsole <program> <args>", "ExecWithHiddenConsole", 0);
            return;
        }

        action?.Invoke();
    }

    static void ExecWithHiddenConsole(string program, string[] args)
    {
        var process = new Process();
        process.StartInfo.FileName = program;
        process.StartInfo.Arguments = string.Join(" ", args);
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
        process.Start();
    }
}