public class Container
{
    private string id = String.Empty;
    public string ID
    {
        get
        {
            return id.Substring(0, 10) + "..";
        }
        set
        {
            id=value;
        }
    }
    
    public string Name { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
}