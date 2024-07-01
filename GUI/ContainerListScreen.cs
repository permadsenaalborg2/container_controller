using TECHCOOL.UI;

public class ContainerListScreen : Screen
{
    private ListPage<Container> listPage;

    public ContainerListScreen()
    {
        listPage = new();
        Refresh();
    }

    private void Refresh()
    {
        var containers = Podman.GetContainers();
        
        listPage = new();
        foreach (Container c in containers)
        {
            listPage.Add(c);
        }
    }


    public override string Title { get; set; } = "List of containers";

    public void Pre_Draw()
    {
        Console.WriteLine("Press F1 to stop container");
        Console.WriteLine("Press F2 to start container");
    }
    protected override void Draw()
    {
        Clear(this);
        Pre_Draw();

        listPage.AddKey(ConsoleKey.F1, Stop);
        listPage.AddKey(ConsoleKey.F2, Start);
        listPage.AddColumn("Name", "Name", 20);
        listPage.AddColumn("State", "State");
        listPage.AddColumn("ID", "ID", 40);

        Container selected = listPage.Select();
        if (selected != null)
        {
            Console.WriteLine(selected);
        }
        else
        {
            Quit();
            return;
        }
    }

    public void Stop(Container c)
    {
        Podman.ContainerCMD("stop", c.ID);
        Console.Clear();
        Console.WriteLine("Stopping ...");
        Thread.Sleep(2000);
        Console.Clear();
        Pre_Draw();
        Refresh();
    }
    public void Start(Container c)
    {
        Podman.ContainerCMD("start", c.ID);
        Refresh();
    }
}