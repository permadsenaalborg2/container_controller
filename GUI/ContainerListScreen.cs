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

        Clear(this); //Clean the screen

        listPage.AddColumn("ID", "ID");
        listPage.AddColumn("Name", "Name");
        listPage.AddColumn("State", "State");

        Container selected = listPage.Select();
        if (selected != null)
        {
            //Screen.Display(new EditTodoScreen(selected))
            System.Console.WriteLine(selected);
        }
        else
        {
            Quit();
            return;
        }
    }
}