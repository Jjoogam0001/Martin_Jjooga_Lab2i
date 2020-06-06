<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebRole1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1 style="align-content:center;text-align:center">&nbsp;</h1>
        <br />
        <br />
        <div style="margin:0 auto; border:solid 0.5px rgb(240, 240, 240);border-radius:5px; width:450px; background-color:brown;padding:20px">
            <h2 style="align-content:center;text-align:center">Charter Resor Services</h2>
            <br />
            <br />
            <h3 style="font-weight:bold;text-decoration:underline">TRAVEL DETAILS</h3>
            <br />
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Departure city" Width="150px" Font-Bold="true" Font-Size="20px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:DropDownList ID="From_city" runat="server" Width="130px">
                        <asp:ListItem>STO </asp:ListItem>  
                        <asp:ListItem>CPH</asp:ListItem>  
                        <asp:ListItem>CDG</asp:ListItem>  
                        <asp:ListItem>LHK</asp:ListItem>  
                        <asp:ListItem>FRA</asp:ListItem> 
                    </asp:DropDownList>
                </span>
            </div>
            <div style="margin:10px;padding:10px">
                <span style="width:150px">
                    <asp:Label Text="Destination city" Font-Bold="true" Width="150px" Font-Size="20px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:DropDownList ID="To_city" runat="server"  Width="130px">
                        <asp:ListItem>FRA </asp:ListItem>  
                        <asp:ListItem>STO</asp:ListItem>  
                        <asp:ListItem>LHK</asp:ListItem>  
                        <asp:ListItem>STO</asp:ListItem>  
                        <asp:ListItem>CDG</asp:ListItem>
                    </asp:DropDownList>
                </span>
            </div>
            <div style="margin-top:20px;padding:10px">
                <h5>PASSENGER(S)</h5>
                <div style="margin-bottom:10px">
                    <span style="width:150px;">
                        <asp:Label Text="Adult( < 66)" Font-Bold="true" Width="150px" Font-Size="15px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:DropDownList ID="Adult" runat="server" Width="70px">
                            <asp:ListItem>1 </asp:ListItem>  
                            <asp:ListItem>2</asp:ListItem>  
                            <asp:ListItem>3</asp:ListItem>  
                            <asp:ListItem>4</asp:ListItem>  
                        </asp:DropDownList>
                    </span>
                </div>
                <div style="margin-bottom:10px">
                    <span style="width:150px;">
                        <asp:Label Text="Child( < 15 )" Font-Bold="true" Width="150px" Font-Size="15px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:DropDownList ID="Child" runat="server" Width="70px">
                            <asp:ListItem>0 </asp:ListItem>  
                            <asp:ListItem>1</asp:ListItem>  
                            <asp:ListItem>2</asp:ListItem>  
                            <asp:ListItem>3</asp:ListItem>  
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </div>
                <div style="margin-bottom:10px">
                    <span style="width:150px;">
                        <asp:Label Text="Infant( < 3 )" Font-Bold="true" Width="150px" Font-Size="15px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:DropDownList ID="Infant" runat="server" Width="70px">
                            <asp:ListItem>0 </asp:ListItem>  
                            <asp:ListItem>1</asp:ListItem>  
                            <asp:ListItem>2</asp:ListItem>  
                            <asp:ListItem>3</asp:ListItem>  
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </div>
                <div style="margin-bottom:10px">
                    <span style="width:150px;">
                        <asp:Label Text="Senior ( > 65 )" Font-Bold="true" Width="150px" Font-Size="15px" runat="server" />
                    </span>
                    <span style="margin-top:5px; margin-left:30px">
                        <asp:DropDownList ID="Senior" runat="server" Width="70px">
                            <asp:ListItem>0 </asp:ListItem>  
                            <asp:ListItem>1</asp:ListItem>  
                            <asp:ListItem>2</asp:ListItem>  
                            <asp:ListItem>3</asp:ListItem>  
                            <asp:ListItem>4</asp:ListItem>
                        </asp:DropDownList>
                    </span>
                </div>

            </div>
            <div>
                <div style="margin-top:30px;padding:10px">
                    <asp:Button  OnClick="Submit" runat="server" Text="SUBMIT SEARCH" />
                 </div>
                <div style="margin-top:30px;padding:10px">
                    <asp:Button  OnClick="Get_Sum" runat="server" Text="GET OFFER OF YOUR FLIGHT" />
                 </div>

            <div>
                <span style="width:150px;">
                    <asp:Label Text="Sum: " Font-Bold="true" Width="150px" Font-Size="15px" runat="server" />
                </span>
                <span style="margin-top:5px; margin-left:30px">
                    <asp:Label ID="Sum" runat="server" Width="100px"></asp:Label>
                </span>
            </div>
            </div>
            <div style="margin:10px 0px;padding:10px">
                <asp:Button ID="HotelBookinBtn" OnClick="GotoHotelResPortal" runat="server" Width="100%" Text="Add an hotel reservetion"/><br />
                <br />
                <asp:Button ID="RentCarBtn" OnClick="GotoCarRentPortal" runat="server" Width="100%" Text="Rent a car"/>
            </div>
            <div style="margin-top:20px;padding:10px">
                <asp:Button ID="Button1" OnClick="GotoPayment1" runat="server" Text="GO TO PAYMENT" />
            </div>

        </div>
    </div>

</asp:Content>