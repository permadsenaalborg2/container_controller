using TECHCOOL.UI;

public class ContainerListScreen : Screen
{
    private ListPage<Container> listPage;

    public ContainerListScreen(List<Container> lst)
    {
        listPage = new ListPage<Container>();
        foreach (Container c in lst)
        {
            listPage.Add(c);
        }
    }

    public override string Title { get; set; } = "List of containers";
    protected override void Draw()
    {

        Clear(this);

        listPage.AddKey(ConsoleKey.F1, Stop);
        listPage.AddKey(ConsoleKey.F2, Start);
        Console.WriteLine("Press F1 to stop container");
        Console.WriteLine("Press F2 to start container");

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
        Podman.ContainerCMD(c.ID, "stop");
    }
    public void Start(Container c)
    {
        Podman.ContainerCMD(c.ID, "start");
    }
}