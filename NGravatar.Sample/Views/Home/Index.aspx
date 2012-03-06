﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NGravatar.IGrofileInfo>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h3>Gravatars can be rendered with an HtmlHelper extension method.</h3>    
        <%= Html.Gravatar("ngravatar@kendoll.net", 110, null, NGravatar.Rating.PG, new { style = "display:block;margin:0px auto;", title = "This Gravatar was created with an HtmlHelper extension method for MVC.", alt = "NGravatar Gravatar" }) %>
    </div>

    <div>
        <h3>...Or you can get the source from the UrlHelper extension method.</h3>
        <img src="<%=Url.Gravatar("ngravatar@kendoll.net", 200) %>" widht="200px" height="200px" style="display:block;margin:0px auto;" title="This Gravatar's source was gotten with a UrlHelper extension method for MVC." alt="NGravatar Gravatar" />
    </div>

    <div>
        <h3>The UrlHelper and HtmlHelper can also render links to a Gravatar user's profile page.</h3>
        <a href="<%= Url.Grofile("ngravatar@kendoll.net") %>">UrlHelper</a>
        <%= Html.GrofileLink("HtmlHelper", "ngravatar@kendoll.net") %>
    </div>

    <div>
        <h3>Gravatar profiles can be rendered as JSON data.</h3>
        See the <a href="http://en.gravatar.com/site/implement/profiles/json/">Gravatar API</a> for details. The HtmlHelper
        can be used to render the appropriate script tag and callback to deal with the profile JSON data when it is rendered.
        See the page source for more script information.
    </div>
    
    <script type="text/javascript">
        // This is the callback that we'll pass to the Grofile script.
        function changeTitle(profile) {
            document.title = profile.entry[0].displayName + " Gravatar";
        }
    </script>

    <!-- Create a script tag that loads the Gravatar profile JSON data and calls our callback. -->
    <%= Html.GrofileScript("ngravatar@kendoll.net", "changeTitle") %>

    <div>
        <h3>Gravatar profile information can also be loaded into a CLR object.</h3>
        Here, we get all of the pictures associated with a profile.<br />
        <% foreach (var photo in Model.Photos) { %>
            <img src="<%= photo.Value %>" />
        <% } %>
    </div>
</asp:Content>
