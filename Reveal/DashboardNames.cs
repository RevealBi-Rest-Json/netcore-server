namespace RevealSdk.Server.Reveal
{
    // ****
    // Class used for Optional endpoint to get the list of dashboard names
    // app.MapGet("/dashboards/names", () => { ... // located in Program.cs
    // ****
    public class DashboardNames
    {
        public string? DashboardFileName { get; set; }
        public string? DashboardTitle { get; set; }
    }
}
