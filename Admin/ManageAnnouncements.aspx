<%@ Page Title="Manage Announcements" Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="ManageAnnouncements.aspx.cs" Inherits="Aarambha.Admin.ManageAnnouncements" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Announcements</h3>
    <asp:Literal ID="ltAlert" runat="server"></asp:Literal>

    <div class="card p-3 mb-3">
        <h5>Create Announcement</h5>
        <form>
            <div class="row">
                <div class="col-md-12 mb-3">
                    <label for="txtTitle">Title</label>
                    <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" placeholder="Announcement Title" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12 mb-3">
                    <label for="txtContent">Content</label>
                    <asp:TextBox ID="txtContent" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" placeholder="Announcement Content..." />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnAdd" runat="server" Text="Post" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                </div>
            </div>
        </form>
    </div>
    <asp:GridView ID="gvAnnouncements" runat="server" CssClass="table table-borderless" AutoGenerateColumns="false" OnRowCommand="gvAnnouncements_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="card mb-3">
                        <div class="card-body">
                            <div class="d-flex justify-content-between">
                                <div>
                                    <h5 class="card-title mb-1"><%# Eval("Title") %></h5>
                                    <p class="card-text text-muted small mb-1"><%# Eval("Message") != null && ((string)Eval("Message")).Length > 120 ? ((string)Eval("Message")).Substring(0,120) + "..." : Eval("Message") %></p>
                                    <div class="text-muted small">Posted: <%# Eval("PostedAt", "{0:g}") %></div>
                                </div>
                                <div>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("AnnouncementID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Are you sure you want to delete this announcement?');">Delete</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>