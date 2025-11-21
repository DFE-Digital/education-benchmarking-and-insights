namespace Web.App.Domain;

public struct Ks4ProgressProps
{
    public bool Show { get; }
    public string Urn { get; }

    public Ks4ProgressProps()
    {
        Show = false;
        Urn = string.Empty;
    }

    public Ks4ProgressProps(string urn)
    {
        Show = true;
        Urn = urn;
    }
}