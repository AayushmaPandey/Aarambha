<%@ Page Title="Manage Subjects" Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="ManageSubjects.aspx.cs" Inherits="Aarambha.Admin.ManageSubjects" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Subjects</h3>
    <asp:Literal ID="ltAlert" runat="server"></asp:Literal>

    <div class="card p-3 mb-3">
        <h5>Add Subject</h5>
        <form>
            <div class="row">
                <div class="col-md-6 mb-3">
                    <label for="txtSubjectName">Subject Name</label>
                    <asp:TextBox ID="txtSubjectName" runat="server" CssClass="form-control" placeholder="Subject Name" />
                </div>
                <div class="col-md-6 mb-3">
                    <label for="txtDescription">Description</label>
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" placeholder="Description" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="btnAdd" runat="server" Text="Add Subject" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                </div>
            </div>
        </form>
    </div>
    <asp:GridView ID="gvSubjects" runat="server" CssClass="table table-hover" AutoGenerateColumns="false" OnRowCommand="gvSubjects_RowCommand">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <div class="d-flex align-items-center">
                        <img src='<%# ResolveUrl(Aarambha.Helpers.UiHelper.SubjectImage(Eval("SubjectName"))) %>' alt="" class="me-3" style="width:48px;height:48px;" />
                        <div>
                            <strong><%# Eval("SubjectName") %></strong>
                            <div class="text-muted small"><%# Eval("Description") %></div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# (bool)Eval("IsActive") ? "<span class=\"badge bg-success\">Active</span>" : "<span class=\"badge bg-secondary\">Inactive</span>" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="btnToggle" runat="server" CommandName="Toggle" CommandArgument='<%# Eval("SubjectID") %>' CssClass="btn btn-sm btn-warning">Toggle</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("SubjectID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Are you sure you want to delete this subject?');">Delete</asp:LinkButton>
                    </div>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>