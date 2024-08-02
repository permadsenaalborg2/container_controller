using TECHCOOL.UI;

public class ContainerListScreen : Screen
{
    private ListPage<Container> listPage;

    public ContainerListScreen()
    {
        listPage = new();
        Refresh();
    }

    private void ProgressBar(Task ProgressTask)
    {
        int x, y;

        (x, y) = Console.GetCursorPosition();

        string[] progress = ["|", "\\", "-", "/"];

        int counter = 0;
        while (!ProgressTask.IsCompleted)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(progress[counter++ % 4]);
            Thread.Sleep(300);
        }
        Console.SetCursorPosition(x, y);
    }
    private void Refresh()
    {
        Console.Clear();  // Clear();
        Console.WriteLine("  getting podman data ...");
        var PodmanTask = Podman.GetContainers();
        ProgressBar(PodmanTask);

        // to do re-enable
        //listPage.Clear();
        foreach (var container in PodmanTask.Result)
        {
            listPage.Add(container);
        }
        Clear();

        Pre_Draw();
    }

    public override string Title { get; set; } = "List of containers";

    public void Pre_Draw()
    {
        Console.WriteLine();
        Console.WriteLine("Press F1 to stop container");
        Console.WriteLine("Press F2 to start container");
        Console.WriteLine("Press F5 to refresh");
        Console.WriteLine();
    }
    protected override void Draw()
    {
        Clear();
        Pre_Draw();

        listPage.AddKey(ConsoleKey.F1, Stop);
        listPage.AddKey(ConsoleKey.F2, Start);
        listPage.AddKey(ConsoleKey.F5, Refresh);
        listPage.AddColumn("Name", "Name", 20);
        listPage.AddColumn("State", "State");
        listPage.AddColumn("ID", "PrettyID", 8);
        listPage.AddColumn("Image", "Image", 50);
        listPage.AddColumn("CreatedAt", "CreatedAt", 30);

        Container selected = listPage.Select();
        if (selected != null)
        {
            Clear();

            Console.WriteLine(selected);
            Console.ReadLine();

        }
        else
        {
            Quit();
            return;
        }
    }

    public void Stop(Container c)
    {
        Console.Clear();
        Console.WriteLine($"  Stopping {c.ID}");

        var StopTask = Podman.RunPodmanCmdAsync("stop", c.ID);
        ProgressBar(StopTask);
        Console.WriteLine(StopTask.Result.StdOut);
        Console.WriteLine(StopTask.Result.StdErr);
 
        Refresh();
    }
    public void Start(Container c)
    {
        Console.Clear();
        Console.WriteLine($"  Starting  {c.ID}");

        var StartTask = Podman.RunPodmanCmdAsync("start", c.ID);
        ProgressBar(StartTask);
        Console.WriteLine(StartTask.Result.StdOut);
        Console.WriteLine(StartTask.Result.StdErr);

        Refresh();
    }
    public void Refresh(Container c)
    {
        Refresh();
    }
}