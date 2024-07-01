using System.Diagnostics;
using System.Text.Json;
using System;
public class Podman
{
    public static string ContainerCMD(string cmd, string arg)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "podman",
                Arguments = $" {cmd} {arg}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                // RedirectStandardError = true,
                CreateNoWindow = true
            }
        };

        proc.Start();
        var output = proc.StandardOutput.ReadToEnd();
        //var error = proc.StandardError.ReadToEnd();
        return output;
    }

    public static List<Container> GetContainers()
    {
        var output = ContainerCMD("ps", "-a --format json");

        JsonDocument doc = JsonDocument.Parse(output);

        List<Container> containers = [];

        foreach (JsonElement j in doc.RootElement.EnumerateArray())
        {
            containers.Add(new Container()
            {
                ID = j.GetProperty("Id").ToString(),
                Name = j.GetProperty("Names")[0].ToString(),
                State = j.GetProperty("State").ToString(),
            });
        }
        return containers;
    }
}