using System.Diagnostics;
using System.Text.Json;
using System;

public class Podman
{

    public class PodmanResult
    {
        public string StdOut { get; set; } = String.Empty;
        public string StdErr { get; set; } = String.Empty;
        public int ExitCode { get; set; }
    }

    public static PodmanResult RunPodmanCmd(string cmd, string args)
    {

        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "podman",
                Arguments = $" {cmd} {args}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            }
        };
        
        proc.Start();
        var output = proc.StandardOutput.ReadToEnd();
        var error = proc.StandardError.ReadToEnd();
        return new PodmanResult { StdErr = error, StdOut = output, ExitCode = 0 };
    }

    public static async Task<PodmanResult> RunPodmanCmdAsync(string cmd, string args)
    {
        PodmanResult res = new();

        await Task.Run(() =>
        {
            res = RunPodmanCmd(cmd, args);
        });

        // Thread.Sleep(5000);

        return res;
    }

    public async static Task<List<Container>> GetContainers()
    {
        var output = await RunPodmanCmdAsync("ps", "-a --format json");

        JsonDocument doc = JsonDocument.Parse(output.StdOut);

        List<Container> containers = [];

        foreach (JsonElement j in doc.RootElement.EnumerateArray())
        {
            containers.Add(new Container()
            {
                ID = j.GetProperty("Id").ToString(),
                Name = j.GetProperty("Names")[0].ToString(),
                State = j.GetProperty("State").ToString(),
                CreatedAt = j.GetProperty("CreatedAt").ToString(),
                Image = j.GetProperty("Image").ToString(),
            });
        }
        return containers;
    }
}