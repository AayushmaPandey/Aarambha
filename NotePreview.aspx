<%@ Page Title="Note Preview" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" %>
<%@ Import Namespace="Aarambha.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <% 
                int nid = 0;
                if (int.TryParse(Request.QueryString["noteId"], out nid))
                {
                    try
                    {
                        var bll = new NoteBLL();
                        var note = bll.GetById(nid);
                        if (note != null)
                        {
            %>
            <div class="card mb-3">
                <div class="card-body">
                    <h3><%= Server.HtmlEncode(note.Title) %></h3>
                    <div class="mt-3"><%= note.ContentHtml %></div>
                    <div class="mt-3">
                        <% var resources = new Aarambha.BLL.ResourceBLL().GetByNoteId(nid); if (resources != null && resources.Count > 0) { %>
                        <h6>Resources</h6>
                        <ul>
                            <% foreach (var r in resources) { var url = ResolveUrl("~/Content/uploads/notes/" + System.IO.Path.GetFileName(r.FilePath)); %>
                            <li><a href="<%= url %>" target="_blank"><%= Server.HtmlEncode(r.Title) %></a></li>
                            <% } %>
                        </ul>
                        <% } %>
                    </div>
                </div>
            </div>
            <%
                        }
                        else
                        {
                            Response.Write("<div class=\"alert alert-warning\">Note not found.</div>");
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<div class=\"alert alert-danger\">Error: " + Server.HtmlEncode(ex.Message) + "</div>");
                    }
                }
                else
                {
                    Response.Write("<div class=\"alert alert-warning\">Invalid note id.</div>");
                }
            %>
        </div>
    </div>
</asp:Content>