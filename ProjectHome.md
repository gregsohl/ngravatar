**NGravatar** contains `HtmlHelper` and `UrlHelper` extension methods for retrieving avatars from [gravatar.com](http://gravatar.com).

Gravatars can be rendered with an HtmlHelper extension method...
```
<%= Html.Gravatar("ngravatar@kendoll.net", 220, null, NGravatar.Rating.PG, new { style = "margin:0px auto;" }) %>
```

...Or you can get the image location from the UrlHelper extension method...
```
<img src="<%=Url.Gravatar("ngravatar@kendoll.net", 340) %>" alt="NGravatar Gravatar" />
```

Just include the NGravatar.Html namespace in your Web.config.
```
<namespaces>
    <!-- Other namespaces will be here. -->
    <add namespace="NGravatar.Html" />
</namespaces>
```

The source contains **example** and **test** projects to help get you started.

**Install with NuGet!**
```
PM> Install-Package NGravatar
```

See http://nuget.org/packages/NGravatar for more information.