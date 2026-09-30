<%@ Page Title="Notes" Language="C#" MasterPageFile="~/Masterpages/Site.Master" AutoEventWireup="true" CodeBehind="Notes.aspx.cs" Inherits="Aarambha.Member.Notes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-4">
            <div class="card p-3 shadow-sm">
                <h5>Subjects</h5>
                <asp:DropDownList ID="ddlSubjects" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlSubjects_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="col-md-8">
            <asp:Literal ID="ltNotes" runat="server"></asp:Literal>
        </div>
    </div>
</asp:Content>