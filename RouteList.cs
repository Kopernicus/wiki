namespace kopernicus_wiki;

public class RouteList {
    private Dictionary<string, string> routes = new()
    {
        {"Home", "/"},
        {"Config Nodes", "/Prerequisites/ConfigNodes"},
        {"Data Types", "/Prerequisites/DataTypes"},
        {"Getting Started", "/Guides/GettingStarted"},
        {"Atmosphere", "/Syntax/Atmosphere"}
    };

    public string this[string page] {
        get {
            if (routes.Count == 0)
            {
                SetupRouteDictionary();
            }
            return routes[page];
        }
    }

    private static RouteList _instance = new RouteList();

    public static RouteList Instance {
        get { return _instance; }
    }

    private void SetupRouteDictionary() {

    }
}