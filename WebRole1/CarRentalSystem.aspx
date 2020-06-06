<%@ Page Title="About" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarRentalSystem.aspx.cs" Inherits="WebRole1.CarRentalSystem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
 
        <div style="margin:0 auto; border:solid 0.5px rgb(240, 240, 240);border-radius:5px; width:450px; background-color:brown;padding:20px">
            <h2 style="align-content:center;text-align:center">Charter Resor Services</h2>
            <br />
            <br />
            <h3 style="font-weight:bold;text-decoration:underline">CAR RENTAL SERVICE</h3>
            <br />
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Number of seats" Width="150px" Font-Bold="true" Font-Size="14px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:TextBox ID="NumberOfSeat" runat="server" Width="100px"></asp:TextBox>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Model (Year)" Font-Bold="true" Width="150px" Font-Size="14px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:DropDownList ID="CarYearOfManufacturing" runat="server">
                        <asp:ListItem >2011 </asp:ListItem>  
                        <asp:ListItem >2012</asp:ListItem>
                        <asp:ListItem >2013 </asp:ListItem>  
                        <asp:ListItem >2014</asp:ListItem>
                        <asp:ListItem >2015 </asp:ListItem>  
                        <asp:ListItem >2016</asp:ListItem>
                        <asp:ListItem >2017 </asp:ListItem>  
                        <asp:ListItem >2018</asp:ListItem>
                        <asp:ListItem >2019 </asp:ListItem>  
                        <asp:ListItem>2020</asp:ListItem>      
                    </asp:DropDownList>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Driver's age" Font-Bold="true" Width="150px" Font-Size="14px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:TextBox ID="DriverAge" runat="server"  Width="100px"></asp:TextBox>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <asp:RadioButtonList ID="GazType" runat="server">
                    <asp:ListItem Selected="True" Value="1">Diesel </asp:ListItem>  
                    <asp:ListItem Value="2">Benzine</asp:ListItem>  
                </asp:RadioButtonList>
            </div>
            <div style="margin-top:30px;padding:10px">
                <asp:LinkButton ID="HotelBookinBtn" runat="server" PostBackUrl="~/HotelBooking.aspx">Click here to add an hotel reservetion to your booking</asp:LinkButton><br />
            </div>
            <div style="margin-top:30px;padding:10px">
                <asp:LinkButton Font-Bold="true" BackColor="#ffffff" BorderColor="Wheat" BorderStyle="Solid" ID="PaymentPageBtn" Onclick="GoToPayment" runat="server">GO TO PAYMENT</asp:LinkButton>
            </div>
            <div style="margin-top:30px;padding:10px">
                <asp:LinkButton Font-Bold="true" BackColor="#ffffff" BorderColor="Wheat" BorderStyle="Solid" ID="Abandon" PostBackUrl="~/Default.aspx" runat="server">ABANDON PROCESS</asp:LinkButton>
            </div>

        </div>
</asp:Content>

