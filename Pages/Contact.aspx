<%@ Page Title="" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="Aarambha.Pages.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">Contact</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="page-header">
        <div class="container">
            <h1>Contact Us</h1>
            <p class="mb-0">Questions about Aarambha? Send us a message and a teacher will get back to you.</p>
        </div>
    </div>

    <div class="container mb-5">
        <div class="row justify-content-center">
            <div class="col-md-7">
                <div class="feature-card">

                    <asp:Panel ID="pnlSuccess" runat="server" CssClass="alert alert-success" Visible="false">
                        Your message has been sent. Thank you!
                    </asp:Panel>
                    <asp:Panel ID="pnlError" runat="server" CssClass="alert alert-danger" Visible="false">
                        Something went wrong. Please try again.
                    </asp:Panel>

                    <div class="mb-3">
                        <label class="form-label">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtName"
                            CssClass="text-danger small" ErrorMessage="Name is required" Display="Dynamic" ValidationGroup="contact" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" TextMode="Email" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger small" ErrorMessage="Email is required" Display="Dynamic" ValidationGroup="contact" />
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="txtEmail"
                            CssClass="text-danger small" ErrorMessage="Enter a valid email" Display="Dynamic"
                            ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" ValidationGroup="contact" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Subject</label>
                        <asp:TextBox ID="txtSubject" runat="server" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtSubject"
                            CssClass="text-danger small" ErrorMessage="Subject is required" Display="Dynamic" ValidationGroup="contact" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label">Message</label>
                        <asp:TextBox ID="txtMessage" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="5" />
                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMessage"
                            CssClass="text-danger small" ErrorMessage="Message is required" Display="Dynamic" ValidationGroup="contact" />
                    </div>

                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary px-4"
                        Text="Send Message" OnClick="btnSubmit_Click" ValidationGroup="contact" />

                </div>
            </div>
        </div>
    </div>

</asp:Content>