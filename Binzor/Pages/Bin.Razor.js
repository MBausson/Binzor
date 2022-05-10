var editor;

export function loadTextarea(id, obj){

    editor = CodeMirror.fromTextArea(document.getElementById(id), {
        lineNumbers: true,
        readOnly: "nocursor",
        lineWrapping: true,
        wrap: true,
        theme: "monokai",
        autofocus: true,
    });

    editor.setValue(obj.content);
    editor.setSize("97vw", "30vw");

    //  Doesn't work here, we need to wait before we can set the mode
    editor.setOption("mode", obj.language);
}

export function loadModeScript(mode){
    if (mode == "Plain text"){
        return "";
    }
    
    let script = document.createElement("script");
    script.src = `CodeMirror/mode/${mode}/${mode}.js`;
    document.head.appendChild(script);
}

export function copyText(text){
    //  Copies to clipboard specified text
    navigator.clipboard.writeText(text);
}
