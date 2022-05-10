let languages = ['apl', 'asciiarmor', 'asn.1', 'asterisk', 'brainfuck', 'clike', 'clojure', 'cmake', 'cobol', 'coffeescript', 'commonlisp', 'crystal', 'css', 'cypher', 'd', 'dart', 'diff', 'django', 'dockerfile', 'dtd', 'dylan', 'ebnf', 'ecl', 'eiffel', 'elm', 'erlang', 'factor', 'fcl', 'forth', 'fortran', 'gas', 'gfm', 'gherkin', 'go', 'groovy', 'haml', 'handlebars', 'haskell', 'haskell-literate', 'haxe', 'htmlembedded', 'htmlmixed', 'http', 'idl', 'index.html', 'javascript', 'jinja2', 'jsx', 'julia', 'livescript', 'lua', 'markdown', 'mathematica', 'mbox', 'meta.js', 'mirc', 'mllike', 'modelica', 'mscgen', 'mumps', 'nginx', 'nsis', 'ntriples', 'octave', 'oz', 'pascal', 'pegjs', 'perl', 'php', 'pig', 'powershell', 'properties', 'protobuf', 'pug', 'puppet', 'python', 'q', 'r', 'rpm', 'rst', 'ruby', 'rust', 'sas', 'sass', 'scheme', 'shell', 'sieve', 'slim', 'smalltalk', 'smarty', 'solr', 'soy', 'sparql', 'spreadsheet', 'sql', 'stex', 'stylus', 'swift', 'tcl', 'textile', 'tiddlywiki', 'tiki', 'toml', 'tornado', 'troff', 'ttcn', 'ttcn-cfg', 'turtle', 'twig', 'vb', 'vbscript', 'velocity', 'verilog', 'vhdl', 'vue', 'wast', 'webidl', 'xml', 'xquery', 'yacas', 'yaml', 'yaml-frontmatter', 'z80'];
var editor;

export function loadTextarea(id){
    editor = CodeMirror.fromTextArea(document.getElementById(id), {
        lineNumbers: true,
        smartIndent: true,
        lineWrapping: true,
        autofocus: true,
        theme: "monokai",
        cursorBlinkRate: 300,
        trailingSpace: true,
        placeholder: "Type your code here...",
        wrap: true,
    });

    editor.setValue('\0');
    editor.setValue('');
    
    //  Change max-width of the codemirror editor
    editor.setSize("97vw", "30vw");
}

export function populateLanguageSelect(id) {
    let select = document.getElementById(id);
    
    //  Populates the select with the languages list
    for (let i = 0; i < languages.length; i++) {
        let option = document.createElement("option");
        option.text = languages[i];
        option.value = languages[i];
        select.add(option);
    }
}


//  Returns a dict with bin's name, 
export function getSettingsForm(){
    let languageTag = document.getElementById("language");
    let language = languageTag.options[languageTag.selectedIndex].value;
    
    let visibilityTag = document.getElementById("accessibility");
    let visibility = visibilityTag.options[visibilityTag.selectedIndex].value;
    
    let name = document.getElementById("bin-name").value;
    
    visibility = visibility == "public" ? "1" : "0";
    name = name.length > 0 ? name : "Untitled bin";
    
    return {
        name: name,
        language: language,
        visibility: visibility
    }
}

export function getBinContent(){
    return editor.getValue();
}

export function resetEditor(){
    editor.toTextArea();
}
