<%@ Page Title="Detail" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="Web.Detail" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2 runat="server" id="title"></h2>
    
    <div runat="server" Visible="False" ID="successMessage" class="alert alert-success" role="alert">
        <asp:Literal ID="successMessageText" runat="server" Text='' />
    </div>
    
    <div runat="server" Visible="False" id="errorMessage" class="alert alert-danger" role="alert">
        <asp:Literal ID="errorMessageText" runat="server" Text='' />

    </div>
    
    <div runat="server" id="promptMessage" Visible="False" class="alert alert-secondary" role="alert">
        <asp:Literal ID="promptMessageText" runat="server" Text='' />
        <asp:Button runat="server" Text="Rezervovat" Visible="False" ID="extendNextBook" OnClick="extendNextBook_OnClick"/>
        <asp:Button runat="server" Text="Zrusit" Visible="False" ID="cancelExtendNextBook" OnClick="cancelExtendNextBook_OnClick"/>

    </div>
    <div style="display: flex">
    <table class="table" runat="server">
  <thead>
  </thead>
  <tbody>
    <tr>
      <th scope="row">Nazev</th>
      <td id="nazev" runat="server">Mark</td>

    </tr>
    <tr>
      <th scope="row">Jazyk</th>
      <td id="jazyk" runat="server">Jacob</td>

    </tr>
    <tr>
      <th scope="row">Zanr</th>
      <td id="zanr" runat="server">Larry</td>

    </tr>
  <tr>
      <th scope="row">Stav</th>
      <td  id="stav" runat="server">Larry</td>

  </tr>
  <tr>
      <th scope="row" >Vypujcena</th>
      <td id="vypujcena" runat="server">Larry</td>

  </tr>
  </tbody>
</table>
    
    
    <table class="table">
        <thead>
        <tr>
        </tr>
        </thead>
        <tbody>
        <tr>
            <th scope="row" >Autor</th>
            <td id="autor" runat="server">Mark</td>

        </tr>
        <tr>
            <th scope="row" >Rok vydani</th>
            <td id="rokVydani" runat="server">Jacob</td>

        </tr>
        <tr>
            <th scope="row" >ISBN</th>
            <td id="isbn" runat="server">Larry</td>

        </tr>
        <tr>
            <th scope="row" >Posledni rezervace</th>
            <td id="posledniRezervace" runat="server">Larry</td>

        </tr>
        <tr>
            <th scope="row" >Rezervovana</th>
            <td id="rezervovana" runat="server">Larry</td>

        </tr>
        </tbody>
    </table>
    </div>
    <asp:Button ID="reservateButton" runat="server" OnClick="reservateButton_Click" Text="Rezervace"></asp:Button>
    <asp:Button ID="canceButton" runat="server" Text="Zrusit" OnClick="canceButton_Click"></asp:Button>

    <div id="showCalendar" runat="server" visible="False">
        <asp:Calendar runat="server" ID="calendar" OnLoad="calendar_OnLoad" OnSelectionChanged="calendar_SelectionChanged" OnDayRender="calendar_OnDayRender" SelectionMode="DayWeekMonth">
            <SelectedDayStyle  
                BackColor="Crimson"  
                BorderColor="Tomato"  
            />
        </asp:Calendar>
        
        <asp:Button runat="server" Text="Potvrdit rezervaci" ID="confirmReservation" OnClick="confirmReservation_OnClick"/>
        
        <div runat="server" id="newDateDiv" Visible="False" class="alert alert-secondary" role="alert">
            <asp:Literal ID="newDate" runat="server" Text='' />
            <asp:Button runat="server" Text="Rezervovat" Visible="False" ID="reservateNewDate" OnClick="reservateNewDate_OnClick"/>
            <asp:Button runat="server" Text="Zrusit" Visible="False" ID="cancelReservation" OnClick="cancelExtendNextBook_OnClick"/>

        </div>
    </div>
</asp:Content>
