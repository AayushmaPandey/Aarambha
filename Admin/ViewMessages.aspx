<%@ Page Title="Contact Messages" Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="ViewMessages.aspx.cs" Inherits="Aarambha.Admin.ViewMessages" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Contact Messages</h3>

    <asp:GridView ID="gvMessages" runat="server" CssClass="table table-borderless" AutoGenerateColumns="false" OnRowCommand="gvMessages_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="<%# (bool)Eval("IsRead") ? "card mb-3" : "card mb-3 border-start border-4 border-primary" %>">
                        <div class="card-body">
                            <div class="d-flex">
                                <div class="flex-grow-1">
                                    <h5 class="card-title mb-1"><%# Eval("Name") %> <%# !(bool)Eval("IsRead") ? "<span class=\"badge bg-danger ms-2\">New</span>" : "" %></h5>
                                    <h6 class="card-subtitle mb-2 text-muted"><%# Eval("Subject") %></h6>
                                    <p class="card-text mb-1"><%# Eval("Message") %></p>
                                    <div class="text-muted small">Submitted: <%# Eval("SubmittedAt", "{0:g}") %></div>
                                </div>
                                <div class="ms-3 text-end">
                                    <asp:LinkButton ID="btnToggle" runat="server" CommandName="Toggle" CommandArgument='<%# Eval("MessageID") %>' CssClass="btn btn-sm btn-outline-primary mb-2">
                                        <%# (bool)Eval("IsRead") ? "Mark Unread" : "Mark Read" %>
                                    </asp:LinkButton>
                                    <br />
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("MessageID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Are you sure you want to delete this message?');">Delete</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>