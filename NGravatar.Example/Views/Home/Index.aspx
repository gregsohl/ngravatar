<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>NGravatar Example</title>
    <style type="text/css">
        h1
        {
            color:Gray;
            font-size:large;
            font-weight:bold;
            font-family:Sans-Serif;
            text-align:center;
        }
    </style>
</head>
<body>
    
    <div>
        <h1>Gravatars can be rendered with an HtmlHelper extension method...</h1>
        
        <%= Html.Gravatar("ngravatar@kendoll.net", 220, null, NGravatar.Rating.PG, new { style = "display:block;margin:0px auto;", title = "This Gravatar was created with an HtmlHelper extension method for MVC.", alt = "NGravatar Gravatar" }) %>
    </div>

    <div>
        <h1>...Or you can get the source from the UrlHelper extension method...</h1>
        <img src="<%=Url.Gravatar("ngravatar@kendoll.net", 340) %>" style="display:block;margin:0px auto;" title="This Gravatar's source was gotten with a UrlHelper extension method for MVC.", alt="NGravatar Gravatar" />
    </div>
    
</body>
</html>
