<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Aarambha.Account.Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Sign Up</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="auth-page"><div class="auth-shell">
        <div class="auth-side">
            <img src="<%= ResolveUrl("~/Content/images/hero.svg") %>" alt="Students learning online" />
            <h3 class="fw-bold">Join Aarambha</h3>
            <p>Create a free student account to read notes, take quizzes and track your progress. Teacher accounts are created by the school.</p>
        </div>
        <div class="auth-card">
                    <div class="text-center mb-4">
                        <div class="fw-bold fs-4 text-primary">AARAMBHA</div>
                        <h4 class="mt-2 mb-0">Create Your Account</h4>
                        <p class="text-muted small">Join as a Grade 8 student and start learning</p>
                    </div>

                    <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                        <asp:Literal ID="litError" runat="server" />
                    </asp:Panel>

                    <div class="mb-3">
                        <label class="form-label">Full Name</label>
                        <asp:TextBox ID="txtFullName" runat="server" CssClass="form-control" MaxLength="100" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFullName"
                            CssClass="text-danger small" ErrorMessage="Full name is required" Display="Dynamic" ValidationGroup="register" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Username</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" MaxLength="50" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUsername"
                            CssClass="text-danger small" ErrorMessage="Username is required" Display="Dynamic" ValidationGroup="register" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" MaxLength="150" />
                        <asp:RequiredFieldValidator 
                            runat="server" 
                            ControlToValidate="txtEmail"
                            CssClass="text-danger small" 
                            ErrorMessage="Email is required" 
                            Display="Dynamic" 
                            ValidationGroup="register" />
                        <asp:RegularExpressionValidator 
                            runat="server" 
                            ControlToValidate="txtEmail"
                            CssClass="text-danger small" 
                            ErrorMessage="Enter a valid email" 
                            Display="Dynamic" 
                            ValidationGroup="register"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Phone Number</label>
                        <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control" MaxLength="20" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Password</label>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword"
                            CssClass="text-danger small" ErrorMessage="Password is required" Display="Dynamic" ValidationGroup="register" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPassword"
                            CssClass="text-danger small" ErrorMessage="Password must be at least 6 characters" Display="Dynamic" ValidationGroup="register"
                            ValidationExpression=".{6,}" />
                    </div>

                    <div class="mb-4">
                        <label class="form-label">Confirm Password</label>
                        <asp:TextBox ID="txtConfirmPassword" runat="server" CssClass="form-control" TextMode="Password" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword"
                            CssClass="text-danger small" ErrorMessage="Please confirm your password" Display="Dynamic" ValidationGroup="register" />
                        <asp:CompareValidator 
                            runat="server" 
                            ControlToValidate="txtConfirmPassword" 
                            ControlToCompare="txtPassword"
                            CssClass="text-danger small" 
                            ErrorMessage="Passwords do not match" 
                            Display="Dynamic" 
                            ValidationGroup="register" />
                    </div>

                    <asp:Button ID="btnRegister" runat="server" CssClass="btn btn-primary w-100"
                        Text="Create Account" OnClick="btnRegister_Click" ValidationGroup="register" />

                    <p class="text-center small text-muted mt-3 mb-0">
                        Already have an account?
                        <asp:HyperLink runat="server" NavigateUrl="~/Account/Login.aspx">Log in</asp:HyperLink>
                    </p>
                </div></div></div>

</asp:Content>
