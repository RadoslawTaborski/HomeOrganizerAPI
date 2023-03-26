namespace HomeOrganizerAPI.Models;

public partial record PermanentItem : Model
{
    public byte[] GroupUuid { get; set; }
    public string Name { get; set; }
    public byte[] StateUuid { get; set; }
    public byte[] CategoryUuid { get; set; }
    public long Counter { get; set; }
}
