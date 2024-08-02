using System.Text;

public class Container
{
    public string ID { get; set; } = String.Empty;

    public string PrettyID
    {
        get
        {
            if (ID.Length > 4)
                return ID[..4] + " ..";
            else
                return ID;
        }
    }

    public string Name { get; set; } = String.Empty;
    public string State { get; set; } = String.Empty;
    public string CreatedAt { get; set; } = String.Empty;
    public string Image { get; set; } = String.Empty;


    public override string ToString()
    {
        StringBuilder sb = new();

        sb.AppendLine("Container: ");
        sb.AppendLine($"Id: {ID} ");
        sb.AppendLine($"Name: {Name} ");
        sb.AppendLine($"State: {State} ");
        sb.AppendLine($"Image: {Image} ");
        sb.AppendLine($"CreatedAt: {CreatedAt} ");

        return sb.ToString();
    }
}