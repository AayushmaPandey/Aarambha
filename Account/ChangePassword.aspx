<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Aarambha.Account.ChangePassword" MasterPageFile="~/Masterpages/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row justify-content-center mt-4">
        <div class="col-md-6">
            <div class="card shadow-sm p-3">
                <h4>Change Password</h4>
                <asp:Literal ID="ltAlert" runat="server"></asp:Literal>
                <div class="form-group">
                    <label>Current Password</label>
                    <asp:TextBox ID="txtCurrent" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>New Password</label>
                    <asp:TextBox ID="txtNew" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="form-group">
                    <label>Confirm New Password</label>
                    <asp:TextBox ID="txtConfirm" runat="server" TextMode="Password" CssClass="form-control" />
                </div>
                <div class="text-right">
                    <asp:Button ID="btnChange" runat="server" Text="Change Password" CssClass="btn btn-primary" OnClick="btnChange_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>