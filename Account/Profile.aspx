<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="Aarambha.Account.Profile" MasterPageFile="~/Masterpages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6">
            <div class="card shadow-sm p-3">
                <h4>My Profile</h4>
                <asp:Literal ID="ltAlert" runat="server"></asp:Literal>
                <div class="form-group">
                    <label>Full Name</label>
                    <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Phone Number</label>
                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" />
                </div>
                <div class="text-right">
                    <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <a class="btn btn-link" href="~/Account/ChangePassword.aspx">Change Password</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>