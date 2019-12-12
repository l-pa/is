<%@ Page Title="Vyhledavani" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Web.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Vyhledavani</h2>
    <input runat="server" id="searchInput" />
    <asp:button runat="server" Text="Vyhledat" onclick="searchButton_Click"></asp:button>
    <asp:ListView ID="listView" runat="server">
        <LayoutTemplate>
          <table cellpadding="2" width="640px" border="1" runat="server" id="tblProducts">
            <tr runat="server">
              <th runat="server">Nazev</th>
              <th runat="server">Autor</th>
              <th runat="server">Rok vydani</th>
              <th runat="server">Zanr</th>
              <th runat="server">Jazyk</th>
              <th runat="server">Detail</th>
            </tr>
            <tr runat="server" id="itemPlaceholder" />
          </table>
          <asp:DataPager runat="server" ID="ContactsDataPager" PageSize="12">
            <Fields>
              <asp:NextPreviousPagerField ShowFirstPageButton="true" ShowLastPageButton="true"
                FirstPageText="|&lt;&lt; " LastPageText=" &gt;&gt;|"
                NextPageText=" &gt; " PreviousPageText=" &lt; " />
            </Fields>
          </asp:DataPager>
        </LayoutTemplate>

                <ItemTemplate>
          <tr runat="server">
            <td>
              <asp:Label ID="nameLabel" runat="Server" Text='<%#Eval("nazev") %>' />
            </td>
                <td>
                <asp:Label ID="authorLabel" runat="Server" Text='<%#Eval("autor") %>' />
              </td>
                <td>
                <asp:Label ID="publishYearLabel" runat="Server" Text='<%#Eval("rok_vydani") %>' />
              </td>
                  <td>
                  <asp:Label ID="genreLabel" runat="Server" Text='<%#Eval("zanr") %>' />
            </td>
                  <td>
                  <asp:Label ID="languageLabel" runat="Server" Text='<%#Eval("jazyk") %>' />
            </td>

            <td>
                 <asp:Button ID="detail"  runat="Server" Text="Detail" />
            </td>
          </tr>
        </ItemTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
</asp:Content>
