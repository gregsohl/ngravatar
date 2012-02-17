*******
License
*******
Copyright (c) <2012> <Ken Yourek>

Permission is hereby granted, free of charge, to any person obtaining a copy 
of this software and associated documentation files (the "Software"), to deal 
in the Software without restriction, including without limitation the rights to 
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of 
the Software, and to permit persons to whom the Software is furnished to do so, 
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all 
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR 
PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE 
FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, 
ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

*****
About
*****
NGravatar provides an MVC-ish way of grabbing avatar images from gravatar.com.
The project is hosted at http://code.google.com/p/ngravatar/.
It can also be installed with NuGet: PM> Install-Package NGravatar.
See http://nuget.org/packages/NGravatar for more information.

***
Use
***
First, include the NGravatar.Html namespace in your Web.config, like so:
<namespaces>
    <!-- Other namespaces will be included here. -->
    <add namespace="NGravatar.Html" />
</namespaces>

Then, use the HtmlHelper extension method to render a full Gravatar image by email:
<%= Html.Gravatar("some@email.com", null) %>

Or, just get the Gravatar source with the UrlHelper extension method:
<img src="<%=Url.Gravatar("ngravatar@kendoll.net", 340) %>" alt="NGravatar Gravatar" />
