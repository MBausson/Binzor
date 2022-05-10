using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using BinzorModels;
using Microsoft.AspNetCore.Cors;

namespace BinzorApi.Controllers;

[ApiController]
[Route("[controller]")]
public class BinzorFileController : ControllerBase
{
    private static string _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    [HttpPost("Create")]
    public async Task<ActionResult<BinzorBinFile>> Post(string name, int visibility, string language)
    {
        StreamReader reader = new StreamReader(HttpContext.Request.Body);
        string content = await reader.ReadToEndAsync();

        if (content.Length < 3){
            return BadRequest($"Bin's content is too small.");
        }

        BinzorBinFile bbf = await CreateFileBin(name, visibility, language, content);

        return Ok(bbf);
    }
    
    [HttpGet("Read")]
    public async Task<ActionResult<BinzorBinFile>> Get(string identifier)
    {
        //  Avoids malicious path manipulation
        if (identifier.Contains('.') || identifier.Contains('/') || identifier.Contains('\\')){
            return Unauthorized($"Invalid character specified in the bin tag.");
        }
        
        if (!System.IO.File.Exists($"Bins/{identifier}.binzor")){
            return NotFound($"Could not find a bin with tag {identifier}");
        }

        return Ok(await ReadFileBin(identifier));
    }

    private async Task<BinzorBinFile> CreateFileBin(string name, int visibility, string language, string content)
    {
        string identifier = generateBinTag();
        string fp = $"Bins/{identifier}.binzor";

        if (System.IO.File.Exists(fp)){
            //  This handles the probability that we generated an existing identifier
            //  -> just try to create another file with another identifier by recursivity
            return await CreateFileBin(name, visibility, language, content);
        }
        
        /*
            Typical bin file structure:
            File name: <Bin's tag(ID)>.binzor
            Line1: Bin's name
            Line2: Bin's tag(ID)
            Line3: Bin's language
            Line4: Bin's visibility (1 for public, 0 for private)
            Rest of the file: Bin's content
        */

        string[] lines = new string[5]
        {
            name,
            identifier,
            language,
            visibility.ToString(),
            content
        };
        
        await System.IO.File.WriteAllLinesAsync(fp, lines);
        
        return new BinzorBinFile(name, (BinzorVisibility)visibility, language, content, identifier);
    }

    private async Task<BinzorBinFile> ReadFileBin(string identifier)
    {
        string fp = $"Bins/{identifier}.binzor";
        BinzorBinFile bbf = new BinzorBinFile();

        string[] fileLines = await System.IO.File.ReadAllLinesAsync(fp);

        bbf.Name = fileLines[0];
        bbf.Identifier = fileLines[1];
        bbf.Language = fileLines[2];
        bbf.Visibility = (BinzorVisibility)Int32.Parse(fileLines[3]);

        for (int li = 4; li < fileLines.Length; li++){
            bbf.Content += fileLines[li];
            
            //  We add a new line because the System.IO.File.ReadAllLinesAsync removes the '\n'
            bbf.Content += '\n';
        }

        return bbf;
    }

    private static string generateBinTag()
    {
        var rnd = new Random();

        //Creates a random string with characters of TOKENALPHABET of length 6, url-safe
        string binTag = "";

        for (int i = 0; i < 6; i++){
            binTag += _alphabet[rnd.Next(_alphabet.Length)];
        }

        return binTag;
    }
}
