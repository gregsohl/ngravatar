<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h3>Gravatars can be rendered with an HtmlHelper extension method.</h3>    
        <%= Html.Gravatar("ngravatar@kendoll.net", 220, null, NGravatar.Rating.PG, new { style = "display:block;margin:0px auto;", title = "This Gravatar was created with an HtmlHelper extension method for MVC.", alt = "NGravatar Gravatar" }) %>
    </div>

    <div>
        <h3>...Or you can get the source from the UrlHelper extension method.</h3>
        <img src="<%=Url.Gravatar("ngravatar@kendoll.net", 340) %>" style="display:block;margin:0px auto;" title="This Gravatar's source was gotten with a UrlHelper extension method for MVC." alt="NGravatar Gravatar" />
    </div>

    <div>
        <h3>The UrlHelper and HtmlHelper can also render links to a Gravatar user's profile page.</h3>
        <a href="<%= Url.Grofile("ngravatar@kendoll.net") %>">UrlHelper</a>
        <%= Html.GrofileLink("HtmlHelper", "ngravatar@kendoll.net") %>
    </div>

    <div>
        
    </div>

</asp:Content>
