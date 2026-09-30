<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Aarambha.Account.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Login</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-page"><div class="auth-shell">
        <div class="auth-side">
            <img src="<%= ResolveUrl("~/Content/images/hero.svg") %>" alt="Students learning online" />
            <h3 class="fw-bold">Welcome back!</h3>
            <p>Pick up where you left off: notes, quizzes and results are waiting.</p>
        </div>
        <div class="auth-card">
                    <div class="text-center mb-4">
                        <div class="fw-bold fs-4 text-primary">AARAMBHA</div>
                        <h4 class="mt-2 mb-0">Welcome Back</h4>
                        <p class="text-muted small">Sign in to continue learning</p>
                    </div>

                    <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                        <asp:Literal ID="litError" runat="server" />
                    </asp:Panel>

                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername"
                            CssClass="text-danger small" ErrorMessage="Username is required" Display="Dynamic" ValidationGroup="login" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                            CssClass="text-danger small" ErrorMessage="Password is required" Display="Dynamic" ValidationGroup="login" />
                    </div>

                    <asp:Button ID="btnLogin" runat="server" CssClass="btn btn-primary w-100"
                        Text="Login" OnClick="btnLogin_Click" ValidationGroup="login" />

                    <p class="text-center small text-muted mt-3 mb-0">
                        New here?
                        <asp:HyperLink runat="server" NavigateUrl="~/Account/Register.aspx">Create an account</asp:HyperLink>
                    </p>
                </div></div></div>

</asp:Content>