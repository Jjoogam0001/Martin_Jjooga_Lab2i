<%@ Page Title="Hotel" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HotelBookingSystem.aspx.cs" Inherits="WebRole1.HotelBookingSystem" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1 style="align-content:center;text-align:center">Charter Hotel reservation</h1>
        <br />
        <br />
        <div style="margin:0 auto;  border:solid 0.5px rgb(240, 240, 240);border-radius:5px; width:450px; background-color:brown;padding:20px">
       
            <h3 style="font-weight:bold;text-decoration:underline">HOTEL RESERVATION PAGE</h3>
            <br />
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Number of customer" Width="150px" Font-Bold="true" Font-Size="14px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:DropDownList ID="NumberOfCustomer" runat="server" Width="70px">
                            <asp:ListItem>1 </asp:ListItem>  
                            <asp:ListItem>2</asp:ListItem>  
                            <asp:ListItem>3</asp:ListItem>  
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5 </asp:ListItem>                           
                        </asp:DropDownList>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Number of Nights" Font-Bold="true" Width="150px" Font-Size="14px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:TextBox ID="NumberOfNight" runat="server"  Width="100px"></asp:TextBox>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <asp:RadioButtonList ID="RoomType" runat="server">
                    <asp:ListItem Selected="true" Value="1">Single </asp:ListItem>  
                    <asp:ListItem Value="2">Double</asp:ListItem>
                </asp:RadioButtonList>
                
            </div>
            <div style="margin-top:20px;padding:10px">
                <h5>CUSTOMER(S)</h5>
                 <div>
                    <span style="width:150px;font-size:10px">
                        <asp:Label Text="Main guest name" Font-Bold="true" Width="150px" Font-Size="11px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:TextBox ID="Main_Guest_Name" runat="server" Width="200px"></asp:TextBox>
                    </span>
                </div>
                <div>
                    <span style="width:150px;">
                        <asp:Label Text="Number of Senior ( > 65 )" Font-Bold="true" Width="150px" Font-Size="11px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:TextBox ID="Number_Of_Senior" runat="server" Width="50px"></asp:TextBox>
                    </span>
                </div>

            </div>
            <div style="margin-top:30px;padding:10px">
                <asp:LinkButton ID="RentCarBtn" runat="server" PostBackUrl="~/CarRental.aspx">Click here if you Would like to rent a car</asp:LinkButton>
            </div>
            <div style="margin-top:30px;padding:10px">
                <asp:LinkButton Font-Bold="true" BackColor="#ffffff" BorderColor="Wheat" BorderStyle="Solid" ID="PaymentPageBtn" Onclick="GoToPayment" runat="server">GO TO PAYMENT</asp:LinkButton>
            </div>

        </div>
    </div>
</asp:Content>

