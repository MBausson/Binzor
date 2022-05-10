# The Binzor project
    
This projects let me discord razor.  
The application is not intended to be used as a serious pastebin-like website. However, if you like it, you can install it on a server and use it  
    
##  .binzor
is the extension I created for the paste files.  
The first 4 lines contain informations about the paste (name, accessibility, identifier, language)  
The rest of the file is the paste's content.  

I believe this structure is easier to manipulate and efficencier than structures like *json, xml, etc...*  


If you plan to use the blazor service, make sure to change the API base url in Binzor.Program.cs
You can also decide to restraint CORS in BinzorApi.Program.cs

-----------

###Known issues
The codemirror editor's mode does not load correctly when reviewing a paste.
This is because we set the editor's mode (from the paste's language) too early in the page load, the editor does not updates its mode.  
