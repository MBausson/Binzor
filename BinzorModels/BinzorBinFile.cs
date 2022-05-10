using Newtonsoft.Json;

namespace BinzorModels;

public class BinzorBinFile
{
    public string Name { get; set; }
    public string Identifier { get; set; }
    public BinzorVisibility Visibility { get; set; }
    public string Language { get; set; }
    public string Content { get; set; }

    public BinzorBinFile()
    {
        
    }
    
    public BinzorBinFile(string name, BinzorVisibility visibility, string language, string content, string identifier)
    {
        Name = name;
        Visibility = visibility;
        Language = language;
        Content = content;
        Identifier = identifier;
    }
    
    public static BinzorBinFile FromJson(string json)
    {
        return JsonConvert.DeserializeObject<BinzorBinFile>(json);
    }
}
