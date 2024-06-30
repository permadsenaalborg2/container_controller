using System.Diagnostics;
using System.Text.Json;
using System;
public class Podman
{
        public static bool ContainerCMD(string ID, string cmd)
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "podman",
                Arguments = $" {cmd} {ID}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        proc.Start();
        return true;
    }

    public static List<Container> GetContainers()
    {
        var proc = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "podman",
                Arguments = " ps -a --format json",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                CreateNoWindow = true
            }
        };

        proc.Start();
        JsonDocument doc;

        if (proc.StandardOutput.EndOfStream)
        {
            Console.WriteLine("Error");
            return null;
        }
        else
        {
            var output = proc.StandardOutput.ReadToEnd();
            doc = JsonDocument.Parse(output);
        }

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