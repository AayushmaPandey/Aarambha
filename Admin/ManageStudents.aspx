<%@ Page Title="Manage Students" Language="C#" MasterPageFile="~/Masterpages/Admin.Master" AutoEventWireup="true" CodeBehind="ManageStudents.aspx.cs" Inherits="Aarambha.Admin.ManageStudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h3>Manage Students</h3>
    <asp:Literal ID="ltAlert" runat="server"></asp:Literal>

    <div class="card p-3 mb-3">
        <h5>Add Student</h5>
        <form>
            <div class="row">
                <div class="col-md-4 mb-3">
                    <label for="txtFullName">Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" placeholder="Full Name" />
                </div>
                <div class="col-md-4 mb-3">
                    <label for="txtUsername">Username</label>
                    <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Username" />
                </div>
                <div class="col-md-4 mb-3">
                    <label for="txtPassword">Password</label>
                    <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password" />
                </div>
            </div>
            <div class="row">
                <div class="col-md-4 mb-3">
                    <label for="txtEmail">Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email" />
                </div>
                <div class="col-md-4 mb-3">
                    <label for="txtPhone">Phone</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" placeholder="Phone" />
                </div>
                <div class="col-md-4 d-flex align-items-end mb-3">
                    <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btnAdd_Click" />
                </div>
            </div>
        </form>
    </div>

    <asp:GridView ID="gvStudents" runat="server" CssClass="table table-striped" AutoGenerateColumns="false" DataKeyNames="UserID" OnRowCommand="gvStudents_RowCommand" OnRowEditing="gvStudents_RowEditing" OnRowUpdating="gvStudents_RowUpdating" OnRowCancelingEdit="gvStudents_RowCancelingEdit">
        <Columns>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <%# Eval("FullName") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditFullName" runat="server" CssClass="form-control" Text='<%# Bind("FullName") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Username" HeaderText="Username" ReadOnly="true" />
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# Eval("Email") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditEmail" runat="server" CssClass="form-control" Text='<%# Bind("Email") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Phone">
                <ItemTemplate>
                    <%# Eval("PhoneNumber") %>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtEditPhone" runat="server" CssClass="form-control" Text='<%# Bind("PhoneNumber") %>' />
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# (bool)Eval("IsActive") ? "Active" : "Disabled" %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="btnToggle" runat="server" CommandName="Toggle" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-sm btn-warning mr-1">
                            <%# (bool)Eval("IsActive") ? "Disable" : "Enable" %>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEdit" runat="server" CommandName="Edit" CommandArgument='<%# Container.DataItemIndex %>' CssClass="btn btn-sm btn-secondary mr-1">Edit</asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteStudent" CommandArgument='<%# Eval("UserID") %>' CssClass="btn btn-sm btn-danger" OnClientClick="return confirm('Are you sure you want to delete this student?');">Delete</asp:LinkButton>
                    </div>
                </ItemTemplate>
                <EditItemTemplate>
                    <div class="btn-group" role="group">
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandName="Update" CssClass="btn btn-sm btn-success mr-1">Save</asp:LinkButton>
                        <asp:LinkButton ID="btnCancel" runat="server" CommandName="Cancel" CssClass="btn btn-sm btn-secondary">Cancel</asp:LinkButton>
                    </div>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>