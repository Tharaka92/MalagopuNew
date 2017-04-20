<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Demo.aspx.cs" Inherits="ClientApplication.Demo" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h2><%: Title%>.</h2>
    <br />
    <asp:TextBox ID="ServiceId" placeholder="Service Id" runat="server" CssClass="form-control serviceIdStyles" TextMode="Number"></asp:TextBox>
    <asp:TextBox ID="SecurityId" placeholder="Security Id" runat="server" CssClass="form-control securityIdStyles" TextMode="Number"></asp:TextBox>
    <div class="checkbox wirteDataFlagCbStyles">
        <asp:CheckBox runat="server" ID="WriteDataFlag" />
        <asp:Label runat="server" AssociatedControlID="writeDataFlag">Save data into the temp table?</asp:Label>
    </div>
    <asp:Button runat="server" OnClick="ContactAPI" Text="Submit" CssClass="btn btn-default submitBtnStyles" />
    <br />
    <br />
    <asp:GridView ID="DisplayGrid" runat="server" CssClass="table table-hover table-striped"></asp:GridView>
</asp:Content>
