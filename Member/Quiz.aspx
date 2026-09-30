<%@ Page Title="Quiz" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="Quiz.aspx.cs" Inherits="Aarambha.Member.Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <div class="card p-3 shadow-sm">
                <h5>Quizzes</h5>
                <asp:DropDownList ID="ddlQuizzes" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlQuizzes_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-8">
            <asp:Literal ID="ltQuizArea" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>