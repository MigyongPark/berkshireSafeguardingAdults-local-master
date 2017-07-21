<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="packageinstaller.ascx.cs" Inherits="USNInstaller.usn_minimalcorp_package.packageinstaller" %>
<div class="umb-packages-view-wrapper">
    <!-- Package details -->
    <div class="flex items-center justify-center">
        <div class="umb-info-local-items">
            <div class="umb-package-icon">
                <img alt="uSkinned" src="https://uskinned.net/images/package-icon.png">
            </div>
            <div class="umb-package-info">
                <asp:PlaceHolder ID="pnlSuccess" runat="server" Visible="false">
                <h4 class="umb-info-local-item"><strong><asp:Label ID="lblStarterKit" runat="server" Text="Label"></asp:Label> Successfully Installed</strong></h4>
                <div class="umb-info-local-item">
                    <p><strong>The installer has setup some pages to get you started.</strong></p>
                    <p><strong>To understand all of the layouts and components available please refer to the online demo available on the uSkinned website.</strong></p>
                    <p><strong><a href="http://uskinned.net/demos/1551/" target="_blank" title="Link will open in a new window/tab">Minimal Corp Demo</a></strong></p>
                    <p><strong>Go to the "Content" section to manage the installed website.</strong></p>
                </div>
                </asp:PlaceHolder>
                <asp:PlaceHolder ID="pnlFailed" runat="server" Visible="false">
                <h4 class="umb-info-local-item"><strong>Error installing <asp:Label ID="lblStarterKit2" runat="server" Text="Label"></asp:Label></strong></h4>
                <div class="umb-info-local-item">
                    <asp:PlaceHolder ID="plMediaPassed" runat="server" Visible="false">
                        <p style="color:green"><strong>Media successfully installed</strong></p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plMediaFailed" runat="server" Visible="false">
                         <p style="color:red"><strong>Media installation failed</strong></p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plContentPassed" runat="server" Visible="false">
                        <p style="color:green"><strong>Content successfully installed</strong></p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plContentFailed" runat="server" Visible="false">
                        <p style="color:red"><strong>Content installation failed</strong></p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plConfigPassed" runat="server" Visible="false">
                         <p style="color:green"><strong>Web.config updates successfully installed</strong></p>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="plConfigFailed" runat="server" Visible="false">
                         <p style="color:red"><strong>Web.config updates failed</strong></p>
                    </asp:PlaceHolder>
                    <p><strong>Please refer to the Umbraco log file located in ~/App_Data/Logs for further information.</strong></p>
                </div>
                </asp:PlaceHolder>
            </div>
        </div>
    </div>
</div>