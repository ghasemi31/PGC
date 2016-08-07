<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Gallery.ascx.cs" Inherits="UserControls_Project_Gallery" %>


<div>
    <a class="fancybox-thumbs thumbnail gallery" data-fancybox-group="<%=gallery.ID %>" title="<%=gallery.Title %>" href="<%=ResolveClientUrl(gallery.GalleryThumbImagePath) %>?width=700">
        <img class="width100" src="<%=ResolveClientUrl(gallery.GalleryThumbImagePath) %>?width=300" />
        <div class="gallery-details">
            <span class="gallry-name"><%=gallery.Title %></span>
            <span class="badge badge-danger pic-number"><%=Convert.ToInt32(gallery.GalleryPics.Count())+1 %></span>
        </div>
    </a>
    <div class="hidden">
        <%foreach (var item in gallery.GalleryPics)
          {%>
        <a class="fancybox-thumbs" data-fancybox-group="<%=item.Gallery_ID %>" title="<%=item.Description %>" href="<%=ResolveClientUrl(item.ImagePath) %>?width=700">
            <img class="width100" src="<%=ResolveClientUrl(item.ImagePath) %>?width=100" />
        </a>
        <% } %>
    </div>
</div>
