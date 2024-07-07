public class Container
{
    private string id = String.Empty;
    public string ID
    {
        get
        {
            if (id.Length>10)
                return id.Substring(0, 10) + "..";
            else
                return id;
        }
        set
        {
            id=value;
        }
    }
    
    public string Name { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
}